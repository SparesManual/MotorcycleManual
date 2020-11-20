using System.Collections.Generic;
using System.Data;

namespace MRI.Core
{
    public class PartListDataModel
    {
        #region Public Properties

        /// <summary>
        /// The list of Pages
        /// </summary>
        public List<PartDataModel> Items { get; set; }
            = new List<PartDataModel>();
        #endregion


        #region Constructors

        public PartListDataModel(DataTable table)
        {
            foreach (var row in table.AsEnumerable())
            {
                Items.Add(new PartDataModel(row));
            }
        }
        #endregion
    }
}
