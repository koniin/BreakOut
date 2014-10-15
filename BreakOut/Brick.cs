using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BreakOut {
    public class Brick : GameObject {
        public override bool IsCollidable { get { return true; } }

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
                // Send increase score
                OnMessage(new MessageEventArgs { Message = new Message { Command = Command.IncreaseScore, Score = score } });
                Destroy();
            }
        }
        
        public override string ToString() {
            return string.Format("{0},{1},{2},{3}", texture.Name, position.X, position.Y, score);
        }
    }
}
