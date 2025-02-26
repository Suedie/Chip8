using Chip8.src.frontend.scenes;
using Raylib_CSharp.Windowing;

namespace Chip8.src.frontend.buttons;

class ExitClick : IClickable {

    public string Label{get;} = "Exit";

    //This button behaviour simply closes the window when clicked
    public ExitClick(string label) {
        Label = label;
    }

    public SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked) {
        if (wasClicked) {
            Window.Close();
        }
        return currentScene;
    }
    
}