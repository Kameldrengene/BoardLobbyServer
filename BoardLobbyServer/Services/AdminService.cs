using BoardLobbyServer.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Services
{
    public class AdminService
    {
        private readonly IMongoCollection<Admin> _admins;

        public AdminService(IBoardServerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _admins = database.GetCollection<Admin>(settings.AdminsCollectionName);
        }

        public List<Admin> Get() =>
            _admins.Find(admin => true).ToList();
        public Admin Get(string id) =>
            _admins.Find<Admin>(admin => admin.Id == id).FirstOrDefault();
        public Admin GetByName(string name) =>
            _admins.Find<Admin>(admin => admin.Name == name).FirstOrDefault();
        public Admin Create(Admin admin)
        {
            _admins.InsertOne(admin);
            return admin;
        }
        public void Update(string id, Admin adminIn) =>
            _admins.ReplaceOne(admin => admin.Id == id, adminIn);
        public void Remove(Admin adminIn) =>
            _admins.DeleteOne(admin => admin.Id == adminIn.Id);
        public void Remove(string id) =>
            _admins.DeleteOne(admin => admin.Id == id);
        public Admin Verify(string name,string password)
        {
            Admin admin = _admins.Find<Admin>(admin => admin.Name == name).FirstOrDefault();

            if (BCrypt.Net.BCrypt.Verify(password, admin.Password))
                return admin;
            else
                return null;
        }
    }
}
