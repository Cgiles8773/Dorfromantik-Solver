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
        /// Used to keep track of the q,r,s coordinates 
        /// </summary>
        private class Coordinate
        {
            public int q;
            public int r;
            public int s;
            /// <summary>
            /// Creates a Coordinate with a q,r,s value. 
            /// See CoordinatesExample.png for a visual.
            /// </summary>
            /// <param name="q">The horizontal coordinate, decreases left, increases right</param>
            /// <param name="r">One of the diagonal coordinates. Increases up to the right, and decreases down to the left</param>
            /// <param name="s">One of the diagonal coordinates. Increases up to the left, and decreases down to the right</param>
            public Coordinate(int q, int r, int s)
            {
                this.q = q;
                this.r = r;
                this.s = s;
            }
            /// <summary>
            /// Creates a Coordinate with a q,r,s value of 0,0,0. 
            /// See CoordinatesExample.png for a visual.
            /// </summary>
            public Coordinate()
            {
                this.q = 0;
                this.r = 0;
                this.s = 0;
            }
        }
        private Dictionary<Coordinate, Tile> HexGrid;
        public TileMap()
        {
            HexGrid = new Dictionary<Coordinate, Tile>();
        }
    }
}
