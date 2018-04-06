using System;

namespace PruebaUnoAgioGlobal.Core.Exceptions
{
    public class ConflictException : Exception
    {
        private DateTime _lastUpdate;

        #region Constructors

        public ConflictException(DateTime lastUpdate)
        {
            this._lastUpdate = lastUpdate;
        }

        public ConflictException(string message)
            : base(message)
        {
        }     
        public ConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
