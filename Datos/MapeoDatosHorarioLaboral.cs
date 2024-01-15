using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Middleware.Exceptions.NotFound;
using Dientecitos_BackEnd.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dientecitos_BackEnd.Datos
{
    public class MapeoDatosHorarioLaboral
    {

        readonly SqlConnection connection = new(Environment.GetEnvironmentVariable(StringHandler.Database_String));


        public HorarioLaboral GrabarHorarioLaboral(HorarioLaboralDAO request, int id)
        {

            HorarioLaboral response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_HORARIO_LABORAL, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", id == 0 ? "Insertar" : "Actualizar");
                    command.Parameters.AddWithValue("@HorarioID", id);
                    command.Parameters.AddWithValue("@DiaSemana", Utils.Utils.ConvertirPrimeraLetraMayuscula(request.DiaSemana));
                    command.Parameters.AddWithValue("@HoraInicio", request.HoraInicio);
                    command.Parameters.AddWithValue("@HoraFin", request.HoraFin);
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

                            response.HorarioID = reader["HorarioID"] != DBNull.Value ? Convert.ToInt32(reader["HorarioID"]) : 0;
                            response.DiaSemana = reader["DiaSemana"] != DBNull.Value ? reader["DiaSemana"].ToString() : string.Empty;
                            response.HoraInicio = reader["HoraInicio"] != DBNull.Value ? TimeSpan.Parse(reader["HoraFin"].ToString()) : new TimeSpan();
                            response.HoraFin = reader["HoraFin"] != DBNull.Value ? TimeSpan.Parse(reader["HoraFin"].ToString()) : new TimeSpan();
                            response.Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty;

                            if(reader["MedicoID"] != DBNull.Value)
                            {
                                response.Medico = new MapeoDatosMedico().ConsultarMedico(Convert.ToInt32(reader["MedicoID"]), "")[0];
                            }

                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new Exception("No se pudo realizar la accion sobre el Horario.");
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



        public MessageResponse EliminarHorarioLaboral(int id)
        {

            MessageResponse response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_HORARIO_LABORAL, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", "Eliminar");
                    command.Parameters.AddWithValue("@HorarioID", id);

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
                        throw new Exception("No se pudo realizar la accion sobre el Horario.");
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



        public List<HorarioLaboral> ConsultarHorarioLaboral(int id, bool medico)
        {

            List<HorarioLaboral> response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_HORARIO_LABORAL, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    string accion = "ConsultarTodos";
                    if (id > 0) { accion = "ConsultarPorID"; }
                    if (id > 0 && medico) accion = "ConsultarPorMedico";

                    command.Parameters.AddWithValue("@Accion", accion);
                    command.Parameters.AddWithValue("@HorarioID", id);
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

                            HorarioLaboral horarioLaboral = new HorarioLaboral
                            {

                                HorarioID = reader["HorarioID"] != DBNull.Value ? Convert.ToInt32(reader["HorarioID"]) : 0,
                                DiaSemana = reader["DiaSemana"] != DBNull.Value ? reader["DiaSemana"].ToString() : string.Empty,
                                HoraInicio = reader["HoraInicio"] != DBNull.Value ? TimeSpan.Parse(reader["HoraFin"].ToString()) : new TimeSpan(),
                                HoraFin = reader["HoraFin"] != DBNull.Value ? TimeSpan.Parse(reader["HoraFin"].ToString()) : new TimeSpan(),
                                Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty,
                                Medico = reader["MedicoID"] != DBNull.Value ? new MapeoDatosMedico().ConsultarMedico(Convert.ToInt32(reader["MedicoID"]), "")[0] : null

                            };

                            response.Add(horarioLaboral);
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
