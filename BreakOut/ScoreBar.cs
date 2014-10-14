﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class ScoreBar : GameObject {
        private SpriteFont font;
        private int score;
        private int width;
        private int currentLevel;

        public ScoreBar(SpriteFont font, Vector2 position, int width) {
            this.font = font;
            this.position = position;
            this.width = width;
            score = 0;
            currentLevel = 0;
        }
        
        public override void Update(float deltaTime) {
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) {
            spriteBatch.DrawString(font, "Level: " + currentLevel, new Vector2(10, position.Y), Color.White);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(width - 100, position.Y), Color.White);
        }

        public override void HandleCommand(Command command) {
            
        }
    }
}