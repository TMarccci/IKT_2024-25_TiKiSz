# Simulator_Lib

A **Simulator_Lib** egy rács-alapú szimulációs könyvtár, amely lehetővé teszi a nyulak, rókák és fű interakcióinak modellezését. A könyvtár célja, hogy demonstrálja az ökológiai dinamikát és a populációk kölcsönhatásait.

## Tartalomjegyzék

- [Rendszerkövetelmények](#rendszerkövetelmények)
- [Funkcionalitás](#funkcionalitás)
- [Telepítés és beállítás](#telepítés-és-beállítás)
- [Szimuláció futtatása](#szimuláció-futtatása)
- [Használati példák](#használati-példák)
- [Tesztelési terv](#tesztelési-terv)
- [Hozzájárulás](#hozzájárulás)
- [Licenc](#licenc)

## Rendszerkövetelmények

A szimuláció futtatásához a következő rendszerkövetelmények szükségesek:

- **Operációs rendszer**: Windows, macOS vagy Linux
- **.NET SDK**: .NET 8.0 vagy újabb verzió
- **Fejlesztőkörnyezet**: Visual Studio 2022+, JetBrains Rider vagy bármely más .NET támogatott IDE
- **Internet kapcsolat**: NuGet csomagok telepítéséhez

## Funkcionalitás

- **Rács létrehozása**: A rács méretének és a kezdeti populációknak a megadása.
- **Entitások mozgása**: Nyulak és rókák mozognak a rácson, és kölcsönhatásba lépnek a fűvel.
- **Táplálkozás és éhezés**: Az állatok táplálkoznak, és éhezés következtében elpusztulhatnak.
- **Szaporodás**: A nyulak és rókák szaporodnak, ha elérik a szaporodáshoz szükséges jóllakottsági szintet.
- **Fű növekedése**: A fű növekszik, és regenerálódik, ha nem legelik le.

## Telepítés és beállítás

1. **Letöltés**: Klónozd vagy töltsd le a projektet.
   ```bash
   git clone https://github.com/TMarccci/IKT_2024-25_TiKiSz.git
   ```
   
2. **Telepítés**: Használj NuGet csomagkezelőt a szükséges csomagok telepítésére.
   ```bash
   dotnet add package MSTest.TestFramework
   dotnet add package MSTest.TestAdapter
   ```

3. **Futtatás**: Használj egy fejlesztőkörnyezetet (pl. Visual Studio vagy JetBrains Rider) a szimuláció futtatásához, vagy a parancssorból is indíthatod a szimulációt.
   ```bash
   dotnet run --project Simulator_Program
   ```

## Szimuláció futtatása

A **Program.cs** fájlban állíthatod be a szimuláció paramétereit:

```csharp
int width = 20;            // Rács szélessége
int height = 20;           // Rács magassága
int numberOfTurns = 10;    // A szimuláció hossza (körök száma)
int numberOfSimulations = 3; // Hány szimuláció fusson egymás után
int rabbitCount = 14;      // Kezdeti nyulak száma
int foxCount = 8;          // Kezdeti rókák száma
double? delay = 2;         // A körök közötti várakozás másodpercben
```

### Rács megjelenítése

A rács állapota a konzolon jelenik meg. Különböző szimbólumok jelzik a nyulakat, rókákat és a fű különböző állapotait.

## Használati példák

### Szimuláció futtatása 10x10-es rácson 20 körön keresztül

```csharp
Grid grid = new Grid(10, 10, rabbitProperties, foxProperties);
grid.PopulateGrid(5, 2);

// Szimuláció futtatása
for (int turn = 0; turn < numberOfTurns; turn++)
{
    grid.NextTurn();
}
```

### Testreszabott tulajdonságokkal futtatott szimuláció

```csharp
AnimalProperties rabbitProperties = new AnimalProperties
{
    MaxFullness = 6,
    ReproductionFullness = 4,
    HungerRate = 2,
    FoodValue = 2
};

AnimalProperties foxProperties = new AnimalProperties
{
    MaxFullness = 12,
    ReproductionFullness = 10,
    HungerRate = 3,
    FoodValue = 4
};

Grid grid = new Grid(15, 15, rabbitProperties, foxProperties);
grid.PopulateGrid(8, 4);
```

## Tesztelési terv

A **Simulator_Lib** könyvtár teszteléséhez MSTest keretrendszert használunk. A tesztelés célja a következők ellenőrzése:

1. **Kezdeti állapotok**: Nyulak és rókák megfelelő elhelyezése a rácson.
2. **Entitások viselkedése**: Mozgás, táplálkozás, éhezés és szaporodás helyes működése.
3. **Szimuláció dinamikája**: Populációk egyensúlya, kihalások és szimuláció eredményei.
4. **Eredménykezelés**: A szimuláció végén megjelenő nyertes entitás meghatározása.

A tesztelési részleteket és a kapcsolódó teszteket a **Tesztelési terv** részben találod.

## Hozzájárulás

Hozzájárulásokat szívesen fogadunk! Kérjük, hogy a javasolt változtatásokat a következő lépések szerint küldd el:

1. Forkold a repót.
2. Készíts új branchet a változtatásokhoz.
3. Készíts pull requestet a fő ágra.

## Licenc

Ez a projekt MIT licenc alatt áll. A licenc részleteit a **LICENSE** fájlban találod.
