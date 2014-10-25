using BreakOut.GameEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.States {
    public class GameState : State {
        private SceneManager sceneManager;
        private LevelManager levelManager;
        private InputManager inputManager;
        private int gameWidth = 800;
        private int gameHeight = 800;

        public GameState() {
        }

        public override void Init() {
            inputManager = new InputManager();
            levelManager = new LevelManager();
            sceneManager = new SceneManager(new Rectangle { X = 20, Y = 40, Height = 760, Width = 760 }, new EntityFactory(), EventQueue);

            sceneManager.Add(0, new ScoreBar(ResourceManager.Load<SpriteFont>("monolight12"), new Vector2(0, 5), gameWidth, 5, 1));
            sceneManager.Add(1, new Background(ResourceManager.Load<Texture2D>("wall"), gameWidth, gameHeight, 20, 20));
            sceneManager.Add(2, new PlayerPaddle(ResourceManager.CreateTexture(100, 20), new Vector2((gameWidth / 2) - 50, gameHeight - 60)));
            sceneManager.Add(3, new Ball(ResourceManager.Load<Texture2D>("ball"), new Vector2((gameWidth / 2) - 10, gameHeight - 90)));

            levelManager.AddTextures(new List<Texture2D> { ResourceManager.Load<Texture2D>("green") });
            levelManager.GenerateLevel(sceneManager, "1", 4);
        }

        public override bool Update(float deltaTime) {
            sceneManager.Update(deltaTime);

            if (sceneManager.IsLevelEnd())
                StateManager.PushState(new GameEndedState("Congratulations!"));

            return false;
        }

        public override bool Draw(SpriteBatch spriteBatch) {
            sceneManager.Draw(spriteBatch);
            return true;
        }

        public override bool HandleInput(Keys key) {
            if (key == Keys.Escape)
                StateManager.PopState();
            else if (key == Keys.P)
                StateManager.PushState(new PauseState());
            else
                sceneManager.HandleCommand(inputManager.GetCommand(key));

            return false;
        }
    }
}
