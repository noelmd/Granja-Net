using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LaGranja
{
    public class Ponedero<T> where T : Animal, IOviparo
    {
        private int capacidad = 20;

        private List<Huevo> huevos = new List<Huevo>();

        private List<T> animalesAsignados = new List<T>();

        private T t;

        public Ponedero()
        {
        }

        public Ponedero(T t, int capacidad)
        {
            this.t = t;

            this.capacidad = capacidad;

            huevos = new List<Huevo>(this.capacidad);
        }

        public Ponedero(int capacidad)
        {
            this.capacidad = capacidad;

            huevos = new List<Huevo>(this.capacidad);
        }

        public int AddHuevo(Huevo huevo)
        {
            if (this.huevos.Count < this.capacidad)
            {
                this.huevos.Add(huevo);
            }
            else
            {
                throw new HuevoRotoException();
            }

            return this.huevos.Count;
        }

        public List<Huevo> RecogerHuevos(int numeroHuevos)
        {
            if (huevos.Count >= numeroHuevos)
            {
                List<Huevo> huevosRecogidos = this.huevos.Take(numeroHuevos).ToList();

                huevos.RemoveRange(0, numeroHuevos);

                return huevosRecogidos;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public List<Huevo> RecogerHuevos()
        {
            List<Huevo> huevosRecogidos = this.huevos.Take(this.huevos.Count).ToList();
            return huevosRecogidos;
        }

        public int NumeroHuevos()
        {
            return this.huevos.Count;
        }

        public String GetTipoPonedero()
        {
            return this.GetType().ToString();
        }

        public void RegistrarAnimal(T t)
        {
            if(this.animalesAsignados.Count < this.capacidad)
                animalesAsignados.Add(t);
            else
                throw new PonederoLlenoException();
        }

        public void RegistrarAnimales(List<T> animales)
        {
            if (animalesAsignados.Count + animales.Count <= this.capacidad)
            {   foreach (T t in animales)
                    this.RegistrarAnimal(t);
            }
        }

        public List<T> GetAnimalesAsignados()
        {
            return this.animalesAsignados;
        }

        public static implicit operator Ponedero<T>(Ponedero<Gallina> v)
        {
            return new PonederoGallina();
        }

        public static implicit operator Ponedero<T>(Ponedero<Palomo> v)
        {
            return new PonederoPalomo();
        }

        class PonederoGallina : Ponedero<T>
        {

        }

        class PonederoPalomo : Ponedero<T>
        {

        }
    }
}
