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
                if (BoundingBox.Bottom > message.BoundingBox.Bottom)
                    SendEventAndResetBall();
                else 
                    ReverseDirection(message.BoundingBox);
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
            
            /*
            // Top side
            if (boundingBox.Center.Y > BoundingBox.Top) {
                direction.Y = -direction.Y;
                position.Y = boundingBox.Bottom + texture.Height;
            }
            // Bottom side
            if (boundingBox.Center.Y < BoundingBox.Bottom) {
                direction.Y = -direction.Y;
                position.Y = boundingBox.Top - texture.Height;
            }
            if (boundingBox.Center.X < BoundingBox.Left || boundingBox.Center.X > BoundingBox.Right) {
                direction.X = -direction.X;
            }
            */
            
            float w = 0.5f * (BoundingBox.Width + boundingBox.Width);
            float h = 0.5f * (BoundingBox.Height + boundingBox.Height);
            float dx = BoundingBox.Center.X - boundingBox.Center.X;
            float dy = BoundingBox.Center.Y - boundingBox.Center.Y;

            if (Math.Abs(dx) <= w && Math.Abs(dy) <= h)
            {
                /* collision! */
                float wy = w * dy;
                float hx = h * dx;

                if (wy > hx) {
                    /* collision at the top */
                    if (wy > -hx) {
                        direction.Y = -direction.Y;
                        System.Diagnostics.Debug.WriteLine("Top Collision");
                    }
                    /* on the left */
                    else {
                        direction.X = -direction.X;
                        System.Diagnostics.Debug.WriteLine("Left");
                    }
                }
                else {
                    if (wy > -hx) {
                        /* on the right */
                        direction.X = -direction.X;
                        System.Diagnostics.Debug.WriteLine("Right");
                    }
                    else {
                        /* at the bottom */
                        direction.Y = -direction.Y;
                        System.Diagnostics.Debug.WriteLine("Bottom");
                    }
                }
            }
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
