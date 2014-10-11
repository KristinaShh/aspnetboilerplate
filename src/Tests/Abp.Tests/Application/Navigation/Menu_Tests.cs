﻿using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Xunit;

namespace Abp.Tests.Application.Navigation
{
    public class Menu_Tests : TestBaseWithLocalIocManager
    {
        [Fact]
        public void Test1()
        {
            LocalIocManager.Register<MyNavigationProvider1>();
            LocalIocManager.Register<MyNavigationProvider2>();

            var configuration = new NavigationConfiguration();
            
            configuration.Providers.Add<MyNavigationProvider1>();
            configuration.Providers.Add<MyNavigationProvider2>();

            var menuManager = new MenuManager(LocalIocManager, configuration);

            menuManager.Initialize();
        }
    }

    public class MyNavigationProvider1 : INavigationProvider
    {
        public void SetNavigation(INavigationProviderContext context)
        {
            context.MainMenu.AddItem(
                new MenuItem(new FixedLocalizableString("Administration"))
                {
                    Icon = "fa fa-asterisk",
                    Name = "Abp.Zero.Administration"
                }
                    .AddItem(new MenuItem(new FixedLocalizableString("User management"))
                             {
                                 Icon = "fa fa-users",
                                 Url = "#/admin/users",
                                 RequiredPermissionName = "Abp.Zero.UserManagement"
                             })
                    .AddItem(new MenuItem(new FixedLocalizableString("Role management"))
                             {
                                 Icon = "fa fa-star-o",
                                 Url = "#/admin/roles",
                                 RequiredPermissionName = "Abp.Zero.RoleManagement"
                             })
                );
        }
    }

    public class MyNavigationProvider2 : INavigationProvider
    {
        public void SetNavigation(INavigationProviderContext context)
        {
            var adminMenu = context.MainMenu.GetItemByName("Abp.Zero.Administration");
            adminMenu.AddItem(new MenuItem(new FixedLocalizableString("Role management"))
                              {
                                  Icon = "fa fa-cog",
                                  Url = "#/admin/settings",
                                  RequiredPermissionName = "Abp.Zero.SettingManagement"
                              });
        }
    }
}