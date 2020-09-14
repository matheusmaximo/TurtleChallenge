using System;
using System.Runtime.Serialization;

namespace TurtleChallenge.Core.Exceptions
{
    [Serializable]
    public class IndexOutOfBoardException : Exception
    {
        public IndexOutOfBoardException(string type, int x, int y) : base($"{type} is out of board: x({x}) y({y})")
        {
        }

        protected IndexOutOfBoardException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}