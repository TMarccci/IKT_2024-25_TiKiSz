namespace Simulator_Lib;

public class Grid
{
    private readonly int _width;
    private readonly int _height;
    private readonly Cell[,] _cells;
    private readonly AnimalProperties rabbitProperties;
    private readonly AnimalProperties foxProperties;
    
    public int Width => _width;
    public int Height => _height;

    public Grid(int width, int height, AnimalProperties rabbitProperties, AnimalProperties foxProperties)
    {
        this._width = width;
        this._height = height;
        this.rabbitProperties = rabbitProperties;
        this.foxProperties = foxProperties;
        _cells = new Cell[width, height];
        InitializeCells();
    }

    private void InitializeCells()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _cells[x, y] = new Cell();
            }
        }
    }
    
    // Véletlenszerű elhelyezés nyulak és rókák számára
    public void PopulateGrid(int rabbitCount, int foxCount)
    {
        Random random = new Random();

        for (int i = 0; i < rabbitCount; i++)
        {
            bool placed = false;
            while (!placed)
            {
                int x = random.Next(_width);
                int y = random.Next(_height);
                if (_cells[x, y].IsEmpty())
                {
                    _cells[x, y].Rabbit = new Rabbit(rabbitProperties); // Állat tulajdonságok átadása
                    placed = true;
                }
            }
        }

        for (int i = 0; i < foxCount; i++)
        {
            bool placed = false;
            while (!placed)
            {
                int x = random.Next(_width);
                int y = random.Next(_height);
                if (_cells[x, y].IsEmpty())
                {
                    _cells[x, y].Fox = new Fox(foxProperties); // Állat tulajdonságok átadása
                    placed = true;
                }
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        return _cells[x, y];
    }
    
    public void NextTurn()
    {
        // 1. Nyulak mozgása és táplálkozása
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var cell = _cells[x, y];

                if (cell.HasRabbit())
                {
                    Rabbit rabbit = cell.Rabbit;
                    rabbit.Eat(cell.Grass);
                    rabbit.Hunger();

                    if (!rabbit.IsAlive)
                    {
                        cell.Rabbit = null;
                        continue;
                    }

                    rabbit.Move(this, x, y);
                }
            }
        }

        // 2. Rókák mozgása és táplálkozása
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var cell = _cells[x, y];

                if (cell.HasFox())
                {
                    Fox fox = cell.Fox;
                    fox.Hunger();

                    if (!fox.IsAlive)
                    {
                        cell.Fox = null;
                        continue;
                    }

                    fox.Move(this, x, y);
                }
            }
        }

        // 3. Fű növekedése
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var cell = _cells[x, y];
                if (!cell.HasRabbit())
                {
                    cell.Grass.Grow();
                }
            }
        }

        // 4. Nyulak és rókák szaporodása (nem részletezett)
        ReproduceRabbits();
        ReproduceFoxes();
    }

    private void ReproduceRabbits()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var cell = _cells[x, y];
                if (cell.HasRabbit() && cell.Rabbit.CanReproduce())
                {
                    List<(int, int)> availableCells = GetAvailableAdjacentCells(x, y, true);
                    if (availableCells.Count > 0)
                    {
                        Random random = new Random();
                        (int newX, int newY) = availableCells[random.Next(availableCells.Count)];
                        _cells[newX, newY].Rabbit = new Rabbit(rabbitProperties);
                    }
                }
            }
        }
    }

    private void ReproduceFoxes()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var cell = _cells[x, y];
                if (cell.HasFox() && cell.Fox.CanReproduce())
                {
                    List<(int, int)> availableCells = GetAvailableAdjacentCells(x, y, true);
                    if (availableCells.Count > 0)
                    {
                        Random random = new Random();
                        (int newX, int newY) = availableCells[random.Next(availableCells.Count)];
                        _cells[newX, newY].Fox = new Fox(foxProperties);
                    }
                }
            }
        }
    }

    // Mozgatási logika nyulakhoz
    public void MoveRabbit(int x, int y)
    {
        Rabbit rabbit = _cells[x, y].Rabbit;

        List<(int, int)> availableCells = GetAvailableAdjacentCells(x, y, true); // Csak üres mezők

        if (availableCells.Count > 0)
        {
            (int newX, int newY) = GetBestCellForRabbit(availableCells);
            _cells[newX, newY].Rabbit = rabbit;
            _cells[x, y].Rabbit = null;
        }
    }

    private (int, int) GetBestCellForRabbit(List<(int, int)> availableCells)
    {
        foreach (var (x, y) in availableCells)
        {
            if (_cells[x, y].Grass.State == Grass.GrassState.Mature)
            {
                return (x, y);
            }
        }
        Random random = new Random();
        return availableCells[random.Next(availableCells.Count)];
    }

    // Mozgatási logika rókákhoz
    public bool EatRabbitIfNearby(int x, int y, Fox fox)
    {
        List<(int, int)> nearbyRabbits = GetNearbyRabbits(x, y, 2);
        
        if (nearbyRabbits.Count > 0)
        {
            (int rabbitX, int rabbitY) = nearbyRabbits[0];
            fox.Eat(_cells[rabbitX, rabbitY].Rabbit);
            _cells[rabbitX, rabbitY].Rabbit = null;
            _cells[rabbitX, rabbitY].Fox = fox;
            _cells[x, y].Fox = null;
            return true;
        }
        return false;
    }

    public void MoveFox(int x, int y)
    {
        Fox fox = _cells[x, y].Fox;

        List<(int, int)> availableCells = GetAvailableAdjacentCells(x, y, true); // Csak üres mezők

        if (availableCells.Count > 0)
        {
            Random random = new Random();
            (int newX, int newY) = availableCells[random.Next(availableCells.Count)];
            _cells[newX, newY].Fox = fox;
            _cells[x, y].Fox = null;
        }
    }

    private List<(int, int)> GetAvailableAdjacentCells(int x, int y, bool emptyOnly)
    {
        List<(int, int)> availableCells = new List<(int, int)>();

        // Balra (x-1), ha van érvényes mező
        if (x > 0 && (!emptyOnly || _cells[x - 1, y].IsEmpty())) availableCells.Add((x - 1, y));

        // Jobbra (x+1), ha van érvényes mező
        if (x < _width - 1 && (!emptyOnly || _cells[x + 1, y].IsEmpty())) availableCells.Add((x + 1, y));

        // Fel (y-1), ha van érvényes mező
        if (y > 0 && (!emptyOnly || _cells[x, y - 1].IsEmpty())) availableCells.Add((x, y - 1));

        // Le (y+1), ha van érvényes mező
        if (y < _height - 1 && (!emptyOnly || _cells[x, y + 1].IsEmpty())) availableCells.Add((x, y + 1));

        return availableCells;
    }


    private List<(int, int)> GetNearbyRabbits(int x, int y, int range)
    {
        List<(int, int)> nearbyRabbits = new List<(int, int)>();

        // Ellenőrizzük a környező mezőket a megadott távolságon belül
        for (int dx = -range; dx <= range; dx++)
        {
            for (int dy = -range; dy <= range; dy++)
            {
                if (x + dx >= 0 && x + dx < _width && y + dy >= 0 && y + dy < _height)
                {
                    if (_cells[x + dx, y + dy].HasRabbit())
                    {
                        nearbyRabbits.Add((x + dx, y + dy));
                    }
                }
            }
        }

        return nearbyRabbits;
    }


    public class Cell
    {
        public Grass Grass { get; set; }
        public Rabbit? Rabbit { get; set; }
        public Fox? Fox { get; set; }

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
