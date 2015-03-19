using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace License1.Models
{
    public interface IUser
    {
        void CreateNewUser(User user);
        void Delete(int id);
        IEnumerable<User> GetAllUser();
        void Login(User user);
    }
}
