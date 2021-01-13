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
    /// Collection of books
    /// </summary>
    public DbSet<Book>? Books { get; set; }

    /// <summary>
    /// Collection of makes
    /// </summary>
    public DbSet<Make>? Makes { get; set; }

    /// <summary>
    /// Collection of engines
    /// </summary>
    public DbSet<Carburetor>? Carburetors { get; set; }

    /// <summary>
    /// Collection of engines
    /// </summary>
    public DbSet<Engine>? Engines { get; set; }

    /// <summary>
    /// Collection of models
    /// </summary>
    public DbSet<Model>? Models { get; set; }

    /// <summary>
    /// Collection of sections covering <see cref="Book"/> pages
    /// </summary>
    public DbSet<Section>? Sections { get; set; }

    /// <summary>
    /// Collection of parts documented in <see cref="Sections"/>
    /// </summary>
    public DbSet<Part>? Parts { get; set; }

    public DbSet<SectionModels>? SectionModels { get; set; }

    /// <summary>
    /// Collection of mappings of <see cref="Sections"/> to <see cref="Parts"/>
    /// </summary>
    public DbSet<SectionParts>? SectionParts { get; set; }

    /// <summary>
    /// Collection of mappings of parent <see cref="SectionParts"/> to child <see cref="SectionParts"/>
    /// </summary>
    public DbSet<SectionPartParents>? SectionPartParents { get; set; }

    /// <summary>
    /// Image points mapped to <see cref="SectionParts"/>
    /// </summary>
    public DbSet<ImagePoint>? ImagePoints { get; set; }

    /// <summary>
    /// Table of properties mapped to <see cref="Parts"/>
    /// </summary>
    public DbSet<Property>? Properties { get; set; }

    /// <summary>
    /// Table of property format types
    /// </summary>
    public DbSet<FormatType>? FormatTypes { get; set; }

    /// <summary>
    /// Table of property types
    /// </summary>
    public DbSet<PropertyType>? PropertyTypes { get; set; }

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
      #region Many-to-many SectionParts

      modelBuilder.Entity<SectionParts>()
        .HasOne(bc => bc.Section)
        .WithMany(b => b!.SectionParts)
        .HasForeignKey(bc => bc.SectionId);
      modelBuilder.Entity<SectionParts>()
        .HasOne(bc => bc.Part)
        .WithMany(c => c!.PartSections)
        .HasForeignKey(bc => bc.PartId);

      #endregion

      modelBuilder.Entity<Property>().HasKey(prop => new {prop.PartId, prop.PropertyName});
      modelBuilder.Entity<Property>()
        .HasOne(bc => bc!.Part)
        .WithMany(b => b!.PartProperties)
        .HasForeignKey(bc => bc.PartId);

      modelBuilder.Entity<Section>()
        .HasOne(bc => bc!.Book)
        .WithMany(b => b!.BookSections)
        .HasForeignKey(bc => bc!.BookId);

      #region Many-to-many SectionPartParents

      modelBuilder.Entity<SectionPartParents>().HasKey(section => new {section.ParentId, section.ChildId});
      modelBuilder.Entity<SectionPartParents>()
        .HasOne(bc => bc.Child)
        .WithMany(c => c!.ChildSections)
        .HasForeignKey(bc => bc.ChildId);

      #endregion

      #region Many-to-many SectionModels

      modelBuilder.Entity<SectionModels>().HasKey(section => new {section.SectionId, section.ModelId});
      modelBuilder.Entity<SectionModels>()
        .HasOne(bc => bc.Model)
        .WithMany(c => c.ModelSections)
        .HasForeignKey(bc => bc.ModelId);
      modelBuilder.Entity<SectionModels>()
        .HasOne(bc => bc.Section)
        .WithMany(c => c.SectionModels)
        .HasForeignKey(bc => bc.SectionId);

      #endregion

      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

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
