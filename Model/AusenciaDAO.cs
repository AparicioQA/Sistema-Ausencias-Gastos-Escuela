using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
namespace CentroEducativoPalmarSur.Model
{
    class AusenciaDAO
    {
        private Conexion con = new Conexion();

        public List<Ausencia> Listar(ref string error)
        {
            List<Ausencia> ausencias = new List<Ausencia>();

            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string select = "SELECT * FROM Ausencia INNER JOIN Empleado ON Ausencia.empleado = Empleado.cedula ORDER BY Empleado.nombre";
                    using (SqlCommand comando = conec.CreateCommand())
                    {
                        comando.CommandText = select;

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            Ausencia ausencia;
                            while (reader.Read())
                            {
                                Empleado empleado = new Empleado(Convert.ToInt32(reader["cedula"]), reader["nombre"].ToString(), reader["apellido1"].ToString());

                                ausencia = new Ausencia(Convert.ToInt32(reader["id_ausencia"]), reader["motivo"].ToString(), Convert.ToDateTime(reader["fecha"]), empleado);
                                ausencias.Add(ausencia);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de listar las Ausencias.";
            }
            return ausencias;
        }

        public bool Agregar(Ausencia ausencia, ref string error)
        {

            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string insert = $"INSERT INTO Ausencia (fecha, motivo, empleado) VALUES ( @fecha, '{ausencia.Motivo}', " +
                        $"{ausencia.EmpleadoV.Cedula});";
              

                    using (SqlCommand comando = new SqlCommand(insert, conec))
                    {
                       comando.Parameters.AddWithValue("@fecha", ausencia.Fecha);
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                                

                    }
                    
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de Agregar la Ausencia.";
            }
            return false;
        }

        public bool Actualizar(Ausencia ausencia, ref string error)
        {

            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string update = $"UPDATE Ausencia SET fecha= @fecha, motivo= '{ausencia.Motivo}', empleado= {ausencia.EmpleadoV.Cedula} WHERE id_ausencia = {ausencia.Id};";

                    using (SqlCommand command = new SqlCommand(update, conec))
                    {
                        command.Parameters.AddWithValue("@fecha", ausencia.Fecha);
                        if (command.ExecuteNonQuery() > 0)
                            return true;
                    }
                }
            }
            catch (Exception e)
            {

                error = $"Error al tratar de Actualizar los datos de la Ausencia.";
            }
            return false;
        }

        public bool Eliminar(Ausencia ausencia, ref string error)
        {

            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open(); 
                    string update = $"DELETE FROM Ausencia WHERE id_ausencia = {ausencia.Id}";
                    using (SqlCommand command = new SqlCommand(update, conec))
                    {
                        
                        if (command.ExecuteNonQuery() > 0)
                            return true;
                    }
                }
            }
            catch (Exception e)
            {

                error = $"Error al tratar de eliminar";
            }
            return false;
        }

    }
}
