using BoardLobbyServer.Game.Fields;
using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game
{
    public class Board
    {
        public const string Rolled_More_Message = "Error: Wrong piece chosen. either already done or trying to get new piece out without rolling a 6";
        private List<Field>[] fieldList = { new List<Field>(), new List<Field>(), new List<Field>(), new List<Field>() };
        private List<Piece>[] pieceList = { new List<Piece>(), new List<Piece>(), new List<Piece>(), new List<Piece>() };
        private Dictionary<PieceColor, Field> startingFields = new Dictionary<PieceColor, Field> { };
        private Dictionary<PieceColor, Field> finishFields = new Dictionary<PieceColor, Field> { };

        private List<Piece>[] piecesHome = { new List<Piece>(), new List<Piece>(), new List<Piece>(), new List<Piece>() };
        public List<Piece>[] piecesDone = { new List<Piece>(), new List<Piece>(), new List<Piece>(), new List<Piece>() };


        public void createBoard()
        {
            MakeFields();
            AssignNextFields();
            initPieces();
        }

        public Board()
        {
            createBoard();
        }

        public Board(BoardData data)
        {
            MakeFields();
            AssignNextFields();
            List<PieceData> dataPiecesList = JsonSerializer.Deserialize<List<PieceData>>(data.Pieces);
            Console.WriteLine("dataPieceList len: " + dataPiecesList.Count);
            foreach (PieceData pd in dataPiecesList)
            {
                Piece piece = new YellowPiece(-1);
                switch (pd.PieceColor)
                {
                    case PieceColor.yellow:
                        piece = new YellowPiece(pd.PieceID);
                        break;
                    case PieceColor.green:
                        piece = new GreenPiece(pd.PieceID);
                        break;
                    case PieceColor.red:
                        piece = new RedPiece(pd.PieceID);
                        break;
                    case PieceColor.blue:
                        piece = new BluePiece(pd.PieceID);
                        break;
                }


                if (!pd.IsDone && !pd.IsInPlay) //still home piece
                {
                    piecesHome[(int)pd.PieceColor].Add(piece);
                }
                else if (pd.IsInPlay && !pd.IsDone) // on the board piece
                {
                    pieceList[(int)pd.PieceColor].Add(piece);

                    // findind correct field to stand on
                    Field field = fieldList[pd.FieldQuadrant][pd.FieldID];
                    field.getPieces().Add(piece);
                    piece.field = field;
                }
                else //finished piece
                {
                    piecesDone[(int)pd.PieceColor].Add(piece);
                }
            }
        }

        public void finalizePiece(Piece piece)
        {
            pieceList[(int)piece.getPieceColor()].Remove(piece);
            piecesDone[(int)piece.getPieceColor()].Add(piece);
        }

        public List<PieceData> getColorPieceData(PieceColor color)
        {
            List<PieceData> data = new List<PieceData>();

            Console.WriteLine("len home, len list, len done " + this.piecesHome[(int)color].Count + " " + this.pieceList[(int)color].Count + " " + this.piecesDone[(int)color].Count);

            foreach(Piece p in this.piecesHome[(int)color])
            {
                data.Add(new PieceData(
                    color,
                    p.pieceID,
                    -1,
                    -1,
                    false,
                    false
                    ));
            }

            foreach(Piece p in this.pieceList[(int)color])
            {
                data.Add(new PieceData(
                    color,
                    p.pieceID,
                    p.field.getID(),
                    (int)p.field.getQuadrant(),
                    true,
                    false
                    ));
            }

            foreach(Piece p in this.piecesDone[(int)color])
            {
                data.Add(new PieceData(
                    color,
                    p.pieceID,
                    -1,
                    -1,
                    true,
                    true
                    ));
            }

            return data;
        }

        public void tryToMove(PieceColor color, int c, int roll) //Maybe return boolean? true if legal mode, false if not?
            //no, valid moves are checked in client. 
        {
            int choice = c + 1;
            Console.WriteLine("color: " + color.ToString() +", choice: " + choice + ", roll: " + roll);
            Piece piece = new YellowPiece(-1); //tmp piece for now
            Console.WriteLine("len of lin of pieces out: " + pieceList[(int)color].Count);
            foreach (Piece p in pieceList[(int)color])
            {
                if (p.pieceID == choice)
                {
                    Console.WriteLine("Piece found!");
                    piece = p;
                    break;
                }
            }

            if (piece.pieceID == -1) //Piece not found / not started yet
            {
                foreach (Piece p in piecesDone[(int)color])
                {
                    if (p.pieceID == choice)
                    {
                        piece = p;
                        break;
                    }
                }
                if (piece.pieceID == -1 && piecesHome[(int)color].Count > 0 && roll == 6) //Not found in done pieces and more pieces at home
                { // start new piece
                    setNewPieceOnBoard(color, choice);
                }
                else
                {
                    //TODO make error?
                    Console.Write("Error: Wrong piece chosen. either already done or trying to get new piece out without rolling a 6");
                    throw new ArgumentOutOfRangeException("roll",roll,Rolled_More_Message);
                }
            }

            else //Piece found on board
            {
                movePiece(piece, roll);
            }

            
        }

        private void movePiece(Piece piece, int roll)
        {

            Field currField = piece.field;
            currField.OnMoveOut(piece);
            int x = roll;
            while (x > 0)
            {
                currField = currField.NextField(piece);
                x -= 1;
            }

            currField.OnLand(piece);

        }

        private void initPieces()
        {
            for(int i = 0; i < 4; i++)
            {
                piecesHome[0].Add(new YellowPiece(i + 1));
                piecesHome[1].Add(new GreenPiece(i + 1));
                piecesHome[2].Add(new RedPiece(i + 1));
                piecesHome[3].Add(new BluePiece(i + 1));
            }
        }


        public Field getStart(PieceColor pieceColor)
        {
            return fieldList[(int)pieceColor][16];
        }

        public Field getBank(PieceColor pieceColor)
        {
            return fieldList[(int)pieceColor][10];
        }

        public void sendPieceHome(Piece piece)
        {
            pieceList[(int)piece.getPieceColor()].Remove(piece);
            piece.field.getPieces().Remove(piece);
            piecesHome[(int)piece.getPieceColor()].Add(piece);
        }

        public void setNewPieceOnBoard(PieceColor pieceColor, int choice)
        {
            Piece piece = new YellowPiece(-1);

            foreach(Piece p in piecesHome[(int)pieceColor])
            {
                Console.WriteLine("Color: "+ pieceColor.ToString() +", Choice:" + choice + ", pieceID: " + p.pieceID);
                if (p.pieceID == choice)
                {
                    piece = p;
                    break;
                }
            }

            if (piece.pieceID == -1) {Console.Write("Error: setNewPieceOnBoard did not find piece"); return; } //Error

            piecesHome[(int)pieceColor].Remove(piece);
            pieceList[(int)pieceColor].Add(piece);
            Field startingField = getStart(pieceColor);

            startingField.OnLand(piece);
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
                    else if (6 < j && j < 11) //Middle Fields or Bank Fields
                    {
                        fieldList[i][j].nextField = fieldList[i][j - 1];
                    }

                    else if (j == 6)
                    {
                        fieldList[i][j].nextField = finishFields[(PieceColor)i];
                    }

                    else if (j == 12)
                    {
                        fieldList[i][j].nextField = fieldList[((i + 1) % 4)][0];
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
                            field = new GlobusField((PieceColor)i, pos, this);
                        }
                        else if (j == 4 && k == 2)//Starting field
                        {
                            field = new SafeHomeField((PieceColor)i, pos, this);
                            startingFields[(PieceColor)i] = field;
                        }
                        else if (j == 5 && k == 1) //Entrance
                        {
                            EntranceField eField = new EntranceField((PieceColor)i, pos, this);
                            eField.setBankField(getBank((PieceColor)i));
                            field = eField;
                        }
                        else if (j == 0 && k == 0) //Star
                        {
                            field = new StarField((PieceColor)i, pos, this);
                        }
                        else //Default
                        {
                            field = new NormalField((PieceColor)i, pos, this);
                        }
                        fieldList[i].Add(field);
                    }
                }

                Field finishField = new FinishField((PieceColor)i, -1, this); //finishfields
                finishFields[(PieceColor)i] = finishField;
            }
            
        }

        public bool isWon()
        {
            bool won = false;
            for (int i = 0; i < 4; i++) //Hardcoded for 4 players
            {
                if (this.piecesDone[i].Count == 4)
                {
                    won = true;
                    break;
                }
            }
            return won;
        }

        public List<Piece>[] PieceList   // property
        {
            get { return pieceList; }   // get method
        }

        public List<Piece>[] PieceHome
        {
            get { return piecesHome; }
        }

    }
}
