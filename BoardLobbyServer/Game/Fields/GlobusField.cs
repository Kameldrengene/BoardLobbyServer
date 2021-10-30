using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class GlobusField : Field
    {
        public GlobusField(Color color, Position position) : base(color, position)
        {
            this.Name = FieldType.GLOBUS;
        }

        public override void onlanded(PlayerData playerData)
        {
            throw new NotImplementedException();
        }
    }
}
