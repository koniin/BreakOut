using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class GameObjectManager {
        private readonly SortedList<int, GameObject> gameObjects;
        public GameObjectManager() {
            gameObjects = new SortedList<int, GameObject>();
        }

        public void Add(int index, GameObject gameObject) {
            gameObjects.Add(index, gameObject);
        }

        public void Update(float deltaTime) {
            LoopGameObjects(x => x.Update(deltaTime));
        }

        public void Draw(SpriteBatch spriteBatch) {
            LoopGameObjects(x => x.Draw(spriteBatch));
        }

        public void HandleCommand(Command command) {
            LoopGameObjects(x => x.HandleCommand(command));
        }

        private void LoopGameObjects(Action<GameObject> action) {
            foreach (var go in gameObjects)
                action(go.Value);
        }
    }
}
