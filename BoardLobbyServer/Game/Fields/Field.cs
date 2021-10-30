﻿using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public abstract class Field 
    {
        private FieldType name;
        private Color color;
        private Position position;

        protected Field(Color color, Position position)
        {
            this.color = color;
            this.position = position;
        }
        public FieldType Name {
            get { return this.name; }
            set { this.name = value; }
        }
        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
       
        public abstract void onlanded(PlayerData playerData);
    }
}
