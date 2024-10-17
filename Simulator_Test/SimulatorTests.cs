using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulator_Lib;

namespace Simulator_Test;

[TestClass]
public class SimulatorTests
{
    private Grid grid;

    [TestInitialize]
    public void Initialize()
    {
        // Nyulak tulajdonságai
        AnimalProperties rabbitProperties = new AnimalProperties
        {
            MaxFullness = 5,
            ReproductionFullness = 3,
            HungerRate = 1,
            FoodValue = 1
        };

        // Rókák tulajdonságai
        AnimalProperties foxProperties = new AnimalProperties
        {
            MaxFullness = 10,
            ReproductionFullness = 9,
            HungerRate = 2,
            FoodValue = 3
        };

        // Rács létrehozása
        grid = new Grid(20, 20, rabbitProperties, foxProperties);
    }

    [TestMethod]
    public void TestRabbitAndFoxInitialization()
    {
        // Nyulak és rókák elhelyezése a rácson
        grid.PopulateGrid(rabbitCount: 14, foxCount: 8);

        // Ellenőrizzük, hogy a megfelelő számú nyúl és róka lett elhelyezve
        int rabbitCount = grid.CountThis(Grid.Entities.Rabbit);
        int foxCount = grid.CountThis(Grid.Entities.Fox);

        Assert.AreEqual(14, rabbitCount, "A nyulak száma nem megfelelő.");
        Assert.AreEqual(8, foxCount, "A rókák száma nem megfelelő.");
    }

    [TestMethod]
    public void TestNextTurnUpdatesGrid()
    {
        // Nyulak és rókák elhelyezése
        grid.PopulateGrid(rabbitCount: 14, foxCount: 8);

        // Készítünk egy mentést az aktuális állapotról
        int initialRabbitCount = grid.CountThis(Grid.Entities.Rabbit);
        int initialFoxCount = grid.CountThis(Grid.Entities.Fox);

        // Kör lefuttatása
        grid.NextTurn();

        // Ellenőrizzük, hogy történtek változások (mozogtak a nyulak, rókák vadásztak, stb.)
        int updatedRabbitCount = grid.CountThis(Grid.Entities.Rabbit);
        int updatedFoxCount = grid.CountThis(Grid.Entities.Fox);

        Assert.IsTrue(updatedRabbitCount != initialRabbitCount || updatedFoxCount != initialFoxCount, 
                      "A kör végén nem történt változás a rácson.");
    }

    [TestMethod]
    public void TestGrassRegrowth()
    {
        // Nyulak és rókák elhelyezése a rácson
        grid.PopulateGrid(rabbitCount: 0, foxCount: 0);  // Nem helyezünk el állatokat

        // Ellenőrizzük a kezdeti állapotot (minden mező üres, fűnövekedés elkezdődik)
        for (int i = 0; i < 3; i++)
        {
            grid.NextTurn(); // 3 kört futtatunk le
        }

        int matureGrassCount = 0;
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                var cell = grid.GetCell(x, y);
                if (cell.Grass.State == Grass.GrassState.Mature)
                {
                    matureGrassCount++;
                }
            }
        }

        Assert.IsTrue(matureGrassCount > 0, "A fű nem nőtt kifejlett állapotba a körök alatt.");
    }

    [TestMethod]
    public void TestSimulationResult()
    {
        // Eredmények inicializálása
        Result result = new Result();

        // Nyulak és rókák elhelyezése
        grid.PopulateGrid(rabbitCount: 14, foxCount: 8);

        // Néhány kör lefuttatása
        for (int turn = 0; turn < 5; turn++)
        {
            grid.NextTurn();
        }

        // Eredmény meghatározása
        result.DetermineResult(grid);

        // Ellenőrizzük, hogy megfelelő eredmény lett meghatározva
        string resultString = result.ToString();
        Assert.IsTrue(resultString.Contains("Rabbit") || resultString.Contains("Fox") || resultString.Contains("Extinction"),
                      "A szimuláció eredménye nem megfelelő.");
    }
    
    [TestMethod]
    public void TestRabbitReproduction()
    {
        // Nyulak és rókák elhelyezése
        grid.PopulateGrid(rabbitCount: 1, foxCount: 0);

        // Kör lefuttatása, hogy a nyúl szaporodni tudjon
        for (int turn = 0; turn < 10; turn++)
        {
            grid.NextTurn();
        }

        // Ellenőrizzük, hogy legalább egy új nyúl született
        int rabbitCount = grid.CountThis(Grid.Entities.Rabbit);
        Assert.IsTrue(rabbitCount > 1, "A nyulak nem szaporodtak megfelelően.");
    }

    [TestMethod]
    public void TestGrassRegrowthMultipleTurns()
    {
        // Fű elhelyezése nyulak és rókák nélkül
        grid.PopulateGrid(rabbitCount: 0, foxCount: 0);

        // Kör lefuttatása a fű növekedéséhez
        for (int turn = 0; turn < 10; turn++)
        {
            grid.NextTurn();
        }

        // Ellenőrizzük, hogy minden mezőn legalább zsenge fű van
        int matureGrassCount = 0;
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                var cell = grid.GetCell(x, y);
                if (cell.Grass.State == Grass.GrassState.Mature)
                {
                    matureGrassCount++;
                }
            }
        }

        Assert.IsTrue(matureGrassCount > 0, "A fű nem regenerálódott megfelelően.");
    }

    
}