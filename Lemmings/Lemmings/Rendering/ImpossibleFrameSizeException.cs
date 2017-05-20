using System;
using System.Runtime.Serialization;

namespace Lemmings.Rendering
{
    [Serializable]
    public class ImpossibleFrameSizeException : Exception
    {
        public ImpossibleFrameSizeException() : base("The frame in the sprite is not calculatable.")
        {
        }

        protected ImpossibleFrameSizeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImpossibleFrameSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}