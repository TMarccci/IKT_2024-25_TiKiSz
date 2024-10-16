namespace Simulator_Lib;

public class Fox
{
    private readonly AnimalProperties properties;
    public int Fullness { get; private set; }      // Aktuális jóllakottság

    public bool IsAlive => Fullness > 0;

    public Fox(AnimalProperties properties)
    {
        this.properties = properties;
        this.Fullness = properties.MaxFullness;    // Kezdő jóllakottság
    }

    // Róka táplálkozás nyúllal
    public void Eat(Rabbit rabbit)
    {
        Fullness = Math.Min(Fullness + properties.FoodValue, properties.MaxFullness);
    }

    // Róka éhezik minden kör végén
    public void Hunger()
    {
        Fullness -= properties.HungerRate;
    }

    // Róka mozgása
    public void Move(Grid grid, int currentX, int currentY)
    {
        if (!grid.EatRabbitIfNearby(currentX, currentY, this))
        {
            grid.MoveFox(currentX, currentY);
        }
    }

    // Róka szaporodása
    public bool CanReproduce() => Fullness >= properties.ReproductionFullness;
}
