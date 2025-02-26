using Raylib_CSharp.Windowing;

namespace Chip8.src.frontend;

class ExitClick : IClickable {

    public string Label{get;} = "Exit";

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