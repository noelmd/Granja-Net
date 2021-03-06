﻿using System;
using System.Collections.Generic;

namespace LaGranja
{
    class Program
    {
        private const string NOMBRE_GRANJERO = "Tom El Granjero";

        private const int MAX_PALOMOS = 10;
        private const int MAX_GALLINAS = 8;
        private const int CAPACIDAD_PONEDERO_PALOMOS = 20;
        private const int CAPACIDAD_PONEDERO_GALLINAS = 50;
        private double ChickenEggValue;
        private double PigeonEggValue;

        private const int DIAS = 3;

        private const int PEREZA_MORTAL = 90;
        private const int HAMBRE_MORTAL = 50;

        private enum ACCIONES { COMER, DORMIR };

        private Granja granja;
        private Granjero granjero;

        static void Main(string[] args)
        {
            Console.WriteLine("Executing Granja Soution -> Main Thread");

            Program p = new Program();

            p.Init();
             
            p.Activity();
           
            Console.ReadKey(true);

            Console.WriteLine("Finishing Main Thread");
        }

        public void Init()
        {
            this.ChickenEggValue = new Random().NextDouble() * (1000 - 0.01) + 0.01;
            this.PigeonEggValue = new Random().NextDouble() * (1000 - 0.01) + 0.01;


            granja = new Granja();
            granjero = new Granjero("Anastasio", granja)
            {
                Granja = granja
            };

            granjero.ComprarPonederos();
            granjero.ComprarAnimales();
        }
        
        private void ConsultarPonederos()
        {
            Console.WriteLine("El pondero de {0} tiene {1} huevos", granja.PonederosPalomos[0].GetTipoPonedero(), granja.PonederosPalomos[0].NumeroHuevos());
            Console.WriteLine("El pondero de {0} tiene {1} huevos", granja.PonederosGallinas[0].GetTipoPonedero(), granja.PonederosGallinas[0].NumeroHuevos());
        }

        private void Activity()
        {
            for (int i = 0; i < DIAS; i++)
            {
                Console.WriteLine("Empezando Día {0}.....................", i + 1);

                Console.WriteLine("El nivel de hambre de {0} es {1} y de Pereza {2}", granjero.Nombre, granjero.Hambre, granjero.Pereza);

                this.ConsultarPonederos();

                if (granjero.Pereza < PEREZA_MORTAL)
                {
                    this.Acciones(granja.PonederosPalomos[0].GetAnimalesAsignados(), ACCIONES.COMER);
                    Console.WriteLine("{0} ha dado de comer a sus palomos", granjero.Nombre);

                    this.Acciones(granja.PonederosGallinas[0].GetAnimalesAsignados(), ACCIONES.COMER);
                    Console.WriteLine("{0} ha dado de comer a sus gallinas", granjero.Nombre);
                }
                else
                {
                    Console.WriteLine("{0} se ha olvidado de alimentar a sus animales");
                }

                foreach (Palomo p in granja.PonederosPalomos[0].GetAnimalesAsignados())
                    try
                    {
                        p.PonerHuevo();   
                    }
                    catch (HuevoRotoException)
                    {
                        Console.WriteLine("Hay un ponedero de {0} que está lleno y se ha roto un huevo", granja.PonederosPalomos[0].GetTipoPonedero());
                    }

                foreach (Gallina g in granja.PonederosGallinas[0].GetAnimalesAsignados())
                    try
                    {
                        g.PonerHuevo();
                    }
                    catch (HuevoRotoException)
                    {
                        Console.WriteLine("Hay un ponedero de {0} que está lleno y se ha roto un huevo", granja.PonederosGallinas[0].GetTipoPonedero());
                    }

                if (granjero.Hambre > HAMBRE_MORTAL)
                {

                    foreach ( Animal a in this.granja.Animales)
                    {
                        if (a is Conejo)
                        {
                            this.granja.Animales.Remove(a);

                            break;

                            
                        }
                    }

                    Console.WriteLine("El granjero tiene hambre y ha sacrificado a un conejo");
                }

                    if (granjero.Pereza < PEREZA_MORTAL)
                {
                    int huevosGallinaUnaYema = 0;
                    int huevosGallinaDosYemas = 0;
                    int huevosGallinaTresYemas = 0;

                    int huevosPalomo = granja.PonederosPalomos[0].RecogerHuevos().Count;
                    this.granja.HuevosPalomoParaVenta = huevosPalomo;

                    foreach (Huevo h in granja.PonederosGallinas[0].RecogerHuevos())
                    {
                        if (h.Yemas == 2)
                            huevosGallinaDosYemas++;
                        else if (h.Yemas == 3)
                            huevosGallinaTresYemas++;
                        else
                            huevosGallinaUnaYema++;
                    }
                    this.granja.HuevosGallinaParaVenta = huevosGallinaUnaYema + huevosGallinaDosYemas + huevosGallinaTresYemas;

                    Console.WriteLine("{0} dispone hoy en su granja de {1} huevos de palomo!", granjero.Nombre, huevosPalomo);
                    Console.WriteLine("{0} dispone hoy en su granja de {1} huevos de gallina, de los que {2} son de doble yema y {3} de triple yema", granjero.Nombre, huevosGallinaUnaYema + huevosGallinaDosYemas + huevosGallinaTresYemas, huevosGallinaDosYemas, huevosGallinaTresYemas);
                }
                else
                {
                    Console.WriteLine("La pereza hizo que {0} no recogiera hoy huevos", granjero.Nombre);
                }

                Console.WriteLine("Llega la Noche del día {0}.....................", i + 1);

                granjero.SellEggs(this.ChickenEggValue, this.PigeonEggValue);

                this.Acciones(granja.PonederosPalomos[0].GetAnimalesAsignados(), ACCIONES.DORMIR);
                Console.WriteLine("Los palomos se acuestan...", granjero.Nombre);
                this.Acciones(granja.PonederosGallinas[0].GetAnimalesAsignados(), ACCIONES.DORMIR);
                Console.WriteLine("Las gallinas se duermen...", granjero.Nombre);

                granjero.Pereza += new Random().Next(10, 30);

                this.ConsultarPonederos();
            }
        }

        private void Acciones<T>(List<T> t, ACCIONES accion) where T : Animal
        {
            foreach (T animal in t)
            {
                if (accion == ACCIONES.COMER)
                    animal.Comer();
                else if (accion == ACCIONES.DORMIR)
                    animal.Dormir();
            }
        }
    }
}
