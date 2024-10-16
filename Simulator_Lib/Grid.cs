namespace Simulator_Lib;

public class Grid
{
    private readonly int width;
    private readonly int height;
    private readonly Cell[,] cells;
    
    public int Width => width;
    public int Height => height;

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;
        cells = new Cell[width, height];
        InitializeCells();
    }

    private void InitializeCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new Cell();
            }
        }
    }
    
    // Véletlenszerű elhelyezés nyulak és rókák számára
    public void PopulateGrid(int rabbitCount, int foxCount)
    {
        Random random = new Random();

        // Véletlenszerűen helyezünk el nyulakat
        for (int i = 0; i < rabbitCount; i++)
        {
            bool placed = false;
            while (!placed)
            {
                int x = random.Next(width);
                int y = random.Next(height);
                if (cells[x, y].IsEmpty()) // Ha a mező üres, elhelyezzük a nyulat
                {
                    cells[x, y].Rabbit = new Rabbit();
                    placed = true;
                }
            }
        }

        // Véletlenszerűen helyezünk el rókákat
        for (int i = 0; i < foxCount; i++)
        {
            bool placed = false;
            while (!placed)
            {
                int x = random.Next(width);
                int y = random.Next(height);
                if (cells[x, y].IsEmpty()) // Ha a mező üres, elhelyezzük a rókát
                {
                    cells[x, y].Fox = new Fox();
                    placed = true;
                }
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        return cells[x, y];
    }
    
    public void NextTurn()
    {
        // 1. Nyulak mozgása és táplálkozása
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell = cells[x, y];

                if (cell.HasRabbit())
                {
                    Rabbit rabbit = cell.Rabbit;

                    // Nyúl táplálkozik
                    rabbit.Eat(cell.Grass);

                    // Nyúl éhezik
                    rabbit.Hunger();

                    // Ha elpusztult, eltávolítjuk
                    if (!rabbit.IsAlive)
                    {
                        cell.Rabbit = null;
                        continue;
                    }

                    // Nyúl mozog, ha szükséges
                    MoveRabbit(x, y);
                }
            }
        }

        // 2. Rókák mozgása és táplálkozása
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell = cells[x, y];

                if (cell.HasFox())
                {
                    Fox fox = cell.Fox;

                    // Róka táplálkozik, ha van nyúl a közelben
                    if (!EatRabbitIfNearby(x, y, fox))
                    {
                        // Ha nem talált nyulat, mozogjon
                        MoveFox(x, y);
                    }

                    // Róka éhezik
                    fox.Hunger();

                    // Ha elpusztult, eltávolítjuk
                    if (!fox.IsAlive)
                    {
                        cell.Fox = null;
                    }
                }
            }
        }

        // 3. Fű növekedése minden mezőn
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell = cells[x, y];
                if (!cell.HasRabbit()) // Ha nincs nyúl, a fű nőhet
                {
                    cell.Grass.Grow();
                }
            }
        }

        // 4. Nyulak és rókák szaporodása
        ReproduceRabbits();
        ReproduceFoxes();
    }

    private void ReproduceRabbits()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell = cells[x, y];
                if (cell.HasRabbit() && cell.Rabbit.CanReproduce())
                {
                    List<(int, int)> availableCells = GetAvailableAdjacentCells(x, y, true);
                    if (availableCells.Count > 0)
                    {
                        Random random = new Random();
                        (int newX, int newY) = availableCells[random.Next(availableCells.Count)];
                        cells[newX, newY].Rabbit = new Rabbit();
                    }
                }
            }
        }
    }

    private void ReproduceFoxes()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell = cells[x, y];
                if (cell.HasFox() && cell.Fox.CanReproduce())
                {
                    List<(int, int)> availableCells = GetAvailableAdjacentCells(x, y, true);
                    if (availableCells.Count > 0)
                    {
                        Random random = new Random();
                        (int newX, int newY) = availableCells[random.Next(availableCells.Count)];
                        cells[newX, newY].Fox = new Fox();
                    }
                }
            }
        }
    }

    // Mozgatási logika nyulakhoz
    private void MoveRabbit(int x, int y)
    {
        Rabbit rabbit = cells[x, y].Rabbit;

        List<(int, int)> availableCells = GetAvailableAdjacentCells(x, y, true); // Csak üres mezők

        if (availableCells.Count > 0)
        {
            (int newX, int newY) = GetBestCellForRabbit(availableCells);
            cells[newX, newY].Rabbit = rabbit;
            cells[x, y].Rabbit = null;
        }
    }

    private (int, int) GetBestCellForRabbit(List<(int, int)> availableCells)
    {
        foreach (var (x, y) in availableCells)
        {
            if (cells[x, y].Grass.State == Grass.GrassState.Mature)
            {
                return (x, y);
            }
        }
        Random random = new Random();
        return availableCells[random.Next(availableCells.Count)];
    }

    // Mozgatási logika rókákhoz
    private bool EatRabbitIfNearby(int x, int y, Fox fox)
    {
        List<(int, int)> nearbyRabbits = GetNearbyRabbits(x, y, 2);
        
        if (nearbyRabbits.Count > 0)
        {
            (int rabbitX, int rabbitY) = nearbyRabbits[0];
            fox.Eat(cells[rabbitX, rabbitY].Rabbit);
            cells[rabbitX, rabbitY].Rabbit = null;
            cells[rabbitX, rabbitY].Fox = fox;
            cells[x, y].Fox = null;
            return true;
        }
        return false;
    }

    private void MoveFox(int x, int y)
    {
        Fox fox = cells[x, y].Fox;

        List<(int, int)> availableCells = GetAvailableAdjacentCells(x, y, true); // Csak üres mezők

        if (availableCells.Count > 0)
        {
            Random random = new Random();
            (int newX, int newY) = availableCells[random.Next(availableCells.Count)];
            cells[newX, newY].Fox = fox;
            cells[x, y].Fox = null;
        }
    }

    private List<(int, int)> GetAvailableAdjacentCells(int x, int y, bool emptyOnly)
    {
        List<(int, int)> availableCells = new List<(int, int)>();

        // Szomszédos mezők ellenőrzése itt (pl. [x-1, y], [x+1, y], stb.)

        return availableCells;
    }

    private List<(int, int)> GetNearbyRabbits(int x, int y, int range)
    {
        List<(int, int)> nearbyRabbits = new List<(int, int)>();

        // Közeli nyulak keresése itt (pl. adott sugarú körben)

        return nearbyRabbits;
    }

    public class Cell
    {
        public Grass Grass { get; set; }
        public Rabbit Rabbit { get; set; }
        public Fox Fox { get; set; }

        public Cell()
        {
            Grass = new Grass();
        }

        public bool IsEmpty()
        {
            return Rabbit == null && Fox == null;
        }

        public bool HasRabbit()
        {
            return Rabbit != null;
        }

        public bool HasFox()
        {
            return Fox != null;
        }
    }
}
