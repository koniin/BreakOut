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
        private readonly Vector2 startPosition;

        public override bool IsCollidable {
            get {
                return true;
            }
        }

        public Ball(Texture2D texture2D, Vector2 position)
            : base(texture2D, position) {
                startPosition = position;
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

            position = startPosition;
        }

        private void ReverseDirection(Rectangle boundingBox) {
            if (BoundingBox.Left < boundingBox.Left || BoundingBox.Right > boundingBox.Right) {
                direction.X = -direction.X;
            }
            if (BoundingBox.Top < boundingBox.Top) {
                direction.Y = -direction.Y;
            }
        }

        private void Bounce(Rectangle collisionTarget) {
            //speed *= 1.04f;

            float w = 0.5f * (BoundingBox.Width + collisionTarget.Width);
            float h = 0.5f * (BoundingBox.Height + collisionTarget.Height);
            float dx = BoundingBox.Center.X - collisionTarget.Center.X;
            float dy = BoundingBox.Center.Y - collisionTarget.Center.Y;
            /*
            if (Math.Abs(dx) <= w && Math.Abs(dy) <= h)
            {
            */
                float wy = w * dy;
                float hx = h * dx;

                if (wy > hx) {
                    if (wy > -hx) {
                        direction.Y = -direction.Y;
                        position.Y = collisionTarget.Bottom;
                        //System.Diagnostics.Debug.WriteLine("Bottom");
                    } else {
                        direction.X = -direction.X;
                        position.X = collisionTarget.Left - texture.Width;
                        //System.Diagnostics.Debug.WriteLine("Left");
                    }
                }
                else {
                    if (wy > -hx) {
                        direction.X = -direction.X;
                        position.X = collisionTarget.Right;
                        //System.Diagnostics.Debug.WriteLine("Right");
                    } else {  
                        direction.Y = -direction.Y;
                        position.Y = collisionTarget.Top - texture.Height;
                        //System.Diagnostics.Debug.WriteLine("Top");
                    }
                }
            //}
        }


        private void SetRandomDirection() {
            float randomY = (float)rand.Next(3,7) / 10;
            direction = new Vector2(randomY, -1);
        }

        public override void Accept(EventQueue queue) {
            queue.Visit(this);
        }
    }
}
