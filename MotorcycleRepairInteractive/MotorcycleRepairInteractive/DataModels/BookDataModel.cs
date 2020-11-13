using System;
using System.Collections.Generic;
using System.Text;

namespace MRI.Core
{
    /// <summary>
    /// Represents a row from the Book table in the database
    /// </summary>

    public class BookDataModel
    {
        /// <summary>
        /// The unique Id number used for the row in the Book Table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the motorcycle manual.
        /// Example:  1976-77 Triumph Bonneville T140V and Tiger TR7RV
        /// </summary>
        public string BookTitle { get; set; }

    }
}
