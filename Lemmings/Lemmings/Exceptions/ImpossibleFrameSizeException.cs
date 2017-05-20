using System;
using System.Runtime.Serialization;

namespace Lemmings.Exceptions
{
    [Serializable]
    public class ImpossibleFrameSizeException : Exception
    {
        #region Public Constructors

        public ImpossibleFrameSizeException() : base("The frame in the sprite is not calculatable.")
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected ImpossibleFrameSizeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImpossibleFrameSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}