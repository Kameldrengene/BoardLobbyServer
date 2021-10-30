﻿using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class SafeHomeField : Field
    {
        public SafeHomeField(Color color, Position position) : base(color, position)
        {
            this.Name = FieldType.SAFEHOME;
        }

        public override void onlanded(PlayerData playerData)
        {
            throw new NotImplementedException();
        }
    }
}
