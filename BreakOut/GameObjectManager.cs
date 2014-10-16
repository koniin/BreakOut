using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class GameObjectManager {
        private readonly SortedList<int, GameObject> gameObjects;
        private readonly Rectangle worldBounds;
        private Queue<Message> messageQueue;
        private GameObjectFactory gameObjectFactory;
        private EventQueue eventQueue;
        
        public GameObjectManager(Rectangle worldBounds, GameObjectFactory gameObjectFactory, EventQueue eventQueue) {
            this.worldBounds = worldBounds;
            this.gameObjectFactory = gameObjectFactory;
            this.eventQueue = eventQueue;
            gameObjects = new SortedList<int, GameObject>();
            messageQueue = new Queue<Message>();
        }

        public void Add(int index, GameObject gameObject) {
            gameObject.Accept(eventQueue);
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
            RemoveDestroyedObjects();
        }

        public void HandleEvents() {
            eventQueue.HandleEvents();
        }

        public bool IsLevelEnd() {
            return !gameObjects.Values.Any(g => g is Brick);
        }

        private void HandleWorldCollisions() {
            foreach (var obj in gameObjects) {
                if (obj.Value.IsCollidable 
                    && (obj.Value.BoundingBox.Right > worldBounds.Right || obj.Value.BoundingBox.Left < worldBounds.Left
                    || obj.Value.BoundingBox.Bottom > worldBounds.Bottom || obj.Value.BoundingBox.Top < worldBounds.Top)) {
                        obj.Value.SendMessage(new Message { Command = Command.WorldCollision, BoundingBox = worldBounds });
                }
            }
        }
        
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

        private void RemoveDestroyedObjects() {
            var destroyedObjects = gameObjects.Where(g => g.Value.IsDestroyed).ToList();
            if (!destroyedObjects.Any()) return;
            foreach (var gameObject in destroyedObjects)
                gameObjects.Remove(gameObject.Key);
        }
    }
}
