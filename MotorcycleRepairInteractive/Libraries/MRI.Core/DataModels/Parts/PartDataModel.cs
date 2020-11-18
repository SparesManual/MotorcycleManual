using System;
using System.Collections.Generic;
using System.Text;

namespace MRI.Core
{
    /// <summary>
    /// Represents a row from the Part table in the database
    /// </summary>
    public class PartDataModel : BaseDataModel
    {
        /// <summary>
        /// The id that came from the SQL table
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// The official part number for the part
        /// Usually ##-####
        /// </summary>
        public string PartNumber { get; private set; }

        /// <summary>
        /// A second part number, used for some parts that have a 
        /// part number from the maker that is different from the original
        /// part number
        /// </summary>
        public string MakersPartNumber { get; private set; }

        /// <summary>
        /// The part description that doesn't include additional info
        /// or remarks
        /// </summary>
        public string Description { get; private set; }

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// Takes in parameters from the SQL Pages table
        /// </summary>
        public PartDataModel(
            string id, string partNumber, string makersPartNumber, string description)
        {
            // Set the Public properties for this class
            Id = IntParse(id);
            PartNumber = partNumber;
            MakersPartNumber = makersPartNumber;
            Description = description;
        }
        #endregion
    }
}
