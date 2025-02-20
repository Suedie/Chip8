using Raylib_CSharp.Interact;

namespace Chip8.src.controller;

abstract class AbstractMenu : IScene {

    public Button[] Buttons{get; set;}

    public int ButtonWidth{get;}
    public int ButtonHeight{get;}
    public int Spacing{get;}
    public SceneIdentifier NextScene{get;set;}

    public AbstractMenu() {
        ButtonWidth = Program.WindowWidth / 4;
        ButtonHeight = Program.WindowHeight / 10;
        Spacing = ButtonHeight / 3;
        Buttons = CreateButtons();
    }

    public SceneIdentifier Update() {
        Draw();
        if (Input.IsMouseButtonPressed(MouseButton.Left)) {
            foreach (Button button in Buttons) {
                SceneIdentifier switchScene = button.OnClick(Input.GetMouseX(), Input.GetMouseY(), NextScene);
                if (switchScene != NextScene) {
                    return switchScene;
                }
            }
        } else if (Input.IsKeyPressed(KeyboardKey.Escape)) {
            return Back();
        }
        return NextScene;
    }

    abstract public Button[] CreateButtons();

    abstract public SceneIdentifier Back();

    public void AlignButtons() {
        int firstButtonX = (Program.WindowHeight / 2) - (((Buttons.Length * ButtonHeight) + ((Buttons.Length-1) * Spacing)) / 2);

        for (int i = 0; i < Buttons.Length; i++) {
            if (Buttons[i] != null) {
                Buttons[i].PosY = (Spacing * i) + (i * ButtonHeight) + firstButtonX;
            }
        }
    }

    private void Draw() {
        for (int i = 0; i < Buttons.Length; i++) {
            Buttons[i]?.DrawButton();
        }
    }
}