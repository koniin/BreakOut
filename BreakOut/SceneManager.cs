using BreakOut.GameEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class SceneManager {
        private readonly SortedList<int, GameEntity> entities;
        private readonly Rectangle worldBounds;
        private EntityFactory entityFactory;
        private EventQueue eventQueue;

        public SceneManager(Rectangle worldBounds, EntityFactory entityFactory, EventQueue eventQueue) {
            this.worldBounds = worldBounds;
            this.entityFactory = entityFactory;
            this.eventQueue = eventQueue;
            entities = new SortedList<int, GameEntity>();
            entityFactory.AttachEvents(eventQueue);
        }

        public void Add(int index, GameEntity entity) {
            entity.Accept(eventQueue);
            entities.Add(index, entity);
        }

        public void Update(float deltaTime) {
            foreach (var entity in entities)
                entity.Value.Update(deltaTime);

            HandleCollisions();
            HandleEvents();
            RemoveDestroyedObjects();
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (var entity in entities)
                entity.Value.Draw(spriteBatch);
        }
        
        public void HandleCommand(Command command) {
            foreach (var entity in entities)
                entity.Value.SendMessage(new Message() { Command = command });
        }
        
        public void HandleCollisions() {
            HandleWorldCollisions();
            HandleEntityCollisions();
        }

        public void HandleEvents() {
            eventQueue.HandleEvents();
        }

        public bool IsLevelEnd() {
            return !entities.Values.Any(g => g is Brick);
        }

        private void HandleWorldCollisions() {
            foreach (var obj in entities) {
                if (obj.Value.IsCollidable 
                    && (obj.Value.BoundingBox.Right > worldBounds.Right || obj.Value.BoundingBox.Left < worldBounds.Left
                    || obj.Value.BoundingBox.Bottom > worldBounds.Bottom || obj.Value.BoundingBox.Top < worldBounds.Top)) {
                        obj.Value.SendMessage(new Message { Command = Command.WorldCollision, BoundingBox = worldBounds });
                }
            }
        }
        
        private void HandleEntityCollisions() {
            foreach (var first in entities) {
                foreach (var second in entities) {
                    if (first.Value.IsCollidable && second.Value.IsCollidable && first.Key != second.Key && first.Value.BoundingBox.Intersects(second.Value.BoundingBox)) {
                        HandleCollision(first.Value, second.Value);
                    }
                }
            }
        }

        private void HandleCollision(GameEntity first, GameEntity second) {
            if (first.IsPlayer || second.IsPlayer) {
                first.SendMessage(new Message { Command = Command.EntityPlayerCollision, BoundingBox = second.BoundingBox });
                second.SendMessage(new Message { Command = Command.EntityPlayerCollision, BoundingBox = first.BoundingBox });
            }
            else {
                first.SendMessage(new Message { Command = Command.EntityCollision, BoundingBox = second.BoundingBox });
                second.SendMessage(new Message { Command = Command.EntityCollision, BoundingBox = first.BoundingBox });
            }
        }

        public void RemoveDestroyedObjects() {
            var destroyedEntities = entities.Where(g => g.Value.IsDestroyed).ToList();
            if (!destroyedEntities.Any()) return;
            foreach (var entity in destroyedEntities)
                entities.Remove(entity.Key);
        }
    }
}
