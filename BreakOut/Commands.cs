using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public enum Command {
        None,
        Exit,
        MoveLeft,
        MoveRight,
        WorldCollision,
    }
}
