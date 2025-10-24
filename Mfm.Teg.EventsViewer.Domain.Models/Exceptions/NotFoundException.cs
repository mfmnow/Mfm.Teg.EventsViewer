using System;

namespace Mfm.Teg.EventsViewer.Domain.Models.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message): base(message)
        {
        }
    }
}
