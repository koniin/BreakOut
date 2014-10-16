using BreakOut.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class GameObjectFactory : IEventListener {
        public void AttachEvents(EventQueue eventQueue) {
            eventQueue.Attach(typeof(DestroyedEvent), this);
        }

        public Action Handle(DestroyedEvent message) {
            return (() => SpawnPowerUp(message.Position));
        }

        private void SpawnPowerUp(string position) {
            Console.WriteLine("CreateRandomPowerup(" + position + ");");
        }

        public Action Handle(OutOfBoundsEvent e) { return () => { }; }
    }
}
