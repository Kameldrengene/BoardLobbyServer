﻿using BoardLobbyServer.Game.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game
{
    public class Board
    {
        private List<Field>[] fieldList = { new List<Field>(), new List<Field>(), new List<Field>(), new List<Field>() };
        private Dictionary<PieceColor,Field> startingFields = new Dictionary<PieceColor, Field> { };
        private Dictionary<PieceColor,Field> finishFields = new Dictionary<PieceColor, Field> { };
        public void createBoard()
        {
            MakeFields();
           
        }

        public Field getStart(PieceColor pieceColor)
        {
            return fieldList[(int)pieceColor][16];
        }

        public Field getBank(PieceColor pieceColor)
        {
            return fieldList[(int)pieceColor][10];
        }

        private void AssignNextFields()
        {
            for (int i = 0; i < fieldList.Length; i++)
            {
                for (int j = 0; j < fieldList[i].Count; j++)
                {
                    if (j < 5)
                    {
                        fieldList[i][j].nextField = fieldList[i][j + 1];
                    }
                    else if (j == 5)
                    {
                        fieldList[i][j].nextField = fieldList[i][11];
                    }
                    else if (j == 11)
                    {
                        fieldList[i][j].nextField = fieldList[i][17];
                    }
                    else if (12 < j)
                    {
                        fieldList[i][j].nextField = fieldList[i][j - 1];
                    }
                    else if (6 < j && j < 11) //Middle fields
                    {
                        fieldList[i][j].nextField = fieldList[i][j - 1];
                    }

                    else if (j == 6)
                    {
                        fieldList[i][j].nextField = finishFields[(PieceColor)i];
                    }
                }

                finishFields[(PieceColor)i].nextField = fieldList[i][10];
            }


        }

        private void MakeFields()
        {

            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int j = 0; j < 6; j++)
                    {

                        int pos = k * 6 + j;
                        Field field = null;
                        if (j == 3 && k == 0) //Globus
                        {
                            field = new GlobusField((PieceColor)i, pos);
                        }
                        else if (j == 4 && k == 2)//Starting field
                        {
                            field = new SafeHomeField((PieceColor)i, pos);
                            startingFields[(PieceColor)i] = field;
                        }
                        else if (j == 5 && k == 1) //Entrance
                        {
                            EntranceField eField = new EntranceField((PieceColor)i, pos);
                            eField.setBankField(getBank((PieceColor)i));
                            field = eField;
                        }
                        else if (j == 0 && k == 0) //Star
                        {
                            field = new StarField((PieceColor)i, pos);
                        }
                        else //Default
                        {
                            field = new NormalField((PieceColor)i, pos);
                        }
                        fieldList[i].Add(field);
                    }
                }

                Field finishField = new NormalField((PieceColor)i, 18*4+i); //finishfields
                finishFields[(PieceColor)i] = finishField;
            }
            AssignNextFields();
        }

    }
}
