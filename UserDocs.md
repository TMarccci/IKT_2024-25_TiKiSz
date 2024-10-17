# Felhasználói dokumentáció

### Simulator Felhasználói Dokumentáció

Ez a dokumentáció bemutatja, hogyan kell használni a **Simulator** könyvtárat egy rácsalapú szimuláció létrehozására, ahol nyulak, rókák és fű interakciói zajlanak. A szimuláció során az állatok mozognak, táplálkoznak, szaporodnak és éheznek a rácson belül, és a szimulációs körök végén eredményt kapunk.

* * *

## Tartalomjegyzék

1. [Telepítés és beállítás](http://#telep%C3%ADt%C3%A9s-%C3%A9s-be%C3%A1ll%C3%ADt%C3%A1s)
2. [Szimuláció futtatása](http://#szimul%C3%A1ci%C3%B3-futtat%C3%A1sa)
    *   [Program paraméterek](http://#program-param%C3%A9terek)
    *   [Rács megjelenítése](http://#r%C3%A1cs-megjelen%C3%ADt%C3%A9se)
    *   [Szimulációs körök](http://#szimul%C3%A1ci%C3%B3s-k%C3%B6r%C3%B6k)
    *   [Eredmények](http://#eredm%C3%A9nyek)
3. [Nyulak és rókák tulajdonságainak testreszabása](http://#nyulak-%C3%A9s-r%C3%B3k%C3%A1k-tulajdons%C3%A1gainak-testreszab%C3%A1sa)
4. [Használati példák](http://#haszn%C3%A1lati-p%C3%A9ld%C3%A1k)
5. [Gyakori hibák](http://#gyakori-hib%C3%A1k)

* * *

## Telepítés és beállítás

A szimuláció használatához a következő lépésekre van szükség:

1. **Letöltés**: Szerezd be a forráskódot vagy a binárisokat.
2. **Projekt felépítése**: Biztosítsd, hogy a projekt tartalmazza a következő fájlokat:
    *   `Program.cs`
    *   `Grid.cs`
    *   `Fox.cs`
    *   `Rabbit.cs`
    *   `Grass.cs`
    *   `AnimalProperties.cs`
    *   `Result.cs`
3. **Futtatás**: Használj egy fejlesztőkörnyezetet (pl. Visual Studio vagy JetBrains Rider), vagy a parancssorból is futtathatod a szimulációt a `dotnet run` paranccsal.

* * *

## Szimuláció futtatása

### Program paraméterek

A szimuláció paraméterei a **Program.cs** fájlban vannak meghatározva. Ezeket a paramétereket módosítva befolyásolhatod a szimuláció menetét.

Például:

```cs
int width = 20;              // Rács szélessége
int height = 20;             // Rács magassága
int numberOfTurns = 10;      // A szimuláció hossza (körök száma)
int numberOfSimulations = 3; // Hány szimuláció fusson egymás után
int rabbitCount = 14;        // Kezdeti nyulak száma
int foxCount = 8;            // Kezdeti rókák száma
double? delay = 2;           // A körök közötti várakozás másodpercben
```

*   **Rács mérete**: A `width` és `height` paraméterek határozzák meg a rács méretét.
*   **Szimuláció hossza**: A `numberOfTurns` határozza meg, hány körig fut a szimuláció.
*   **Kezdeti populáció**: A `rabbitCount` és `foxCount` paraméterek határozzák meg, hány nyúl és róka kezd a szimulációban.
*   **Várakozás**: A `delay` értéke határozza meg, mennyi idő telik el a körök között.

### Rács megjelenítése

A rács konzolon jelenik meg. Minden szimbólum különböző entitásokat jelöl:

*   **🦊** – Róka
*   **🐰** – Nyúl
*   **🍀** – Kifejlett fű
*   **☘️** – Zsenge fű
*   **🌱** – Fűkezdemény vagy üres mező

Minden kör végén a program kirajzolja a rács aktuális állapotát.

### Szimulációs körök

A program a szimuláció során minden körben végrehajtja a következőket:

1. **Mozgás**: A nyulak és rókák mozognak a rácson.
2. **Táplálkozás**: A nyulak füvet esznek, a rókák nyulakat vadásznak.
3. **Éhezés**: Az állatok minden körben éheznek, és ha nem találnak ételt, elpusztulnak.
4. **Szaporodás**: Ha a nyulak vagy rókák elérik a szaporodáshoz szükséges jóllakottsági szintet, új egyedeket hoznak létre.
5. **Fű növekedése**: A fű minden körben növekszik, ha nem legelték le.

### Eredmények

A szimuláció végén a **Result** objektum meghatározza, hogy melyik entitás volt a domináns (nyulak, rókák vagy fű). Az eredmények a következők lehetnek:

*   **Rabbit** – A nyulak domináltak.
*   **Fox** – A rókák domináltak.
*   **Extinction** – Mindkét állat kihalt, és a fű dominált.

Az eredmények a következő formában jelennek meg:

```yaml
At the end:
Rabbit: X, Fox: Y, Extinction: Z
```

* * *

## Nyulak és rókák tulajdonságainak testreszabása

A nyulak és rókák tulajdonságait az **AnimalProperties** osztály definiálja. Ezek a tulajdonságok könnyen módosíthatók a szimuláció testreszabásához.

Például:

```cs
AnimalProperties rabbitProperties = new AnimalProperties
{
    MaxFullness = 5,                // Nyulak maximális jóllakottsága
    ReproductionFullness = 3,       // Jóllakottsági szint szaporodáshoz
    HungerRate = 1,                 // Éhezési ütem (minden körben csökken)
    FoodValue = 1                   // A fű tápértéke
};

AnimalProperties foxProperties = new AnimalProperties
{
    MaxFullness = 10,               // Rókák maximális jóllakottsága
    ReproductionFullness = 9,       // Jóllakottsági szint szaporodáshoz
    HungerRate = 2,                 // Éhezési ütem (gyorsabban éhezik, mint a nyúl)
    FoodValue = 3                   // A nyúl tápértéke a rókának
};
```

### Tulajdonságok:

*   **MaxFullness**: Az állat maximális jóllakottsági szintje.
*   **ReproductionFullness**: A szaporodáshoz szükséges jóllakottsági szint.
*   **HungerRate**: Mennyit veszít az állat minden körben a jóllakottságából.
*   **FoodValue**: Mennyi tápértéket nyer az állat, amikor eszik (nyulak esetében a fű, rókák esetében a nyúl).

* * *

## Használati példák

### 1\. Szimuláció futtatása 10x10-es rácson 20 körön keresztül

```cs
int width = 10;
int height = 10;
int numberOfTurns = 20;
int rabbitCount = 5;
int foxCount = 2;
```

### 2\. Testreszabott tulajdonságokkal futtatott szimuláció

```cs
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
```

* * *

## Gyakori hibák

### 1\. **Nem indul el a szimuláció**

*   Ellenőrizd, hogy minden szükséges paramétert helyesen állítottál-e be.

### 2\. **Nem jelenik meg semmi a konzolon**

*   Győződj meg arról, hogy a `DisplayGrid()` függvény megfelelően működik, és a rács állapota helyesen van frissítve minden körben.

### 3\. **Az állatok túl gyorsan kihalnak**

*   Lehet, hogy a **HungerRate** túl magas. Próbáld csökkenteni, hogy az állatok lassabban éhezzenek el.

### 4\. **Túl sok nyúl vagy róka van**

*   Csökkentsd a kezdeti **rabbitCount** vagy **foxCount** értékeit, illetve növeld a **ReproductionFullness** értéket.