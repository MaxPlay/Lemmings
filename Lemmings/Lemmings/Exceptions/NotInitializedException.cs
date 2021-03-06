﻿using System;
using System.Runtime.Serialization;

namespace Lemmings.Exceptions
{
    [Serializable]
    internal class NotInitializedException : Exception
    {
        #region Public Constructors

        public NotInitializedException(object obj) : base(string.Format("Object {0} was not initialized when method was called.", obj))
        {
        }

        #endregion Public Constructors

        #region Private Constructors

        private NotInitializedException(string message) : base(message)
        {
        }

        private NotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private NotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Private Constructors
    }
}