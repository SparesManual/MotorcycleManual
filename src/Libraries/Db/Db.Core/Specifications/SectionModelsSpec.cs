using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specifications for querying <see cref="Model"/> entities based on their parent <see cref="Section"/> entity
  /// </summary>
  public class SectionModelsSpec
    : BaseSpecification<Section, Model>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="id">Parent section id</param>
    /// <param name="search">Fuzzy model search</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public SectionModelsSpec(int id, string? search, int size, int index)
      : base(model => string.IsNullOrEmpty(search)
                      || model.Name.Contains(search)
                      || search.Contains(model.Year.ToString()))
    {
      SetExtractor(sections => sections
        .Where(section => section.Id.Equals(id))
        .Include(section => section.SectionModels)
        .Select(section => section.SectionModels)
        .SelectMany(sectionModels => sectionModels)
        .Include(sectionModel => sectionModel.Model)
        .Select(sectionModel => sectionModel.Model));

      ApplyPaging(size, index);
      SetOrderBy(model => model.Year);
    }
  }
}