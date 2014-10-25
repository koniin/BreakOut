using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.States {
    public class MenuState : State {
        public override void Init() {}

        public override bool Update(float deltaTime) {
            return false;
        }

        public override bool Draw(SpriteBatch spriteBatch) {
            return false;
        }

        public override bool HandleInput(Keys key) {
            return false;
            /*
            if (key == "g") {
                StateManager.PopState();
                StateManager.PushState(new GameState());
            }
            if (key == "q") {
                StateManager.PopState();
            }*/
        }
    }
}
