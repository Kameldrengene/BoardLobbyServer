﻿using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class NormalField : Field
    {
        public NormalField(Color color, int row, int column) : base(color, row, column)
        {
            this.Name = FieldType.NORMAL;
        }

        public override void onlanded(Piece piece)
        {
            throw new NotImplementedException();
        }
    }
}
