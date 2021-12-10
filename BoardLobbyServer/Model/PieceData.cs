﻿using BoardLobbyServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public class PieceData
    {
        public PieceColor pieceColor;
        public int pieceID;
        public int fieldID;
        public bool isInPlay;
        public bool isDone;

        public PieceData(PieceColor pc, int pID, int fID, bool inPlay, bool Done)
        {
            this.pieceColor = pc;
            this.pieceID = pID;
            this.fieldID = fID;
            this.isInPlay = inPlay;
            this.isDone = Done;
        }

    }


}
