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

        Character Player;

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
            // initialize player   main  skin    face   hair  framestand  framewalk
            // frame's default value is : 4 for all skin in maple story
            // frame stand is 3, but i have been inserted 1 frame to improve smoothing
            Player = new Character(this,"Skin1","0004","0001", 4, 4);
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

            KeyboardState keyState = Keyboard.GetState(); 
            // option
            // choose skin and gadget
            if (keyState.IsKeyDown(Keys.D1))
                Player.Skin = "Skin1";
                // face
            else if (keyState.IsKeyDown(Keys.D3))
                Player.Face = "0001";
            else if (keyState.IsKeyDown(Keys.D4))
                Player.Face = "0002";
            else if (keyState.IsKeyDown(Keys.D5))
                Player.Face = "0003";
            else if (keyState.IsKeyDown(Keys.D6))
                Player.Face = "0004";
                // hair
            else if (keyState.IsKeyDown(Keys.D7))
                Player.Hair = "0001";
            else if (keyState.IsKeyDown(Keys.D8))
                Player.Hair = "0002";
            else if (keyState.IsKeyDown(Keys.D9))
                Player.Hair = "0003";
            else if (keyState.IsKeyDown(Keys.D0))
                Player.Hair = "0004";
            // debug : haft-press
            // - slow 
            else if (keyState.IsKeyDown(Keys.PageUp))
            {
                if (Player.DEBUG == false) Player.DEBUG = true;
                else Player.DEBUG = false;
            } // show/hide infomation of body and gadget
            else if (keyState.IsKeyDown(Keys.PageDown)) // haft-press
            {
                if (Player.INFO == false) Player.INFO = true;
                else Player.INFO = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //tileMap.DrawMap(spriteBatch, tileSheet);

            Player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
