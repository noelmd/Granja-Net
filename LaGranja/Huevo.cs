using System;
using System.Collections.Generic;
using System.Text;

namespace LaGranja
{
    public enum Cascara { BLANCA, MORENA};

    public class Huevo
    {
        public static int YEMA_UNICA = 1;
        public static int YEMA_DOBLE = 2;
        public static int YEMA_TRIPLE = 3;

        private int yemas = 1;

        private Cascara cascara = Cascara.BLANCA;

        //Constructores
        public Huevo() => this.yemas = Huevo.randomYemas();

        public Huevo(int yemas) => this.yemas = yemas;

        public Huevo(Cascara cascara) : this() => this.cascara = cascara;

        public Huevo(int yemas, Cascara cascara) : this() => cascara = Cascara.MORENA;

        //Propiedades
        public int Yemas {
            get => yemas;
            set => yemas = value;
        }

        public Cascara Cascara{
            get => cascara;
            set => cascara = value;
        }

        private static int randomYemas()
        {
            int randomNumero = new Random().Next(101);

            if (randomNumero > 99){
                return Huevo.YEMA_TRIPLE;
            }
            else if (randomNumero > 90){
                return Huevo.YEMA_DOBLE;
            }
            else{
                return Huevo.YEMA_UNICA;
            }
        }
    }
}
