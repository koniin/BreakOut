using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.GameEntities {
    public class Ball : GameEntity {
        private Vector2 speed = new Vector2(0.45f, 0.45f);
        private Vector2 direction;
        private Random rand = new Random();
        public event EventHandler<OutOfBoundsEvent> OutOfBounds;

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
                if (BoundingBox.Bottom > message.BoundingBox.Bottom) {
                    SendEventAndResetBall();
                }
            }
            if (message.Command == Command.EntityCollision) {
                Bounce(message.BoundingBox);
            }
        }

        private void SendEventAndResetBall() {
            if (OutOfBounds != null)
                OutOfBounds(this, new OutOfBoundsEvent());

            // Reset Ball
        }

        private void ReverseDirection(Rectangle boundingBox) {
            if (BoundingBox.Left < boundingBox.Left || BoundingBox.Right > boundingBox.Right) {
                direction.X = -direction.X;
            }
            if (BoundingBox.Top < boundingBox.Top) {
                direction.Y = -direction.Y;
            }
        }

        private void Bounce(Rectangle boundingBox) {
            //speed *= 1.04f;

            // Update for all bounces - left side, right side, upper and lower sides of bricks + paddle


            direction.Y = -direction.Y;
            position.Y = boundingBox.Top - texture.Height;
        }

        private void SetRandomDirection() {
            // Get a random angle pointing right from 55 to 125 degrees
            direction = Calc2D.GetRightPointingAngledPoint(rand.Next(55, 125));
            if (rand.Next(2) == 1)
                direction = -direction;
        }

        public override void Accept(EventQueue queue) {
            queue.Visit(this);
        }
    }
}
