using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.States {
    public class GameEndedState : State {
        SpriteFont spriteFont;
        private string reason;

        public GameEndedState(string reason) {
            this.reason = reason;
        }
        
        public override void Init() { spriteFont = ResourceManager.Load<SpriteFont>("monolight12"); }

        public override bool Update(float deltaTime) {
            return false;
        }

        public override bool Draw(SpriteBatch spriteBatch) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(reason);
            sb.AppendLine();
            sb.AppendLine("Made by: Henrik Aronsson");
            sb.AppendLine();
            sb.AppendLine("Press Esc or Q to quit");
            spriteBatch.DrawString(spriteFont, sb, new Vector2(300, 320), Color.Yellow);
            return true;
        }

        public override bool HandleInput(Keys key) {
            if (key == Keys.Escape || key == Keys.Q) {
                StateManager.Clear();
            }
            return false;
        }
    }
}
