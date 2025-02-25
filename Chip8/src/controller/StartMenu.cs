using Raylib_CSharp.Windowing;

namespace Chip8.src.controller;

class StartMenu : AbstractMenu {

    public StartMenu() {
        ThisScene = SceneIdentifier.StartMenu;
        Buttons = CreateButtons();
    }

    public override Button[] CreateButtons() {
        Buttons = new Button[3];
        Buttons[0] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Load Game", SceneIdentifier.GameSelect));
        Buttons[1] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Options", SceneIdentifier.OptionsMenu));
        Buttons[2] = new Button(0, 0, ButtonWidth, ButtonHeight, new ExitClick("Exit to desktop"));
        AlignButtons();

        return Buttons;
    }

    public override SceneIdentifier Back() {
        Window.Close();
        return SceneIdentifier.StartMenu;
    }

}