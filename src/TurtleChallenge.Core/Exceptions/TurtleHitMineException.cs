using System;
using System.Runtime.Serialization;

namespace TurtleChallenge.Core.Exceptions
{
    [Serializable]
    public class TurtleHitMineException : Exception
    {
        public TurtleHitMineException()
        {
        }

        protected TurtleHitMineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}