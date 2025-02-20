using Raylib_CSharp.Interact;

namespace Chip8.src;

class Chip8Core : ICore {

    public Processor Processor{get;}
    public int TargetFPS{set;get;}
    public float InstructionsPerFrame{set;get;}

    //Set between 500-1000
    //Higher results in smoother controls
    //Lower gives smoother gameplay
    //600-700 is a good middle ground
    private float _CPUCyclesPerSecond = 700f;
    private bool _debugEnabled = false;
    private bool _isHalted = false;
    private string _currentGamepath;

    public Chip8Core(int targetFPS) {
        this.TargetFPS = targetFPS;
        InstructionsPerFrame = _CPUCyclesPerSecond / (float) targetFPS;
        Processor = ProcessorFactory.MakeProecessor();
    }

    public void LoadGame(string filepath) {
        _currentGamepath = filepath;
        Processor.LoadGame(_currentGamepath);
    }

    public byte[,] GetScreenMatrix() {
        return Processor.GetScreenMatrix();
    }

    public void Update() {
        if (!_debugEnabled)
            Step();
        else
            DebugStep();
    }

    private void Step() {
        //If targetFPS == 60 then update at 60hz
        Processor.updateTimers();
        //CPU runs multiple instructions per frame (ipf)
        //if target is 1000 hz then at 60 fps that becomes 17 ipf
        for(int i = 0; i < InstructionsPerFrame; i++) {
            Processor.Decode(Processor.Fetch());
        }

        if (Input.IsKeyPressed(KeyboardKey.Backspace))
            _debugEnabled = !_debugEnabled;
    }

    private void DebugStep() {
        Processor.updateTimers();
        DebugControls();
        if (!_isHalted) {
            for(int i = 0; i < InstructionsPerFrame; i++) {
                Processor.Decode(Processor.Fetch());
                }
        } else if (_isHalted) { //Manually step through each opcode and print it
            if (Input.IsKeyPressed(KeyboardKey.Space)) {
                Processor.Decode(Processor.Fetch());
                Processor.PrintCurrentOpcode();
            }
        }

        if (Input.IsKeyPressed(KeyboardKey.Backspace))
            _debugEnabled = !_debugEnabled;
    }

    //When debugmode is enabled allows manual control of printing opcodes from Memory
    public void DebugControls() {
        if (Input.IsKeyPressed(KeyboardKey.P)) { //Pauses
            _isHalted = !_isHalted;
        }
        if (Input.IsKeyPressed(KeyboardKey.M)) { //Prints current opcode being executed
            Processor.PrintCurrentOpcode();
        }
        if (Input.IsKeyPressed(KeyboardKey.N)) { //Dumps entire memory in console
            Processor.PrintRAM();
        }
        if (Input.IsKeyPressed(KeyboardKey.B)) {  //Dumps entire memory following current location of PC in console
            Processor.PrintFollowingMemory();
        }
        if (Input.IsKeyPressed(KeyboardKey.K)) { //Dumps upcoming 10 locations in memory. Each opcode is 2 bytes so 20 / 2
            Processor.PrintMemorySnippet(20);
        }
        if (Input.IsKeyPressed(KeyboardKey.Enter)) { //Reloads game
            Processor.LoadGame(_currentGamepath);
        }
    }
}