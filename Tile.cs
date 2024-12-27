using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topia
{
    
    
    public class Tile : Entity
    {
        public Utils.Int2 TilePos => Position / 16;

        public int Light;

        public int TileID;

        public Tile(Utils.Int2 pos, Utils.Int4 Rect, int ID)
        {
            this.Position = pos;
            this.Hitbox = Rect;
            this.Frame = new(0, 0, 16, 16);
            this.color = Color.White;
            this.TileID = ID;
            
        }

        public bool active = false;

        

        public override void Draw(SpriteBatch sb)
        {
            active = true;
            Utils.Int2 drawpos = this.Position - (Utils.Int2)Game1.CameraPosition; 
            sb.Draw(this.Texture, drawpos.ToDestination(this.Frame.Bounds()), this.Frame.ToRect(), this.color, 0f, Vector2.Zero, SpriteEffects.None,0);
        }

        public string AdjToString(Tile[] adj)
        {
            string s = "";
            for(int i = 0; i < adj.Length; i++)
            {
                s += adj[i] == null ? "0" : "1";
            }
            return s;
        }

        public Dictionary<string, Utils.Int4> AdjToFrame = new()
        {
            {"0000", new(0,0,16,16) },
            {"0010", new(18,0,16,16) },
            {"1010", new(36,0,16,16) },
            {"1000", new(54,0,16,16) },
            {"0001", new(0,18,16,16) },
            {"0011", new(18,18,16,16) },
            {"1011", new(36,18,16,16) },
            {"1001", new(54,18,16,16) },
            {"0101", new(0,36,16,16) },
            {"0111", new(18,36,16,16) },
            {"1111", new(36,36,16,16) },
            {"1101", new(54,36,16,16) },
            {"0100", new(0,54,16,16) },
            {"0110", new(18,54,16,16) },
            {"1110", new(36,54,16,16) },
            {"1100", new(54,54,16,16) },
        };

        public static bool Alone(int i, int j)
        {
            Tile[] adjacent = new Tile[4]
            {
                i - 1 >= 0 ? Game1.TileArray[i - 1, j] : null,
                j - 1 >= 0 ? Game1.TileArray[i, j - 1] : null,
                i + 1 < Game1.WX ? Game1.TileArray[i + 1, j] : null,
                j + 1 < Game1.WY ? Game1.TileArray[i, j + 1] : null,
            };

            return adjacent.Where(k => k == null).Count() == 4 ? true : false;
        }
        public override void Update()
        {
            this.Texture = TextureAssets.TileTextures[this.TileID];

            Tile[] adjacent = new Tile[4]
            {
                TilePos.x - 1 >= 0 ? Game1.TileArray[TilePos.x - 1, TilePos.y] : null,
                TilePos.y - 1 >= 0 ? Game1.TileArray[TilePos.x, TilePos.y - 1] : null,
                TilePos.x + 1 < Game1.WX ? Game1.TileArray[TilePos.x + 1, TilePos.y] : null,
                TilePos.y + 1 < Game1.WY ? Game1.TileArray[TilePos.x, TilePos.y + 1] : null,
            };

            for(int i = 0; i < adjacent.Length; i++)
            {
                if (adjacent[i] != null)
                {
                    if ((!TileData.TileMergeData[TileID].Contains(adjacent[i].TileID)) && adjacent[i].TileID != TileID)adjacent[i] = null;
                }
            }

            float light = Game1.Light[TilePos.x, TilePos.y];
            this.color = new(light,light, light);

            /*if (Game1.state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.O))
            {
                Debug.WriteLine($"Position {TilePos.x} {TilePos.y} {AdjToString(adjacent)}");
            }*/

            this.Frame = AdjToFrame[AdjToString(adjacent)];

            active = false;
        }
    }
}
