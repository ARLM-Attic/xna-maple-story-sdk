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
using Maplestory_SDK.User_Class;
using Maplestory_SDK.Root_Class;

namespace Maplestory_SDK
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Run : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int size = 32;

        Texture2D bg;
        Texture2D gridCell;
        Texture2D tileSheet;
        Texture2D crosshair;
        Texture2D window_backing;

        Character Player;

        Editor editor;
        Map tileMap;

        // enable editor mode
        bool editorOn = false;
        // cursor location
        Vector2 cursorLoc = new Vector2(0, 0);

        public Run()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Player = new Character(this,"Skin1","0004","0001", 4, 4);

            bg = this.Content.Load<Texture2D>("stage");
            gridCell = this.Content.Load<Texture2D>("gridcell");
            tileSheet = this.Content.Load<Texture2D>("tileSheet");
            crosshair = this.Content.Load<Texture2D>("crosshair");
            window_backing = this.Content.Load<Texture2D>("color_backing");

            editor = new Editor(GraphicsDevice); //Initializes editor
            tileMap = new Map(GraphicsDevice);   //Initializes map
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            Player.KeyInput();
            Player.Move();

            KeyboardState keyState = Keyboard.GetState(); //The below code changes the game to/from editor mode
            if (keyState.IsKeyDown(Keys.PageUp))
                editorOn = true;
            else if (keyState.IsKeyDown(Keys.PageDown))
                editorOn = false;
            else if (keyState.IsKeyDown(Keys.D1))
                Player.Skin = "Skin1";
            else if (keyState.IsKeyDown(Keys.D2))
                Player.Skin = "Skin2";
            else if (keyState.IsKeyDown(Keys.D3))
                Player.Face = "0001";
            else if (keyState.IsKeyDown(Keys.D4))
                Player.Face = "0002";
            else if (keyState.IsKeyDown(Keys.D5))
                Player.Face = "0003";
            else if (keyState.IsKeyDown(Keys.D6))
                Player.Face = "0004";
            else if (keyState.IsKeyDown(Keys.D7))
                Player.Hair = "0001";
            else if (keyState.IsKeyDown(Keys.D8))
                Player.Hair = "0002";
            else if (keyState.IsKeyDown(Keys.D9))
                Player.Hair = "0003";
            else if (keyState.IsKeyDown(Keys.D0))
                Player.Hair = "0004";

            MouseState mouseState = Mouse.GetState();
            Vector2 cursorLoc = new Vector2((mouseState.X / size) * size - 2, (mouseState.Y / size) * size - 2);
            if (editorOn) //Performs editor code based on current condition of the mouse
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    editor.Click(cursorLoc, tileMap, tileSheet);
                else
                    editor.Release();

                if (mouseState.RightButton == ButtonState.Pressed)
                    editor.Erase(cursorLoc, tileMap);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            tileMap.DrawMap(spriteBatch, tileSheet);

            Player.Draw(spriteBatch);

            if (editorOn)
                editor.DrawEditor(spriteBatch, crosshair, gridCell, tileSheet, window_backing);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
