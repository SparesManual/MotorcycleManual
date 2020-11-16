using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Infrastructure.Data
{
  /// <inheritdoc />
  public class ManualContext
    : DbContext
  {
    #region Properties

    /// <summary>
    /// Collection of manuals
    /// </summary>
    public DbSet<Manual>? Manuals { get; set; }

    /// <summary>
    /// Collection of drawings from <see cref="Manuals"/>
    /// </summary>
    public DbSet<Drawing>? Drawings { get; set; }

    /// <summary>
    /// Collection of models from <see cref="Drawings"/>
    /// </summary>
    public DbSet<Model>? Models { get; set; }

    /// <summary>
    /// Mapping of <see cref="Models"/> to <see cref="Drawings"/>
    /// </summary>
    public DbSet<DrawingModels>? DrawingModels { get; set; }

    /// <summary>
    /// Collection of <see cref="Part"/> items
    /// </summary>
    public DbSet<Part>? Parts { get; set; }

    /// <summary>
    /// <see cref="Part"/> properties
    /// </summary>
    public DbSet<Property>? Properties { get; set; }

    /// <summary>
    /// <see cref="Property"/> types
    /// </summary>
    public DbSet<PropertyType>? PropertyTypes { get; set; }

    /// <summary>
    /// <see cref="Parts"/> within an <see cref="Core.Entities.Assembly"/>
    /// </summary>
    public DbSet<AssemblyParts>? AssemblyParts { get; set; }

    /// <summary>
    /// Assemblies within an <see cref="Core.Entities.Assembly"/>
    /// </summary>
    public DbSet<AssemblySubAssemblies>? SubAssemblies { get; set; }

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="options">Context options</param>
    public ManualContext(DbContextOptions<ManualContext> options)
      : base(options) { }

    /// <remarks>
    /// The SQLite Database does not support decimal points
    ///
    /// This method is used to modify all models using a decimal type to use a double instead
    /// </remarks>
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      #region Many-to-many drawing models

      modelBuilder.Entity<DrawingModels>()
        .HasKey(bc => new { bc.DrawingId, bc.ModelId });
      modelBuilder.Entity<DrawingModels>()
        .HasOne(bc => bc.Drawing)
        .WithMany(b => b!.DrawingModels)
        .HasForeignKey(bc => bc.DrawingId);
      modelBuilder.Entity<DrawingModels>()
        .HasOne(bc => bc.Model)
        .WithMany(c => c!.DrawingModels)
        .HasForeignKey(bc => bc.ModelId);

      #endregion

      #region Many-to-many assembly parts

      modelBuilder.Entity<AssemblyParts>()
        .HasKey(bc => new { bc.AssemblyId, bc.PartId });
      modelBuilder.Entity<AssemblyParts>()
        .HasOne(bc => bc.Part)
        .WithMany(b => b!.ParentAssemblies)
        .HasForeignKey(bc => bc.PartId);
      modelBuilder.Entity<AssemblyParts>()
        .HasOne(bc => bc.Assembly)
        .WithMany(c => c!.AssemblyParts)
        .HasForeignKey(bc => bc.AssemblyId);

      #endregion

      #region Many-to-many assembly sub-assemblies

      modelBuilder.Entity<AssemblySubAssemblies>()
        .HasKey(bc => new { bc.AssemblyId, bc.SubAssemblyId });
      modelBuilder.Entity<AssemblySubAssemblies>()
        .HasOne(bc => bc.SubAssembly)
        .WithMany(b => b!.AssemblyParentAssemblies)
        .HasForeignKey(bc => bc.SubAssemblyId);
      modelBuilder.Entity<AssemblySubAssemblies>()
        .HasOne(bc => bc.Assembly)
        .WithMany(c => c!.AssemblySubAssemblies)
        .HasForeignKey(bc => bc.AssemblyId);

      #endregion

      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
      modelBuilder.Entity<Property>().HasNoKey();

      // If the current provider is not SQLite..
      if (!Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.Sqlite"))
        // do nothing
        return;

      // Otherwise for each model entity..
      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        // Get its properties with decimal types
        var properties = entityType.ClrType
          .GetProperties()
          .Where(info => info.PropertyType == typeof(decimal));

        // For each decimal type property..
        foreach (var property in properties)
          modelBuilder
            // Find the entity
            .Entity(entityType.Name)
            // Find the property
            .Property(property.Name)
            // Add a to double converter
            .HasConversion<double>();
      }
    }
  }
}
