using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.States {
    public abstract class State {
        public StateManager StateManager { get; set; }
        public EventQueue EventQueue { get; set; }

        public abstract void Init();

        // the bool return value marks if its fall through or not
        public abstract bool Update(float deltaTime);
        public abstract bool Draw(SpriteBatch spriteBatch);
        public abstract bool HandleInput(Keys key);
    }
}
