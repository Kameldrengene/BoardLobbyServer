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
                msg = base.Message;
                msg += "Class name: " + className + " resources not found";
                return msg;
            }
        }

    }
}
