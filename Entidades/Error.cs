namespace Dientecitos_BackEnd.Entidades
{
    public class Error : IDisposable
    {
        public string? error;
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
                    error = null;
                }
                disposed = true;
            }
        }

        ~Error()
        {
            Dispose(false);
        }

    }
}
