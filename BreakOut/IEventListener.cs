using BreakOut.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public interface IEventListener {
        Action Handle(DestroyedEvent e);
        Action Handle(OutOfBoundsEvent e);
    }
}
