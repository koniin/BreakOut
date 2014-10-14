using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class Ball : GameObject {
        private Vector2 speed = new Vector2(0.45f, 0.45f);
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
            position += direction * speed * deltaTime;
        }
        
        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public override void SendMessage(Message message) {
            if (message.Command == Command.WorldCollision) {
                ReverseDirection(message.BoundingBox);
            }
            if (message.Command == Command.EntityCollision) {
                Bounce(message.BoundingBox);
            }
        }

        private void ReverseDirection(Rectangle boundingBox) {
            if (BoundingBox.Left < boundingBox.Left || BoundingBox.Right > boundingBox.Right) {
                direction.X = -direction.X;
            }
            if (BoundingBox.Top < boundingBox.Top) {
                direction.Y = -direction.Y;
            }
            if (BoundingBox.Bottom > boundingBox.Bottom) {
                // Loose life and reset ball, Timer?
            }
        }

        private void Bounce(Rectangle boundingBox) {
            speed *= 1.04f;

            // Calculate a new direction depending on where on the paddle the ball bounces
            /*
            float differenceToTargetCenter = boundingBox.Center.Y - BoundingBox.Center.Y;
            direction = Calc2D.GetRightPointingAngledPoint((int)(90 + (differenceToTargetCenter * 1.3f)));
            */
            
            direction.Y = -direction.Y;
            position.Y = boundingBox.Top - texture.Height;
        }

        private void SetRandomDirection() {
            // Get a random angle pointing right from 55 to 125 degrees
            direction = Calc2D.GetRightPointingAngledPoint(rand.Next(55, 125));
            if (rand.Next(2) == 1)
                direction = -direction;
        }
    }
}
