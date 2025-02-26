using Raylib_CSharp.Interact;

namespace Chip8.src.frontend;

class GameSelectMenu : AbstractMenu {

    public SceneIdentifier PreviousScene;

    private ICore _gameCore;
    private string[] _gamePaths;
    private int _scrollOffset = 0;

    public GameSelectMenu(SceneIdentifier previousScene, ICore gameCore) {
        ThisScene = SceneIdentifier.GameSelect;
        PreviousScene = previousScene;
        _gameCore = gameCore;
        _gamePaths = Directory.GetFiles(Program.GAME_DIRECTORY);
        Buttons = CreateButtons();
    }

    public override void AlignButtons() {
        CheckIfScroll();
        ButtonWidth = Program.WindowWidth - (Program.WindowWidth / 10);
        for (int i = 0; i < _gamePaths.Length; i++) {
            Buttons[i].Width = ButtonWidth;
        }

        int buttonX = (Program.WindowWidth / 2) - (ButtonWidth / 2);
        int firstButtonY = (Program.WindowHeight / 2) - (((Buttons.Length * ButtonHeight) + ((Buttons.Length-1) * Spacing)) / 2);
        if (firstButtonY < (Program.WindowHeight / 20)) {
            firstButtonY = Program.WindowHeight / 20;
        }

        for (int i = 0; i < Buttons.Length; i++) {
            if (Buttons[i] != null) {
                Buttons[i].PosX = buttonX;
                Buttons[i].PosY = (Spacing * i) + (i * ButtonHeight) + firstButtonY + _scrollOffset;
            }
        }
    }

    private void CheckIfScroll() {
        int scroll = (int) Input.GetMouseWheelMove() * 20;
        if (Program.WindowHeight < GetAllButtonHeight()) {
            if (_scrollOffset + scroll >= 0) {
                _scrollOffset = 0;
            } else if (_scrollOffset + scroll <= Program.WindowHeight - (Program.WindowHeight / 20) - GetAllButtonHeight()) {
                _scrollOffset = Program.WindowHeight - (Program.WindowHeight / 20) - GetAllButtonHeight();
            } else {
                _scrollOffset += scroll;
            }
        } else {
            _scrollOffset = 0;
        }
    }

    private int GetAllButtonHeight() {
        int height = (Buttons.Length * ButtonHeight) + (Buttons.Length * Spacing);
        return height;
    }

    public override Button[] CreateButtons() {
        UpdateGameList(Program.GAME_DIRECTORY);
        Buttons = new Button[_gamePaths.Length];
        for (int i = 0; i < _gamePaths.Length; i++) {
            Buttons[i] = new Button(0,0, ButtonWidth, ButtonHeight, new GameSelectClick(_gamePaths[i], _gameCore));
        }
        AlignButtons();

        return Buttons;
    }

    public void UpdateGameList(string gameDirectory) {
        _gamePaths = Directory.GetFiles(gameDirectory);
        Array.Sort(_gamePaths);
    }

    public override SceneIdentifier Back() {
        return PreviousScene;
    }
}