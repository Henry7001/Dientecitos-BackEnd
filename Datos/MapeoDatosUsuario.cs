using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dientecitos_BackEnd.Datos
{
    public class MapeoDatosUsuario
    {
        readonly SqlConnection connection = new(Environment.GetEnvironmentVariable(StringHandler.Database_String));

        public Usuario GrabarUsuario(NuevoUsuario request, RolesEnum rol)
        {
            Usuario response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new("GestionarUsuario", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros del stored procedure
                    command.Parameters.AddWithValue("@Accion", "Insertar");
                    command.Parameters.AddWithValue("@Cedula", request.Cedula);
                    command.Parameters.AddWithValue("@Nombre", request.Nombre);
                    command.Parameters.AddWithValue("@Telefono", request.Telefono);
                    command.Parameters.AddWithValue("@Contraseña", request.Contraseña);
                    command.Parameters.AddWithValue("@Rol", rol.ToString());

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            response.UsuarioID = reader["UsuarioID"] != DBNull.Value ? Convert.ToInt32(reader["UsuarioID"]) : 0;
                            response.Cedula = reader["Cedula"] != DBNull.Value ? reader["Cedula"].ToString() : string.Empty;
                            response.Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : string.Empty;
                            response.Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : string.Empty;
                            response.Rol = reader["Rol"] != DBNull.Value ? reader["Rol"].ToString() : string.Empty;
                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new Exception("No se pudo insertar el usuario.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return response;
        }

        public Usuario ActualizarUsuario(NuevoUsuario request, int id, RolesEnum rol)
        {
            Usuario response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new("GestionarUsuario", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros del stored procedure
                    command.Parameters.AddWithValue("@Accion", "Actualizar");
                    command.Parameters.AddWithValue("@UsuarioID", id);
                    command.Parameters.AddWithValue("@Cedula", request.Cedula);
                    command.Parameters.AddWithValue("@Nombre", request.Nombre);
                    command.Parameters.AddWithValue("@Telefono", request.Telefono);
                    command.Parameters.AddWithValue("@Rol", rol.ToString());

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            response.UsuarioID = reader["UsuarioID"] != DBNull.Value ? Convert.ToInt32(reader["UsuarioID"]) : 0;
                            response.Cedula = reader["Cedula"] != DBNull.Value ? reader["Cedula"].ToString() : string.Empty;
                            response.Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : string.Empty;
                            response.Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : string.Empty;
                            response.Rol = reader["Rol"] != DBNull.Value ? reader["Rol"].ToString() : string.Empty;
                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new Exception("No se pudo actualizar al usuario.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return response;
        }

        public Boolean EliminarUsuario(int id)
        {

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new("GestionarUsuario", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros del stored procedure
                    command.Parameters.AddWithValue("@Accion", "Eliminar");
                    command.Parameters.AddWithValue("@UsuarioID", id);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public Usuario Login(String cedula, string contraseña)
        {
            Usuario response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new("GestionarUsuario", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros del stored procedure
                    command.Parameters.AddWithValue("@Accion", "IniciarSesion");
                    command.Parameters.AddWithValue("@Cedula", cedula);
                    command.Parameters.AddWithValue("@Contraseña", contraseña);

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            response.UsuarioID = reader["UsuarioID"] != DBNull.Value ? Convert.ToInt32(reader["UsuarioID"]) : 0;
                            response.Cedula = reader["Cedula"] != DBNull.Value ? reader["Cedula"].ToString() : string.Empty;
                            response.Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : string.Empty;
                            response.Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : string.Empty;
                            response.Rol = reader["Rol"] != DBNull.Value ? reader["Rol"].ToString() : string.Empty;
                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new Exception("No se pudo iniciar sesión del usuario.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return response;
        }

    }
}
