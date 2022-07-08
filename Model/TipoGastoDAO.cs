using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CentroEducativoPalmarSur.Model
{
    class TipoGastoDAO
    {
        private Conexion con = new Conexion();

        public List<TipoGasto> Listar(ref string error)
        {
            List<TipoGasto> tgastos = new List<TipoGasto>();

            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string select = "SELECT * FROM Tipo_gasto;";
                    using (SqlCommand comando = conec.CreateCommand())
                    {
                        comando.CommandText = select;

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            TipoGasto tgasto;
                            while (reader.Read())
                            {
                                tgasto = new TipoGasto(Convert.ToInt32(reader["id_tipogasto"]), reader["tipo"].ToString());
                                tgastos.Add(tgasto);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de listar las Ausencias.";
            }
            return tgastos;
        }
    }
}
