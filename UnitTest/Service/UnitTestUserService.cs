using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    [TestClass]
    public class UnitTestUserService
    {
        public UserService userService;
        public UnitTestUserService(IMapper mapper)
        {
            userService = new UserService(mapper);
        }
        //TestGetInformation
        [TestMethod]
        public void TestIInformationNotNull()
        {
            var result = userService.GetUser("1");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIInformationValueNotExist()
        {
            var result = userService.GetUser("-1");
            Assert.IsNull(result);
        }

        //TestLogin
        [TestMethod]
        public void TestILoginNull()
        {
            var result = userService.Login(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestILoginTrue()
        {
            var result = userService.Login(new UserLogin() { UserName = "admin1", Password = "123456"});
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestILoginFalse()
        {
            var result = userService.Login(new UserLogin() { UserName = "", Password = "" });
            Assert.IsFalse(result);
        }
    }
}
