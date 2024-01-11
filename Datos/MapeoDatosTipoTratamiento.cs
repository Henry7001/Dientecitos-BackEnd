using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dientecitos_BackEnd.Datos
{
    public class MapeoDatosTipoTratamiento
    {

        readonly SqlConnection connection = new(Environment.GetEnvironmentVariable(StringHandler.Database_String));

        public TipoTratamiento GrabarTipoTratamiento(TipoTratamientoDAO request, int id)
        {
            TipoTratamiento response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_TIPO_TRATAMIENTO, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", id == 0 ? "Insertar" : "Actualizar");
                    command.Parameters.AddWithValue("@TipoTratamientoId", id);
                    command.Parameters.AddWithValue("@NombreTratamiento", request.NombreTratamiento);
                    command.Parameters.AddWithValue("@Descripcion", request.Descripcion);
                    command.Parameters.AddWithValue("@CostoAsociado", request.CostoAsociado);
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

                            response.TipoTratamientoID = reader["TipoTratamientoID"] != DBNull.Value ? Convert.ToInt32(reader["TipoTratamientoID"]) : 0;
                            response.NombreTratamiento = reader["NombreTratamiento"] != DBNull.Value ? reader["NombreTratamiento"].ToString() : string.Empty;
                            response.Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : string.Empty;
                            response.CostoAsociado = reader["CostoAsociado"] != DBNull.Value ? Convert.ToDecimal(reader["CostoAsociado"]) : 0;
                            response.Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty;
                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new Exception("No se pudo grabar al usuario.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return response;
        }



        public MessageResponse EliminarTipoTratamiento(int id)
        {
            MessageResponse response = new();

            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_TIPO_TRATAMIENTO, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", "Eliminar");
                    command.Parameters.AddWithValue("@TipoTratamientoId", id);

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
                        throw new Exception("No se pudo grabar al usuario.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return response;
        }



        public List<TipoTratamiento> ConsultarTipoTratamiento(int id)
        {
            List<TipoTratamiento> response = new();
            using (connection)
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new(StringHandler.SP_TIPO_TRATAMIENTO, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Accion", id == 0 ? "ConsultarTodos" : "ConsultarPorID");
                    command.Parameters.AddWithValue("@TipoTratamientoId", id);

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount == 1 && reader["Mensaje"] != DBNull.Value)
                            {
                                throw new BadRequestException(reader["Mensaje"].ToString() ?? "");
                            }

                            TipoTratamiento tipoTratamiento = new TipoTratamiento
                            {
                                TipoTratamientoID = reader["TipoTratamientoID"] != DBNull.Value ? Convert.ToInt32(reader["TipoTratamientoID"]) : 0,
                                NombreTratamiento = reader["NombreTratamiento"] != DBNull.Value ? reader["NombreTratamiento"].ToString() : string.Empty,
                                Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : string.Empty,
                                CostoAsociado = reader["CostoAsociado"] != DBNull.Value ? Convert.ToDecimal(reader["CostoAsociado"]) : 0,
                                Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty
                            };

                            response.Add(tipoTratamiento);
                        }
                    }
                    else
                    {
                        connection.Close();
                        throw new Exception("No se pudo grabar al usuario.");
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
