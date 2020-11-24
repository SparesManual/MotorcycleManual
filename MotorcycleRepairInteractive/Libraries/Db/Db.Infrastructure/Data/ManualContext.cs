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
    /// Collection of mappings of <see cref="Books"/> to <see cref="Makes"/>
    /// </summary>
    public DbSet<BookMakes>? BookMakes { get; set; }

    /// <summary>
    /// Collection of models
    /// </summary>
    public DbSet<Model>? Models { get; set; }

    /// <summary>
    /// Collection of mappings of <see cref="Makes"/> to <see cref="Models"/>
    /// </summary>
    public DbSet<MakeModels>? MakeModels { get; set; }

    /// <summary>
    /// Collection of sections covering <see cref="Book"/> pages
    /// </summary>
    public DbSet<Section>? Sections { get; set; }

    /// <summary>
    /// Collection of parts documented in <see cref="Sections"/>
    /// </summary>
    public DbSet<Part>? Parts { get; set; }

    /// <summary>
    /// Collection of mappings of <see cref="Sections"/> to <see cref="Parts"/>
    /// </summary>
    public DbSet<SectionParts>? SectionParts { get; set; }

    /// <summary>
    /// Image points mapped to <see cref="SectionParts"/>
    /// </summary>
    public DbSet<ImagePoint>? ImagePoints { get; set; }

    /// <summary>
    /// Table of properties mapped to <see cref="Parts"/>
    /// </summary>
    public DbSet<Property>? Properties { get; set; }

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
        .HasKey(bc => new { bc.SectionId, bc.PartId });
      modelBuilder.Entity<SectionParts>()
        .HasOne(bc => bc.Section)
        .WithMany(b => b!.SectionParts)
        .HasForeignKey(bc => bc.SectionId);
      modelBuilder.Entity<SectionParts>()
        .HasOne(bc => bc.Part)
        .WithMany(c => c!.PartSections)
        .HasForeignKey(bc => bc.PartId);

      #endregion

      #region Self-reference SectionParts

      modelBuilder.Entity<SectionParts>()
        .HasKey(x => x.Id);
      modelBuilder.Entity<SectionParts>()
        .HasOne(x => x.ParentSectionParts)
        .WithMany(x => x!.ParentSectionPartsList!)
        .HasForeignKey(x => x.ParentSectionPartsId)
        .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

      #endregion

      #region Many-to-one BookMakes

      modelBuilder.Entity<BookMakes>()
        .HasKey(bc => new { bc.BookId, bc.MakeId });
      modelBuilder.Entity<BookMakes>()
        .HasOne(bc => bc.Book)
        .WithMany(b => b!.BookMakes)
        .HasForeignKey(bc => bc.BookId);
      modelBuilder.Entity<BookMakes>()
        .HasOne(bc => bc.Make)
        .WithOne(c => c!.ParentBook);

      #endregion

      #region Many-to-many MakeModels

      modelBuilder.Entity<MakeModels>()
        .HasKey(bc => new { bc.MakeId, bc.ModelId });
      modelBuilder.Entity<MakeModels>()
        .HasOne(bc => bc.Make)
        .WithMany(b => b!.MakeModels)
        .HasForeignKey(bc => bc.MakeId);
      modelBuilder.Entity<MakeModels>()
        .HasOne(bc => bc.Model)
        .WithMany(c => c!.ParentMakes)
        .HasForeignKey(bc => bc.ModelId);

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
