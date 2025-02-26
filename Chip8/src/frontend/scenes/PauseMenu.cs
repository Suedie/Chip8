using Chip8.src.emulator;
using Chip8.src.frontend.buttons;

namespace Chip8.src.frontend.scenes;

class PauseMenu : AbstractMenu
{
    private ICore _gameCore;
    public PauseMenu(ICore gameCore)
    {
        _gameCore = gameCore;
        ThisScene = SceneIdentifier.PauseMenu;
        Buttons = CreateButtons();
    }

    public override Button[] CreateButtons()
    {
        Buttons = new Button[6];
        Buttons[0] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Resume", SceneIdentifier.GameScreen));
        Buttons[1] = new Button(0, 0, ButtonWidth, ButtonHeight, new ReloadClick("Reload game", _gameCore));
        Buttons[2] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Load new game", SceneIdentifier.GameSelect));
        Buttons[3] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Options", SceneIdentifier.OptionsMenu));
        Buttons[4] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Exit to main menu", SceneIdentifier.StartMenu));
        Buttons[5] = new Button(0, 0, ButtonWidth, ButtonHeight, new ExitClick("Exit to desktop"));
        AlignButtons();

        return Buttons;
    }

    public override SceneIdentifier Back()
    {
        return SceneIdentifier.GameScreen;
    }

}