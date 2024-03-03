/// <summary>
/// Author:    Collin Giles
/// Date:      3/2/24
///
/// I, Collin Giles, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// File Contents
///
/// The section class defines a section of a tile, by defining its type and count.
/// Note that the only valid types are defined as follows:
/// Plain, forest, house, field, river, track, station
/// </summary>
namespace TileClass
{
    /// <summary>
    /// The section class is used to help organize the information of each edge/sub-tile in a tile
    /// This class has two fields
    /// Type - A string representing the type of the section
    /// Types are limited to the following strings 
    /// Count - An int representing the count of elements in a section i.e. how many trees are in a forest type section?
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Represents the count of elements in this section
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Represents the typing of this section.
        /// </summary>
        public string Typing { get; private set; }
        /// <summary>
        /// Creates a section with no typing and count 0
        /// </summary>
        public Section()
        {
            Typing = "";
            Count = 0;
        }
        /// <summary>
        /// Creates a section with a typing, and count 0
        /// Note that the only valid types are defined as follows:
        /// Plain, forest, house, field, river, track, station
        /// </summary>
        /// <param name="Typing"></param>
        public Section(string Typing) : this()
        {
            SetType(Typing);
        }
        /// <summary>
        /// Creates a section with a typing, and a count
        /// Note that the only valid types are defined as follows:
        /// Plain, forest, house, field, river, track, station
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Count"></param>
        public Section(string Typing, int Count) : this()
        {
            SetType(Typing);
            this.Count = Count;
        }
        /// <summary>
        /// Sets the typing of this section. Note that the only valid types are defined as follows:
        /// Plain, forest, house, field, river, track, station
        /// The string will be normalized to be all caps
        /// </summary>
        /// <param name="SectionType">The type of this section</param>
        public void SetType(string SectionType)
        {
            SectionType = SectionType.ToUpper();
            switch (SectionType)
            {
                case "PLAIN":
                case "FOREST":
                case "HOUSE":
                case "FIELD":
                case "RIVER":
                case "TRACK":
                case "STATION":
                    Typing = SectionType;
                    return;
                default:
                    throw new ArgumentException(SectionType + " is not a valid typing.");
            }
        }
        /// <summary>
        /// Setter for this sections count, representing the count of elements of the given type. I.e. 4 with a type of forests, means 4 trees
        /// </summary>
        /// <param name="Count"></param>
        public void SetCount(int Count)
        { this.Count = Count; }
    }
}
