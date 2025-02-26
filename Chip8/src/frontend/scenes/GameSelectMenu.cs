using Chip8.src.emulator;
using Chip8.src.frontend.buttons;
using Raylib_CSharp.Interact;

namespace Chip8.src.frontend.scenes;

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

    //Resizes the buttons specifically for this menu to be the width of the screen -10%
    //Aligns them along the middle but allows for scrolling in case there are more games than fits on the screen
    public override void AlignButtons() {
        CheckIfScroll();
        ButtonWidth = Program.WindowWidth - (Program.WindowWidth / 10);
        for (int i = 0; i < Buttons.Length; i++) {
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

    //Checks if the mouse wheel has been scrolled and adds that to an offset that will move the buttons up or down
    private void CheckIfScroll() {
        int scrollSpeed = Program.WindowHeight / 25;
        int scroll = (int) Input.GetMouseWheelMove() * scrollSpeed;
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

    //Scans the games directory and creates a button for each game that will load that game
    public override Button[] CreateButtons() {
        UpdateGameList(Program.GAME_DIRECTORY);
        Buttons = new Button[_gamePaths.Length];
        for (int i = 0; i < _gamePaths.Length; i++) {
            Buttons[i] = new Button(0,0, ButtonWidth, ButtonHeight, new GameSelectClick(_gamePaths[i], _gameCore));
        }
        AlignButtons();

        return Buttons;
    }

    //Scans the game directory
    // TODO Only load files with the correct file ending
    public void UpdateGameList(string gameDirectory) {
        _gamePaths = Directory.GetFiles(gameDirectory);
        Array.Sort(_gamePaths);
    }

    public override SceneIdentifier Back() {
        return PreviousScene;
    }
}