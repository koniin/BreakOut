using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class GameObjectManager {
        private readonly SortedList<int, GameObject> gameObjects;
        private Rectangle worldBounds;
        private Message worldCollisionMessage;

        public GameObjectManager(Rectangle worldBounds) {
            this.worldBounds = worldBounds;
            gameObjects = new SortedList<int, GameObject>();
            worldCollisionMessage = new Message { Command = Command.WorldCollision, BoundingBox = worldBounds };
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
            LoopGameObjects(x => x.SendMessage(new Message() { Command = command }));
        }

        private void LoopGameObjects(Action<GameObject> action) {
            foreach (var go in gameObjects)
                action(go.Value);
        }

        public void HandleCollisions() {
            HandleWorldCollisions();
            HandleEntityCollisions();
        }

        private void HandleWorldCollisions() {
            foreach (var obj in gameObjects) {
                if (obj.Value.IsCollidable 
                    && (obj.Value.BoundingBox.Right > worldBounds.Right || obj.Value.BoundingBox.Left < worldBounds.Left
                    || obj.Value.BoundingBox.Bottom > worldBounds.Bottom || obj.Value.BoundingBox.Top < worldBounds.Top)) {
                        obj.Value.SendMessage(worldCollisionMessage);
                }
            }
        }
        
        // Ball and paddle
        // Ball and levelobject
        private void HandleEntityCollisions() {
            foreach (var first in gameObjects) {
                foreach (var second in gameObjects) {
                    if (first.Value.IsCollidable && second.Value.IsCollidable && first.Key != second.Key && first.Value.BoundingBox.Intersects(second.Value.BoundingBox)) {
                        first.Value.SendMessage(new Message { Command = Command.EntityCollision, BoundingBox = second.Value.BoundingBox });
                        second.Value.SendMessage(new Message { Command = Command.EntityCollision, BoundingBox = first.Value.BoundingBox });
                    }
                }
            }
        }
    }
}
