using System.Net;

namespace Dientecitos_BackEnd.Middleware.Exceptions.NotFound
{
    public class NotFoundException : Exception
    {

        public NotFoundException(string mensaje) : base(mensaje)
        {
        }
    }
}
