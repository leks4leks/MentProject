using MentData;
using MentRepository.RepModel;
using MentRepository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentTest
{
    [TestClass]
    public class BusinessLogicUserRepositoryTesting
    {
        readonly IUserRepository _userRepository = new UserRepository();
        readonly IRewardRepository _rewardRepository = new RewardRepository();

        private UserRepModel CreateTestUserModel()
        {
            return new UserRepModel()
            {
                BDay = DateTime.Now.Date,
                Name = "TestUser"
            };
        }

        [TestMethod]
        public void AddUser()
        {
            var user = CreateTestUserModel();
            var res = _userRepository.SaveUser(user);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void DeleteUser()
        {
            var res = _userRepository.DeleteUser(1); // изначально не возвращаю Id, тут по идее его нужно вернуть и потом в базу сходить проверить           
            Assert.IsTrue(res);            
        }

        [TestMethod]
        public void AddBabgeToPerson()
        {
            var dic = new Dictionary<int, int> { };
            dic.Add(1, 1);
            var res = _rewardRepository.SaveUserInReward(dic);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void GetPersonByName()
        {
            // Добаляем несколько пользователей
            _userRepository.SaveUser(CreateTestUserModel());
            _userRepository.SaveUser(CreateTestUserModel());

            // добавляем пользователя, которого будем искать по имени
            var name = "user_test";
            var saved = _userRepository.SaveUser(new UserRepModel()
            {
                Name = "user_test",
                BDay = DateTime.Now.Date,
            });

            //еще одного пользователя
            _userRepository.SaveUser(CreateTestUserModel());

            var user = _userRepository.GetAllUsers(name); // тут таже беда с Id но мы можем по имени проверить

            Assert.AreEqual(name, user.FirstOrDefault().Name, "Извекли не того пользователя");
        }

        [TestMethod]
        public void UpdateUser()
        {
            var name = "SorryForDidntReturnId";
            var newUser = new UserRepModel()
            {
                Name = "SorryForDidntReturnId",
                BDay = DateTime.Now.Date,
            };
            var user = _userRepository.SaveUser(newUser);

            var res = _userRepository.SaveUser(CreateTestUserModel());

            var thatSavedUser = _userRepository.GetAllUsers(name);

            string newName = "Name " + DateTime.Now.Ticks;

            newUser.Name = newName;
            newUser.Id = thatSavedUser.FirstOrDefault().Id;

            _userRepository.SaveUser(newUser);
            
            var thatResavedUser = _userRepository.GetAllUsers(newName);
            
            Assert.AreEqual(newName, thatResavedUser.FirstOrDefault().Name);
        }

    }
}
