using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class Message {
        public Command Command { get; set; }
        public Rectangle BoundingBox { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
    }
}
