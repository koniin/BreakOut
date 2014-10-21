using Microsoft.Xna.Framework.Input;
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
        
        public Command GetCommand(Keys key) {
            if(key.HasFlag(Keys.A))
                return Command.MoveLeft;
            if (key.HasFlag(Keys.D))
                return Command.MoveRight;
            return Command.None;
        }
    }
}
