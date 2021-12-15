using System;

namespace BoardLobbyServer.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        private string msg;
        private string className;
        public ResourceNotFoundException(string classname)
        {
            this.className = classname;
        }
        public override string Message
        {
            get
            {
                msg = "We could not find what you are looking for.";
                return msg;
            }
        }

    }
}
