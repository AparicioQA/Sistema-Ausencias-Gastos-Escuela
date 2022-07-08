using System;


using Microsoft.Data.SqlClient;

namespace CentroEducativoPalmarSur.Model
{
    class Conexion
    {
        private string server, basedatos, seguridad;

        public SqlConnection GetConexion() {
            server = @"SERVER=(local)";
            basedatos = "DATABASE= SACGP";
            seguridad = "Integrated security = true; TrustServerCertificate=True";

            string ConexionString = $"{server}; {basedatos}; {seguridad}";

            SqlConnection conexion = new SqlConnection(ConexionString);
            return conexion;
        }
    }
}
