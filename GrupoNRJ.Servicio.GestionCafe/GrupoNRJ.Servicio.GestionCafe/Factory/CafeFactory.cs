namespace GrupoNRJ.Servicio.GestionCafe.Factory
{
    using GrupoNRJ.Servicio.GestionCafe;

    public static class CafeFactory
    {
        public static ICafe CrearCafe(string tipo, string nivelTueste, EjecutarSP ejecutor)
        {
            tipo = tipo.ToLower();
            nivelTueste = nivelTueste.ToLower();

            if (tipo == "arabica")
            {
                switch (nivelTueste)
                {
                    case "claro": return new cafeArabicaClaro(ejecutor);
                    case "medio": return new cafeArabicaMedio(ejecutor);
                    case "oscuro": return new cafeArabicaOscuro(ejecutor);
                }
            }
            else if (tipo == "robusta")
            {
                switch (nivelTueste)
                {
                    case "claro": return new cafeRobustaClaro(ejecutor);
                    case "medio": return new cafeRobustaMedio(ejecutor);
                    case "oscuro": return new cafeRobustaOscuro(ejecutor);
                }
            }
            else if (tipo == "blends")
            {
                switch (nivelTueste)
                {
                    case "claro": return new cafeBlendsClaro(ejecutor);
                    case "medio": return new cafeBlendsMedio(ejecutor);
                    case "oscuro": return new cafeBlendsOscuro(ejecutor);
                }
            }

            throw new ArgumentException("Tipo o nivel de tueste inválido.");
        }
    }

}
