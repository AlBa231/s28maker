using System;
using System.Runtime.Serialization;

namespace S28Maker.Services
{
    [Serializable]
    public class S28Exception : Exception
    {
        public S28Exception()
        {
        }

        public S28Exception(string message) : base(message)
        {
        }

        public S28Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected S28Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
