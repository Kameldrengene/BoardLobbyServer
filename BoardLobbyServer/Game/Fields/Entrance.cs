using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class Entrance : Field
    {
        public Entrance(Color color, int position, Field next) : base(color, position, next)
        {
            this.Name = FieldType.ENTRANCE;
        }

        public override void onlanded(Piece piece)
        {
            piece.Location = this;
        }
    }
}
