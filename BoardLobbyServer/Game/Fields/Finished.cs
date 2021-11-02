using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class Finished : Field
    {
        public Finished(Color color, int row, int column) : base(color, row, column)
        {
            this.Name = FieldType.FINISHED;
        }

        public override void onlanded(Piece piece)
        {
            throw new NotImplementedException();
        }
    }
}
