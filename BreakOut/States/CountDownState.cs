using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.States {
    public class CountDownState : State {
        private SpriteFont spriteFont;
        private int countdown = 3;
        private float deltaTimer = 0;

        public override void Init() { spriteFont = ResourceManager.Load<SpriteFont>("monolight12"); }

        public CountDownState() {
        }

        public override bool Update(float deltaTime) {
            deltaTimer += deltaTime;
            if (deltaTimer > 1000) {
                countdown--;
                deltaTimer = 0;
            }

            if (countdown <= 0) {
                StateManager.PopState();
            }

            return false;
        }

        public override bool Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) {
            spriteBatch.DrawString(spriteFont, countdown.ToString(), new Vector2(380, 400), Color.White);
            return true;
        }

        public override bool HandleInput(Microsoft.Xna.Framework.Input.Keys key) {
            return false;
        }
    }
}
