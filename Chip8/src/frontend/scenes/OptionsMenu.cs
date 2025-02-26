using Chip8.src.frontend.buttons;

namespace Chip8.src.frontend.scenes;

class OptionsMenu : AbstractMenu
{

    public SceneIdentifier PreviousScene;

    public OptionsMenu(SceneIdentifier previousScene)
    {
        ThisScene = SceneIdentifier.OptionsMenu;
        PreviousScene = previousScene;
        Buttons = CreateButtons();
    }

    public override Button[] CreateButtons()
    {
        Buttons = new Button[3];
        Buttons[0] = new Button(0, 0, ButtonWidth, ButtonHeight, new FullscreenSwitchClick());
        Buttons[1] = new Button(0, 0, ButtonWidth, ButtonHeight, new ResolutionChangeClick());
        Buttons[2] = new Button(0, 0, ButtonWidth, ButtonHeight, new SceneChangeClick("Back", PreviousScene));
        AlignButtons();

        return Buttons;
    }

    public override SceneIdentifier Back()
    {
        return PreviousScene;
    }

    public override void AlignButtons()
    {
        ButtonWidth = Program.WindowWidth / 4;
        ButtonHeight = Program.WindowHeight / 10;
        Spacing = ButtonHeight / 3;

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].Width = ButtonWidth;
            Buttons[i].Height = ButtonHeight;
        }
        base.AlignButtons();
    }

}