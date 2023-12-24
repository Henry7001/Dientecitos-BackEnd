using Newtonsoft.Json;

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
        public decimal CostoAsociado { get; set; }

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
                }
                disposed = true;
            }
        }

        ~TipoTratamiento()
        {
            Dispose(false);
        }
    }
}
