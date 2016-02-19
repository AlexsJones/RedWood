using System;
using System.Runtime.Serialization;

namespace RedWood.Implementation.FileService
{
    [Serializable]
    public class FileException : Exception
    {
        public FileException()
        {
        }

        public FileException(string message)
            : base(message)
        {
        }

        public FileException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public FileException(string formatter, params string[] arguments)
            : base(string.Format(formatter, arguments))
        {
        }

        protected FileException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}