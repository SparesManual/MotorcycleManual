using System;
using System.Collections.Generic;
using System.Text;

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
        /// A flag indicating if it has measurements for Thread diameter,
        /// Minimum length under the thread, Thickness, etc...
        /// This is only used for parts such as Bolts, Nuts, Washers, Springs
        /// </summary>
        public bool HasMeasurementsBool { get; private set; }

        /// <summary>
        /// The material that the part is made of.  
        /// For Example: Steel, or Spring Steel
        /// </summary>
        public string Material { get; private set; }

        /// <summary>
        /// The size and type of thread.  For example:
        /// 5/16" UNF = Unified Fine Thread (Pitch = 28 threads/inch or .907mm/thread)
        /// 5/16" UNEF = Unified Extra Fine Thread (Pitch = 32 thr./inch or .794mm/thread)
        /// 5/16" UNC = Unified Coarse Thread (Pitch = 20 thr./inch or 1.27mm/thread)
        /// </summary>
        public string ThreadDiameter { get; private set; }

        /// <summary>
        /// The length of the bolt or screw that is under the head.
        /// </summary>
        public string LengthUnderHead { get; private set; }

        /// <summary>
        /// The minimum length of thread that must be on the LengthUnderHead
        /// </summary>
        public string MinimumLengthOfThread { get; private set; }

        /// <summary>
        /// Only used with screws.  Indicates the type of head on the screw
        /// Example:  Cheese head, Hexagonal head, Countersunk, Socket head
        /// </summary>
        public string TypeOfHead { get; private set; }

        /// <summary>
        /// Only used with screws.  Indicates the type of drive used for this screw.
        /// Example:  Cross recess, Slot, Pozidrive, Hexagonal socket
        /// </summary>
        public string TypeOfDrive { get; private set; }

        /// <summary>
        /// Only used with bolts.  Measures the distance between two of the 
        /// flat surfaces on the sides of the head of the bolt
        /// </summary>
        public string HexagonAcrossFlats { get; private set; }

        /// <summary>
        /// Used for Nuts, Plain washers and Spring washers
        /// Measures the thickness (top-bottom) of a nut or plain washer
        /// Measures the thickness of the material used to make a spring
        /// </summary>
        public string Thickness { get; private set; }

        /// <summary>
        /// Used with washers and springs.  Inside diameter of hole
        /// </summary>
        public string InsideDiameter { get; private set; }
        /// <summary>
        /// Used with washers and springs.  Outside diameter of hole
        /// </summary>

        public string OutsideDiameter { get; private set; }

        /// <summary>
        /// Used with springs only... the 0/a height, whatever that is
        /// </summary>
        public string Height { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="partNumber" - Required></param>
        /// <param name="makersPartNumber" - Required></param>
        /// <param name="description" - Required></param>
        /// <param name="hasMeasurements" - Optional></param>
        /// <param name="material" - Optional></param>
        /// <param name="threadDiameter" - Optional></param>
        /// <param name="lengthUnderHead" - Optional></param>
        /// <param name="minimumLengthOfThread" - Optional></param>
        /// <param name="typeOfHead" - Optional></param>
        /// <param name="typeOfDrive" - Optional></param>
        /// <param name="hexagonAcrossFlats" - Optional></param>
        /// <param name="thickness" - Optional></param>
        /// <param name="insideDiameter" - Optional></param>
        /// <param name="outsideDiameter" - Optional></param>
        /// <param name="height" - Optional></param>
        public PartDataModel(
            string partNumber,
            string makersPartNumber,
            string description,
            bool hasMeasurements = false,
            string material = "",
            string threadDiameter = "",
            string lengthUnderHead = "",
            string minimumLengthOfThread = "",
            string typeOfHead = "",
            string typeOfDrive = "",
            string hexagonAcrossFlats = "",
            string thickness = "",
            string insideDiameter = "",
            string outsideDiameter = "",
            string height = "" )
            
        {
            PartNumber = partNumber;
            MakersPartNumber = makersPartNumber;
            Description = description;
            HasMeasurementsBool = hasMeasurements;
            Material = material;
            ThreadDiameter = threadDiameter;
            LengthUnderHead = lengthUnderHead;
            MinimumLengthOfThread = minimumLengthOfThread;
            TypeOfHead = typeOfHead;
            TypeOfDrive = typeOfDrive;
            HexagonAcrossFlats = hexagonAcrossFlats;
            Thickness = thickness;
            InsideDiameter = insideDiameter;
            OutsideDiameter = outsideDiameter;
            Height = height;

        }
    }
}
