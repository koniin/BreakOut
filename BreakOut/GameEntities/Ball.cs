using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.GameEntities {
    public class Ball : GameEntity {
        private Vector2 speed = new Vector2(0.40f, 0.40f);
        private Vector2 direction;
        private Random rand = new Random();
        public event EventHandler<OutOfBoundsEvent> OutOfBounds;
        private readonly Vector2 startPosition;

        public override bool IsCollidable {
            get {
                return true;
            }
        }

        public override bool IsPlayer { get { return false; } }

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
            if (message.Command == Command.EntityPlayerCollision) {
                BounceOnPlayer(message.BoundingBox);
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
                }
                else {
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
                }
                else {
                    direction.Y = -direction.Y;
                    position.Y = collisionTarget.Top - texture.Height;
                    //System.Diagnostics.Debug.WriteLine("Top");
                }
            }
            //}
        }

        private void BounceOnPlayer(Rectangle collisionTarget) {
            float w = 0.5f * (BoundingBox.Width + collisionTarget.Width);
            float h = 0.5f * (BoundingBox.Height + collisionTarget.Height);
            float dx = BoundingBox.Center.X - collisionTarget.Center.X;
            float dy = BoundingBox.Center.Y - collisionTarget.Center.Y;
            float wy = w * dy;
            float hx = h * dx;

            if (wy > hx) {
                if (wy < -hx) {
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
                }
                else {
                    direction.Y = -direction.Y;
                    position.Y = collisionTarget.Top - texture.Height;

                    float zoneLength = collisionTarget.Width / 4;

                    if (BoundingBox.Center.X < collisionTarget.Left + zoneLength) {
                        direction = Calc2D.GetAngledPoint(-125);
                    }
                    else if (BoundingBox.Center.X < collisionTarget.Left + zoneLength * 2) {
                        direction = Calc2D.GetAngledPoint(-150);
                    }
                    else if (BoundingBox.Center.X < collisionTarget.Left + zoneLength * 3) {
                        direction = Calc2D.GetAngledPoint(150);
                    }
                    else {
                        direction = Calc2D.GetAngledPoint(125);
                    }
                }
            }
        }

        private void SetRandomDirection() {
            direction = Calc2D.GetAngledPoint(-125);
        }

        public override void Accept(EventQueue queue) {
            queue.Visit(this);
        }
    }
}
