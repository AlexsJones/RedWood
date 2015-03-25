using System;
using System.Runtime.Serialization;

namespace RedWood.Pages.Implementation.Page
{
    [Serializable]
    public class PageException : Exception
    {
        public PageException()
        {
        }

        public PageException(string message)
            : base(message)
        {
        }

        public PageException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public PageException(string formatter, params string[] arguments)
            : base(string.Format(formatter, arguments))
        {
        }

        protected PageException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
