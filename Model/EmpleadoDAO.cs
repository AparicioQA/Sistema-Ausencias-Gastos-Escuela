using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CentroEducativoPalmarSur.Helper;
using System.Data.SqlTypes;
namespace CentroEducativoPalmarSur.Model
{
    class EmpleadoDAO
    {
        private Conexion con = new Conexion();

        public int PuntajeEmpleado(Empleado emp)
        {
            int puntaje = 100;
            using (SqlConnection conec = con.GetConexion())
            {
                conec.Open();
                string select = $"SELECT COUNT(empleado) AS Puntaje FROM Ausencia WHERE empleado = {emp.Cedula} " +
                    $"AND YEAR(fecha) = @year;";
                using (SqlCommand comando = conec.CreateCommand())
                {
                    comando.CommandText = select;
                    comando.Parameters.AddWithValue("@year", DateTime.Now.Year);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        
                        if (reader.Read())
                        {
                            puntaje -= Convert.ToInt32(reader["Puntaje"].ToString()) * 10;
                            if(puntaje < 0)
                            {
                                puntaje = 0;
                            }
                        }
                    }
                }
            }
            return puntaje;
        }

        public List<Empleado> Listar(ref string error)
        {
            List<Empleado> empleados = new List<Empleado>();

            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string select = "SELECT * FROM Empleado;";
                    using (SqlCommand comando = conec.CreateCommand())
                    {
                        comando.CommandText = select;

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            Empleado empleado;
                            while (reader.Read())
                            {
                                empleado = new Empleado(reader.GetInt32(0), reader.GetString(1),
                                 reader.GetString(2), reader.GetString(3), reader.GetString(5), reader.GetDateTime(4), reader.GetInt32(6));
                                empleado.Puntaje = PuntajeEmpleado(empleado);
                                empleados.Add(empleado);
                            }
                        }

                    }
                }
            }
            catch (Exception e )
            {
                error = $"Error al tratar de listar al Personal.";
            }
            return empleados;
        }

        public bool Agregar(Empleado emp,ref string error)
        {
            string ex = null;
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    if (!Listar(ref ex).Exists(x => x.Cedula == emp.Cedula))
                    {
                        conec.Open();
                        string insert = "INSERT INTO Empleado (cedula, nombre, apellido1, apellido2, direccion, telefono, fecha_nacimiento)" +
                            $"VALUES ({emp.Cedula}, '{emp.Nombre}', '{emp.Apellido1}', '{emp.Apellido2}', '{emp.Direccion}',  {emp.Telefono}, @date);";

                        using (SqlCommand comando = new SqlCommand(insert, conec))
                        {
                            comando.Parameters.AddWithValue("@date", emp.FechaNacimiento);
                            if (comando.ExecuteNonQuery() > 0)
                                return true;

                        }
                    }

                }
            }
            catch (Exception e)
            {

                error = $"Error al tratar de agregar a un colaborador al Personal.";
            }
            return false;
        } 

        public bool Eliminar(Empleado emp, ref string error)
        {
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string delete = $"DELETE FROM Empleado WHERE cedula = {emp.Cedula}";
                    using (SqlCommand command = new SqlCommand(delete, conec))
                    {
                        if (command.ExecuteNonQuery() > 0)
                            return true;
                    }
                }
            }
            catch (Exception e)
            {

                error = $"Error al tratar de eliminar al Empleado.";
            }
            return false;
        }

        public bool Actualizar(Empleado emp, ref string error)
        {
            
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string update = $"UPDATE Empleado SET nombre = '{emp.Nombre}', apellido1 = '{emp.Apellido1}', " +
                        $"apellido2 = '{emp.Apellido2}', direccion = '{emp.Direccion}', telefono= '{emp.Telefono}', fecha_Nacimiento = @date " +
                        $"WHERE cedula = {emp.Cedula};";
                    using (SqlCommand command = new SqlCommand(update, conec))
                    {
                        command.Parameters.AddWithValue("@date", emp.FechaNacimiento);
                        if (command.ExecuteNonQuery() > 0)
                            return true;
                    }
                }
            }
            catch (Exception e)
            {

                error = $"Error al tratar de Actualizar los datos del Empleado.";
            }
            return false;
        }

    }
}
