namespace BoardLobbyServer.Controllers
{
    public class UserCredentials
    {
        private string _password;
        private string _username;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
    }
}