using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class InputManager {
        public Command GetCommand(Microsoft.Xna.Framework.Input.KeyboardState keyboardState) {
            if(keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
                return Command.MoveRight;
            if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                return Command.MoveLeft;
            return Command.None;
        }
    }
}
