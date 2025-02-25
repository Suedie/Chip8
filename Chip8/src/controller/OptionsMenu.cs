namespace Chip8.src.controller;

class OptionsMenu : AbstractMenu {

    public SceneIdentifier PreviousScene;

    public OptionsMenu(SceneIdentifier previousScene) {
        ThisScene = SceneIdentifier.OptionsMenu;
        PreviousScene = previousScene;
    }

    public override Button[] CreateButtons() {
        Buttons = new Button[3];
        Buttons[0] = new Button(0, 0, ButtonWidth, ButtonHeight, new FullscreenSwitchClick());
        Buttons[1] = new Button(0, 0, ButtonWidth, ButtonHeight, new ResolutionChangeClick());
        Buttons[2] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Back", PreviousScene));
        AlignButtons();

        return Buttons;
    }

    public override SceneIdentifier Back() {
        return PreviousScene;
    }

}