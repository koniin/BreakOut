using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut.Events {
    public class DestroyedEvent : EventArgs {
        public int Score { get; private set; }
        public string Position { get; private set; }
        public DestroyedEvent(int score, string position) {
            Score = score;
            Position = position;
        }
    }
}
