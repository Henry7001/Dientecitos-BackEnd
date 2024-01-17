using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Middleware.Exceptions.NotFound;
using Dientecitos_BackEnd.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dientecitos_BackEnd.Datos
{
    public class MapeoDatosMedico
    {

        readonly SqlConnection connection = new(Environment.GetEnvironmentVariable(StringHandler.Database_String));


        public Medico GrabarMedico(MedicoDAO request, int id)
        {

            Medico response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_MEDICO, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", id == 0 ? "Insertar" : "Actualizar");
                    command.Parameters.AddWithValue("@MedicoID", id);
                    command.Parameters.AddWithValue("@UsuarioID", request.UsuarioID);
                    command.Parameters.AddWithValue("@Especialidad", request.Especialidad);
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

                            response.MedicoID = reader["MedicoID"] != DBNull.Value ? Convert.ToInt32(reader["MedicoID"]) : 0;
                            response.Especialidad = reader["Especialidad"] != DBNull.Value ? reader["Especialidad"].ToString() : string.Empty;
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
                        throw new Exception("No se pudo realizar la accion sobre el Medico.");
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



        public MessageResponse EliminarMedico(int id)
        {

            MessageResponse response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_MEDICO, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", "Eliminar");
                    command.Parameters.AddWithValue("@MedicoID", id);

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
                        throw new Exception("No se pudo realizar la accion sobre el Medico.");
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



        public List<Medico> ConsultarMedico(int id, string cedula)
        {

            List<Medico> response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_MEDICO, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    string accion = string.Empty;

                    accion = id switch
                    {
                        0 => "ConsultarTodos",
                        -1 => "ConsultarPorCedula",
                        _ => "ConsultarPorID",
                    };

                    command.Parameters.AddWithValue("@Accion", accion);
                    command.Parameters.AddWithValue("@MedicoID", id);
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

                            Medico Medico = new Medico
                            {

                                MedicoID = reader["MedicoID"] != DBNull.Value ? Convert.ToInt32(reader["MedicoID"]) : 0,
                                Especialidad = reader["Especialidad"] != DBNull.Value ? reader["Especialidad"].ToString() : string.Empty,
                                Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty,
                                //Usuario = reader["UsuarioID"] != DBNull.Value ? new MapeoDatosUsuario().ConsultarUsuario(Convert.ToInt32(reader["UsuarioID"])) : null

                            };

                            response.Add(Medico);
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
