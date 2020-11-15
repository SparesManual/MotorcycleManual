using System;
using System.Linq;
using System.Threading.Tasks;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Db.Infrastructure.Data
{
  /// <summary>
  /// Helper class for seeding data into the database
  /// </summary>
  public static class ManualContextSeed
  {
    /// <summary>
    /// Seeds test data into the database <paramref name="context"/>
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="loggerFactory">Logger provider</param>
    public static async Task SeedAsync(this ManualContext context, ILoggerFactory loggerFactory)
    {
      async Task Populate<T>(Func<ManualContext, DbSet<T>> extractor, string path)
        where T : class, IEntity
      {
        //const string root = "../Infrastructure/Data/SeedData";
        var extracted = extractor(context);
        if (!extracted.Any())
        {
          // TODO

          await context.SaveChangesAsync().ConfigureAwait(false);
        }
      }

      try
      {
        await Populate(c => c.Books!, "books.csv").ConfigureAwait(false);
      }
      catch (Exception e)
      {
        var logger = loggerFactory.CreateLogger<ManualContext>();
        logger.LogError(e, "Failed to seed data");
      }
    }
  }
}
