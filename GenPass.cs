using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topia
{
    public class GenPass
    {
        public string name;
        public static Tile[,] Map => Game1.TileArray;
        public int MapWidth => Game1.WorldSize.x;
        public int MapHeight => Game1.WorldSize.y;

        public static List<GenPass> Registry = new()
        {
            new GenPasses.StartPass("Base"),
            new GenPasses.StonePass("StoneLayer"),
            new GenPasses.CavePass("Caves"),
        };

        public Random ran => Game1.rand;
        public GenPass(string name)
        {
            this.name = name;
        }

        public virtual void Generate() { }
    }
}
