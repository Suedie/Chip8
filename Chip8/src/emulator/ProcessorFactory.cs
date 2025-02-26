namespace Chip8.src.emulator;

class ProcessorFactory {

    public static Processor MakeProecessor() {

    Memory memory = new(new Font());
    Display display = new();
    Keypad keypad = new();
    DelayTimer delay = new();
    SoundTimer sound = new();

    Processor processor = new(memory, display, keypad, delay, sound);

    return processor;
    }
}