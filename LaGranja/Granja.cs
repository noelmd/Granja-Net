using System;
using System.Collections.Generic;
using System.Text;

namespace LaGranja
{
    public class Granja
    {
        public static int CAPACIDAD_PONEDERO_GALLINAS = 30;
        public static int CAPACIDAD_PONEDERO_PALOMOS = 20;

        public const int MAX_PALOMOS = 10;
        public const int MAX_GALLINAS = 8;

        private static int PONEDEROS_GALLINAS_DEFAULT = 1;
        private static int PONEDEROS_PALOMOS_DEFAULT = 1;

        private List <Ponedero<Gallina>> ponederosGallinas = new List<Ponedero<Gallina>> (PONEDEROS_GALLINAS_DEFAULT);
        private List<Ponedero<Palomo>> ponederosPalomos = new List<Ponedero<Palomo>>(PONEDEROS_PALOMOS_DEFAULT);

        private List<Animal> animales = new List<Animal>();

        public Granja()
        {
        }

        public Granja(List<Ponedero<Gallina>> ponederosGallinas, List<Ponedero<Palomo>> ponederosPalomos)
        {
            this.ponederosGallinas = ponederosGallinas;
            this.ponederosPalomos = ponederosPalomos;
        }

        public Granja(Ponedero<Gallina> ponederoGallinas, Ponedero<Palomo> ponederoPalomos)
        {
            this.ponederosGallinas = new List<Ponedero<Gallina>>(1);
            this.ponederosGallinas.Add(ponederoGallinas);

            this.ponederosPalomos = new List<Ponedero <Palomo>>(1);
            this.ponederosPalomos.Add(ponederoPalomos);
        }

        public Granja(Ponedero<Palomo> ponederoPalomos, Ponedero<Gallina> ponederoGallinas)
        {
            this.ponederosGallinas = new List<Ponedero<Gallina>>(1);
            this.ponederosGallinas.Add(ponederoGallinas);

            this.ponederosPalomos = new List<Ponedero<Palomo>>(1);
            this.ponederosPalomos.Add(ponederoPalomos);
        }

        /*Propiedades */
        public List<Ponedero<Gallina>> PonederosGallinas { get => ponederosGallinas; set => ponederosGallinas = value; }
        public List<Ponedero<Palomo>> PonederosPalomos { get => ponederosPalomos; set => ponederosPalomos = value; }
        public List<Animal> Animales { get => animales; set => animales = value; }

        public void DistribuirAnimales(List<Animal> animales)
        {
            foreach (Animal animal in animales)
            {
               bool agregar = true;

               if (animal is Palomo)
               {
                    ((Palomo)animal).Ponedero = this.PonederosPalomos[0];
                    this.PonederosPalomos[0].RegistrarAnimal((Palomo)animal);

                    Console.WriteLine("Asignando Palomo a Ponedero");
               }
               else if (animal is Gallina)
               {
                    ((Gallina)animal).Ponedero = this.PonederosGallinas[0];
                    this.PonederosGallinas[0].RegistrarAnimal((Gallina)animal);

                    Console.WriteLine("Asignando Gallina a Ponedero");
               }
               else if (animal is Conejo)
               {
                    Console.WriteLine("Nuevo Conejo en la Granja");
               }
               else
               {
                   agregar = false;
                   Console.WriteLine("Animal no permitido en la Granja.");
               }

               if (agregar)
                   this.Animales.Add(animal);
            }
        }

        public void EventoDia()
        {
            this.ponerHuevosPalomos();
            this.ponerHuevosGallinas();
        }

        private void ponerHuevosPalomos()
        {
            foreach(Palomo p in this.PonederosPalomos[0].GetAnimalesAsignados())
            {
                p.PonerHuevo();
            }
        }

        private void ponerHuevosGallinas()
        {
            foreach (Gallina g in this.PonederosGallinas[0].GetAnimalesAsignados())
            {
                g.PonerHuevo();
            }
        }

    }
}
