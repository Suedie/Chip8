using Raylib_CSharp.Windowing;

namespace Chip8.src.controller;

class ResolutionChangeClick : IClickable {

    public string Label{get; set;}

    public ResolutionChangeClick() {
        setLabel();
    }

    public SceneIdentifier onClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked) {
        if (wasClicked) {
        }
        return currentScene;
    }

    private void setLabel() {
        Label = Program.WindowWidth + " x " + Program.WindowHeight;
    }

    
}