using System;

namespace Mfm.Teg.EventsViewer.Domain.Models.Exceptions
{
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException(string message): base(message)
        {
        }
    }
}
