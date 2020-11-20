using System.Data;

namespace MRI.Core
{
    /// <summary>
    /// Represents a row from the Part table in the database
    /// </summary>
    public class PartDataModel
    {
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

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="partNumber">Required</param>
        /// <param name="makersPartNumber">Required</param>
        /// <param name="description">Required</param>
        public PartDataModel(string partNumber, string makersPartNumber, string description)
        {
            PartNumber = partNumber;
            MakersPartNumber = makersPartNumber;
            Description = description;
        }

        public PartDataModel(DataRow row)
        {
        }
    }
}
