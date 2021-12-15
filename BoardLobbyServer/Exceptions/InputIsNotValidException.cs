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
                msg = base.Message;
                msg += "Class name: " + className;
                return msg;
            }
        }
    }
}
