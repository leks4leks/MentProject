using MentProject.Controllers;
using MentRepository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MentTest
{
    [TestClass]
    public class UserControllerTesting
    {
        readonly UserController controller = new UserController(new UserRepository());

        [TestMethod]
        public void UserControllerListAction()
        {
            ActionResult result = controller.Index();
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            ViewResult view = (ViewResult)result;

            var title = view.ViewBag.Title.ToString();

            Assert.AreEqual("Пользователи", title);
        }

        [TestMethod]
        public void UserControllerCreate()
        {
            var result = controller.Create();
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("AddEdit", ((ViewResult)result).ViewName);

        }
    }
}
