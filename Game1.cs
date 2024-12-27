using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Topia
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Tile Tile1;

        public static Utils.Int2 CameraPosition;

        public static Tile[,] TileArray;

        public static Random rand;

        public static Utils.Int2 WorldSize;

        public static Texture2D BG;

        public static float[,] Light;

        public static int WX => WorldSize.x;
        public static int WY => WorldSize.y;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            rand = new();

            WorldSize = new(1000, 1000);

            CameraPosition = new(0, 0);

            TileArray = new Tile[WX, WY];

            Light = new float[WX, WY];


            foreach(GenPass pass in GenPass.Registry)
            {
                pass.Generate();
            }

            /*for(int i =  0; i < WX; i++)
            {
                for(int j = 0; j < WY; j++)
                {
                    TileArray[i, j] = rand.Next(0,10) < 5 ? new(new(16 * i, 16 * j), new(0, 0, 16, 16), 0) : null;
                    TileArray[i, j] = rand.Next(0,10) > 5 ? new(new(16 * i, 16 * j), new(0, 0, 16, 16), 1) : TileArray[i,j];
                }
            }*/
            //Tile1 = new(new(50, 50), new(0, 0, 16, 16), Content.Load<Texture2D>("Tile0"));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureAssets.TileTextures.Add(0, Content.Load<Texture2D>("Tile0"));
            TextureAssets.TileTextures.Add(1, Content.Load<Texture2D>("Tile1"));

            BG = Content.Load<Texture2D>("BG1");
        }
        int randomTimerFart = 0;
        public static KeyboardState state => Keyboard.GetState();
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CameraPosition.x += Utils.BoolToInt(state.IsKeyDown(Keys.D)) * CameraSpeed;
            CameraPosition.x -= Utils.BoolToInt(state.IsKeyDown(Keys.A)) * CameraSpeed;
            CameraPosition.y += Utils.BoolToInt(state.IsKeyDown(Keys.S)) * CameraSpeed;
            CameraPosition.y -= Utils.BoolToInt(state.IsKeyDown(Keys.W)) * CameraSpeed;

            CameraPosition.x = Math.Clamp(CameraPosition.x, 0, (WX * 16) - Window.ClientBounds.Width);
            CameraPosition.y = Math.Clamp(CameraPosition.y, 0, (WY * 16) - Window.ClientBounds.Height);

            Utils.Int2 TopLeft = new((int)Math.Floor(CameraPosition.x / 16f), (int)Math.Floor(CameraPosition.y / 16f));
            Utils.Int2 BottomRight = TopLeft + new Utils.Int2((int)Math.Floor(Window.ClientBounds.Width / 16f) + 1, (int)Math.Floor(Window.ClientBounds.Height / 16f) + 1);
            BottomRight.x = Math.Min(WX, BottomRight.x);
            BottomRight.y = Math.Min(WY, BottomRight.y);

            //Array.Clear(Light);

            for(int i = 0; i < WX; i++)
            {
                for(int j = 0; j < WY; j++)
                {
                    Light[i, j] = 0;
                }
            }

            for (int i = TopLeft.x; i < BottomRight.x; i++)
            {
                for (int j = TopLeft.y; j < BottomRight.y; j++)
                {
                   
                    if (TileArray[i, j] == null)
                    {
                        int minX = (int)Math.Max(Math.Floor(i - 5f), 0);
                        int maxX = (int)Math.Min(Math.Ceiling(i + 5f), WX - 1);
                        int minY = (int)Math.Max(Math.Floor(j - 5f), 0);
                        int maxY = (int)Math.Min(Math.Ceiling(j + 5f), WY - 1);

                        for (int x = minX; x < maxX; x++)
                        {
                            for (int y = minY; y < maxY; y++)
                            {
                                float distance = (float)Math.Sqrt(((i - x) ^ 2) + ((j - y) ^ 2));

                                if (distance <= 5f)
                                {
                                    float brightness = 0;
                                    if (distance > 0)
                                    {
                                        brightness = 1f / (distance * distance);
                                    }

                                    Light[i, j] = brightness;
                                    //Debug.Write($"{brightness} ");
                                }
                            }
                        }
                    }


                    if (TileArray[i, j] != null)
                        TileArray[i, j].Update();
                }
            }

            base.Update(gameTime);
        }

        public static int CameraSpeed = 10;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp, null, null, null, null);

            _spriteBatch.Draw(BG,new(0,0,Window.ClientBounds.Width,Window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

            Utils.Int2 TopLeft = new((int)Math.Floor(CameraPosition.x / 16f), (int)Math.Floor(CameraPosition.y / 16f));
            Utils.Int2 BottomRight = TopLeft + new Utils.Int2((int)Math.Floor(Window.ClientBounds.Width / 16f) + 1, (int)Math.Floor(Window.ClientBounds.Height / 16f) + 1);
            BottomRight.x = Math.Min(WX, BottomRight.x);
            BottomRight.y = Math.Min(WY, BottomRight.y);
            for (int i = TopLeft.x; i < BottomRight.x; i++)
            {
                for (int j = TopLeft.y; j < BottomRight.y; j++)
                {
                    if (TileArray[i, j] != null)
                        TileArray[i, j].Draw(_spriteBatch);
                }
            }

            _spriteBatch.End();
        }
    }
}
