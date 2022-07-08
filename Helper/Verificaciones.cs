using System;

namespace CentroEducativoPalmarSur.Helper
{
    public class Verificaciones
    {
        public static bool TieneNumeros(string texto)
        {
            bool condicion = false;
            for (int i=0; i < 10; i++ )
            {
                if (texto.Contains(i.ToString()))
                {
                    condicion = true;
                }
            }
            return condicion;
        } 

       
    }
}

