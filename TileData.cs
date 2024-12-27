using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topia
{
    public class TileData
    {
        public static Dictionary<int, int[]> TileMergeData = new Dictionary<int, int[]>() 
        {
            {(int)TileID.Dirt, new int[] { (int)TileID.Stone} },
            {(int)TileID.Stone, new int[] { (int)TileID.Dirt} }
        };

        public enum TileID
        {
            Dirt = 0,
            Stone = 1
        }
    }
}
