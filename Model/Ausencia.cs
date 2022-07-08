using System;


namespace CentroEducativoPalmarSur.Model
{
    class Ausencia
    {
        private int id;
        private string motivo;
        private DateTime fecha;
        private Empleado empleadoV;

        public Ausencia(int id, string motivo, DateTime fecha, Empleado empleado)
        {
            this.id = id;
            this.motivo = motivo;
            this.fecha = fecha;
            this.empleadoV = empleado;
        }


        public Ausencia(string motivo, DateTime fecha, Empleado empleado)
        {
            this.motivo = motivo;
            this.fecha = fecha;
            this.empleadoV = empleado;
        }

        public int Id { get => id; set => id = value; }
        public string Motivo { get => motivo; set => motivo = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public Empleado EmpleadoV { get => empleadoV; set => empleadoV = value; }
    }
}
