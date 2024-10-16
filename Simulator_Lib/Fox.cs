namespace Simulator_Lib;

public class Fox
{
    public int Fullness { get; private set; } = 10; // Maximális jóllakottság

    public bool IsAlive => Fullness > 0;

    // Róka táplálkozás nyúllal
    public void Eat(Rabbit rabbit)
    {
        Fullness = Math.Min(Fullness + 3, 10);
    }

    // Róka kör végén éhezik
    public void Hunger()
    {
        Fullness--;
    }

    // Róka mozgása egy szomszédos mezőre
    public void Move(Grid grid)
    {
        // Implementáljuk később a mozgási logikát
    }

    // Róka szaporodása
    public bool CanReproduce() => Fullness >= 8;
}
