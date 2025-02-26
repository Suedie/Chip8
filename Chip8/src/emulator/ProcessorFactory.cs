namespace Chip8.src.emulator;

class ProcessorFactory {

    public static Processor MakeProecessor() {

    Memory memory = new Memory(new Font());
    Display display = new Display();
    Keypad keypad = new Keypad();
    DelayTimer delay = new DelayTimer();
    SoundTimer sound = new SoundTimer();

    Processor processor = new Processor(memory, display, keypad, delay, sound);

    return processor;
    }
}