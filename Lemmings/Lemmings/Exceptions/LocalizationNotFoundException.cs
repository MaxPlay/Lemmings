using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lemmings.Exceptions
{
    public class LocalizationNotFoundException : Exception
    {
        private LocalizationNotFoundException()
        {
        }

        public LocalizationNotFoundException(string culture) : base(string.Format("The localization for the given culture \"{0}\" could not be found.", culture))
        {
        }

        private LocalizationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LocalizationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
