namespace Chip8.src.frontend;

class SceneChangeClick : IClickable {

    public string Label{get;}
    private SceneIdentifier _nextScene;

    public SceneChangeClick(string label, SceneIdentifier nextScene) {
        Label = label;
        _nextScene = nextScene;
    }

    public SceneIdentifier onClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked) {
        if (wasClicked) {
            return _nextScene;
        }
        return currentScene;
    }
    
}