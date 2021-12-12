using NUnit.Framework;
using BoardLobbyServer;
using BoardLobbyServer.Game;
using BoardLobbyServer.Game.Fields;

namespace TestProject1
{
    public class BoardTest
    {

        [Test]
        public void Test_TryToMove_WrongRoll()
        {
            // Arrange
            Board board = new Board();
          
            try
            {
                //Act
                board.tryToMove(PieceColor.yellow, 2, 7);
                Assert.Fail();
            }catch (System.ArgumentOutOfRangeException e)
            {
                //Assert
                Assert.IsTrue(e.Message.Contains(Board.Rolled_More_Message));
            }

        }



        [Test]
        public void Test_TryToMove_onHomeField_CorrectPiece()
        {
            //Arrange
            Board board = new Board();

            var choice = 2;
            var roll = 6;
            var colorVal = PieceColor.yellow;
            ;

            // Act
            board.tryToMove(colorVal, choice, roll);
            
            // Assert
            Assert.AreEqual(choice, board.PieceList[(int)PieceColor.yellow][0].pieceID);

        }




    }

    public class FieldTest
    {

        // tests if the piece lands correctly for the first time
        [Test]
        public void TestField_onLand_FirstTime_Piece()
        {
            Piece piece = new YellowPiece(1);
            Field field = new SafeHomeField(PieceColor.yellow,16,null);
            
            field.OnLand(piece);

            Assert.AreEqual(piece,field.getPieces()[0]);
         

        }


        //checks if the the previous piece is removed if new piece is of fields color.
        [Test]
        public void TestField_onLand_SecondTime_Piece()
        {
            Piece pieceyellow = new YellowPiece(1);
            Piece pieceblue = new BluePiece(2);
           
            Board board = new Board();
            Field field = new SafeHomeField(PieceColor.yellow, 16, board);

            field.OnLand(pieceblue);
            field.OnLand(pieceyellow);

            Assert.AreEqual(pieceyellow, field.getPieces()[0]);
            Assert.AreEqual(1, field.getPieces().Count);
            
        }


        // checks Star fields on land and on jump methods, two scenarios
        [Test]
        public void TestField_OnLand_handleJump()
        {

            Field starField = new StarField(PieceColor.red, 22, new Board());
            Piece pieceyellow = new YellowPiece(1);
            Piece pieceblue = new BluePiece(2);
            starField.nextField = new StarField(PieceColor.blue,48 , new Board());

            // first test if blue piece lands correctly on empty star field
            starField.OnLand(pieceblue);
            Assert.AreEqual(pieceblue, starField.nextField.getPieces()[0]);

            // second test if yellow piece lands correctly and blue peace gets removed
            starField.OnLand(pieceyellow);
            Assert.AreEqual(pieceyellow, starField.nextField.getPieces()[0]);



        }
    }
}