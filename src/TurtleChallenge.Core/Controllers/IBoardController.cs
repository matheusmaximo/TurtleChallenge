﻿using TurtleChallenge.Data.Reporter.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public interface IBoardController
    {
        void Reset();
        void MoveTurtle();
        void RotateTurtle();
        MovesResultPossibilities GetResult();
    }
}