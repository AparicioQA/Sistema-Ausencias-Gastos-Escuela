using System;


namespace CentroEducativoPalmarSur.Model
{
    class Gasto
    {
        private int id, presupuesto;
        private DateTime fecha;
        private Decimal monto;
        private TipoGasto tipo;
        private Usuario user;
       
        public Gasto(int id, DateTime fecha, decimal monto, TipoGasto tipo, Usuario user, int presu =0)
        {
            this.Id = id;
            this.Fecha = fecha;
            this.Monto = monto;
            this.Tipo = tipo;
            this.User = user;
            this.presupuesto = presu;
        }

        public int Id { get => id; set => id = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public decimal Monto { get => monto; set => monto = value; }
        public int Presupuesto { get => presupuesto; set => presupuesto = value; }
        public TipoGasto Tipo { get => tipo; set => tipo = value; }
        public Usuario User { get => user; set => user = value; }
    }
}
