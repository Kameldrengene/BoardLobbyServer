using System;

namespace BoardLobbyServer.Exceptions
{
    public class InputIsNotValidException : Exception
    {
        private string msg;
        private string className;

        public InputIsNotValidException(string classname)
        {
            this.className = classname;
        }
        
        public override string Message
        {
            get
            {
                msg = "Input is not valid";
                return msg;
            }
        }
    }
}
