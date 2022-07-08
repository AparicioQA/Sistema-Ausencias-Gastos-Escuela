using System;


namespace CentroEducativoPalmarSur.Model
{
    class Presupuesto
    {
        private int idPresupuesto, year;
        private decimal monto, restante, totalGastos;

        public Presupuesto(int idPresupuesto, int year, decimal monto, decimal restante=0, decimal tatalGastos=0)
        {
            this.IdPresupuesto = idPresupuesto;
            this.Year = year;
            this.Monto = monto;
            this.restante = restante;
            this.TotalGastos = totalGastos;
        }

        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
        public int Year { get => year; set => year = value; }
        public decimal Monto { get => monto; set => monto = value; }
        public decimal Restante { get => restante; set => restante = value; }
        public decimal TotalGastos { get => totalGastos; set => totalGastos = value; }
    }
}
