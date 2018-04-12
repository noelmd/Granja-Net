using System;
using System.Collections.Generic;
using System.Text;

namespace LaGranja
{
    public abstract class Animal
    {
        protected bool alimentado = true;

        public virtual void Comer()
        {
            Console.WriteLine("Soy un {0} y estoy comiendo...",this.GetType().Name);
            this.alimentado = true;
        }

        public virtual void Dormir()
        {
            Console.WriteLine("Soy un {0} y estoy durmiendo...", this.GetType().Name);
            this.alimentado = false;
        }
    }
}
