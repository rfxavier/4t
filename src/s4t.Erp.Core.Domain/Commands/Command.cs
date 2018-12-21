using System;

namespace s4t.Erp.Core.Domain.Commands
{
    public abstract class Command 
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool EstaValido();
    }
}