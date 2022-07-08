using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
namespace CentroEducativoPalmarSur.Model
{
    class PresupuestoDAO
    {
        private Conexion con = new Conexion();

        private Decimal RestantePresupuesto(int id)
        {
            Decimal resto = 0;
            string error = null;
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string select = $"SELECT monto FROM Registro_Gasto WHERE presupuesto = {id}";
                    using (SqlCommand comando = conec.CreateCommand())
                    {
                        comando.CommandText = select;
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resto += Convert.ToDecimal(reader["monto"]);
                            }
                        }
                       
                    }
                }
            }
            catch (Exception e)
            {
                error = $"Error en Operacion";

            }
            return resto; 
        }

        public List<Presupuesto> Listar(ref string error)
        {
            List<Presupuesto> presupuestos = new List<Presupuesto>();
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string select = "SELECT * FROM Presupuesto";
                    using (SqlCommand comando = conec.CreateCommand())
                    {
                        comando.CommandText = select;
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            Presupuesto presu;
                            while (reader.Read())
                            {
                                decimal tgastos = RestantePresupuesto(Convert.ToInt32(reader["id_presupuesto"]));
                                decimal monto = Convert.ToDecimal(reader["monto"]);           
                                decimal restante = monto - tgastos;
                                presu = new Presupuesto(Convert.ToInt32(reader["id_presupuesto"]), 
                                    Convert.ToInt32(reader["fecha_year"]), monto,  restante);
                                presu.TotalGastos = tgastos;
                                presupuestos.Add(presu);
                            }
                        }
                        return presupuestos;
                    }
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de listar Presupuestos";

            }
            return presupuestos;
        }

        /*
         * Funcion creada para poder asignar de manera dinamica basada en un año el presepuesto al que
         * se asocia un gasto. Es publica porque el metodo debe poderse usar desde fuera de la clase.
         */
        public int BuscarId(int year, ref string error)
        {
            int id = 0;
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string select = $"SELECT * FROM Presupuesto WHERE fecha_year = {year}";
                    using (SqlCommand comando = conec.CreateCommand())
                    {
                        comando.CommandText = select;
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                           
                            while (reader.Read())
                            {
                                id = Convert.ToInt32(reader["id_presupuesto"]);
                            }
                        }                    
                    }
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de listar Presupuestos";

            }
            return id;
        }

        public bool Agregar(Presupuesto presu, ref string error)
        {
            string ex = null;
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    if (!Listar(ref ex).Exists(x => x.Year == presu.Year))
                    {
                        string insert = "INSERT INTO Presupuesto (monto, fecha_year)" +
                            $" VALUES (@monto, {presu.Year})";
                        using (SqlCommand comando = new SqlCommand(insert, conec))
                        {
                            comando.Parameters.AddWithValue("@monto", presu.Monto);
                            if (comando.ExecuteNonQuery() > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        error = "Error al tratar de agregar\n Ya existe un presupuesto en ese año.";
                    }
                }
            }
            catch (Exception e)
            {
                error = "Error al tratar de agregar.";
            }

            return false;
        }

        public bool Modificar(Presupuesto presu, ref string error)
        {
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    if (presu.Year >= 2010 && presu.Year <= 3000 && presu.Monto >= 0)
                    {
                        string update = $"UPDATE Presupuesto SET monto = @monto WHERE id_presupuesto = {presu.IdPresupuesto}";
                        using (SqlCommand comando = new SqlCommand(update, conec))
                        {
                            comando.Parameters.AddWithValue("@monto", presu.Monto);
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
                error = "Error al tratar de Modificar";
            }

            return false;
        }

    }
}
