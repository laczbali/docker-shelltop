using NetCurses.Components;
using NetCurses.Models;

namespace DockerShelltop.Components.Main;
internal class MainMenuScroller : ScrollMenu
{
    private readonly List<(string key, string displayText)> _items;

    public MainMenuScroller(List<(string key, string displayText)> menuItems)
    {
        _items = menuItems;
    }

    public string SelectedKey => _items[CursorItemIndex].key;

    protected override IEnumerable<string> Items
        => _items.Select(x => x.displayText);

    protected override UpdateResult UpdateInner(string? keypressed)
    {
        if (keypressed == "up" || keypressed == "down")
            StepCursor(keypressed);

        DisplayMenu();
        return new UpdateResult();
    }
}
