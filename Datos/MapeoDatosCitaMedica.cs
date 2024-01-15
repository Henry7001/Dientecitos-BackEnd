using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Middleware.Exceptions.NotFound;
using Dientecitos_BackEnd.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dientecitos_BackEnd.Datos
{
    public class MapeoDatosCitaMedica
    {

        readonly SqlConnection connection = new(Environment.GetEnvironmentVariable(StringHandler.Database_String));

        public CitaMedica GrabarCitaMedica(CitaMedicaDAO request, int id)
        {

            CitaMedica response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_CITA_MEDICA, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", id == 0 ? "Insertar" : "Actualizar");
                    command.Parameters.AddWithValue("@CitaMedicaID", id);
                    command.Parameters.AddWithValue("@TipoTratamientoID", request.TipoTratamientoID);
                    command.Parameters.AddWithValue("@MedicoID", request.MedicoID);
                    command.Parameters.AddWithValue("@PacienteID", request.PacienteID);
                    command.Parameters.AddWithValue("@Observaciones", request.Observaciones);
                    command.Parameters.AddWithValue("@Diagnostico", request.Diagnostico);
                    command.Parameters.AddWithValue("@FechaHoraCita", request.FechaHoraCita);
                    command.Parameters.AddWithValue("@Estado", Utils.Utils.ConvertirPrimeraLetraMayuscula(request.Estado));

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount == 1 && reader["Mensaje"] != DBNull.Value)
                            {
                                throw new BadRequestException(reader["Mensaje"].ToString() ?? "");
                            }

                            response.CitaMedicaID = reader["CitaMedicaID"] != DBNull.Value ? Convert.ToInt32(reader["CitaMedicaID"]) : 0;
                            response.Observaciones = reader["Observaciones"] != DBNull.Value ? reader["Observaciones"].ToString() : string.Empty;
                            response.Diagnostico = reader["Diagnostico"] != DBNull.Value ? reader["Diagnostico"].ToString() : string.Empty;
                            response.FechaHoraCita = reader["FechaHoraCita"] != DBNull.Value ? Convert.ToDateTime(reader["FechaHoraCita"]) : new DateTime();
                            response.Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty;

                            if (reader["MedicoID"] != DBNull.Value)
                            {
                                response.Medico = new MapeoDatosMedico().ConsultarMedico(Convert.ToInt32(reader["MedicoID"]), "")[0];
                            }

                            if (reader["TipoTratamientoID"] != DBNull.Value)
                            {
                                response.TipoTratamiento = new MapeoDatosTipoTratamiento().ConsultarTipoTratamiento(Convert.ToInt32(reader["TipoTratamientoID"]))[0];
                            }

                            if (reader["PacienteID"] != DBNull.Value)
                            {
                                //response.Paciente = new MapeoDatosPaciente().ConsultarPaciente(Convert.ToInt32(reader["PacienteID"]))[0];
                            }

                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new Exception("No se pudo realizar la accion sobre la cita medica.");
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



        public MessageResponse CancelarCitaMedica(int id)
        {

            MessageResponse response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_CITA_MEDICA, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", "Cancelar");
                    command.Parameters.AddWithValue("@CitaMedicaId", id);

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
                        throw new Exception("No se pudo realizar la accion sobre la cita medica.");
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


        
        public List<CitaMedica> ConsultarCitaMedica(int id, 
            int? TipoTratamientoID, int? MedicoID, int? PacienteID, DateTimeOffset? Fecha, string? Estado)
        {

            List<CitaMedica> response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_CITA_MEDICA, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", id == 0 ? "ConsultarTodos" : "ConsultarPorID");
                    command.Parameters.AddWithValue("@CitaMedicaId", id);
                    command.Parameters.AddWithValue("@TipoTratamientoId", TipoTratamientoID);
                    command.Parameters.AddWithValue("@MedicoId", MedicoID);
                    command.Parameters.AddWithValue("@PacienteId", PacienteID); 
                    command.Parameters.AddWithValue("@FechaHoraCita", Fecha);
                    command.Parameters.AddWithValue("@Estado", Estado);

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount == 1 && reader["Mensaje"] != DBNull.Value)
                            {
                                throw new BadRequestException(reader["Mensaje"].ToString() ?? "");
                            }

                            CitaMedica CitaMedica = new CitaMedica
                            {
                                CitaMedicaID = reader["CitaMedicaID"] != DBNull.Value ? Convert.ToInt32(reader["CitaMedicaID"]) : 0,
                                Observaciones = reader["Observaciones"] != DBNull.Value ? reader["Observaciones"].ToString() : string.Empty,
                                Diagnostico = reader["Diagnostico"] != DBNull.Value ? reader["Diagnostico"].ToString() : string.Empty,
                                FechaHoraCita = reader["FechaHoraCita"] != DBNull.Value ? Convert.ToDateTime(reader["FechaHoraCita"]) : new DateTime(),
                                Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty,

                                Medico = reader["MedicoID"] != DBNull.Value ? new MapeoDatosMedico().ConsultarMedico(Convert.ToInt32(reader["MedicoID"]), "")[0] : null,
                                TipoTratamiento = reader["TipoTratamientoID"] != DBNull.Value ? new MapeoDatosTipoTratamiento().ConsultarTipoTratamiento(Convert.ToInt32(reader["TipoTratamientoID"]))[0] : null
                                //Paciente = reader["PacienteID"] != DBNull.Value ? new MapeoDatosPacient().ConsultarPaciente(Convert.ToInt32(reader["PacienteID"]))[0] : null
                            };

                            response.Add(CitaMedica);
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
