using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
                Items.Add(new PartDataModel(
                    row.ItemArray[0].ToString(),
                    row.ItemArray[1].ToString(),
                    row.ItemArray[2].ToString(),
                    row.ItemArray[3].ToString()
                    ));
            }
        }
        #endregion
    }
}
