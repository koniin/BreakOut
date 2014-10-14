using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class PlayerPaddle : GameObject {
        private Vector2 direction;
        private float speed;
        private float maxSpeed = 0.3f;
        public int Height { get { return texture.Height; } }
        public int Score { get; set; }

        public override bool IsCollidable {
            get {
                return true;
            }
        }

        public PlayerPaddle(Texture2D texture, Vector2 position)
            : base(texture, position) {
        }
             
        public void SetPosition(Vector2 position) {
            this.Position = position;
        }

        public override void Update(float deltaTime) {
            Position += direction * speed * deltaTime;

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
                Vector2 playerPos = Position;
                playerPos.X = Math.Max(playerPos.X, 20);
                playerPos.X = Math.Min(playerPos.X, 780 - 100);
                Position = playerPos;
            }
        }
        
        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
