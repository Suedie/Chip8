namespace Chip8.src.controller;

interface IScene {
    SceneIdentifier Update();

    SceneIdentifier Back();
}