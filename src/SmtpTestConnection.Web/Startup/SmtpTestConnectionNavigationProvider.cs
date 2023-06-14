using Abp.Application.Navigation;
using Abp.Localization;

namespace SmtpTestConnection.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class SmtpTestConnectionNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fa fa-home"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Documentation,
                        L("Documentation"),
                        url: "/Home/Documentation",
                        icon: "fa fa-book"
                        )
                
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SmtpTestConnectionConsts.LocalizationSourceName);
        }
    }
}
