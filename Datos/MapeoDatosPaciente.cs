using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Middleware.Exceptions.NotFound;
using Dientecitos_BackEnd.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dientecitos_BackEnd.Datos
{
    public class MapeoDatosPaciente
    {

        readonly SqlConnection connection = new(Environment.GetEnvironmentVariable(StringHandler.Database_String));


        public Paciente GrabarPaciente(PacienteDAO request, int id)
        {

            Paciente response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_PACIENTE, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", id == 0 ? "Insertar" : "Actualizar");
                    command.Parameters.AddWithValue("@PacienteID", id);
                    command.Parameters.AddWithValue("@UsuarioID", request.UsuarioID);
                    command.Parameters.AddWithValue("@Direccion", request.Direccion);
                    command.Parameters.AddWithValue("@Estado", request.Estado);

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount == 1 && reader["Mensaje"] != DBNull.Value)
                            {
                                throw new BadRequestException(reader["Mensaje"].ToString() ?? "");
                            }

                            response.PacienteID = reader["PacienteID"] != DBNull.Value ? Convert.ToInt32(reader["PacienteID"]) : 0;
                            response.Direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : string.Empty;
                            response.NumeroContacto = reader["NumeroContacto"] != DBNull.Value ? reader["NumeroContacto"].ToString() : string.Empty;
                            response.Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty;

                            if (reader["UsuarioID"] != DBNull.Value)
                            {
                                //response.Usuario = new MapeoDatosUsuario().ConsultarUsuario(Convert.ToInt32(reader["UsuarioID"]));
                            }

                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new Exception("No se pudo realizar la accion sobre el Paciente.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally { connection.Close(); }
            }

            return response;
        }



        public MessageResponse EliminarPaciente(int id)
        {

            MessageResponse response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_PACIENTE, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", "Eliminar");
                    command.Parameters.AddWithValue("@PacienteID", id);

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount == 1 && reader["Mensaje"] != DBNull.Value)
                            {
                                throw new BadRequestException(reader["Mensaje"].ToString() ?? "");
                            }

                            response.Mensaje = reader["Mensaje"] != DBNull.Value ? reader["Mensaje"].ToString() : string.Empty;

                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new Exception("No se pudo realizar la accion sobre el Paciente.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally { connection.Close(); }
            }

            return response;
        }



        public List<Paciente> ConsultarPaciente(int id, string cedula)
        {

            List<Paciente> response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_PACIENTE, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    string accion = string.Empty;

                    accion = id switch
                    {
                        0 => "ConsultarTodos",
                        -1 => "ConsultarPorCedula",
                        _ => "ConsultarPorID",
                    };

                    command.Parameters.AddWithValue("@Accion", accion);
                    command.Parameters.AddWithValue("@PacienteID", id);
                    command.Parameters.AddWithValue("@Cedula", cedula);

                    using SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount == 1 && reader["Mensaje"] != DBNull.Value)
                            {
                                throw new BadRequestException(reader["Mensaje"].ToString() ?? "");
                            }

                            Paciente Paciente = new Paciente
                            {

                                PacienteID = reader["PacienteID"] != DBNull.Value ? Convert.ToInt32(reader["PacienteID"]) : 0,
                                NumeroContacto = reader["NumeroContacto"] != DBNull.Value ? reader["NumeroContacto"].ToString() : 0,
                                Direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : string.Empty,
                                Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty,
                                //Usuario = reader["UsuarioID"] != DBNull.Value ? new MapeoDatosUsuario().ConsultarUsuario(Convert.ToInt32(reader["UsuarioID"])) : null

                            };

                            response.Add(Paciente);
                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new RegisterNotFoundException();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally { connection.Close(); }
            }

            return response;
        }


    }
}
