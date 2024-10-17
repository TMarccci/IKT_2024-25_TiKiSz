namespace Simulator_Lib;

public class Grass
{
    public enum GrassState
    {
        JustAte,    // Épp lelegelt
        Seedling,   // Fűkezdemény
        Young,      // Zsenge fű
        Mature      // Kifejlett fűcsomó
    }

    public GrassState State { get; private set; }

    public Grass()
    {
        State = GrassState.Seedling;
    }

    // Fű növekedése, ha nincs nyúl a területen
    public void Grow()
    {
        if (State == GrassState.JustAte)
        {
            State = GrassState.Seedling;
        }
        else if (State == GrassState.Seedling)
        {
            State = GrassState.Young;
        }
        else if (State == GrassState.Young)
        {
            State = GrassState.Mature;
        }
    }

    // Nyúl legelése
    public void Eaten()
    { 
        State = GrassState.JustAte;
    }

    // Tápérték meghatározása
    public int GetNutritionalValue()
    {
        return State switch
        {
            GrassState.Mature => 2,
            GrassState.Young => 1,
            _ => 0
        };
    }
}
