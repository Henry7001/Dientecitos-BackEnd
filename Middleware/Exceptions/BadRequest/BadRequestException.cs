using System.Net;

namespace Dientecitos_BackEnd.Middleware.Exceptions.BadRequest
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensaje) : base(mensaje)
        {
        }
    }
}
