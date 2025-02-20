namespace Chip8.src.controller;

class PauseMenu : AbstractMenu {

    public PauseMenu() {
    }

    public override Button[] CreateButtons() {
        ThisScene = SceneIdentifier.PauseMenu;
        Buttons = new Button[4];
        Buttons[0] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Resume", SceneIdentifier.GameScreen));
        Buttons[1] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Options", SceneIdentifier.OptionsMenu));
        Buttons[2] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Exit to main menu", SceneIdentifier.StartMenu));
        Buttons[3] = new Button(0, 0, ButtonWidth, ButtonHeight, new ExitClick("Exit to desktop"));
        AlignButtonsVertically();

        return Buttons;
    }

    public override SceneIdentifier Back() {
        return SceneIdentifier.GameScreen;
    }

}