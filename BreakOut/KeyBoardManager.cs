using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class KeyBoardManager {
        private KeyboardState OldKeyState;

        // Should be handled in different manner
        // like separate handling for realtime and one press
        // in the states / entities
        // Will work for this game though, not many keys ;>
        public Keys GetPressedKey() {
            KeyboardState newKeyState = Keyboard.GetState();
            Keys key = Keys.None;
            
            // Realtime input
            if (newKeyState.IsKeyDown(Keys.A)) {
                return Keys.A;
            }
            if (newKeyState.IsKeyDown(Keys.D)) {
                return Keys.D;
            }
            if (newKeyState.IsKeyDown(Keys.Q)) {
                return Keys.Q;
            }
            if (newKeyState.IsKeyDown(Keys.Escape)) {
                return Keys.Escape;
            }
            // One press key
            if (newKeyState.IsKeyDown(Keys.P) && OldKeyState.IsKeyUp(Keys.P)) {
                key = Keys.P;
            }

            OldKeyState = newKeyState;

            return key;
        }
    }
}
