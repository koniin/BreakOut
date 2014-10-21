#region Using Statements
using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using BreakOut.GameEntities;
using BreakOut.States;
#endregion

namespace BreakOut {

    /*
     * Test of: 
     *-----------------------
     * SceneManager architecture
     * Command structure to send messages
     * 
     * What to learn:
     *-----------------------
     * Powerups (Longer paddle, magnetic paddle, shooting paddle) 
     * Maps (brick arrangements) - loading and advancing to next etc
     * 
    */

    public class BreakOutGame : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private StateManager stateManager;
        private KeyBoardManager keyBoardManager;
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
            ResourceManager.Init(Content, GraphicsDevice);
            stateManager = new StateManager();
            keyBoardManager = new KeyBoardManager();
            stateManager.PushState(new GameState());
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent() {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime) {
            stateManager.HandleInput(keyBoardManager.GetPressedKey());
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            stateManager.Update(deltaTime);

            if (stateManager.IsEmpty()) {
                Exit();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            stateManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
