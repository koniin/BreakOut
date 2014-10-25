using BreakOut.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.GameEntities {
    public class ScoreBar : GameEntity {
        private SpriteFont font;
        private int score;
        private int width;
        private int lives;
        private int currentLevel;
        public event EventHandler<LifesZeroEvent> LifesZero;

        public override bool IsCollidable {
            get {
                return false;
            }
        }

        public override bool IsPlayer { get { return false; } }

        public ScoreBar(SpriteFont font, Vector2 position, int width, int lives, int currentLevel) {
            this.font = font;
            this.position = position;
            this.width = width;
            //this.lives = lives;
            this.lives = 1;
            this.currentLevel = currentLevel;
            score = 0;
        }
        
        public override void Update(float deltaTime) {
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) {
            spriteBatch.DrawString(font, "Level: " + currentLevel, new Vector2(10, position.Y), Color.White);
            spriteBatch.DrawString(font, "Lives: " + lives, new Vector2((width / 2) - 50, position.Y), Color.White);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(width - 100, position.Y), Color.White);
        }

        public override void Accept(EventQueue queue) {
            queue.Attach(typeof(DestroyedEvent), this);
            queue.Attach(typeof(OutOfBoundsEvent), this);
            queue.Visit(this);
        }

        public override Action Handle(DestroyedEvent message) {
            return (() => score += message.Score);
        }

        public override Action Handle(OutOfBoundsEvent message) {
            return () => {
                lives--;
                if (lives <= 0)
                    LifesZero(this, new LifesZeroEvent());
            };
        }
    }
}
