using Raylib_CSharp.Interact;

namespace Chip8.src.controller;

class MainMenu : IScene {

    private Button[] buttons;

    private int _windowWidth = 1280;
    private int _windowHeight = 640;

    private int _buttonWidth;
    private int _buttonHeight;
    private int _spacing;
    private SceneIdentifier _nextScene;

    public MainMenu() {
        _nextScene = SceneIdentifier.MainMenu;
        _buttonWidth = _windowWidth / 4;
        _buttonHeight = _windowHeight / 10;
        _spacing = _buttonHeight / 3;
        buttons = new Button[3];
        buttons[0] = new Button((_windowWidth / 2) - (_buttonWidth / 2), 0, _buttonWidth, _buttonHeight, "Play", new SceneChangeClick(SceneIdentifier.GameplayScreen));
        buttons[1] = new Button((_windowWidth / 2) - (_buttonWidth / 2), 0, _buttonWidth, _buttonHeight, "Options", new SceneChangeClick(SceneIdentifier.OptionsMenu));
        buttons[2] = new Button((_windowWidth / 2) - (_buttonWidth / 2), 0, _buttonWidth, _buttonHeight, "Exit", new ExitClick());
        SetButtonPosition();
    }

    public SceneIdentifier Update() {
        Draw();
        if (Input.IsMouseButtonPressed(MouseButton.Left)) {
            foreach (Button button in buttons) {
                _nextScene = button.OnClick(Input.GetMouseX(), Input.GetMouseY(), _nextScene);
                if (_nextScene != SceneIdentifier.MainMenu) {
                    break;
                }
            }
        }
        return _nextScene;
    }

    private void SetButtonPosition() {
        int firstButtonX = (_windowHeight / 2) - (((buttons.Length * _buttonHeight) + ((buttons.Length-1) * _spacing)) / 2);

        for (int i = 0; i < buttons.Length; i++) {
            if (buttons[i] != null) {
                buttons[i].PosY = (_spacing * i) + (i * _buttonHeight) + firstButtonX;
            }
        }
    }

    private void Draw() {
        for (int i = 0; i < buttons.Length; i++) {
            if (buttons[i] != null) {
                buttons[i].DrawButton();
            }
        }
    }

}