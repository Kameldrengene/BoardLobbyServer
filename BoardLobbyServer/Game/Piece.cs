using BoardLobbyServer.Game.Fields;
using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game
{
    public class Piece
    {
        public Piece(Color colorType, Player owner, Field location)
        {
            ColorType = colorType;
            Owner = owner;
            Location = location;
        }

        public Color ColorType { get; set; }
        public Player Owner { get; set; }
        public Field Location { get; set; }

    }
}
