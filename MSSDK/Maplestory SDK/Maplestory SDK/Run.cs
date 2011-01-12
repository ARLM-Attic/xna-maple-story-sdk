using Maplestory_SDK.Root_Class;
using Maplestory_SDK.User_Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maplestory_SDK
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Run : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player Actor;

        EnemyBase enemy1;

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

            //                 main   Skin     Face    Hair        Stat           Name          Title
            Actor = new Player(this, "Skin1", "0004", "0001", new int[] { 1, 1 }, "Actor Name", "Actor Title");

            enemy1 = new EnemyBase(this, "000001", false, false);
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

            Actor.Update();

            enemy1.Animation();

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyUp(Keys.Home))
                Actor.player.DEBUG = true;
            if (keyState.IsKeyUp(Keys.End))
                Actor.player.DEBUG = false;

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

            //Player.Draw(spriteBatch);
            Actor.Draw(spriteBatch);

            enemy1.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}