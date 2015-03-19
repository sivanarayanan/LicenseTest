using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using License1.Models;
using License1.Controllers;
using System.Web.Routing;
using System.Web;
using System.Data.Entity;
using System.Security.Principal;
namespace License1.Tests.Controllers
{
    [TestClass]
    public class TestUserController
        
    {
        LicenseEntities dd = new LicenseEntities();
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        private static UserController GetEmployeeController(IUser userrepository)
        {

            UserController userController = new UserController(userrepository);

            userController.ControllerContext = new ControllerContext()

            {

                Controller = userController,

                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())

            };

            return userController;

        }

        [TestMethod]

        public void Create_PostEmployeeInRepository()
        {

            TestUserRepository testRepository = new TestUserRepository();
            UserController usercontroller = GetEmployeeController(testRepository);

            User user = GetDemoUser();

            usercontroller.Register(user);

            IEnumerable<User> users = testRepository.GetAllUser();

            Assert.IsTrue(users.Contains(user));

        }
        [TestMethod]
        public void Post_Delete_User()
        {
            TestUserRepository testRepository = new TestUserRepository();
            UserController usercontroller = GetEmployeeController(testRepository);
            int id = 6;
            User user = dd.Users.Find(id);
            usercontroller.DeleteConfirmed(id);
            IEnumerable<User> users = testRepository.GetAllUser();
            Assert.IsFalse(users.Contains(user));
        
        }
        [TestMethod]
        public void Post_Edit_user()
        {
            User user=new User();
            TestUserRepository testRepository = new TestUserRepository();
            UserController usercontroller = GetEmployeeController(testRepository);
            user = GetDemoUser();
            usercontroller.Edit(user);
            IEnumerable<User> users = testRepository.GetAllUser();
            Assert.IsFalse(users.Contains(user));
        }

        User GetDemoUser()
        {
            return new User {Id=7, Username="siva123456789", First_Name = "f_name", Last_Name = "l_name", Email = "user@gmail.com", Password = "user123", Last_Login = DateTime.Now, Date_Joined = DateTime.Now };
        }
        private class MockHttpContext : HttpContextBase
        {

            private readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("someUser"), null);



            public override IPrincipal User
            {

                get
                {

                    return _user;

                }

                set
                {

                    base.User = value;

                }
            }
        }
    }
}
