using BoardLobbyServer.Exceptions;
using BoardLobbyServer.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoardLobbyServer.Services
{
    public class AdminService
    {
        private readonly IMongoCollection<Admin> _admins;
        Validator _validator = new Validator();

        public AdminService(IBoardServerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _admins = database.GetCollection<Admin>(settings.AdminsCollectionName);
        }

        public List<Admin> Get() =>
            _admins.Find(admin => true).ToList();
        public Admin Get(string id)
        {
            Admin admin = _admins.Find<Admin>(admin => admin.Id == id).FirstOrDefault();
            if (admin == null)
            {
                throw new ResourceNotFoundException(this.ToString());
            }
            else
            {
                return admin;
            }
        }

        public Admin GetByName(string name)
        {
            Admin admin = _admins.Find<Admin>(admin => admin.Name == name).FirstOrDefault();
            if (admin == null)
            {
                throw new ResourceNotFoundException(this.ToString());
            }
            else
            {
                return admin;
            }

        }

        public Admin Create(Admin admin)
        {
            if (_validator.validateAdmin(ValidateEmail, ValidatePassword, admin))
            {
                admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
                _admins.InsertOne(admin);
            }
            else
            {
                throw new InputIsNotValidException(this.ToString());
            }
           
            return admin;
        }
        public void Update(string id, Admin adminIn)
        {
            Admin admin = _admins.Find<Admin>(admin => admin.Id == id).FirstOrDefault();
            if (admin == null)
            {
                throw new ResourceNotFoundException(this.ToString());
            }

            if (_validator.validateAdmin(ValidateEmail, ValidatePassword, adminIn))
            {
                adminIn.Password = BCrypt.Net.BCrypt.HashPassword(adminIn.Password);
                _admins.ReplaceOne(admin => admin.Id == id, adminIn);
            }
            else
            {
                throw new InputIsNotValidException(this.ToString());
            }
        }

        public void Remove(Admin adminIn)
        {
            Admin admin = _admins.Find<Admin>(admin => admin.Id == adminIn.Id).FirstOrDefault();
            if (admin == null)
            {
                throw new ResourceNotFoundException(this.ToString());
            }
            else
            {
                _admins.DeleteOne(admin => admin.Id == adminIn.Id);
            }
        }

        public void Remove(string id)
        {
            try
            {
                _admins.DeleteOne(admin => admin.Id == id);
            }catch (Exception ex)
            {
               throw new ResourceNotFoundException(this.ToString());
            }
            
        }
        public Admin Verify(string name, string password)
        {
            Admin admin = _admins.Find<Admin>(admin => admin.Name == name).FirstOrDefault();

            if (admin == null)
            {
                throw new ResourceNotFoundException(this.ToString());            
            }

            if (BCrypt.Net.BCrypt.Verify(password, admin.Password))
                return admin;
            else
                throw new ResourceNotFoundException(this.ToString());
        }
        public bool isMaster(string id)
        {
            Admin admin = this.Get(id);
            if (admin.AdminType == Admin.Type.Master)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidateEmail(Admin admin)
        {

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(admin.Name);
            if (match.Success)
                return true;
            else
                return false;
        }
        public bool ValidatePassword(Admin admin)
        {
            Regex Valid = new Regex("^[A-Za-z0-9]+$");
            return Valid.IsMatch(admin.Password); ;
        }
    }
}
