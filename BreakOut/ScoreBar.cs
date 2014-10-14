using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class ScoreBar : GameObject {
        private SpriteFont font;
        private int score;
        private int width;
        private int lives;
        private int currentLevel;

        public override bool IsCollidable {
            get {
                return false;
            }
        }

        public ScoreBar(SpriteFont font, Vector2 position, int width) {
            this.font = font;
            Position = position;
            this.width = width;
            score = 0;
            currentLevel = 0;
        }
        
        public override void Update(float deltaTime) {
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) {
            spriteBatch.DrawString(font, "Level: " + currentLevel, new Vector2(10, Position.Y), Color.White);
            spriteBatch.DrawString(font, "Lives: " + lives, new Vector2((width / 2) - 50, Position.Y), Color.White);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(width - 100, Position.Y), Color.White);
        }


        public override void SendMessage(Message message) {
            
        }
    }
}
