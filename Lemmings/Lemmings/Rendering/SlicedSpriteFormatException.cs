using System;
using System.Runtime.Serialization;

namespace Lemmings.Rendering
{
    [Serializable]
    public class SlicedSpriteFormatException : Exception
    {
        public SlicedSpriteFormatException(string orientation) : base(string.Format("{0} impossible format in SlicedSprite."))
        {
        }

        protected SlicedSpriteFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SlicedSpriteFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}