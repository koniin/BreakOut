using BreakOut.States;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class StateManager {
        private readonly Stack<State> stateStack;
        private readonly Queue<Action> stateQueue;

        public StateManager() {
            stateStack = new Stack<State>();
            stateQueue = new Queue<Action>();
        }

        public void Update(float deltaTime) {
            foreach (var state in stateStack) {
                if (!state.Update(deltaTime))
                    break;
            }
            ApplyChanges();
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (var state in stateStack) {
                if (!state.Draw(spriteBatch))
                    break;
            }
        }

        public void HandleInput(Keys key) {
            if (key == Keys.None)
                return;

            foreach (var state in stateStack) {
                if (!state.HandleInput(key))
                    break;
            }
            ApplyChanges();
        }

        public void PopState() {
            stateQueue.Enqueue(() => stateStack.Pop());
        }

        public void PushState(State state) {
            InitState(state);
            stateQueue.Enqueue(() => stateStack.Push(state));
        }

        private void InitState(State state) {
            state.StateManager = this;
        }

        private void ApplyChanges() {
            while (stateQueue.Count != 0) {
                Action action = stateQueue.Dequeue();
                action();
            }
        }

        public bool IsEmpty() {
            return stateStack.Count == 0;
        }
    }
}
