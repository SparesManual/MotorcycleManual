using System;
using System.Collections.Generic;
using System.Text;

namespace MRI.Core
{
    /// <summary>
    /// Represents Fastener Specifications that may be additional to a part
    /// Bolts, Nuts, Washers, Springs and Screws have additional information
    /// </summary>
    public class FastenerSpec
    {
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
    }
}
