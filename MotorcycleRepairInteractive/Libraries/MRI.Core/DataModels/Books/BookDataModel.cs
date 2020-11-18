using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MRI.Core
{
    /// <summary>
    /// Represents a row from the Book table in the database
    /// </summary>
    public class BookDataModel : BaseDataModel
    {
        /// <summary>
        /// The unique Id number used for the row in the Book Table
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// The title of the motorcycle manual.
        /// Example:  1976-77 Triumph Bonneville T140V and Tiger TR7RV
        /// </summary>
        public string BookTitle { get; private set; }

        /// <summary>
        /// Default Constructor
        /// Takes in parameters from the SQL Books table
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookTitle"></param>
        public BookDataModel(string id, string bookTitle)
        {
            // Set the Public properties for this class
            Id = IntParse(id);
            BookTitle = bookTitle;
        }

    }
}
