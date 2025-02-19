namespace Chip8.src.controller;

interface IClickable {
    SceneIdentifier onClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked);
}