using System;
using System.Runtime.Serialization;

namespace TurtleChallenge.Core.Exceptions
{
    [Serializable]
    public class IndexOcupiedException : Exception
    {
        public IndexOcupiedException(string type, int x, int y) : base($"{type} is trying ot use a space already ocupied: x({x}) y({y})")
        {
        }

        protected IndexOcupiedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}