using Raylib_CSharp.Windowing;

namespace Chip8.src.controller;

class ExitClick : IClickable {

    public SceneIdentifier onClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked) {
        if (wasClicked) {
            Window.Close();
        }
        return currentScene;
    }
    
}