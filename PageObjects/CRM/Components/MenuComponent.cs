using NwbPlaywright.Drivers;

namespace NwbPlaywright.PageObjects.CRM.Components
{
    public class MenuComponent : ICrmMenuComponent
    {
        private readonly ILocator menuComponent;

        private readonly Dictionary<string, string> menuItems = new Dictionary<string, string>
        {
            { "HOME", "NWB CRM - DEV" },
            { "MYWORK", "My work" },
            { "ORGANIZATIONS", "Organizations" },
            { "ACCOUNTS", "Accounts" },
            { "CONTACTS", "Contacts" },
        };

        public MenuComponent(IPlaywrightDriver playwrightDriver)
        {
            var page1 = playwrightDriver.Page.Result;
            menuComponent = page1.Locator("nav[aria-label='Main']");
        }

        public async Task OpenMenuItem(string menuItem)
        {
            if (!menuItems.ContainsKey(menuItem.ToUpper()))
            {
                throw new ArgumentException($"Menu item '{menuItem}' does not exist.");
            }

            var menuItemLocator = menuComponent.Locator($"li[name={menuItems[menuItem.ToUpper()]}]");
            await menuItemLocator.WaitForAsync(new LocatorWaitForOptions
                { Timeout = 10000, State = WaitForSelectorState.Visible });
            await menuItemLocator.ClickAsync(new LocatorClickOptions { Delay = 200 });
        }
    }

    public interface ICrmMenuComponent
    {
        Task OpenMenuItem(string menuItem);
    }
}