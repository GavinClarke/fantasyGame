using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fantasy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState currentKeyboardState, previousKeyboardState;
        const byte MAINMENU = 0, OVERWORLD = 1, FIGHTING = 2, DOORS = 4, ETC = 3;
        byte gameMode = OVERWORLD;
        
        ///////////////////
        //Combat Variables
        ///////////////////
        Combat combat;

        //////////////////////
        //non combat Variables
        //////////////////////

        Texture2D pixel, tile;
        SpriteFont font;
        Color[] data;

        ///////////////////
        // Overworld Variables
        Camera camera;
        Entity entities;
        Level level;
        const int TILESIZE = 30;
        // Size of the World/Level in Pixels.
        // Does not effect screensize.
        const int WORLDWIDTH = 720;
        const int WORLDHEIGHT = 720;
        Vector2[] points = new Vector2[4];
        ///////////////////

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 480;
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Creating entity player
            camera = new Camera(GraphicsDevice.Viewport, WORLDWIDTH, WORLDHEIGHT);
            entities = new Entity();
            level = new Level();
            // One Pixel Texture, handy for testing without importing texture
            pixel = new Texture2D(base.GraphicsDevice, 1, 1);
            data = new Color[] { Color.White };
            pixel.SetData<Color>(data);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load in font
            font = Content.Load<SpriteFont>("font");
            tile = Content.Load<Texture2D>("tilemap");
            combat = new Combat(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Keyboard Update
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            switch (gameMode)
            {
                case MAINMENU:
                    UpdateMainMenu(gameTime);
                    break;
                case OVERWORLD:
                    UpdateOverworld(gameTime);
                    break;
                case FIGHTING:
                    UpdateFighting(gameTime);
                    break;
            }
            base.Update(gameTime);
        }

        ///////////////////////////////
        //  Update for different states
        ///////////////////////////////
        public void UpdateMainMenu(GameTime gametime)
        {
        }
        public void UpdateOverworld(GameTime gametime)
        {
            entities.Update(currentKeyboardState, previousKeyboardState, TILESIZE);
            camera.Update(entities.GetCentre);
            EntityCollision();

        }
        public void UpdateFighting(GameTime gametime)
        {
            combat.Update(gametime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            switch (gameMode)
            {
                case MAINMENU:
                    DrawMainMenu();
                    break;
                case OVERWORLD:
                    DrawOverworld();
                    break;
                case FIGHTING:
                    DrawFighting();
                    break;
            }
            base.Draw(gameTime);
        }
        ///////////////////////////////
        //  Draw for different states
        ///////////////////////////////
        public void DrawMainMenu()
        {
        }
        public void DrawOverworld()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.GetTranslation);
            level.Draw(spriteBatch, tile);
            entities.Draw(spriteBatch, pixel);
            spriteBatch.DrawString(font, "" + entities.GetDirection, new Vector2(100, 100), Color.White);
            // spriteBatch.DrawString(font, "" + entities.GetTilePosition, new Vector2(100, 150), Color.White);
            spriteBatch.End();
        }
        public void DrawFighting()
        {
            spriteBatch.Begin();
            combat.Draw(spriteBatch);
            spriteBatch.End();
        }

        ///////////////////////////////
        //  Entity Collision with Walls
        ///////////////////////////////
        public void EntityCollision()
        {
            points[0] = new Vector2((int)entities.GetPosition.X / TILESIZE, (int)entities.GetPosition.Y / TILESIZE);
            points[1] = new Vector2(((int)entities.GetPosition.X + entities.GetSize) / TILESIZE, (int)entities.GetPosition.Y / TILESIZE);

            points[2] = new Vector2((int)entities.GetPosition.X / TILESIZE, ((int)entities.GetPosition.Y + entities.GetSize) / TILESIZE);
            points[3] = new Vector2(((int)entities.GetPosition.X + entities.GetSize) / TILESIZE, ((int)entities.GetPosition.Y + entities.GetSize) / TILESIZE);

            for (int i = 0; i < 4; i++)
            {
                if (level.Collision(points[i]))
                {
                    if (entities.GetDirection == 0)
                    {
                        entities.GetPosition = new Vector2(entities.GetPosition.X, (points[0].Y + 1) * TILESIZE + 1);
                        entities.SetMoveUp = false;
                    }
                    else if (entities.GetDirection == 2)
                    {
                        entities.GetPosition = new Vector2((points[0].X + 1) * TILESIZE + 1, entities.GetPosition.Y);
                        entities.SetMoveLeft = false;
                    }
                    else if (entities.GetDirection == 1)
                    {
                        entities.GetPosition = new Vector2(entities.GetPosition.X, (points[2].Y - 1) * TILESIZE - 1);
                        entities.SetMoveDown = false;
                    }
                    else if (entities.GetDirection == 3)
                    {
                        entities.GetPosition = new Vector2((points[3].X - 1) * TILESIZE - 1, entities.GetPosition.Y);
                        entities.SetMoveRight = false;
                    }
                    gameMode = FIGHTING;
                }
            }
        }
    }
}
