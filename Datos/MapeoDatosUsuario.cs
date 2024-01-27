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

        public Usuario ActualizarUsuario(Usuario request)
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
                    command.Parameters.AddWithValue("@UsuarioID", request.UsuarioID);
                    command.Parameters.AddWithValue("@Cedula", request.Cedula);
                    command.Parameters.AddWithValue("@Nombre", request.Nombre);
                    command.Parameters.AddWithValue("@Telefono", request.Telefono);
                    command.Parameters.AddWithValue("@Rol", request.Rol);

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

        public MessageResponse EliminarUsuario(int id)
        {
            MessageResponse response = new();
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
                    response.Mensaje = "Se ha eliminado el usuario";
                    command.BeginExecuteNonQuery();
                    return response;
                }
                catch (Exception)
                {
                    response.Mensaje = "No se ha eliminado el usuario";
                    return response;
                }
            }
        }

        public Usuario Login(Login login)
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
                    command.Parameters.AddWithValue("@Cedula", login.Cedula);
                    command.Parameters.AddWithValue("@Contraseña", login.Contraseña);

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

        public Usuario GetUsuarioByID(int id)
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
                    command.Parameters.AddWithValue("@Accion", "ConsultarPorId");
                    command.Parameters.AddWithValue("@UsuarioID", id);

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
                        throw new Exception("No se pudo buscar al usuario.");
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
