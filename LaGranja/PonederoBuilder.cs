namespace LaGranja
{
    public enum PONEDERO_TIPO { GALLINAS, PALOMOS };

    public class PonederoBuilder
    {

        public static Ponedero<T> GetPonedero<T>(int capacidad, PONEDERO_TIPO tipoPonedero) where T : Animal, IOviparo
        {
            Ponedero<T> o;

            if (tipoPonedero == PONEDERO_TIPO.GALLINAS)
            {
                o = new Ponedero<Gallina>(capacidad);
            }
            else
            {
                o = new Ponedero<Palomo>(capacidad);
            }

            return o;
        }
    }
}
