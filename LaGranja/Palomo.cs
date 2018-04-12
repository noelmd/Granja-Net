using System;
using System.Collections.Generic;
using System.Text;


namespace LaGranja
{
    public class Palomo : Animal, IOviparo
    {
        //Atributos
        private Ponedero <Palomo> ponedero;


        //Constructores
        public Palomo (Ponedero <Palomo> ponedero)
        {
            this.ponedero = ponedero;
        }

        //Propiedades
        public Ponedero <Palomo> Ponedero
        {
            get => ponedero;
            set => ponedero = value;
        }

        //Eventos
        public event handlerHuevo AlertaHuevoEvent;

        //Métodos
        public void PonerHuevo() 
        {
            if (this.alimentado)
            {
                Huevo h = new Huevo(Huevo.YEMA_UNICA, Cascara.MORENA);

                try
                {
                    this.ponedero.AddHuevo(h);
                }
                catch (HuevoRotoException hrex)
                {
                    throw hrex;
                }
                //Se lanza evento Alerta huevo
                AlertaHuevoEvent();
            }
        }
       
    }
}
