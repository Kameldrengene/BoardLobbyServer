using BoardLobbyServer.Model;
using System;
using System.Text.RegularExpressions;

namespace BoardLobbyServer.Services
{
    public class Validator
    {
        public bool validateAdmin(Func<Admin,bool> validateEmail, Func<Admin, bool> validatePassword,Admin admin)
        {
            bool result = validateEmail(admin) && validatePassword(admin);
            return result;
        }
    }
}
