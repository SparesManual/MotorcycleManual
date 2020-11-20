using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MRI.Core
{
    public class PageItemListDataModel
    {
        #region Public Properties

        /// <summary>
        /// The list of Pages
        /// </summary>
        public List<PageItemDataModel> Items { get; set; }
            = new List<PageItemDataModel>();
        #endregion


        #region Constructors

        public PageItemListDataModel(DataTable table)
        {
            foreach (var row in table.AsEnumerable())
            {
                Items.Add(new PageItemDataModel(row));
            }
        }
        #endregion
    }
}
