using System;
using System.Runtime.Serialization;

namespace IEcommerceAPI.Middleware
{
    [Serializable]
    internal class MyUnauthorizedException : Exception
    {
        private const string message = "Não foi possível atender a solicitação. Um ou mais dados da requisição já existem cadastrados.";

        public MyUnauthorizedException()
        {
        }

        public MyUnauthorizedException(string message) : base(message)
        {
        }

        public MyUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}