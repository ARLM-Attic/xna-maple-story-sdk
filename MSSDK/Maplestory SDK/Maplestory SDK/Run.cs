using System;
using Maplestory_SDK.Root_Class;
using Maplestory_SDK.Tool;
using Maplestory_SDK.User_Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;

namespace Maplestory_SDK
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Run : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // create test
        Enemy enemy;
        Player player;

        // create IDE menu
        Manager manager;
        Manager mapeditor;
        Manager UI;
        IDEMain IDE;
        // map
        Map map;

        public Run()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Maple Story GDK";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            manager = new Manager(this, graphics, "Default");
            mapeditor = new Manager(this, graphics, "Default");
            UI = new Manager(this, graphics, "Default");
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
            base.Initialize();

            manager.Initialize();
            //manager.RenderTarget = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            //manager.TargetFrames = 60;
            mapeditor.Initialize();
            //mapeditor.RenderTarget = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            //mapeditor.TargetFrames = 60;
            UI.Initialize();

            map = new Map(this);
            map.LoadMap("testmap");
            map.DrawCollusion = false;
            // create test
            enemy = new Enemy(this, "000001", true, false, true);
            player = new Player(this, UI, "Skin1", "0004", "0001", "NamKazt");
            // create manager
            IDE = new IDEMain(manager, mapeditor, player, map);
            // enable IDE
            IDE.ENABLE = false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public void FPSUpdate(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            enemy.Update();
            player.Update(map, spriteBatch);
            FPSUpdate(gameTime);
            UI.Update(gameTime);
            base.Update(gameTime);
            if (IDE.ENABLE)
                IDE.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            frameCounter++;
            if (IDE.ENABLE)
            {
                IDE.Draw(gameTime);
                GraphicsDevice.Clear(Color.SkyBlue);
                base.Draw(gameTime);
                // TODO: Add your drawing code here
                // for titles editor
                IDE.DrawEditor(gameTime);
                GraphicsDevice.Clear(Color.SkyBlue);
                IDE.EndDrawEditor();

                spriteBatch.Begin();
                map.Draw(spriteBatch);
                IDE.MapEditor.CollusionDraw(spriteBatch);
                enemy.Draw(spriteBatch);
                player.Draw(spriteBatch, gameTime);
                spriteBatch.DrawString(Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), string.Format("fps: {0}", frameRate), new Vector2(33, 33), Color.Black);

                UI.BeginDraw(gameTime);
                UI.EndDraw();

                spriteBatch.End();

                IDE.EndDraw();
            }
            else
            {
                // draw interface
                UI.BeginDraw(gameTime);
                GraphicsDevice.Clear(Color.SkyBlue);
                base.Draw(gameTime);

                spriteBatch.Begin();
                map.Draw(spriteBatch);
                spriteBatch.End();

                spriteBatch.Begin();
                enemy.Draw(spriteBatch);
                spriteBatch.End();

                UI.EndDraw();

                spriteBatch.Begin();
                player.Draw(spriteBatch, gameTime);
                spriteBatch.End();
            }
        }
    }
}