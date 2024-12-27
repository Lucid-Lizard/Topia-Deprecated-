using Microsoft.Xna.Framework;
using SharpDX;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topia
{
    public static class GenPasses
    {
        public class StartPass : GenPass
        {
            public StartPass(string name) : base(name)
            {
                this.name = name;
            }

            public override void Generate()
            {
                int baseHeight = 25;
                int fluctuation = 10;

                OpenSimplexNoise noise = new(ran.Next(-10000, 10000));

                for(int i = 0; i < MapWidth; i++)
                {

                    int MaxHeight = baseHeight;

                    float ran1 = (float)noise.Evaluate(i / 25f, 0);
                    float ran2 = (float)noise.Evaluate((i + 1000000) / 5f, 1234);
                    ran1 += ran2 / 4f;

                    MaxHeight += (int)(fluctuation * ran1);

                    for(int  j = 0; j < MapHeight; j++)
                    {
                        if(j >= MaxHeight)
                        {
                            Map[i, j] = new Tile(new(i * 16, j * 16), new(0, 0, 16, 16), 0);
                        }
                    }
                }
            }
        }

        public class StonePass : GenPass
        {
            public StonePass(string name) : base(name)
            {
                this.name = name;
            }

            public override void Generate()
            {
                int baseHeight = 38;
                int fluctuation = 10;

                OpenSimplexNoise noise = new(ran.Next(-10000, 10000));

                for (int i = 0; i < MapWidth; i++)
                {

                    int MaxHeight = baseHeight;

                    float ran1 = (float)noise.Evaluate(i / 25f, 0);
                    float ran2 = (float)noise.Evaluate((i + 1000000) / 5f, 1234);
                    ran1 += ran2 / 4f;

                    MaxHeight += (int)(fluctuation * ran1);

                    for (int j = 0; j < MapHeight; j++)
                    {
                        if (j >= MaxHeight)
                        {
                            if (Map[i,j] != null)
                                Map[i, j].TileID = (int)TileData.TileID.Stone;
                        }
                    }
                }
            }

            
        }
        public class CavePass : GenPass
        {
            public CavePass(string name) : base(name)
            {
                this.name = name;
            }

            public override void Generate()
            {
                OpenSimplexNoise noise = new(ran.Next(-10000, 10000));

                for (int i = 0; i < MapWidth; i++)
                {

                    for (int j = 0; j < MapHeight; j++)
                    {
                        double ran1 = noise.Evaluate((i + 6969f) / 25f, (j - 6969f) / 25f);
                        double ran2 = noise.Evaluate((-i + 6969f) / 25f, (-j - 6969f) / 25f);
                        //double ran2 = noise.Evaluate((i + 123513f) / 25f, (j - 12341234f) / 20f);
                        double ran3 = MathHelper.Lerp(-0.2f, 0.2f, (float)ran2);

                        ran1 = Math.Clamp(ran1 + ran3, 0f, 1f);

                        if (ran1 > 0.1f && ran1 < 0.35f) Map[i, j] = null;
                    }
                }
            }
        }
    }
}
