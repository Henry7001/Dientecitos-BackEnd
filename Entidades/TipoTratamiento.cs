using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Utils;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Dientecitos_BackEnd.Entidades
{
    public class TipoTratamiento : IDisposable
    {

        [JsonProperty("tipoTratamientoID")]
        public int TipoTratamientoID { get; set; }


        [JsonProperty("nombreTratamiento")]
        public string? NombreTratamiento { get; set; }


        [JsonProperty("descripcion")]
        public string? Descripcion { get; set; }


        [JsonProperty("costoAsociado")]
        public decimal? CostoAsociado { get; set; }


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
                    TipoTratamientoID = 0;
                    NombreTratamiento = null;
                    Descripcion = null;
                    CostoAsociado = 0;
                    Estado = null;
                }
                disposed = true;
            }
        }

        ~TipoTratamiento()
        {
            Dispose(false);
        }
    }



    public class TipoTratamientoDAO : IDisposable
    {


        [JsonProperty("nombreTratamiento")]
        [StringLength(100, ErrorMessage = "El nombre del tratamiento debe contener máximo 100 caracteres.")]
        public string? NombreTratamiento { get; set; }


        [JsonProperty("descripcion")]
        [StringLength(500, ErrorMessage = "La descripcion del tratamiento debe contener máximo 500 caracteres.")]
        public string? Descripcion { get; set; }


        [JsonProperty("costoAsociado")]
        public decimal? CostoAsociado { get; set; }


        [JsonProperty("estado")]
        public string? Estado { get; set; }


        private bool disposed = false;


        public void ValidarIngreso()
        {

            if (string.IsNullOrWhiteSpace(NombreTratamiento))
            {
                throw new InvalidFieldException("El nombre del tratamiento es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(Descripcion))
            {
                throw new InvalidFieldException("La Descripcion del tratamiento es obligatorio.");
            }

            if (CostoAsociado == null || CostoAsociado <= 0)
            {
                throw new InvalidFieldException("El Costo Asociado del tratamiento es obligatorio.");
            }

        }


        public void ValidarActualizacion()
        {

            if (CostoAsociado != null && CostoAsociado <= 0)
            {
                throw new InvalidFieldException("El Costo Asociado del tratamiento debe ser mayor a 0.");
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
                    NombreTratamiento = null;
                    Descripcion = null;
                    CostoAsociado = 0;
                    Estado = null;
                }
                disposed = true;
            }
        }

        ~TipoTratamientoDAO()
        {
            Dispose(false);
        }
    }
}
