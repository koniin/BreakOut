using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.GameEntities {
    public class PlayerPaddle : GameEntity {
        private Vector2 direction;
        private float speed;
        private float maxSpeed = 0.3f;
        private Vector2 originalPosition;
        public int Height { get { return texture.Height; } }
        public int Score { get; set; }

        public override bool IsCollidable {
            get {
                return true;
            }
        }

        public PlayerPaddle(Texture2D texture, Vector2 position)
            : base(texture, position) {
                originalPosition = position;
        }
             
        public void SetPosition(Vector2 position) {
            this.position = position;
        }

        public override void Update(float deltaTime) {
            position += direction * speed * deltaTime;

            if (speed > 0) {
                speed -= 0.005f * deltaTime;
                if (speed < 0.1f)
                    speed = 0;
            }
        }
        
        public override void SendMessage(Message message) {
            if (message.Command == Command.MoveLeft) {
                this.speed += 0.5f;
                if (speed > maxSpeed)
                    speed = maxSpeed;
                this.direction = new Vector2(-1, 0);
            }
            if (message.Command == Command.MoveRight) {
                this.speed += 0.5f;
                if (speed > maxSpeed)
                    speed = maxSpeed;
                this.direction = new Vector2(1, 0);
            }
            if(message.Command == Command.WorldCollision) {
                Vector2 playerPos = position;
                playerPos.X = Math.Max(playerPos.X, 20);
                playerPos.X = Math.Min(playerPos.X, 780 - 100);
                position = playerPos;
            }
        }
        
        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public override void Accept(EventQueue queue) {
            queue.Attach(typeof(OutOfBoundsEvent), this);
            queue.Visit(this);
        }

        public override Action Handle(OutOfBoundsEvent e) {
            return () => { position = originalPosition; }; 
        }
    }
}
