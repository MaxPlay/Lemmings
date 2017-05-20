using System;
using System.Runtime.Serialization;

namespace Lemmings.Exceptions
{
    [Serializable]
    public class SlicedSpriteFormatException : Exception
    {
        #region Public Constructors

        public SlicedSpriteFormatException(string orientation) : base(string.Format("{0} impossible format in SlicedSprite."))
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected SlicedSpriteFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SlicedSpriteFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}