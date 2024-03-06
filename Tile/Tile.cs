using System.Collections;

/// <summary>
/// Author:    Collin Giles
/// Date:      3/2/24
///
/// I, Collin Giles, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used are cited in my README file.
///
/// File Contents
///
/// The Tile class represents a hexagon with each edge being of a different type. This is a direct parallel to the hexagon tiles used in Dorfromantik game
/// by Toukana Interactive. 
/// 
/// Each tile is organized into 7 pieces. The central piece, known as the "Sub-tile", and the six edges of the hexagon.
/// The tiles are numbered in a clockwise order, with 1 being at the top.
/// See TileStructure.png to see an example of the tile structure.
/// Each edge and sub-tile is one of 7 main types: plain, forest, house, field, water, river, tracks, and water train station.
/// Note that at least two of the edges must match the sub-tile in type.
/// 
/// Tiles create groups, and matches.
/// Matches are made between adjacent tiles, when the edge of a tile matches the edge of another in typing.
/// Groups are the total count of each element of a type, that are connected via matches
/// <example>
/// If a tile has 6 houses, and matches to another tile that has 10, the group is 16 houses total. Groups can snake through tiles, and not all 
/// elements in a group need to be adjacent to be considered a group - meaning the groups are transitive
/// </example>
/// </summary>
namespace Solver
{
    /// <summary>
    /// The tile class representing a hexagon with six typed edges, and a sub-tile.
    /// An empty tile is not considered null, but is considered to have a type of "", and a count of 0.
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// The following are the edges and sub-tile of the tile
        /// </summary>
        public Section Edge1 { get; private set; }
        public Section Edge2 { get; private set; }
        public Section Edge3 { get; private set; }
        public Section Edge4 { get; private set; }
        public Section Edge5 { get; private set; }
        public Section Edge6 { get; private set; }
        public Section Subtile { get; private set; }
        /// <summary>
        /// Creates a new Tile without any typing, or count
        /// </summary>
        public Tile()
        {
            Edge1 = new Section();
            Edge2 = new Section();
            Edge3 = new Section();
            Edge4 = new Section();
            Edge5 = new Section();
            Edge6 = new Section();
            Subtile = new Section();
        }
        /// <summary>
        /// Creates a Tile with a given sub-tile and count.
        /// </summary>
        /// <param name="Subtile">The type of the sub-tile</param>
        /// <param name="count">The count of the element</param>
        public Tile(string SubtileType, int count) : this()
        {
            this.Subtile = new Section(SubtileType, count);
        }
        /// <summary>
        /// Creates a tile with the given sections. The first index should be the subtile, and then the next six should be sections 1-6.
        /// <requires>The IEnumerable contains no more and no less than seven sections.</requires>
        /// </summary>
        /// <param name="Sections">The sections to assign to this tile</param>
        public Tile(IEnumerable<Section> Sections)
        {
            if (Sections.Count() != 7)
            { throw new ArgumentException("The required amount of given sections is seven!"); }
            else
            {
                Subtile = Sections.ElementAt(0);
                Edge1 = Sections.ElementAt(1);
                Edge2 = Sections.ElementAt(2);
                Edge3 = Sections.ElementAt(3);
                Edge4 = Sections.ElementAt(4);
                Edge5 = Sections.ElementAt(5);
                Edge6 = Sections.ElementAt(6);
            }
        }
        /// <summary>
        /// Sets the edge numbered 1-6, to the type, and sets its elements count to the given count
        /// </summary>
        /// <param name="edge">The number of the edge</param>
        /// <param name="type">The type of the edge</param>
        /// <param name="count">The count of the element</param>
        /// <returns>True if the edge was updated successfully</returns>
        public bool SetEdge(int edge, string type, int count)
        {
            switch (edge)
            {
                case 1: this.Edge1 = new Section(type, count); return true;
                case 2: this.Edge2 = new Section(type, count); return true;
                case 3: this.Edge3 = new Section(type, count); return true;
                case 4: this.Edge4 = new Section(type, count); return true;
                case 5: this.Edge5 = new Section(type, count); return true;
                case 6: this.Edge6 = new Section(type, count); return true;
                default: return false;
            }
        }
        /// <summary>
        /// Sets the sub-tile type, and the count of elements
        /// </summary>
        /// <param name="Subtile">The type of the sub-tile</param>
        /// <param name="count">The count of the element</param>
        public void SetSubtile(string type, int count)
        {
            Subtile = new Section(type, count);
        }
        /// <summary>
        /// Gets the given edge, if it exists (edges 1-6)
        /// </summary>
        /// <param name="edge">The number of the edge to get</param>
        /// <returns>The edge corresponding to the given number</returns>
        /// <exception cref="IndexOutOfRangeException">If the index is not in the range 1-6</exception>
        public Section GetEdge(int edge)
        {
            switch (edge)
            {
                case 1: return this.Edge1;
                case 2: return this.Edge2;
                case 3: return this.Edge3;
                case 4: return this.Edge4;
                case 5: return this.Edge5;
                case 6: return this.Edge6;
                default: throw new IndexOutOfRangeException();
            }
        }
        /// <summary>
        /// Returns a list representing this tile object. The list is 7 indexes in length, with the sub-tile being index 0.
        /// Edge1 is index 1, Edge2 is index 2, and so on.
        /// </summary>
        /// <returns>A list containing all sections of this tile</returns>
        public IEnumerable ToList()
        {
            List<Section> Sections =
            [
                Subtile,
                Edge1,
                Edge2,
                Edge3,
                Edge4,
                Edge5,
                Edge6,
            ];
            return Sections;
        }
        /// <summary>
        /// An access override that allows quick retrieval of an edge in a tile, if the edge exists.
        /// Note that the edges are indexed 1-6, not 0-5. Refer to the example picture for more info.
        /// If an edge is retrieved that is outside of these indexes, an IndexOutOfRangeException may be thrown
        /// </summary>
        /// <param name="edge">The number of the edge to get</param>
        /// <returns>The edge corresponding to the given number</returns>
        /// <exception cref="IndexOutOfRangeException">If the index is not in the range 1-6</exception>
        public Section this[int edge]
        {
            get { return GetEdge(edge); }
            private set { }
        }
        /// <summary>
        /// Returns a set of groupings, which contains the sections in this tile that are grouped with each other. 
        /// In order for sections to be considered grouped, they must be adjacent within the same tile, 
        /// or have an adjacent section with a matching type.
        /// Note that each section of this Tile is in at most one group, so no section will be absent from the set of groups.
        /// <example>Looking at the TileStructure, if section 1 and 4 are both forests, they can only be
        /// connected if the sub-tile type is also a forest</example>
        /// <example>If section1 was a forest, and section 2 was a forest, then 1 and 2 would be connected. Additionally, if the sub-tile
        /// was also a forest, then 1, 2, and the sub-tile would be connected</example>
        /// </summary>
        /// <returns>A set of lists of connected sub-tiles.</returns>
        public IEnumerable<IEnumerable<Section>> GetGroups()
        {
            List<List<Section>> SetOfGroups = new List<List<Section>>();
            // Captures the subtile grouping of the tile
            List<Section> SubtileGroup = new List<Section> { Subtile };
            // Used to keep track of ungrouped/unvisted edges
            List<int> Ungrouped = new List<int> { 1,2,3,4,5,6 };
            // Do the subtile grouping first to reduce the complexity of the remaining groups.
            for (int i = 1; i <= 6; i++)
            {
                if (this[i].Typing.Equals(Subtile.Typing))
                {
                    Ungrouped.Remove(i);
                    SubtileGroup.Add(this[i]);
                }
            }
            SetOfGroups.Add(SubtileGroup);
            while (Ungrouped.Any())
            {
                int i = Ungrouped.ElementAt(0);
                Ungrouped.Remove(i);
                Section Current = this[i];
                List<Section> CurrentGroup = new List<Section> { Current };
                GroupLeftNeighbor(i, CurrentGroup, Ungrouped);
                GroupRightNeighbor(i, CurrentGroup, Ungrouped);
                SetOfGroups.Add(CurrentGroup);
            }
            /**
            foreach (int i in Ungrouped)
            {
                Section Current = this[i];
                List<Section> CurrentGroup = new List<Section> { Current };
                GroupLeftNeighbor(i, CurrentGroup, Ungrouped);
                GroupRightNeighbor(i, CurrentGroup, Ungrouped);
                SetOfGroups.Add(CurrentGroup);
            }**/
            return SetOfGroups;
        }
        /// <summary>
        /// Recursively groups the neighboring edges of a tile based on typing, and places the neighboring tiles into CurrentGroup, and removes them
        /// from Ungrouped.
        /// </summary>
        /// <param name="CurrentEdge">The current edge to check the neighbor of</param>
        /// <param name="CurrentGroup">The group to add to, if the neighbor is a match</param>
        /// <param name="Ungrouped">The edges that aren't in a group - used for housekeeping with the driver method</param>
        private void GroupLeftNeighbor(int CurrentEdge, List<Section> CurrentGroup, List<int> Ungrouped)
        {
            int LeftNeighbor = CalculateLeftNeighbor(CurrentEdge);
            if (this[LeftNeighbor].Typing.Equals(this[CurrentEdge].Typing) && Ungrouped.Contains(LeftNeighbor))
            {
                Ungrouped.Remove(LeftNeighbor);
                CurrentGroup.Add(this[LeftNeighbor]);
                GroupLeftNeighbor(LeftNeighbor, CurrentGroup, Ungrouped);
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// Calculates the left neighbor of the given index. This is useful for wraparound cases.
        /// </summary>
        /// <param name="CurrentEdge">The edge to get the left neighbor of</param>
        /// <returns>The index of the neighbor</returns>
        public int CalculateLeftNeighbor(int CurrentEdge)
        {
            int LeftNeighbor = (CurrentEdge - 1) % 6;
            if (LeftNeighbor == 0)
                LeftNeighbor = 6;
            return LeftNeighbor;
        }
        /// <summary>
        /// Recursively groups the neighboring edges of a tile based on typing, and places the neighboring tiles into CurrentGroup, and removes them
        /// from Ungrouped.
        /// </summary>
        /// <param name="CurrentEdge">The current edge to check the neighbor of</param>
        /// <param name="CurrentGroup">The group to add to, if the neighbor is a match</param>
        /// <param name="Ungrouped">The edges that aren't in a group - used for housekeeping with the driver method</param>
        private void GroupRightNeighbor(int CurrentEdge, List<Section> CurrentGroup, List<int> Ungrouped)
        {
            int RightNeighbor = CalculateRightNeighbor(CurrentEdge);
            if (this[RightNeighbor].Typing.Equals(this[CurrentEdge].Typing) && Ungrouped.Contains(RightNeighbor))
            {
                Ungrouped.Remove(RightNeighbor);
                CurrentGroup.Add(this[RightNeighbor]);
                GroupRightNeighbor(RightNeighbor, CurrentGroup, Ungrouped);
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// Calculates the right neighbor of the given index. This is useful for wraparound cases.
        /// </summary>
        /// <param name="CurrentEdge">The edge to get the right neighbor of</param>
        /// <returns>The index of the neighbor</returns>
        public int CalculateRightNeighbor(int CurrentEdge)
        {
            int RightNeighbor = (CurrentEdge + 1) % 6;
            if (RightNeighbor == 0)
                RightNeighbor = 6;
            return RightNeighbor;
        }
    }
}
