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
                Items.Add(new PageItemDataModel(
                    row.ItemArray[0].ToString(),
                    row.ItemArray[1].ToString(),
                    row.ItemArray[2].ToString(),
                    row.ItemArray[3].ToString(),
                    row.ItemArray[4].ToString(),
                    row.ItemArray[5].ToString(),
                    row.ItemArray[6].ToString(),
                    row.ItemArray[7].ToString(),
                    row.ItemArray[8].ToString(),
                    row.ItemArray[9].ToString()
                    ));
            }
        }
        #endregion
    }
}
