using System.Linq;
using System.Reflection;
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
    /// Collection of pages from <see cref="Books"/>
    /// </summary>
    public DbSet<Page>? Pages { get; set; }
    /// <summary>
    /// Collection of items from <see cref="Pages"/>
    /// </summary>
    public DbSet<PageItem>? PageItems { get; set; }
    /// <summary>
    /// Collection of <see cref="Part"/> items
    /// </summary>
    public DbSet<Part>? Parts { get; set; }

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
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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
