using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class GlobusField : Field
    {
        public GlobusField(Color color, int row, int column) : base(color, row, column)
        {
            this.Name = FieldType.GLOBUS;
        }

        public override void onlanded(Piece piece)
        {
            throw new NotImplementedException();
        }
    }
}
