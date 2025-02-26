using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;

namespace Chip8.src.controller;

class Button {

    public int PosX{get; set;}
    public int PosY{get; set;}
    public int Width;
    public int Height;
    private string _label;
    private IClickable _clickBehaviour;

    public Button (int x, int y, int width, int height, IClickable clickBehaviour) {
        PosX = x;
        PosY = y;
        Width = width;
        Height = height;
        _clickBehaviour = clickBehaviour;
        _label = _clickBehaviour.Label;
    }

    public void DrawButton() {
        Color color = Color.RayWhite;

        if (mouseOverlaps(Input.GetMouseX(), Input.GetMouseY())) {
            color = Color.Gray;
        }
        Graphics.DrawRectangle(PosX, PosY, Width, Height, color);
        _label = _clickBehaviour.Label;
        Graphics.DrawText(_label,PosX + (Width / 10), PosY + (Height / 2), Height / 3, Color.Black);
    }

    private bool mouseOverlaps(int mouseX, int mouseY) {
        if (mouseX < PosX || mouseX > PosX + Width || mouseY < PosY || mouseY > PosY + Height) {
            return false;
        } else {
            return true;
        }
    }

    public SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene) {
        return _clickBehaviour.onClick(mouseX, mouseY, currentScene, mouseOverlaps(mouseX, mouseY));
    }
}