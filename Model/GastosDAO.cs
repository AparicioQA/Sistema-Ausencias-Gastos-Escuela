using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
namespace CentroEducativoPalmarSur.Model
{
    class GastosDAO
    {
        private Conexion con = new Conexion();

        public List<Gasto> Listar(ref string error)
        {
            List<Gasto> gastos = new List<Gasto>();

            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string select = "SELECT * FROM Registro_gasto INNER JOIN " +
                        "Tipo_gasto ON Registro_gasto.tipo = Tipo_gasto.id_tipogasto INNER JOIN Usuario  " +
                        "ON Registro_gasto.usuario = Usuario.id_Usuario INNER JOIN Presupuesto ON Registro_gasto.presupuesto = " +
                        "Presupuesto.id_presupuesto;";

                    using (SqlCommand comando = conec.CreateCommand())
                    {
                        comando.CommandText = select;

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            Gasto gasto;
                            while (reader.Read())
                            {
                                int idgasto = Convert.ToInt32(reader["id_gasto"]);
                                DateTime fecha = DateTime.Parse(reader["fecha"].ToString());
                                Decimal monto = Convert.ToDecimal(reader["monto"]);
                                
                                int idTipoGasto = Convert.ToInt32(reader["id_tipogasto"]);
                                string tipo = reader["tipo"].ToString();
                                TipoGasto tgasto = new TipoGasto(idTipoGasto, tipo);

                                Usuario user = new Usuario();
                                user.NombreUsuario = reader["nombre_usuario"].ToString();

                                gasto = new Gasto(idgasto, fecha,monto, tgasto, user);
                                gastos.Add(gasto);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de listar los Gastos.";
            }
            return gastos;
        }
        public bool Agregar(Gasto gasto, ref string error)
        {
            try
            {
             
                using (SqlConnection conec = con.GetConexion())
                {
                    if (gasto.Monto >=0 && gasto.Fecha.Year >=2010)
                    {
                        conec.Open();
                        int idPresupuesto = new PresupuestoDAO().BuscarId(gasto.Fecha.Year,ref error);
                        string insert = $"INSERT INTO Registro_gasto (tipo, monto, fecha, presupuesto, usuario) " +
                            $"VALUES ({gasto.Tipo.Id}, @monto, @fecha, {idPresupuesto}, {gasto.User.IdUsuario});";
                        using (SqlCommand comando = new SqlCommand(insert, conec))
                        {
                            comando.Parameters.AddWithValue("@fecha", gasto.Fecha);
                            comando.Parameters.AddWithValue("@monto", gasto.Monto);
                            if (comando.ExecuteNonQuery() > 0)
                                return true;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de Registrar un nuevo Gasto.";
            }
            return false;
        }
        public bool Actualizar(Gasto gasto, ref string error)
        {
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    if (gasto.Monto >= 0 && gasto.Fecha.Year >= 2010)
                    {
                        conec.Open();
                        int idPresupuesto = new PresupuestoDAO().BuscarId(gasto.Fecha.Year, ref error);
                        gasto.Presupuesto = idPresupuesto;
                        string update = $"UPDATE Registro_Gasto SET tipo = {gasto.Tipo.Id}, monto = @monto, fecha= @fecha," +
                            $"presupuesto= {gasto.Presupuesto}, usuario= {gasto.User.IdUsuario} WHERE id_gasto= {gasto.Id}";
                        using (SqlCommand comando = new SqlCommand(update, conec))
                        {
                            comando.Parameters.AddWithValue("@fecha", gasto.Fecha);
                            comando.Parameters.AddWithValue("@monto", gasto.Monto);
                            if (comando.ExecuteNonQuery() > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de Modificar Gasto";
            }

            return false;
        }
        public bool Eliminar(Gasto gasto, ref string error)
        {
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string update = $"DELETE FROM Registro_gasto WHERE id_gasto = {gasto.Id}";
                    using (SqlCommand command = new SqlCommand(update, conec))
                    {

                        if (command.ExecuteNonQuery() > 0)
                            return true;
                    }
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de Eliminar el Gasto";
            }
            return false;
        }
    }
}