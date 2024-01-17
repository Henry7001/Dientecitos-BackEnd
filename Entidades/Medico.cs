using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Dientecitos_BackEnd.Entidades
{
    public class Medico : IDisposable
    {

        [JsonProperty("medicoID")]
        public int MedicoID { get; set; }


        [JsonProperty("especialidad")]
        public string Especialidad { get; set; }


        [JsonProperty("usuario")]
        public virtual Usuario? Usuario { get; set; }


        [JsonProperty("estado")]
        public string Estado { get; set; }


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
                    MedicoID = 0;
                    Usuario = null;
                    Especialidad = null;
                    Estado = null;
                }
                disposed = true;
            }
        }


        ~Medico()
        {
            Dispose(false);
        }


    }


    public class MedicoDAO : IDisposable
    {

        [JsonProperty("especialidad")]
        [StringLength(50, ErrorMessage = "La especialidad debe contener máximo 50 caracteres.")]
        public string? Especialidad { get; set; }


        [JsonProperty("usuarioID")]
        public int? UsuarioID { get; set; }


        [JsonProperty("estado")]
        public string? Estado { get; set; }


        private bool disposed = false;



        public void ValidarIngreso()
        {

            if (string.IsNullOrWhiteSpace(Especialidad))
            {
                throw new InvalidFieldException("La especialidad es obligatoria.");
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
                    Especialidad = null;
                    Estado = null;
                }
                disposed = true;
            }
        }


        ~MedicoDAO()
        {
            Dispose(false);
        }


    }

}
