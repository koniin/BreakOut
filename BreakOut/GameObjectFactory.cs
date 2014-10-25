using BreakOut.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class EntityFactory : IEventListener {
        public void AttachEvents(EventQueue eventQueue) {
            eventQueue.Attach(typeof(DestroyedEvent), this);
        }

        public Action Handle(DestroyedEvent message) {
            return (() => SpawnPowerUp(message.Position));
        }

        public Action Handle(OutOfBoundsEvent e) { return () => { }; }
        public Action Handle(LifesZeroEvent e) { return () => { }; }

        private void SpawnPowerUp(string position) {
            Console.WriteLine("CreateRandomPowerup(" + position + ");");
        }
    }
}
