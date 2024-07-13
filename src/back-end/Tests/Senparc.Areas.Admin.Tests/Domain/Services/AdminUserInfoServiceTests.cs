﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Areas.Admin.Domain.Models;
using Senparc.Areas.Admin.Tests;

namespace Senparc.Areas.Admin.Domain.Tests
{
    [TestClass()]
    public class AdminUserInfoServiceTests : TestBase
    {
        AdminUserInfoService _adminUserInfoService;

        public AdminUserInfoServiceTests()
        {
           _adminUserInfoService = new AdminUserInfoService(mockAdminUserInfoRepository.Object, null, base._serviceProvider);
        }


        [TestMethod()]
        public async Task GetUserInfoAsyncTest()
        {
            var userName = "Admin";

            var result = await _adminUserInfoService.GetUserInfoAsync(userName);
            Assert.AreEqual(_adminUserInfoList[0], result);

            result = await _adminUserInfoService.GetUserInfoAsync(userName + "  ");
            Assert.AreEqual(_adminUserInfoList[0], result);

            result = await _adminUserInfoService.GetUserInfoAsync(" " + userName);
            Assert.AreEqual(_adminUserInfoList[0], result);

            result = await _adminUserInfoService.GetUserInfoAsync(" " + userName + "  ");
            Assert.AreEqual(_adminUserInfoList[0], result);

            result = await _adminUserInfoService.GetUserInfoAsync("admin");
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetListTest()
        {
            var result = await _adminUserInfoService.GetObjectListAsync(1, 10, z => true, z => z.Id, Ncf.Core.Enums.OrderingType.Descending, null);
            Assert.IsNotNull(result);

            var data = base.dataLists[typeof(AdminUserInfo)];
            Assert.AreEqual(data.Count, result.Count);
        }
    }
}