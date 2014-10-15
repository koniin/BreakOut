using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut
{
    public class MessageEventArgs : EventArgs {
        public Message Message { get; set; }
    }
}
