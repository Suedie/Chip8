namespace Chip8.src.frontend.scenes;

interface IScene
{
    SceneIdentifier Update();

    SceneIdentifier Back();

    SceneIdentifier ThisScene { get; }
}