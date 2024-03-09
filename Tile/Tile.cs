// Ignore Spelling: Subtile

using System.Collections;
using System.Security.Cryptography.X509Certificates;

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
        /// The array of sections used to organize the information in this tile. Note that the 0th element is the subtile.
        /// </summary>
        private Section[] Sections;
        /// <summary>
        /// Creates a new Tile without any typing, or count
        /// </summary>
        public Tile()
        {
            Sections = new Section[7];
            for(int i = 0; i < 7; i++)
                Sections[i] = new Section();
        }
        /// <summary>
        /// Creates a Tile with a given sub-tile and count.
        /// </summary>
        /// <param name="Subtile">The type of the sub-tile</param>
        /// <param name="count">The count of the element</param>
        public Tile(string SubtileType, int count) : this()
        {
            Sections[0] = new Section(SubtileType, count);
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (Section s in Sections)
            {
                hash += s.Typing.GetHashCode() % 100;
                hash += s.Count;
            }
            return hash;
        }
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            return this.GetHashCode() == obj.GetHashCode();
            
        }
        /// <summary>
        /// Creates a tile with the given sections. The first index should be the subtile, and then the next six should be sections 1-6.
        /// <requires>The IEnumerable contains no more and no less than seven sections.</requires>
        /// </summary>
        /// <param name="TileSections">The sections to assign to this tile</param>
        public Tile(IEnumerable<Section> TileSections) : this()
        {
            if (TileSections.Count() != 7)
            { throw new ArgumentException("The required amount of given sections is seven!"); }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    Sections[i] = TileSections.ElementAt(i);
                }
            }
        }
        /// <summary>
        /// Sets the section to the given type and count. Note: Section 0 is the subtile
        /// </summary>
        /// <param name="section">The number of the section</param>
        /// <param name="type">The type of the section</param>
        /// <param name="count">The count of the element</param>
        /// <returns>True if the section was updated successfully</returns>
        public bool SetEdge(int section, string type, int count)
        {
            return SetEdge(section, new Section(type, count));
        }
        /// <summary>
        /// Sets the section to the given type and count. Note: Section 0 is the subtile
        /// </summary>
        /// <param name="section">The number of the section</param>
        /// <param name="section">The section to assign to this tile</param>
        /// <returns>True if the section was updated successfully</returns>
        public bool SetEdge(int edge, Section section)
        {
            if (edge <= 7 && edge >= 0)
            {
                Sections[edge] = section;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns the given section of this tile if it exists. the 0th section of the tile is the subtile, and sections 1-6 are the edge sections
        /// in clockwise ordering.
        /// </summary>
        /// <param name="section">The section to get</param>
        /// <returns>Returns the given section of this tile if it exists</returns>
        /// <exception cref="IndexOutOfRangeException">If the section isn't between 0 and 7 inclusive</exception>
        public Section GetSection(int section)
        {
            if (section <= 7 && section >= 0)
            {
                return Sections[section];
            }
            else throw new IndexOutOfRangeException();
        }
        /// <summary>
        /// Returns a list representing this tile object. The list is 7 indexes in length, with the sub-tile being index 0.
        /// Edge1 is index 1, Edge2 is index 2, and so on.
        /// </summary>
        /// <returns>A list containing all sections of this tile</returns>
        public List<Section> ToList()
        {
            return Sections.ToList();
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
            List<Section> SubtileGroup = [Sections[0]];
            // Used to keep track of ungrouped/unvisted edges
            List<int> Ungrouped = [1,2,3,4,5,6];
            // Do the subtile grouping first to reduce the complexity of the remaining groups.
            for (int i = 1; i <= 6; i++)
            {
                if (Sections[i].Typing.Equals(Sections[0].Typing))
                {
                    Ungrouped.Remove(i);
                    SubtileGroup.Add(Sections[i]);
                }
            }
            SetOfGroups.Add(SubtileGroup);
            while (Ungrouped.Count != 0)
            {
                int i = Ungrouped.ElementAt(0);
                Ungrouped.Remove(i);
                Section Current = Sections[i];
                List<Section> CurrentGroup = [Current];
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
            if (Sections[LeftNeighbor].Typing.Equals(Sections[CurrentEdge].Typing) && Ungrouped.Contains(LeftNeighbor))
            {
                Ungrouped.Remove(LeftNeighbor);
                CurrentGroup.Add(Sections[LeftNeighbor]);
                GroupLeftNeighbor(LeftNeighbor, CurrentGroup, Ungrouped);
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// Calculates the left neighbor of the given edge. This is useful for wraparound cases.
        /// </summary>
        /// <param name="CurrentEdge">The edge to get the left neighbor of</param>
        /// <returns>The index of the neighbor</returns>
        public static int CalculateLeftNeighbor(int CurrentEdge)
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
            if (Sections[RightNeighbor].Typing.Equals(Sections[CurrentEdge].Typing) && Ungrouped.Contains(RightNeighbor))
            {
                Ungrouped.Remove(RightNeighbor);
                CurrentGroup.Add(Sections[RightNeighbor]);
                GroupRightNeighbor(RightNeighbor, CurrentGroup, Ungrouped);
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// Calculates the right neighbor of the given edge. This is useful for wraparound cases.
        /// </summary>
        /// <param name="CurrentEdge">The edge to get the right neighbor of</param>
        /// <returns>The index of the neighbor</returns>
        public static int CalculateRightNeighbor(int CurrentEdge)
        {
            int RightNeighbor = (CurrentEdge + 1) % 6;
            if (RightNeighbor == 0)
                RightNeighbor = 6;
            return RightNeighbor;
        }
        /// <summary>
        /// Rotates the tile clockwise "rotations" amount of times.
        /// </summary>
        /// <requires>Rotations cannot be negative!</requires>
        /// <param name="rotations">Amount of times to rotate</param>
        /// <exception cref="ArgumentException">If rotations is negative</exception>
        public void RotateClockwise(int rotations)
        {
            Rotate(rotations);
        }
        /// <summary>
        /// Rotates the tile counter clockwise "rotations" amount of times.
        /// </summary>
        /// <requires>Rotations cannot be negative!</requires>
        /// <param name="rotations">Amount of times to rotate</param>
        /// <exception cref="ArgumentException">If rotations is negative</exception>
        public void RotateCounterclockwise(int rotations)
        {
            Rotate(Math.Abs(rotations - 6) % 6);
        }
        /// <summary>
        /// Rotates the sections in this tile clockwise "rotations" amount of times. 
        /// </summary>
        /// <param name="rotations">Amount of times to rotate</param>
        /// <exception cref="ArgumentException">If rotations is negative</exception>
        private void Rotate(int rotations) 
        {
            if (rotations < 0)
                throw new ArgumentException();
            //6 rotations gives us the same hexagon that we started with
            if (rotations == 0 || rotations % 6 == 0)
                return;

            Queue<Section> Queue = new Queue<Section>();
            foreach (Section section in ToList())
            { Queue.Enqueue(section); }
            //Ignore Subtile
            Queue.Dequeue();
            for (int i = 1; i <= 6; i++)
            {
                int EndPosition = (i + rotations) % 6;
                if(EndPosition == 0)
                    EndPosition = 6;
                Sections[EndPosition] = Queue.Dequeue();
            }
        }
    }
}
