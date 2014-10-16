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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private InputManager inputManager;
        private LevelManager levelManager;
        private GameObjectManager gameObjectManager;
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
            gameObjectManager = new GameObjectManager(new Rectangle { X = 20, Y = 40, Height = 760, Width = 760 }, new GameObjectFactory(), new EventQueue());
            inputManager = new InputManager();
            levelManager = new LevelManager();

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameObjectManager.Add(0, new ScoreBar(Content.Load<SpriteFont>("monolight12"), new Vector2(0, 5), gameWidth));
            gameObjectManager.Add(1, new Background(Content.Load<Texture2D>("wall"), gameWidth, gameHeight, 20, 20));
            gameObjectManager.Add(2, new PlayerPaddle(TextureManager.CreateTexture(GraphicsDevice, 100, 20), new Vector2((gameWidth / 2) - 50, gameHeight - 60)));
            gameObjectManager.Add(3, new Ball(Content.Load<Texture2D>("ball"), new Vector2((gameWidth / 2) - 10, gameHeight / 2)));

            levelManager.AddTextures(new List<Texture2D> { Content.Load<Texture2D>("green") });
            levelManager.GenerateLevel(gameObjectManager, "1", 4);
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
            gameObjectManager.HandleCollisions();
            gameObjectManager.HandleEvents();

            if (gameObjectManager.IsLevelEnd()) {
                // Change to next level / state
            }

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
