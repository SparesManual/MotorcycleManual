using System;
using System.Collections.Generic;
using System.Text;

namespace MRI.Core
{
    /// <summary>
    /// Represents a row from the PageItem table in the database
    /// </summary>
    public class PageItemDataModel
    {
        /// <summary>
        /// The unique id used for the row in the PageItem Table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The index number used to point the part in the list to the 
        /// location on the image.
        /// </summary>
        public int IndexNumber { get; set; }

        /// <summary>
        /// The unique part number for the page item
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// The page number that the list of parts came from
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StartPageNumber_FKey { get; set; }

        public string Description { get; set; }

        public string Additional_Info { get; set; }

        public int Quantity { get; set; }

        public string Remarks { get; set; }
    }
}
