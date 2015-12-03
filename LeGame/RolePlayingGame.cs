﻿using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LeGame.Models.Characters;
using LeGame.Models.Characters.Player;
using LeGame.Models.Levels;

namespace LeGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RolePlayingGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D testObject;
        Character testChar;
        private Level testLevel;

        public RolePlayingGame()
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
            IsMouseVisible = true;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            testLevel = new Level(@"..\..\..\Content\Maps\testMap2.txt", Content);
            testObject = Content.Load<Texture2D>(@"TestObjects/kappa");

            graphics.PreferredBackBufferWidth = GlobalVariables.WINDOW_WIDTH; // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = GlobalVariables.WINDOW_HEIGHT;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            Vector2 pos = new Vector2(0, 0);
            testChar = new TestChar(pos, "Pesho", 100, 100, 15, testObject);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            testChar.Move();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            testLevel.Tiles.ForEach(t => spriteBatch.Draw(t.Image, t.Position));
            spriteBatch.Draw(testChar.Texture, testChar.Position, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
