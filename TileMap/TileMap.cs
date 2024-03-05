/// <summary>
/// Author:    Collin Giles
/// Date:      3/3/24
///
/// I, Collin Giles, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used are cited in my README file.
///
/// File Contents
///
/// The TileMap class provides a model that organizes Tiles into a coordinate plane.
/// The Tiles are organized into a hexagonal grid, in the flat top hexagon orientation.
/// The coordinates of the grid are q,r,s - these are analogous to x,y,z. q,r,s is used to avoid confusion.
/// See the CoordinatesExample.png in this solution for an example.
/// The center of the grid is considered to be 0,0,0
/// 
/// <note>
/// q+r+s = 0 as a constraint
/// </note>
/// 
/// </example>
/// </summary>
namespace Solver
{
    public class TileMap
    {
        /// <summary>
        /// The TileMap is organized into a collection of Coordinates, used to determine position, and Tiles, which contain information about groups,
        /// section types, etc.
        /// </summary>
        private Dictionary<Coordinate, Tile> TileGrid;
        public TileMap()
        {
            TileGrid = new Dictionary<Coordinate, Tile>();
        }
        /// <summary>
        /// A list of the six transformations used to move over one hexagon in any direction.
        /// The directions are ordered as follows: The top edge is associated with 1, and going clockwise, each edge is numbered 1-6.
        /// </summary>
        private static List<Coordinate> Coord_Directions = new List<Coordinate>
        {
            new Coordinate(0, -1, 1), new Coordinate(1, -1, 0), new Coordinate(1, 0, -1),
            new Coordinate(0, 1, -1), new Coordinate(-1, 1, 0), new Coordinate(-1, 0, 1)
        };
        /// <summary>
        /// Gets the directional vector for a given direction. Note that the top edge is associates with 1, and going clockwise, 
        /// each edge is numbered 1-6.
        /// <example>
        /// Say you wanted to get the directional coordinates for the tile directly below 0,0,0. This would be done by writing CoordinateDirection(4),
        /// which would return a Coordinate with the values (0,1,-1), which defines the transformation necessary to arrive at the desired tile.
        /// </example>
        /// </summary>
        /// <param name="direction">A number 1-6</param>
        /// <returns>The directional vector for a given direction.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the given number is not in the range 1-6</exception>
        public static Coordinate CoordinateDirection(int direction)
        {
            if (direction < 1 || direction >= 7)
                throw new ArgumentOutOfRangeException(nameof(direction), "Direction must be between 1 and 6");
            return Coord_Directions[direction - 1];
        }
        /// <summary>
        /// Returns the coordinates of a neighboring tile in a given direction 1-6.
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Coordinate Neighbor(Coordinate coord, int direction)
        {
            return Coordinate.Add(coord, CoordinateDirection(direction));
        }
        /// <summary>
        /// Gets the neighboring tile of "home" in a given direction. Note that the top edge of a tile is associated with 1, and going clockwise,
        /// each edge is numbered 1-6.
        /// </summary>
        /// <param name="home">The starting tile</param>
        /// <param name="direction">The direction to travel in</param>
        /// <returns>The neighboring tile, if it exists.</returns>
        public Tile? GetNeighboringTile(Coordinate home, int direction)
        {
            Tile? neighbor;
            TileGrid.TryGetValue(Neighbor(home, direction), out neighbor);
            return neighbor;
        }
    }
}
