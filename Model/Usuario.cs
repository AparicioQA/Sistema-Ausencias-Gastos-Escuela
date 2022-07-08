using System;


namespace CentroEducativoPalmarSur.Model
{
    public class Usuario
    {
        private string nombreUsuario, respuesta;
        private bool esAdmin, estado;
        private int idUsuario;
        public Usuario()
        {
           
        }
        public Usuario(string nombreUsuario, string respuesta, bool esAdmin,  bool estado)
        {
            this.NombreUsuario = nombreUsuario;
            this.Respuesta = respuesta;
            this.EsAdmin = esAdmin;
          
            this.Estado = estado;
        }
        public Usuario(string nombreUsuario, string respuesta, bool esAdmin, int id, bool estado)
        {
            this.NombreUsuario = nombreUsuario;
            this.Respuesta = respuesta;
            this.EsAdmin = esAdmin;
            this.IdUsuario = id;
            this.Estado = estado;
        }

        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Respuesta { get => respuesta; set => respuesta = value; }
        public bool EsAdmin { get => esAdmin; set => esAdmin = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public bool Estado { get => estado; set => estado = value; }

    }
}
