using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BreakOut.States {
    public class PauseState : State {
        public override void Init() { }

        public override bool Update(float deltaTime) {
            return false;
        }

        public override bool Draw(SpriteBatch spriteBatch) {
            return true;
        }

        public override bool HandleInput(Keys key) {
            if (key == Keys.Escape || key == Keys.P) {
                StateManager.PopState();
            }
            return false;
        }
    }
}
