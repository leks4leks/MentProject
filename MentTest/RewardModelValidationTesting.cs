using MentProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class RewardModelValidationTesting
    {
        [TestMethod]
        public void DescriptionNotMoreThen250symbols()
        {
            string longString = GenerateString(300);

            var rew = new RewardModel()
            {
                Description = longString
            };

            var context = new ValidationContext(rew);

            var validationResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(rew, context, validationResult, true);

            Assert.IsFalse(isValid, "Модель не должна пройти валидацию");

            var hasError = validationResult.Where(item => item.MemberNames.Contains("Description")).
                Any();
            
            Assert.IsTrue(hasError);

        }

        private static string GenerateString(int length)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append("A");
            }

            //строка, которая длинее 250 символов
            string longString = stringBuilder.ToString();
            return longString;
        }

        [TestMethod]
        public void TitleIsReqiredPropertyForReward()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            var rew = new RewardModel();

            var context = new ValidationContext(rew);

            var validationResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(rew, context, validationResult, true);

            Assert.IsFalse(isValid, "Модель не должна пройти валидацию");

            var hasError = validationResult.Where(item => item.MemberNames.Contains("Title")).
                Any();
            
            Assert.IsTrue(hasError);
        }

        [TestMethod]
        public void TitleNotMoreThen50()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            const int maxLength = 50;

            var rew = new RewardModel()
            {
                Title = GenerateString(maxLength + 2)
            };

            var context = new ValidationContext(rew);

            var validationResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(rew, context, validationResult, true);

            Assert.IsFalse(isValid, "Модель не должна пройти валидацию");

            var hasError = validationResult.Where(item => item.MemberNames.Contains("Title")).
                Any();
            
            Assert.IsTrue(hasError);
        }

        [TestMethod]
        public void TitleMustContainsOnlyLatynicLetters()
        {
            var rew = new RewardModel()
            {
                Title = "Кирилица"
            };

            var context = new ValidationContext(rew);

            var validationResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(rew, context, validationResult, true);

            Assert.IsFalse(isValid, "Модель не должна пройти валидацию");

            var hasError = validationResult.Where(item => item.MemberNames.Contains("Title")).
                Any();
            
            Assert.IsTrue(hasError);
        }


        [TestMethod]
        public void RewardValidModel()
        {
            var rew = new RewardModel()
            {
                Title = "Testing",
                Description = "Testing"
            };

            var context = new ValidationContext(rew);

            var validationResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(rew, context, validationResult, true);

            Assert.IsTrue(isValid, "Модель должна пройти валидацию");
        }
    }
}
