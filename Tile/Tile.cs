/// <summary>
/// Author:    [Your Name]
/// Date:      [Date of Creation]
///
/// I, Collin Giles, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// File Contents
///
/// The Tile class represents a hexagon with each edge being of a different type. This is a direct parallel to the hexagon tiles used in Dorfromantik game
/// by Toukana Interactive. 
/// 
/// Each tile is organized into 7 pieces. The central piece, known as the "Sub-tile", and the six edges of the hexagon.
/// Each edge and sub-tile is one of 7 main types: plain, forest, house, field, water, river, and tracks.
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
namespace Tile
{
    /// <summary>
    /// The tile class representing a hexagon with six typed edges, and a sub-tile
    /// </summary>
    public class Tile
    {
        
        public Tile()
        { }
    }
}
