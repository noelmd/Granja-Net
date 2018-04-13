using System;
using System.Collections.Generic;
using System.Text;

namespace LaGranja
{
    public class Granjero
    {
        private String nombre;
        private Granja granja;

        private int hambre = 20;
        private int pereza = 30;

        //Constructores
        public Granjero (string nombre, Granja granja)
        {
            this.nombre = nombre;
            this.granja = granja;
        }


        //Propiedades 

        public string Nombre { get => nombre; set => nombre = value; }
        public Granja Granja { get => granja; set => granja = value; }
        public int Pereza { get => pereza; set => pereza = value; }
        public int Hambre { get => hambre; set => hambre = value; }

        public void ComprarPonederos()
        {
            Ponedero <Gallina> ponederoGallinas = PonederoBuilder.GetPonedero<Gallina>(Granja.CAPACIDAD_PONEDERO_GALLINAS, PONEDERO_TIPO.GALLINAS);
            Console.WriteLine("{0} adquiere un ponedero de gallinas con capacidad para {1} huevos!", this.Nombre, Granja.CAPACIDAD_PONEDERO_GALLINAS);

            Ponedero <Palomo> ponederoPalomos = PonederoBuilder.GetPonedero<Palomo>(Granja.CAPACIDAD_PONEDERO_PALOMOS, PONEDERO_TIPO.PALOMOS);
            Console.WriteLine("{0} adquiere un ponedero de palomos con capacidad para {1} huevos!", this.Nombre, Granja.CAPACIDAD_PONEDERO_PALOMOS);

            this.granja.PonederosGallinas.Add(ponederoGallinas);
            this.granja.PonederosPalomos.Add(ponederoPalomos);
        }

        //Eventos



        public void ComprarAnimales()
        {
            // Palomos
            int numeroPalomos = new Random().Next(2, Granja.MAX_PALOMOS);
            //Se asume solo un ponerdero de palomos en el array de posibles ponderos
            this.granja.PonederosPalomos[0].RegistrarAnimales(new List<Palomo>(numeroPalomos));

            Console.WriteLine("{0} acaba de comprar {1} palomos!", this.nombre, numeroPalomos);

            for (int i = 0; i < numeroPalomos; i++)
            {
                Palomo p = new Palomo(this.granja.PonederosPalomos[0]);
                p.AlertaHuevoEvent += NuevoHuevoHandler;

                this.granja.PonederosPalomos[0].RegistrarAnimal(p);
            }

            //Gallinas

            int numeroGallinas = new Random().Next(2, Granja.MAX_GALLINAS);
            //Se asume solo un ponerdero de palomos en el array de posibles ponderos
            this.granja.PonederosGallinas[0].RegistrarAnimales(new List<Gallina>(numeroGallinas));

            Console.WriteLine("{0} acaba de comprar {1} gallinas!", this.nombre, numeroGallinas);

            for (int i = 0; i < numeroGallinas; i++)
            {
                Gallina g = new Gallina(this.granja.PonederosGallinas[0]);
                g.AlertaHuevoEvent += NuevoHuevoHandler;

                this.granja.PonederosGallinas[0].RegistrarAnimal(g);
            }
        }

        public void AlimentarAnimales()
        {
            foreach (Animal animal in this.Granja.Animales)
            {
                animal.Comer();
            }
        }

        public void RecogerHuevos(int cantidad, PONEDERO_TIPO tipo)
        {
            try
            {
                if (tipo == PONEDERO_TIPO.GALLINAS)
                    this.granja.PonederosGallinas[0].RecogerHuevos(cantidad);
                else
                    this.granja.PonederosPalomos[0].RecogerHuevos(cantidad);
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("No hay {0} huevos en el Ponedeor {1}", cantidad, tipo);
            }
        }

        public void RecogerHuevos()
        {
            Console.WriteLine("{0} ha recogido {1} huevos de gallina", this.nombre, this.granja.PonederosGallinas[0].RecogerHuevos().Count);
            Console.WriteLine("{0} ha recogido {1} huevos de palomo", this.nombre, this.granja.PonederosPalomos[0].RecogerHuevos().Count);
        }

        //public void NuevoHuevoHandler(Huevo h)
        public void NuevoHuevoHandler()
        {
            Console.WriteLine("Se ha puesto un nuevo huevo!!!");

            if (this.Hambre > 50)
            {
                int parOimpar = new Random().Next(1, 2);

                if (parOimpar == 1)
                {
                    RecogerHuevos(new Random().Next(1, 2), PONEDERO_TIPO.GALLINAS);
                }
                else
                {
                    RecogerHuevos(new Random().Next(1, 5), PONEDERO_TIPO.PALOMOS);
                }
            }
            else
            {
                this.Hambre += new Random().Next(1, 20);
            }
        }
    }
}
