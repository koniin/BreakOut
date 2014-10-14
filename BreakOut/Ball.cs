using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class Ball : GameObject {
        private Vector2 speed = new Vector2(0.35f, 0.35f);
        private Vector2 direction;
        private Random rand = new Random();

        public override bool IsCollidable {
            get {
                return true;
            }
        }

        public Ball(Texture2D texture2D, Vector2 position)
            : base(texture2D, position) {
                SetRandomDirection();
        }

        public override void Update(float deltaTime) {
            Position += direction * speed * deltaTime;
        }
        
        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public override void SendMessage(Message message) {
            if (message.Command == Command.WorldCollision) {
                ReverseDirection();
            }
        }

        private void ReverseDirection() {
            if (BoundingBox.Left < 20 || BoundingBox.Right > 780) {
                direction.X = -direction.X;
            }
            if (BoundingBox.Top < 40) {
                direction.Y = -direction.Y;
            }
            if (BoundingBox.Bottom > 800) {

            }
        }
        
        private void SetRandomDirection() {
            // Get a random angle pointing right from 55 to 125 degrees
            direction = Calc2D.GetRightPointingAngledPoint(rand.Next(55, 125));
            if (rand.Next(2) == 1)
                direction = -direction;
        }
    }
}
