using NetCurses;
using NetCurses.Models;

namespace DockerShelltop.Components.Main;
internal class MainMenu : NcWindow
{
    private const int ROW_OFFSET = 2;
    private const int COLUMN_OFFSET = 10;

    private string? scrollMenuId = null;

    protected override void InitializeInner()
    {
        InitScrollMenu();
    }

    protected override UpdateResult UpdateInner(string? keypressed)
    {
        if (keypressed == "enter" && scrollMenuId is not null)
        {
            var scrollMenu = Children.First(x => x.Id == scrollMenuId);
            Children.Remove(scrollMenu);
        }

        WriteAtPosition(ROW_OFFSET, 10, "Docker SHELLtop");
        return new UpdateResult();
    }

    private void InitScrollMenu()
    {
        var menuSelector = new MainMenuScroller(new List<(string key, string displayText)>
        {
            ("compositions", "Compositions"),
            ("containers", "Containers"),
        });
        menuSelector.Initialize(new WindowSize(
            Size.Rows - ROW_OFFSET - 1,
            Size.Columns - COLUMN_OFFSET,
            ROW_OFFSET + 1,
            COLUMN_OFFSET));

        menuSelector.IsActive = true;
        scrollMenuId = menuSelector.Id;

        Children.Add(menuSelector);
    }
}
