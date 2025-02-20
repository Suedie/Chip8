using Raylib_CSharp.Interact;

namespace Chip8.src.controller;

class PauseMenu : AbstractMenu {

    public PauseMenu() {
    }

    public override Button[] CreateButtons() {
        NextScene = SceneIdentifier.PauseMenu;
        Buttons = new Button[4];
        Buttons[0] = new Button((Program.WindowWidth / 2) - (ButtonWidth / 2), 0, ButtonWidth, ButtonHeight, "Resume", new SceneChangeClick(SceneIdentifier.GameScreen));
        Buttons[1] = new Button((Program.WindowWidth / 2) - (ButtonWidth / 2), 0, ButtonWidth, ButtonHeight, "Options", new SceneChangeClick(SceneIdentifier.OptionsMenu));
        Buttons[2] = new Button((Program.WindowWidth / 2) - (ButtonWidth / 2), 0, ButtonWidth, ButtonHeight, "Exit to main menu", new SceneChangeClick(SceneIdentifier.StartMenu));
        Buttons[3] = new Button((Program.WindowWidth / 2) - (ButtonWidth / 2), 0, ButtonWidth, ButtonHeight, "Exit to desktop", new ExitClick());
        AlignButtons();

        return Buttons;
    }

    public override SceneIdentifier Back() {
        return SceneIdentifier.GameScreen;
    }

}