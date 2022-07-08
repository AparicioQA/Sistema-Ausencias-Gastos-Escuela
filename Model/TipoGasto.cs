using System;


namespace CentroEducativoPalmarSur.Model
{
    class TipoGasto
    {
        private int id;
        private string nombre;

        public TipoGasto(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
