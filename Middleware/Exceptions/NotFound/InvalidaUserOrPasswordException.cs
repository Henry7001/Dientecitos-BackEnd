namespace Dientecitos_BackEnd.Middleware.Exceptions.NotFound
{
    public class InvalidaUserOrPasswordException : NotFoundException
    {
        public InvalidaUserOrPasswordException() : base("Usuario o clave incorrecta.")
        { }

        public InvalidaUserOrPasswordException(string mensaje) : base("Usuario o clave incorrecta. " + mensaje)
        { }
    }
}