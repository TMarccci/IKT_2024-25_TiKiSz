namespace Simulator_Lib;

public class Rabbit
{
    private readonly AnimalProperties properties;
    public int Fullness { get; private set; }      // Aktuális jóllakottság

    public bool IsAlive => Fullness > 0;

    public Rabbit(AnimalProperties properties)
    {
        this.properties = properties;
        this.Fullness = properties.MaxFullness;    // Kezdő jóllakottság
    }

    // Nyúl táplálkozás
    public void Eat(Grass grass)
    {
        int nutritionalValue = grass.GetNutritionalValue();
        if (nutritionalValue > 0 && Fullness < properties.MaxFullness)
        {
            Fullness = Math.Min(Fullness + nutritionalValue, properties.MaxFullness);
            grass.Eaten();
        }
    }

    // Nyúl éhezik minden kör végén
    public void Hunger()
    {
        Fullness -= properties.HungerRate;
    }

    // Nyúl mozgása
    public void Move(Grid grid, int currentX, int currentY)
    {
        grid.MoveRabbit(currentX, currentY);
    }

    // Nyúl szaporodása
    public bool CanReproduce() => Fullness >= properties.ReproductionFullness;
}

