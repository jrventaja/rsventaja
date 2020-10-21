using System;
using System.Collections.Generic;
using System.Text;

namespace RSVentaja.Domain
{
    public abstract class Verifiable
    {
        public bool Valid { get; private set; }

        protected void Assert(Func<bool> expression) => Valid = expression?.Invoke() ?? false;
    }
}
