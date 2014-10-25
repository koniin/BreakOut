using BreakOut.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BreakOut.GameEntities {
    public class Brick : GameEntity {
        public override bool IsCollidable { get { return true; } }
        public override bool IsPlayer { get { return false; } }
        public event EventHandler<DestroyedEvent> OnDestroyed;

        private int score;
        public Brick(Texture2D texture, Vector2 position, int score) : base(texture, position) {
            this.score = score;
        }
        
        public override void Update(float deltaTime) {}

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.Green);
        }

        public override void SendMessage(Message message) {
            if (message.Command == Command.EntityCollision) {
                Destroy();
                if (OnDestroyed != null) {
                    OnDestroyed(this, new DestroyedEvent(score, "not used"));
                    OnDestroyed = null;
                }
            }
        }

        public override void Accept(EventQueue queue) {
            queue.Visit(this);
        }
        
        public override string ToString() {
            return string.Format("{0},{1},{2},{3}", texture.Name, position.X, position.Y, score);
        }
    }
}
