using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MRI.Core
{
    /// <summary>
    /// Represents a row from the Page table in the database
    /// 
    /// A Page consists of the image used to show all the index numbers
    /// that point to parts on the image as well as a list of all the parts 
    /// on the page ordered by index number.
    /// In some cases there are too many parts to fit on one page of the 
    /// manual, in the index list.  In this case, the image is repeated on 
    /// the following page and the rest of the parts are listed on the 
    /// opposite page... this explains why some Start and End pages only 
    /// consist of 2 pages, and others span 4 pages.
    /// </summary>
    public class PageDataModel
    {
        /// <summary>
        /// The unique id used by the book in the Book table
        /// </summary>
        public int BookID { get; set; }

        /// <summary>
        /// The first page number in the page sequence
        /// </summary>
        public int StartPageNumber { get; set; }

        /// <summary>
        /// The last page number in the page sequence
        /// this will be either 1 or 3 more than the first page number
        /// </summary>
        public int EndPageNumber { get; set; }

        /// <summary>
        /// The title of the Page sequence
        /// </summary>
        public string PageHeader { get; set; }

        /// <summary>
        /// The bitmap image for the page
        /// </summary>
        public Bitmap ImageData { get; set; }

        /// <summary>
        /// The number used to represent the image in the manual
        /// All images have their own figure number
        /// </summary>
        public int FigureNumber { get; set; }

        /// <summary>
        /// The description used for the image in the manual
        /// It comes from the title besides the figure number
        /// </summary>
        public string FigureDescription { get; set; }

        /// <summary>
        /// The model of the motorcycle.  
        /// For example... the 1976-77 Triumph manual covers to models:
        /// 1.  T140V model
        /// 2.  TR7RV model
        /// 
        /// Some pages cover only the parts used for one of the models
        /// </summary>
        public SpecificToModel MotorcycleModelType { get; set; }

    }
}
