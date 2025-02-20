using Raylib_CSharp.Interact;

namespace Chip8.src.controller;

class StartMenu : AbstractMenu {

    public StartMenu() {
    }

    public override Button[] CreateButtons() {
        ThisScene = SceneIdentifier.StartMenu;
        Buttons = new Button[3];
        Buttons[0] = new Button((Program.WindowWidth / 2) - (ButtonWidth / 2), 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Load Game", SceneIdentifier.GameScreen));
        Buttons[1] = new Button((Program.WindowWidth / 2) - (ButtonWidth / 2), 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Options", SceneIdentifier.OptionsMenu));
        Buttons[2] = new Button((Program.WindowWidth / 2) - (ButtonWidth / 2), 0, ButtonWidth, ButtonHeight, new ExitClick("Exit to desktop"));
        AlignButtons();

        return Buttons;
    }

    public override SceneIdentifier Back() {
        return SceneIdentifier.StartMenu;
    }

}