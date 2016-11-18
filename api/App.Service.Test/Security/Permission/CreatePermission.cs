﻿namespace App.Service.Test.Security.Permission
{
    using App.Common.DI;
    using App.Common.UnitTest;
    using App.Common.Validation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Service.Security;
    using Service.Security.Permission;
    using System;

    [TestClass]
    public class CreatePermission : BaseUnitTest
    {
        [TestInitialize()]
        public void Init()
        {
            this.Application.OnApplicationStarted();
        }

        [TestCleanup()]
        public void Finished()
        {
            this.Application.OnApplicationEnded();
        }

        [TestMethod]
        public void CreatePermission_ShouldBeSuccess_WithValidRequest()
        {
            string name = "Name of Permission" + Guid.NewGuid();
            string key = "Key of Permission" + Guid.NewGuid();
            string desc = "Desc of Permission";
            IPermissionService service = IoC.Container.Resolve<IPermissionService>();
            App.Entity.Security.Permission permission = this.CreatePermissionItem(name, key, desc);
            Assert.IsNotNull(permission);
        }

        [TestMethod]
        public void CreatePermission_ShouldGetException_WithEmptyName()
        {
            try
            {
                string name = string.Empty;
                string key = "Key of Permission" + Guid.NewGuid();
                string desc = "Desc of Permission";
                IPermissionService service = IoC.Container.Resolve<IPermissionService>();
                this.CreatePermissionItem(name, key, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addPermission.validation.nameIsRequire"));
            }
        }

        [TestMethod]
        public void CreatePermission_ShouldGetException_WithDuplicatedName()
        {
            try
            {
                string name = "Duplicated Name" + Guid.NewGuid();
                string key = "Key of Permission" + Guid.NewGuid();
                string desc = "Desc of Permission";
                IPermissionService service = IoC.Container.Resolve<IPermissionService>();
                this.CreatePermissionItem(name, key, desc);
                this.CreatePermissionItem(name, Guid.NewGuid().ToString(), desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addPermission.validation.nameAlreadyExist"));
            }
        }

        [TestMethod]
        public void CreatePermission_ShouldGetException_WithEmptyKey()
        {
            try
            {
                string name = "Name Of Permission" + Guid.NewGuid();
                string key = string.Empty;
                string desc = "Desc of Permission";
                IPermissionService service = IoC.Container.Resolve<IPermissionService>();
                this.CreatePermissionItem(name, key, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addPermission.validation.keyIsRequire"));
            }
        }

        [TestMethod]
        public void CreatePermission_ShouldGetException_WithDuplicatedKey()
        {
            try
            {
                string name = "Name of Pemrission" + Guid.NewGuid();
                string key = "Duplicated Key" + Guid.NewGuid();
                string desc = "Desc of Permission";
                IPermissionService service = IoC.Container.Resolve<IPermissionService>();
                this.CreatePermissionItem(name, key, desc);
                this.CreatePermissionItem(Guid.NewGuid().ToString(), key, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addPermission.validation.keyAlreadyExist"));
            }
        }

        private App.Entity.Security.Permission CreatePermissionItem(string name, string key, string desc)
        {
            CreatePermissionRequest request = new CreatePermissionRequest() { Name = name, Key = key, Description = desc };
            IPermissionService service = IoC.Container.Resolve<IPermissionService>();
            App.Entity.Security.Permission permission = service.Create(request);
            return permission;
        }
    }
}
