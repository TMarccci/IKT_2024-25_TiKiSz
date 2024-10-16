namespace Simulator_Lib;

public class Rabbit
{
    public int Fullness { get; private set; } = 5; // Maximális jóllakottság

    public bool IsAlive => Fullness > 0;

    // Nyúl táplálkozás
    public void Eat(Grass grass)
    {
        int nutritionalValue = grass.GetNutritionalValue();
        if (nutritionalValue > 0 && Fullness < 5)
        {
            Fullness = Math.Min(Fullness + nutritionalValue, 5);
            grass.Eaten();
        }
    }

    // Nyúl kör végén éhezik
    public void Hunger()
    {
        Fullness--;
    }

    // Nyúl mozgása egy szomszédos mezőre
    public void Move(Grid grid)
    {
        // Implementáljuk később a mozgási logikát
    }

    // Nyúl szaporodása
    public bool CanReproduce() => Fullness >= 4;
}
