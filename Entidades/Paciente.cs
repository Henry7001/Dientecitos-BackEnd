using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Dientecitos_BackEnd.Entidades
{
    public class Paciente : IDisposable
    {
        [JsonProperty("pacienteID")]
        public int PacienteID { get; set; }
        [JsonProperty("usuarioID")]
        public Usuario Usuario { get; set; }
        [JsonProperty("direccion")]
        public string? Direccion { get; set; }
        [JsonProperty("numeroContacto")]
        public string? NumeroContacto { get; set; }
        [JsonProperty("estado")]
        public string? Estado { get; set; }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    PacienteID = 0;
                    Usuario = null;
                    Direccion = null;
                    NumeroContacto = null;
                }
                disposed = true;
            }
        }

        ~Paciente()
        {
            Dispose(false);
        }
    }

    public class PacienteDAO : IDisposable
    {

        [JsonProperty("direccion")]
        [StringLength(100, ErrorMessage = "La direccion debe contener máximo 100 caracteres.")]
        public string? Direccion { get; set; }

        [JsonProperty("NumeroContacto")]
        [StringLength(15, ErrorMessage = "El número de contacto debe contener máximo 15 caracteres.")]
        public string? NumeroContacto { get; set; }

        [JsonProperty("usuarioID")]
        public int? UsuarioID { get; set; }

        [JsonProperty("estado")]
        public string? Estado { get; set; }


        private bool disposed = false;



        public void ValidarIngreso()
        {

            if (string.IsNullOrWhiteSpace(Direccion))
            {
                throw new InvalidFieldException("La especialidad es obligatoria.");
            }

            if (string.IsNullOrWhiteSpace(NumeroContacto))
            {
                throw new InvalidFieldException("El número de contacto es obligatoria.");
            }

            if (UsuarioID == null || UsuarioID <= 0)
            {
                throw new InvalidFieldException("El id del usuario asociado es obligatorio.");
            }

        }



        public void ValidarActualizacion()
        {

            if (UsuarioID != null && UsuarioID <= 0)
            {
                throw new InvalidFieldException("El ID del usuario asociado debe ser mayor a 0.");
            }

            Utils.Utils.ValidarEstado(Estado ?? "");

        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    UsuarioID = null;
                    Direccion = null;
                    NumeroContacto = null;
                    Estado = null;
                }
                disposed = true;
            }
        }


        ~PacienteDAO()
        {
            Dispose(false);
        }


    }

}
