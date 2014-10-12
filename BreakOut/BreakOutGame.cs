#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace BreakOut {

    /*
     * Test of: 
     *-----------------------
     * GameObjectManager architecture
     * Command structure to send messages
     * 
     * What to learn:
     *-----------------------
     * Powerups (Longer paddle, magnetic paddle, shooting paddle) 
     * Maps (brick arrangements) - loading and advancing to next etc
     * 
    */

    public class BreakOutGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager inputManager;
        GameObjectManager gameObjectManager;
        
        private int gameWidth = 800;
        private int gameHeight = 800;

        public BreakOutGame()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = gameWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = gameHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            gameObjectManager = new GameObjectManager();
            inputManager = new InputManager();
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameObjectManager.Add(0, new Background());
            gameObjectManager.Add(1, new PlayerPaddle(TextureManager.CreateTexture(GraphicsDevice, 100, 20), new Vector2((gameWidth / 2) - 50, gameHeight - 60)));
            gameObjectManager.Add(2, new Ball(TextureManager.CreateTexture(GraphicsDevice, 20, 20), new Vector2((gameWidth / 2) - 10, gameHeight / 2)));
            gameObjectManager.Add(3, new LevelManager());
        }

        protected override void UnloadContent() {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime) {
            // TODO: refactor this
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            gameObjectManager.HandleCommand(inputManager.GetCommand(Keyboard.GetState()));
            gameObjectManager.Update(deltaTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            gameObjectManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
