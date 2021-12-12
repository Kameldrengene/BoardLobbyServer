using NUnit.Framework;
using BoardLobbyServer.Game;
using BoardLobbyServer.Game.Fields;
using BoardLobbyServer.Model;
using Moq;
using BoardLobbyServer.Services;
using MongoDB.Driver;

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
            Assert.AreEqual(choice+1, board.PieceList[(int)PieceColor.yellow][0].pieceID);

        }



        [Test]
        public void Test_setNewPieceOnBoard()
        {
            Board board = new Board();
            Piece piece = new YellowPiece(2);

            board.setNewPieceOnBoard((int)PieceColor.yellow,piece.pieceID);
            
            Assert.AreEqual(piece.pieceID,board.PieceList[(int)PieceColor.yellow][0].pieceID);
        }




    }

    public class FieldTest
    {

        // tests if the piece lands correctly for the first time
        [Test]
        public void TestField_onLand_FirstTime_Piece()
        {
            Piece piece = new YellowPiece(1);
            Field field = new SafeHomeField(PieceColor.yellow, 16, null);

            field.OnLand(piece);

            Assert.AreEqual(piece, field.getPieces()[0]);


        }


        //checks if the the previous piece is removed from safehomefield
        //if new piece is of fields color.
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
            starField.nextField = new StarField(PieceColor.blue, 48, new Board());

            // first test if blue piece lands correctly on empty star field
            starField.OnLand(pieceblue);
            Assert.AreEqual(pieceblue, starField.nextField.getPieces()[0]);

            // second test if yellow piece lands correctly and blue peace gets removed
            starField.OnLand(pieceyellow);
            Assert.AreEqual(pieceyellow, starField.nextField.getPieces()[0]);



        }

    }
    public class Test_MongoPlayerService
    {

        private Mock<IBoardServerDatabaseSettings> _mockSettings;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;
        [Test]
        public void Test_MongoDBPlayerservice_playerExists()
        {
            _mockSettings = new Mock<IBoardServerDatabaseSettings>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
            var settings = new BoardServerDatabaseSettings()
            {
                ConnectionString = "mongodb://root:kamel1234@localhost:27017/",
                DatabaseName = "BoardServerDB",
                AdminsCollectionName = "Admins",
                PlayersCollectionName = "Players"
            };

            
            _mockSettings.Setup(s => s.ConnectionString).Returns(settings.ConnectionString);
            _mockSettings.Setup(s => s.DatabaseName).Returns(settings.DatabaseName);
            _mockSettings.Setup(s => s.AdminsCollectionName).Returns(settings.AdminsCollectionName);
            _mockSettings.Setup(s => s.PlayersCollectionName).Returns(settings.PlayersCollectionName);
            _mockClient.Setup(c => c
            .GetDatabase(_mockSettings.Object.DatabaseName, null))
                .Returns(_mockDB.Object);

             
            var context = new PlayerService(_mockSettings.Object);
            PlayerData player = new PlayerData();
            player.Name = "kamel";
            player.password = "1234";

            //Act
            var myCollection = context.Get();
            var newPlayer = myCollection.Find(e => e.Name == player.Name);

            //Assert 
            Assert.Null(newPlayer);

        }

        [Test]
        public void Test_MongoDBPlayerservice_playerCreated()
        {
            _mockSettings = new Mock<IBoardServerDatabaseSettings>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
            var settings = new BoardServerDatabaseSettings()
            {
                ConnectionString = "mongodb://root:kamel1234@localhost:27017/",
                DatabaseName = "BoardServerDB",
                AdminsCollectionName = "Admins",
                PlayersCollectionName = "Players"
            };


            _mockSettings.Setup(s => s.ConnectionString).Returns(settings.ConnectionString);
            _mockSettings.Setup(s => s.DatabaseName).Returns(settings.DatabaseName);
            _mockSettings.Setup(s => s.AdminsCollectionName).Returns(settings.AdminsCollectionName);
            _mockSettings.Setup(s => s.PlayersCollectionName).Returns(settings.PlayersCollectionName);
            _mockClient.Setup(c => c
            .GetDatabase(_mockSettings.Object.DatabaseName, null))
                .Returns(_mockDB.Object);

            
            var context = new PlayerService(_mockSettings.Object);
            PlayerData player = new PlayerData();
            player.Name = "cool";
            player.password = "1234";

            //Act 
            context.Create(player);
            var myCollection = context.Get();
            var newPlayer = myCollection.Find(e => e.Name == player.Name);
           
            //Assert 
            Assert.NotNull(newPlayer);
            
        }

        [Test]
        public void Test_PlayerVerify()
        {
            _mockSettings = new Mock<IBoardServerDatabaseSettings>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
            var settings = new BoardServerDatabaseSettings()
            {
                ConnectionString = "mongodb://root:kamel1234@localhost:27017/",
                DatabaseName = "BoardServerDB",
                AdminsCollectionName = "Admins",
                PlayersCollectionName = "Players"
            };


            _mockSettings.Setup(s => s.ConnectionString).Returns(settings.ConnectionString);
            _mockSettings.Setup(s => s.DatabaseName).Returns(settings.DatabaseName);
            _mockSettings.Setup(s => s.AdminsCollectionName).Returns(settings.AdminsCollectionName);
            _mockSettings.Setup(s => s.PlayersCollectionName).Returns(settings.PlayersCollectionName);
            _mockClient.Setup(c => c
            .GetDatabase(_mockSettings.Object.DatabaseName, null))
                .Returns(_mockDB.Object);


            var context = new PlayerService(_mockSettings.Object);
            PlayerData player = new PlayerData();
            player.Name = "cool";
            player.password = "1234";

            //Act 
            var verified = context.Verify(player.Name, player.password);
            //Assert 
            Assert.NotNull(verified);

        }
    }
    

    public class Test_MongoAdminService
    {
        private Mock<IBoardServerDatabaseSettings> _mockSettings;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;

        [Test]
        public void Test_MongoDBAdminservice_adminExists()
        {
            _mockSettings = new Mock<IBoardServerDatabaseSettings>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
            var settings = new BoardServerDatabaseSettings()
            {
                ConnectionString = "mongodb://root:kamel1234@localhost:27017/",
                DatabaseName = "BoardServerDB",
                AdminsCollectionName = "Admins",
                PlayersCollectionName = "Players"
            };


            _mockSettings.Setup(s => s.ConnectionString).Returns(settings.ConnectionString);
            _mockSettings.Setup(s => s.DatabaseName).Returns(settings.DatabaseName);
            _mockSettings.Setup(s => s.AdminsCollectionName).Returns(settings.AdminsCollectionName);
            _mockSettings.Setup(s => s.PlayersCollectionName).Returns(settings.PlayersCollectionName);
            _mockClient.Setup(c => c
            .GetDatabase(_mockSettings.Object.DatabaseName, null))
                .Returns(_mockDB.Object);


            var context = new AdminService(_mockSettings.Object);
            Admin admin = new Admin();
            admin.Name = "coolboy";
            admin.Password = "1234";

            //Act
            var myCollection = context.Get();
            var newAdmin = myCollection.Find(e => e.Name == admin.Name);

            //Assert 
            Assert.Null(newAdmin);

        }


        [Test]
        public void Test_MongoDBAdminservice_adminCreated()
        {
            _mockSettings = new Mock<IBoardServerDatabaseSettings>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
            var settings = new BoardServerDatabaseSettings()
            {
                ConnectionString = "mongodb://root:kamel1234@localhost:27017/",
                DatabaseName = "BoardServerDB",
                AdminsCollectionName = "Admins",
                PlayersCollectionName = "Players"
            };


            _mockSettings.Setup(s => s.ConnectionString).Returns(settings.ConnectionString);
            _mockSettings.Setup(s => s.DatabaseName).Returns(settings.DatabaseName);
            _mockSettings.Setup(s => s.AdminsCollectionName).Returns(settings.AdminsCollectionName);
            _mockSettings.Setup(s => s.PlayersCollectionName).Returns(settings.PlayersCollectionName);
            _mockClient.Setup(c => c
            .GetDatabase(_mockSettings.Object.DatabaseName, null))
                .Returns(_mockDB.Object);


            var context = new AdminService(_mockSettings.Object);
            Admin admin = new Admin();
            admin.Name = "kamel";
            admin.Password = "1234";

            //Act 
            context.Create(admin);
            var myCollection = context.Get();
            var newAdmin = myCollection.Find(e => e.Name == admin.Name);

            //Assert 
            Assert.NotNull(newAdmin);

        }



        [Test]
        public void Test_MongoDBAdminservice_adminVerify()
        {
            _mockSettings = new Mock<IBoardServerDatabaseSettings>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
            var settings = new BoardServerDatabaseSettings()
            {
                ConnectionString = "mongodb://root:kamel1234@localhost:27017/",
                DatabaseName = "BoardServerDB",
                AdminsCollectionName = "Admins",
                PlayersCollectionName = "Players"
            };


            _mockSettings.Setup(s => s.ConnectionString).Returns(settings.ConnectionString);
            _mockSettings.Setup(s => s.DatabaseName).Returns(settings.DatabaseName);
            _mockSettings.Setup(s => s.AdminsCollectionName).Returns(settings.AdminsCollectionName);
            _mockSettings.Setup(s => s.PlayersCollectionName).Returns(settings.PlayersCollectionName);
            _mockClient.Setup(c => c
            .GetDatabase(_mockSettings.Object.DatabaseName, null))
                .Returns(_mockDB.Object);


            var context = new AdminService(_mockSettings.Object);
            Admin admin = new Admin();
            admin.Name = "kamel";
            admin.Password = "1234";

            //Act 
            var verified = context.Verify(admin.Name, admin.Password);
            //Assert 
            Assert.NotNull(verified);

        }



    }

}