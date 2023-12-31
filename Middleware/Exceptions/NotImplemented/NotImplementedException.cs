using Microsoft.AspNetCore.Http;
using System.Net;

namespace Dientecitos_BackEnd.Middleware.Exceptions.NotImplemented
{
    public class NotImplementedException : Exception
    {
        public NotImplementedException(string mensaje) : base(mensaje)
        {
        }
    }
}
