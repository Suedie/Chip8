using Chip8.src.frontend.buttons;
using Raylib_CSharp.Interact;

namespace Chip8.src.frontend.scenes;

abstract class AbstractMenu : IScene {

    public Button[] Buttons{get; set;} = Array.Empty<Button>();

    public int ButtonWidth{get; set;}
    public int ButtonHeight{get; set;}
    public int Spacing{get; set;}
    public SceneIdentifier ThisScene{get; set;}

    public AbstractMenu() {
        ButtonWidth = Program.WindowWidth / 4;
        ButtonHeight = Program.WindowHeight / 10;
        Spacing = ButtonHeight / 3;
    }

    public SceneIdentifier Update() {
        AlignButtons();
        Draw();
        if (Input.IsMouseButtonPressed(MouseButton.Left)) {
            foreach (Button button in Buttons) {
                SceneIdentifier nextScene = button.OnClick(Input.GetMouseX(), Input.GetMouseY(), ThisScene);
                if (nextScene != ThisScene) {
                    return nextScene;
                }
            }
        } else if (Input.IsKeyPressed(KeyboardKey.Escape)) {
            return Back();
        }
        return ThisScene;
    }

    abstract public Button[] CreateButtons();

    abstract public SceneIdentifier Back();

    public virtual void AlignButtons() {
        int buttonX = (Program.WindowWidth / 2) - (ButtonWidth / 2);
        int firstButtonY = (Program.WindowHeight / 2) - (((Buttons.Length * ButtonHeight) + ((Buttons.Length-1) * Spacing)) / 2);

        for (int i = 0; i < Buttons.Length; i++) {
            if (Buttons[i] != null) {
                Buttons[i].PosX = buttonX;
                Buttons[i].PosY = (Spacing * i) + (i * ButtonHeight) + firstButtonY;
            }
        }
    }

    private void Draw() {
        for (int i = 0; i < Buttons.Length; i++) {
            Buttons[i]?.DrawButton();
        }
    }
}