using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CentroEducativoPalmarSur.Helper;

namespace CentroEducativoPalmarSur.Model
{
    class UsuarioDAO
    {
        private Conexion con = new Conexion(); 

        public List<Usuario> Listar(ref string error, Usuario us = null) 
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    string select = "";
                    if (us == null)
                    {
                         select = "SELECT * FROM Usuario WHERE estado = 1;";
                    }
                    else
                    {
                        select = $"SELECT * FROM Usuario WHERE estado = 1 AND nombre_usuario <> '{us.NombreUsuario}';";
                    }

                    using (SqlCommand comando = conec.CreateCommand())
                    {
                        comando.CommandText = select;
                 
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            Usuario user;
                            while (reader.Read())
                            {
                                user = new Usuario(reader.GetString(0), reader.GetString(4),
                                 reader.GetBoolean(2), reader.GetInt32(3), Convert.ToBoolean(reader["estado"]));
                                usuarios.Add(user);
                            }
                        }
                        return usuarios;
                    }

                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de listar Usuarios.";
            }
            return usuarios;

        }

        public bool Registrar(string nombreUsuario, string clave, string respuesta, ref string error) 
        {
            try
            {
                string ex = null;
                using (SqlConnection conec = con.GetConexion())
                {
                    if (!Listar(ref ex).Exists(x => x.NombreUsuario == nombreUsuario))
                    {
                        conec.Open();
                        string insert = "INSERT INTO Usuario (nombre_usuario, clave, esAdmin, respuesta, estado)" +
                            $" VALUES ('{nombreUsuario}', '{Cifrado.SHA256(clave)}', 1, '{respuesta.ToLower()}', 1)";
                        using (SqlCommand comando = new SqlCommand(insert, conec))
                        {
                            if (comando.ExecuteNonQuery() > 0)
                                return true;
                            
                        }
                    }
                    else
                    {
                        error = "El usuario ya existe";
                    }

                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de Registrar un nuevo Usuario";
            }
            return false;

        }

        public Usuario Ingresar(string nombreUsuario, string clave, ref string error) 
        {
            Usuario user = null;
            try
            {
              
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                  
                    string select = $"SELECT * FROM Usuario WHERE nombre_usuario = '{nombreUsuario}' AND " +
                        $"clave = '{Cifrado.SHA256(clave)}' AND estado = 1";
                    SqlCommand comando = new SqlCommand(select, conec);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new Usuario(reader["nombre_Usuario"].ToString(), reader["respuesta"].ToString(), (bool)reader["esAdmin"],
                                (int)reader["id_Usuario"], true);
                            return user;
                        }
                        
                    }

                }
            }
            catch(Exception e)
            {
                error = $"Error en la funcion Ingresar. ${e.Message}";
            }
                return user;
    
        }

        public bool Restablecer(string nombreUsuario, string clave, string respuesta, ref string error) 
        {
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    string select = $"SELECT * FROM Usuario WHERE nombre_usuario = '{nombreUsuario}' AND " +
                      $"respuesta = '{respuesta.ToLower()}' AND estado = 1";
                    conec.Open();
                    SqlCommand comando;
                    int id = 0;
                    using (comando = new SqlCommand(select, conec))
                    {

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                id = Convert.ToInt32(reader["id_Usuario"].ToString());
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    string update = $"UPDATE Usuario SET clave = '{Cifrado.SHA256(clave)}' " +
                      $"WHERE id_Usuario = {id}";
                    using (comando = new SqlCommand(update, conec))
                    {
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de Restablecer al Usuario.";
            }  
    
            return false;
        }
        

        
        public bool CrearInvitado(Usuario user, string clave, ref string error) {
            string ex = null;
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    if (!Listar(ref ex).Exists(x => x.NombreUsuario == user.NombreUsuario))
                    {
                        string insert = "INSERT INTO Usuario (nombre_usuario, clave, esAdmin, respuesta, estado)" +
                            $" VALUES ('{user.NombreUsuario}', '{Cifrado.SHA256(clave)}', 0, '{user.Respuesta}', 1)";
                        using (SqlCommand comando = new SqlCommand(insert, conec))
                        {
                            if (comando.ExecuteNonQuery() > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        error = "El usuario ya existe";
                    }
                }
            }catch(Exception e)
            {
                error = $"Error al tratar de Crear un nuevo Usuario Invitado.";
            }

            return false;

        }

        public bool Modificar(Usuario user, ref string error) 
        {
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();
                    if (!Listar(ref error).Exists(x => x.NombreUsuario == user.NombreUsuario && user.IdUsuario !=  x.IdUsuario))
                    {
                        string update = $"UPDATE Usuario SET respuesta = '{user.Respuesta}' " +
                      $",esAdmin = {(user.EsAdmin ? 1 : 0)}, nombre_usuario = '{user.NombreUsuario}' WHERE id_Usuario = {user.IdUsuario}";
                        using (SqlCommand comando = new SqlCommand(update, conec))
                        {
                            if (comando.ExecuteNonQuery() > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        error = "El usuario ya existe";
                    }
                  
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de Modificar a un Usuario.";
            }

            return false;
        }

        public bool Desactivar(Usuario user, ref string error)
        {
            try
            {
                using (SqlConnection conec = con.GetConexion())
                {
                    conec.Open();

                    string update = $"UPDATE Usuario SET estado= 0 WHERE id_Usuario = {user.IdUsuario}";
                    using (SqlCommand comando = new SqlCommand(update, conec))
                    {
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                error = $"Error al tratar de Desabilitar a un Usuario.";
            }

            return false;
        }

    }
}
