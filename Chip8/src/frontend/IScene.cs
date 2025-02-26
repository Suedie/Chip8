namespace Chip8.src.frontend;

interface IScene {
    SceneIdentifier Update();

    SceneIdentifier Back();

    SceneIdentifier ThisScene{get;}
}