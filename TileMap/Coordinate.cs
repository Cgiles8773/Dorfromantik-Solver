/// <summary>
/// Author:    Collin Giles
/// Date:      3/4/24
///
/// I, Collin Giles, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used are cited in my README file.
///
/// File Contents
/// 
/// The Coordinates of the grid are q,r,s - these are analogous to x,y,z. q,r,s is used to avoid confusion.
/// The Coordinate class represents a trio of coordinates, q,r,s which are used to determine the position of a Tile in the TileMap.
/// The Coordinate object is immutable, and the sum of q,r,s must equal 0.
/// 
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
    /// <summary>
    /// The Coordinate class represents a trio of coordinates, q,r,s which are used to determine the position of a Tile in the TileMap.
    /// The Coordinate object is immutable, and the sum of q,r,s must equal 0.
    /// 
    /// 
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// The following three ints define the position for this Coordinate object
        /// </summary>
        readonly public int q;
        readonly public int r;
        readonly public int s;
        /// <summary>
        /// Creates a Coordinate with a q,r,s value. 
        /// See CoordinatesExample.png for a visual.
        /// <required>The sum of q + r + s must equal 0 for every coordinate trio.</required>
        /// </summary>
        /// <param name="q">The horizontal coordinate, decreases left, increases right</param>
        /// <param name="r">One of the diagonal coordinates. Increases up to the right, and decreases down to the left</param>
        /// <param name="s">One of the diagonal coordinates. Increases up to the left, and decreases down to the right</param>
        /// <exception cref="ArgumentException">An exception is thrown if q + r + s != 0</exception>
        public Coordinate(int q, int r, int s)
        {
            if (q + r + s == 0)
            {
                this.q = q;
                this.r = r;
                this.s = s;
            }
            else
            { throw new ArgumentException("q + r + s must equals zero!"); }
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
        /// <summary>
        /// Checks the equality of two Coordinates by determining if their q,r,s coordinates are all the same.
        /// </summary>
        /// <returns>True if all three coordinates match</returns>
        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Checks the equality of two Coordinates by determining if their q,r,s coordinates are all the same.
        /// </summary>
        /// <returns>False if all three coordinates match</returns>
        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return !a.Equals(b);
        }
        /// <summary>
        /// Checks if this object is equal to the passed object, by determining if it is the same type, and then
        /// determining if all three coordinates are the same
        /// </summary>
        /// <returns>True if obj is a Coordinate, and all three coordinates match</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Coordinate)
            {
                Coordinate temp = (Coordinate)obj;
                return (q == temp.q && r == temp.r && s == temp.s);
            }
            return false;
        }
        /// <summary>
        /// Adds the q,r,s values of Coordinate a, to Coordinate b
        /// </summary>
        /// <param name="a">The left hand side trio of coordinates</param>
        /// <param name="b">The right hand side trio of coordinates</param>
        /// <returns>A Coordinate with the position (q+q, r+r, s+r)</returns>
        public static Coordinate Add(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.q + b.q, a.r + b.r, a.s + b.s);
        }
        /// <summary>
        /// Subtracts the q,r,s values of Coordinate a, from Coordinate b
        /// </summary>
        /// <param name="a">The left hand side trio of coordinates</param>
        /// <param name="b">The right hand side trio of coordinates</param>
        /// <returns>A Coordinate with the position (q-q, r-r, s-r)</returns>
        public static Coordinate Subtract(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.q - b.q, a.r - b.r, a.s - b.s);
        }
        /// <summary>
        /// Multiplies the q,r,s values of the Coordinate object by the value of k.
        /// </summary>
        /// <param name="a">The Coordinate to multiply the q,r,s values of</param>
        /// <param name="k">The value to multiply q,r,s by</param>
        /// <returns>A Coordinate with the position (q*k, r*k, s*k)</returns>
        public static Coordinate Multiply(Coordinate a, int k)
        {
            return new Coordinate(a.q * k, a.r * k, a.s * k);
        }
        /// <summary>
        /// Returns the hash code for this object
        /// </summary>
        /// <returns>The hash code for this object</returns>
        public override int GetHashCode()
        {
            return q.GetHashCode() + r.GetHashCode() + s.GetHashCode();
        }
    }
}
