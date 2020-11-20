using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MRI.Core
{
    public class PageListDataModel
    {
        #region Public Properties

        /// <summary>
        /// The list of Pages
        /// </summary>
        public List<PageDataModel> Items { get; set; }
            = new List<PageDataModel>();
        #endregion


        #region Constructors

        public PageListDataModel(DataTable table)
        {
            foreach (var row in table.AsEnumerable())
            {
                Items.Add(new PageDataModel(row));
            }
        }
        #endregion
    }
}
