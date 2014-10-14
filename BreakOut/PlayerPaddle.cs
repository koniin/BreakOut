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

        public void SetPosition(int x) {
            this.position.X = x;
        }

        public override void SendMessage(Message message) {
            if (message.Command == Command.MoveLeft) {
                this.speed += 0.5f;
                if (speed > maxSpeed)
                    speed = maxSpeed;
                this.direction = new Vector2(-1, 0);
            }
            else if (message.Command == Command.MoveRight) {
                this.speed += 0.5f;
                if (speed > maxSpeed)
                    speed = maxSpeed;
                this.direction = new Vector2(1, 0);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
