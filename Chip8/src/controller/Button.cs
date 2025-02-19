using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;

namespace Chip8.src.controller;

class Button {

    public int PosX{get; set;}
    public int PosY{get; set;}
    private int _width;
    private int _height;
    private string _label;
    private IClickable _clickBehaviour;

    public Button (int x, int y, int width, int height, string label, IClickable clickBehaviour) {
        this.PosX = x;
        this.PosY = y;
        this._width = width;
        this._height = height;
        this._label = label;
        this._clickBehaviour = clickBehaviour;
    }

    public void DrawButton() {
        Color color = Color.RayWhite;

        if (mouseOverlaps(Input.GetMouseX(), Input.GetMouseY())) {
            color = Color.Gray;
        }
        Graphics.DrawRectangle(PosX, PosY, _width, _height, color);
        Graphics.DrawText(_label,PosX + (_width / 10), PosY + (_height / 2), _height / 2, Color.Black);
    }

    private bool mouseOverlaps(int mouseX, int mouseY) {
        if (mouseX < PosX || mouseX > PosX + _width || mouseY < PosY || mouseY > PosY + _height) {
            return false;
        } else {
            return true;
        }
    }

    public SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene) {
        return _clickBehaviour.onClick(mouseX, mouseY, currentScene, mouseOverlaps(mouseX, mouseY));
    }
}