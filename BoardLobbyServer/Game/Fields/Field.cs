using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public abstract class Field 
    {
        private FieldType _name;
        private Color _color;
        private int _position;
        private Field _next;
        

        protected Field(Color color, int position, Field next)
        {
            _color = color;
            _position = position;
            _next = next;
        }
        

        public Field Next { get => _next; set => _next = value; }
        public FieldType Name { get => _name; set => _name = value; }
        public Color Color { get => _color; set => _color = value; }
        public int Position { get => _position; set => _position = value; }

        public abstract void onlanded(Piece piece);
    }
}
