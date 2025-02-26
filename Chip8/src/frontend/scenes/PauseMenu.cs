using Chip8.src.frontend.buttons;

namespace Chip8.src.frontend.scenes;

class PauseMenu : AbstractMenu
{

    public PauseMenu()
    {
        ThisScene = SceneIdentifier.PauseMenu;
        Buttons = CreateButtons();
    }

    public override Button[] CreateButtons()
    {
        Buttons = new Button[4];
        Buttons[0] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Resume", SceneIdentifier.GameScreen));
        Buttons[1] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Options", SceneIdentifier.OptionsMenu));
        Buttons[2] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Exit to main menu", SceneIdentifier.StartMenu));
        Buttons[3] = new Button(0, 0, ButtonWidth, ButtonHeight, new ExitClick("Exit to desktop"));
        AlignButtons();

        return Buttons;
    }

    public override SceneIdentifier Back()
    {
        return SceneIdentifier.GameScreen;
    }

}