using System;
using System.Runtime.Serialization;

namespace SprintCinco.Middleware
{
    [Serializable]
    internal class NullException : Exception
    {
        private const string message = "Não foi possível atender a solicitação. Um ou mais dados da requisição não foram encontrados.";

        public NullException(string message) : base(message)
        {
        }

        public NullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}