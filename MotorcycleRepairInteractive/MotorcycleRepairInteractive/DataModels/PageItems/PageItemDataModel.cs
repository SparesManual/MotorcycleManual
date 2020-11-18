using System;
using System.Collections.Generic;
using System.Text;

namespace MRI.Core
{
    /// <summary>
    /// Represents a row from the PageItem table in the database
    /// </summary>
    public class PageItemDataModel : BaseDataModel
    {
        #region Public Properties

        /// <summary>
        /// The unique id used for the row in the PageItem Table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The combination of the Page Number and the Index Number
        /// PageIndexId = PageNumber * 100 + IndexNumber
        /// </summary>
        public int PageIndxNumber { get; set; }

        /// <summary>
        /// The unique part number for the page item
        /// Used as a foreign key for the Parts table
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// The starting page number in the page sequence
        /// This is used to map the page item to the page it 
        /// belongs to.... the index of this page item will 
        /// be represented in the image indicating where the 
        /// part belongs on the bike
        /// 
        /// </summary>
        public int StartPageNumber { get; set; }

        /// <summary>
        /// Additional information regarding how the part is used 
        /// in this application in regard to the page image
        /// </summary>
        public string Additional_Info { get; set; }

        /// <summary>
        /// The quantity of a part that is used on a page
        /// This isn't the total number of times that a single
        /// part is used on the entire motorcycle, just how many 
        /// times it is used on this page
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Additional Remarks relating to the part and how it
        /// is  used on this page
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// The PageIndexID group that this child belongs to, if it has one
        /// </summary>
        public int ParentPageItemID { get; set; }

        public int PartID { get; set; }

        public int PageID { get; set; }
        #endregion


        #region Constructors

        /// <summary>
        /// Default Constructor
        /// Takes in parameters from the SQL Pages table
        /// </summary>
        public PageItemDataModel(
            string id, string pageIndxNumber, string partNumber,
            string startPageNumber, string additional_Info,
            string quantity, string remarks, string parentPageItemId,
            string partID, string pageID)
        {
            // Set the Public properties for this class
            Id = IntParse(id);
            PageIndxNumber = IntParse(pageIndxNumber);
            PartNumber = partNumber;
            StartPageNumber = IntParse(startPageNumber);
            Additional_Info = additional_Info;
            Quantity = IntParse(quantity);
            Remarks = remarks;
            ParentPageItemID = IntParse(parentPageItemId);
            //ParentPageItemID = int.Parse(parentPageItemId);
            PartID = IntParse(partID);
            PageID = IntParse(pageID);
        }
        #endregion
    }
}
