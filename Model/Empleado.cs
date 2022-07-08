using System;

namespace CentroEducativoPalmarSur.Model
{
    class Empleado
    {
        private int cedula, telefono, edad, puntaje;
        private String nombre, apellido1, apellido2, direccion;
        private DateTime fechaNacimiento;

        public Empleado(int cedula)
        {
            this.cedula = cedula;
        }

        public Empleado(int cedula, string nombre, string apellido1)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellido1 = apellido1;

        }
        public Empleado(int cedula, string nombre, string apellido1, string apellido2, string direccion, DateTime fechaNacimiento, int telefono)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.direccion = direccion;
            this.fechaNacimiento = fechaNacimiento;
            this.Telefono = telefono;
            int temp = (DateTime.Now.Year - fechaNacimiento.Year) - 1;
            if (temp == -1)
            {
                this.edad = 0;
            }
            else 
            {
                this.edad = temp;
            }
            
        }
        public int Edad { get => edad; set => edad = value; } 
        public int Cedula { get => cedula; set => cedula = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido1 { get => apellido1; set => apellido1 = value; }
        public string Apellido2 { get => apellido2; set => apellido2 = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public int Puntaje { get => puntaje; set => puntaje = value; }
    }
}
