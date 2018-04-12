using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LaGranja
{
    public enum Color {ROJA,BLANCA,PARDA,NEGRA};

    public class Gallina : Animal, IOviparo
    {
        //Atributos
        private Color color;     
        private Ponedero <Gallina> ponedero;


        //Constructores
        public Gallina(Ponedero <Gallina> ponedero)
        {
            this.ponedero = ponedero;
        }
        public Gallina(Color color) => this.color = color;

        public Gallina(Color color, Ponedero <Gallina> ponedero) : this(ponedero)
        {
            this.color = color;
        }    

        //Propiedades;
        public Color Color
        {
            get => color;
        }
 
        public Ponedero <Gallina> Ponedero
        {
            get => ponedero;
            set => ponedero = value;
        }

        //Eventos
        public event handlerHuevo AlertaHuevoEvent;

        /*public void asignarPonedero<T>(Ponedero<T> ponedero) where T : IOviparo
        {
            this.ponedero = ponedero;
        }*/

        //Métodos
        public void PonerHuevo()
        {
            Huevo h = null;
            
            if (this.alimentado)
            {
                 h = new Huevo();
                this.ponedero.AddHuevo(h);
                //Se lanza evento Alerta huevo
                AlertaHuevoEvent();
            }
        }
    }
}
