using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Core.Specifications
{
  public class SectionModelsSpec
    : BaseSpecification<Section, Model>
  {
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