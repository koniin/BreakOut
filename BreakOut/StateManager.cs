using BreakOut.States;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class StateManager : IEventListener {
        private readonly Stack<State> stateStack;
        private readonly Queue<Action> stateQueue;
        private readonly EventQueue eventQueue;

        public StateManager() {
            stateStack = new Stack<State>();
            stateQueue = new Queue<Action>();
            eventQueue = new EventQueue();

            eventQueue.Attach(typeof(OutOfBoundsEvent), this);
            eventQueue.Attach(typeof(LifesZeroEvent), this);
        }

        public void Update(float deltaTime) {
            foreach (var state in stateStack) {
                if (!state.Update(deltaTime))
                    break;
            }
            ApplyChanges();
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (var state in stateStack.Reverse()) {
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

        public void Clear() {
            stateQueue.Enqueue(() => stateStack.Clear());
        }

        private void InitState(State state) {
            state.StateManager = this;
            state.EventQueue = eventQueue;
            state.Init();
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

        public Action Handle(Events.DestroyedEvent e) {
            return () => { };
        }

        public Action Handle(OutOfBoundsEvent e) {
            return () => {
                PushState(new CountDownState()); 
            };
        }

        public Action Handle(LifesZeroEvent e) { 
            return () => {
                PopState();
                PushState(new GameEndedState("Game Over"));
            }; 
        }
    }
}
