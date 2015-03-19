using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Security;
namespace License1.Models
{
    public class UserRepository:IUser
    {
        public LicenseEntities db = new LicenseEntities();

        public IEnumerable<User> GetAllUser()
        {
            return db.Users.ToList();
        }
       
        public void CreateNewUser(User user)
        {

            var newUser = db.Users.Create();
            newUser.Username = user.Username;
            newUser.First_Name = user.First_Name;
            newUser.Last_Name = user.Last_Name;
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            newUser.Last_Login = DateTime.Now;
            newUser.Date_Joined = DateTime.Now;
            db.Users.Add(newUser);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }
        public void Login(User user)
        { 

        
        }
       
    }
}