using System;

namespace BreakOut {
    public class Brick : GameObject {
        public override bool IsCollidable { get { return true; } }
        public Brick(Texture2D texture, Vector2 position) : base(texture, position) { }
        public override void Update(float deltaTime) {}

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.Green);
        }

        public override void SendMessage(Message message) {
            if (message.Command == Command.EntityCollision) {
                // Send increase score
                OnDestroyed(new MessageEventArgs { Message = new Message { Command = Command.IncreaseScore} });
                Destroy();
            }
        }
        
        public override string ToString() {
            return string.Format("{0},{1},{2}", texture.Name, position.X, position.Y);
        }
    }
}
