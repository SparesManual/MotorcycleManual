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
    public class PageDataModel : BaseDataModel
    {
        #region Public Properties

        /// <summary>
        /// The id for the book from the SQL table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The foreign key that points to the Book table
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
        public SpecificToModelEnum SpecificToModel { get; set; }
        #endregion


        #region Constructors

        /// <summary>
        /// Default Constructor
        /// Takes in parameters from the SQL Pages table
        /// </summary>
        public PageDataModel(
            string id, string bookId, string startPageNumber,
            string endPageNumber, string pageHeader, string figureNumber,
            string figureDescription, string specificToModel)
        {
            // Set the Public properties for this class
            Id = IntParse(id);
            BookID = IntParse(bookId);
            StartPageNumber = IntParse(startPageNumber);
            EndPageNumber = IntParse(endPageNumber);
            PageHeader = pageHeader;
            FigureNumber = IntParse(figureNumber);
            FigureDescription = figureDescription;
            SpecificToModel = ParseEnum<SpecificToModelEnum>(specificToModel, SpecificToModelEnum.None);
        }
        #endregion
    }
}
