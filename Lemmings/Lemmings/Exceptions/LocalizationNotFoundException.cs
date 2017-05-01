using System;
using System.Runtime.Serialization;

namespace Lemmings.Exceptions
{
    public class LocalizationNotFoundException : Exception
    {
        #region Public Constructors

        public LocalizationNotFoundException(string culture) : base(string.Format("The localization for the given culture \"{0}\" could not be found.", culture))
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected LocalizationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors

        #region Private Constructors

        private LocalizationNotFoundException()
        {
        }

        private LocalizationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion Private Constructors
    }
}