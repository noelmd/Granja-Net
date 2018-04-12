using System;
using System.Collections.Generic;
using System.Text;

namespace LaGranja
{
    public delegate void handlerHuevo();

    public interface IOviparo
    {
        event handlerHuevo AlertaHuevoEvent;

        void PonerHuevo();
        //void asignarPonedero<T>(Ponedero<T>) where T : IOviparo;
    }
}
