using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using License1.Models;
namespace License1.Tests
{
    class TestUserRepository:IUser
    {
        private List<User> _db = new List<User>();

        public void CreateNewUser(User user)
        {
            _db.Add(user);
        }
        public IEnumerable<User> GetAllUser()
        {
            return _db.ToList();
        }
        public void Delete(int id)
        {
             
        }
        public void Login(User user)
        { 
        
        }
    }
}
