using BreakOut.GameEntities;
using BreakOut.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class EventQueue {
        private Dictionary<Type, List<IEventListener>> eventListeners = new Dictionary<Type, List<IEventListener>>();
        private Queue<Action> events = new Queue<Action>();

        public void Visit(ScoreBar scoreBar) {
        }

        public void Visit(Brick brick) {
            brick.OnDestroyed += OnBrickDestroyed;
        }
        
        public void Visit(Ball ball) {
            ball.OutOfBounds += BallOutOfBounds;
        }

        public void Visit(PlayerPaddle paddle) {
        }
        
        private void BallOutOfBounds(object sender, OutOfBoundsEvent e) {
            var type = e.GetType();
            if (eventListeners.ContainsKey(type)) {
                foreach (IEventListener el in eventListeners[type])
                    events.Enqueue(el.Handle(e));
            }
        }

        private void OnBrickDestroyed(object sender, DestroyedEvent e) {
            var type = e.GetType();
            if (eventListeners.ContainsKey(type)) {
                foreach (IEventListener el in eventListeners[type])
                    events.Enqueue(el.Handle(e));
            }
        }

        public void Attach(Type type, IEventListener go) {
            if (eventListeners.ContainsKey(type))
                eventListeners[type].Add(go);
            else
                eventListeners.Add(type, new List<IEventListener> { go });
        }

        public void HandleEvents() {
            while (events.Count != 0) {
                Action action = events.Dequeue();
                action();
            }
        }
    }
}
