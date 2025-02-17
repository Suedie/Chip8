namespace Chip8.src;

class Chip8 : IFacade {

    public Processor _processor {get;}

    public Chip8() {
        _processor = ProcessorFactory.MakeProecessor();
    }

    public void run() {
        _processor.Decode(_processor.Fetch());
    }
}