using Simulator_Lib;

// Szimuláció Paraméterei
int width = 10;
int height = 10;
int numberOfTurns = 20;
int rabbitCount = 5;
int foxCount = 5;
int? delay = null; // seconds until next turn

// Rácsrendszer inicializálása
Grid grid = new Grid(width, height);

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
        Thread.Sleep(delay.Value * 1000);
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