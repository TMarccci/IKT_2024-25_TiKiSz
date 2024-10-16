using Simulator_Lib;

// Szimuláció Paraméterei
int width = 10;
int height = 10;
int numberOfTurns = 50;
int rabbitCount = 7;
int foxCount = 3;
double? delay = 0.5; // seconds until next turn

// Nyulak tulajdonságainak beállítása
AnimalProperties rabbitProperties = new AnimalProperties
{
    MaxFullness = 5,
    ReproductionFullness = 3,
    HungerRate = 1,
    FoodValue = 1  // A fű tápértéke
};

// Rókák tulajdonságainak beállítása
AnimalProperties foxProperties = new AnimalProperties
{
    MaxFullness = 10,
    ReproductionFullness = 9,
    HungerRate = 2,
    FoodValue = 3  // A nyúl tápértéke
};

// Rácsrendszer inicializálása
Grid grid = new Grid(width, height, rabbitProperties, foxProperties);

// Populáció inicializálása (például 5 nyúl és 1 róka)
grid.PopulateGrid(rabbitCount, foxCount);

// Szimuláció futtatása
for (int turn = 1; turn <= numberOfTurns; turn++)
{
    Console.WriteLine($"Kör {turn}/{numberOfTurns}");
    
    // Rács megjelenítése a konzolon
    DisplayGrid(grid);
    
    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
    if (delay.HasValue)
    {
        Thread.Sleep((int)(delay.Value * 1000));
    }
    else
    {
        // Várakozás a következő kör előtt
        Console.ReadKey();
    }
    
    // Szimuláció következő körének végrehajtása
    grid.NextTurn();   
}
        

// A rácsrendszer megjelenítése a konzolon
static void DisplayGrid(Grid grid)
{
    for (int y = 0; y < grid.Height; y++)
    {
        for (int x = 0; x < grid.Width; x++)
        {
            var cell = grid.GetCell(x, y);

            if (cell.Fox != null)
            {
                Console.Write("🦊 "); // Róka
            }
            else if (cell.Rabbit != null)
            {
                Console.Write("🐰 "); // Nyúl
            }
            else if (cell.Grass.State == Grass.GrassState.Mature)
            {
                Console.Write("🍀 "); // Kifejlett fű
            }
            else if (cell.Grass.State == Grass.GrassState.Young)
            {
                Console.Write("☘️ "); // Zsenge fű
            }
            else
            {
                Console.Write("🌱 "); // Fűkezdemény vagy üres mező
            }
        }
        Console.WriteLine();
    }
}
