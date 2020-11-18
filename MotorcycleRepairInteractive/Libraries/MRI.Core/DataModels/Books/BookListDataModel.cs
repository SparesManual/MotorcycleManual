using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace MRI.Core
{
    /// <summary>
    /// Represents a list of Book Data Models
    /// </summary>
    public class BookListDataModel 
    {
        #region Public Properties

        /// <summary>
        /// The list of Books
        /// </summary>
        public List<BookDataModel> Items { get; set; }
            = new List<BookDataModel>();

        #endregion

        #region Constructors

        public BookListDataModel(DataTable table)
        {
            foreach (var row in table.AsEnumerable())
            {
                Items.Add(new BookDataModel(
                    row.ItemArray[0].ToString(),
                    row.ItemArray[1].ToString()));
            }
        } 
        #endregion
    }
}
