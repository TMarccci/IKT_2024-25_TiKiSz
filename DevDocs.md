# Fejlesztői dokumentáció

### Simulator Fejlesztői Dokumentáció

Ez a dokumentáció részletes magyarázatot ad a **Simulator** könyvtárban található osztályokról és függvényekről. A könyvtár egy rács-alapú világban szimulálja a nyulak, rókák és fű közötti interakciókat, beleértve a mozgást, táplálkozást, szaporodást és túlélést több szimulációs körön keresztül.

* * *

## Tartalomjegyzék

1. Program.cs
2. Grid.cs
    *   Grid osztály
    *   Cell osztály
3. Grass.cs
4. Result.cs
5. Fox.cs
6. Rabbit.cs
7. AnimalProperty.cs

* * *

## Program.cs

Ez a fájl a szimuláció belépési pontja. Meghatározza a szimuláció paramétereit, inicializálja a rácsot, és több szimulációs kört futtat.

### Függvények:

*   **Main()**

Meghatározza a szimuláció paramétereit, majd egy ciklusban végrehajtja a megadott számú szimulációt és kört. A szimuláció végén kiírja az eredményeket a konzolra.

*   **DisplayGrid(Grid grid)**

A konzolon megjeleníti a rács aktuális állapotát. Különböző szimbólumokkal jelzi a nyulakat, rókákat és a különböző növekedési állapotban lévő füvet.

* * *

## Grid.cs

A **Grid.cs** fájl tartalmazza a rácsot kezelő fő logikát. A rács nyulakat, rókákat és füvet tartalmaz, és a szimuláció minden körében kezeli az entitások mozgását, táplálkozását, szaporodását és éhezését.

### Grid osztály

*   **Grid(int width, int height, AnimalProperties rabbitProperties, AnimalProperties foxProperties)**

Inicializálja a rácsot a megadott méretekkel, valamint a nyulak és rókák tulajdonságaival.

*   **InitializeCells()**

Létrehozza a rács minden egyes mezőjét (**Cell** objektumokat).

*   **PopulateGrid(int rabbitCount, int foxCount)**

Véletlenszerűen helyezi el a megadott számú nyulat és rókát a rácson.

*   **GetCell(int x, int y)**

Visszaadja a rács egy adott (x, y) koordinátáján lévő mezőt.

*   **NextTurn()**

Végrehajt egy szimulációs kört, amely magában foglalja a nyulak és rókák mozgását, táplálkozását, éhezését, valamint a fű növekedését.

*   **ReproduceRabbits()**

Ellenőrzi a nyulak szaporodási feltételeit, és új nyulakat hoz létre, ha van hely a rácson.

*   **ReproduceFoxes()**

Ellenőrzi a rókák szaporodási feltételeit, és új rókákat hoz létre, ha van hely a rácson.

*   **MoveRabbit(int x, int y)**

Mozgatja a nyulat az adott (x, y) pozícióból egy szomszédos üres mezőre, előnyben részesítve azokat a mezőket, ahol van fű.

*   **GetBestCellForRabbit(List<(int, int)> availableCells)**

Kiválasztja a legjobb szomszédos mezőt a nyulak számára, előnyben részesítve a kifejlett fűvel borított mezőket.

*   **EatRabbitIfNearby(int x, int y, Fox fox)**

Ellenőrzi, hogy van-e nyúl a róka közelében (legfeljebb 2 mező távolságban), és ha van, a róka megeszi azt.

*   **MoveFox(int x, int y)**

Mozgatja a rókát az adott (x, y) pozícióból egy szomszédos üres mezőre.

*   **GetAvailableAdjacentCells(int x, int y, bool emptyOnly)**

Visszaadja az adott (x, y) pozíció szomszédos mezőinek listáját. Ha az `emptyOnly` paraméter igaz, csak az üres mezőket adja vissza.

*   **GetNearbyRabbits(int x, int y, int range)**

Visszaadja az adott (x, y) pozíció környékén található nyulak koordinátáit a megadott távolságon belül.

*   **CountThis(Entities ent)**

Megszámolja a rácson lévő adott típusú entitásokat (nyulak, rókák, fű).

#### Cell osztály

A **Cell** osztály egy rácsmezőt reprezentál, amely tartalmazhat nyulat, rókát és füvet.

*   **Cell()**

Inicializál egy üres mezőt, amely füvet tartalmaz **Seedling** állapotban.

*   **IsEmpty()**

Ellenőrzi, hogy a mező üres-e (nincs rajta sem nyúl, sem róka).

*   **HasRabbit()**

Ellenőrzi, hogy van-e nyúl a mezőn.

*   **HasFox()**

Ellenőrzi, hogy van-e róka a mezőn.

* * *

## Grass.cs

A **Grass.cs** fájl kezeli a fű állapotait és növekedését. A fű lehet fűkezdemény, zsenge fű, kifejlett fű, és a nyulak lelegelhetik.

### Grass osztály

*   **Grass()**

Inicializálja a füvet **Seedling** állapotban.

*   **Grow()**

A fű növekedését kezeli. Ha a fűt lelegelték, visszaáll **Seedling** állapotba, különben tovább növekszik.

*   **Eaten()**

Ha egy nyúl lelegeli a füvet, annak állapota **JustAte** lesz, ami azt jelzi, hogy a növényzetet éppen megették.

*   **GetNutritionalValue()**

Visszaadja a fű tápértékét. A kifejlett fű több tápértéket ad a nyulaknak, mint a zsenge fű.

* * *

## Result.cs

A **Result.cs** fájl tartalmazza a szimuláció eredményének meghatározásához szükséges logikát. Az eredmény az egyes entitások (nyulak, rókák, fű) túlélését és dominanciáját vizsgálja.

### Result osztály

*   **Result()**

Inicializálja az eredményeket, kezdetben minden entitás száma 0.

*   **BumpRabbitEnd()**

Növeli a nyulak győzelmi számlálóját.

*   **BumpFoxEnd()**

Növeli a rókák győzelmi számlálóját.

*   **BumpGrassEnd()**

Növeli a fű dominanciájának számlálóját, ha a fű dominál a rácson.

*   **DetermineResult(Grid grid)**

Meghatározza, melyik entitás dominál a szimuláció végén, és ennek megfelelően növeli a megfelelő számlálót.

*   **ToString()**

Visszaadja az eredményt szöveges formában, amely tartalmazza a nyulak, rókák és fű dominanciájának végső állapotát.

* * *

## Fox.cs

A **Fox.cs** fájl tartalmazza a **Fox** osztályt, amely a rókák viselkedését, táplálkozását, éhezését és szaporodását kezeli a szimuláció során.

### Fox osztály

*   **Fox(AnimalProperties properties)**

Inicializál egy rókát az adott tulajdonságokkal (**AnimalProperties**), mint például a maximális jóllakottság, éhezési ütem és szaporodási küszöb.

*   **Eat(Rabbit rabbit)**

A róka megeszi a nyulat, és megnöveli a jóllakottsági szintjét. A róka a nyúlból származó tápértéket megkapja, de a jóllakottsága nem haladhatja meg a maximális értéket.

*   **Hunger()**

Csökkenti a róka jóllakottsági szintjét minden kör végén az éhezési ütem alapján. Ha a jóllakottság 0-ra csökken, a róka elpusztul.

*   **Move(Grid grid, int currentX, int currentY)**

A róka mozgását kezeli. Először megnézi, talál-e nyulat a közelben, és ha igen, odamegy és megeszi. Ha nincs nyúl a közelben, akkor egy üres szomszédos mezőre mozdul.

*   **CanReproduce()**

Ellenőrzi, hogy a róka elérte-e a szaporodáshoz szükséges jóllakottsági szintet. Ha igen, képes szaporodni.

*   **IsAlive**

Visszaadja, hogy a róka életben van-e (azaz a jóllakottsága nagyobb, mint 0).

* * *

## Rabbit.cs

A **Rabbit.cs** fájl tartalmazza a **Rabbit** osztályt, amely a nyulak viselkedését, táplálkozását, éhezését és szaporodását kezeli.

### Rabbit osztály

*   **Rabbit(AnimalProperties properties)**

Inicializál egy nyulat az adott tulajdonságokkal (**AnimalProperties**), például a maximális jóllakottsággal, éhezési ütemmel és szaporodási küszöbbel.

*   **Eat(Grass grass)**

A nyúl lelegeli a füvet, és megnöveli a jóllakottsági szintjét a fű tápértékével. Ha a fű kifejlett, több tápértéket nyújt, mint a zsenge fű.

*   **Hunger()**

Csökkenti a nyúl jóllakottsági szintjét minden kör végén. Ha a jóllakottság 0-ra csökken, a nyúl elpusztul.

*   **Move(Grid grid, int currentX, int currentY)**

A nyúl mozgását kezeli. Olyan szomszédos mezőre mozdul, ahol fű van, vagy ha nincs fű, akkor egy üres mezőt választ.

*   **CanReproduce()**

Ellenőrzi, hogy a nyúl elérte-e a szaporodáshoz szükséges jóllakottsági szintet. Ha igen, képes szaporodni.

*   **IsAlive**

Visszaadja, hogy a nyúl életben van-e (azaz a jóllakottsága nagyobb, mint 0).

* * *

## AnimalProperties.cs

Az **AnimalProperties.cs** fájl tartalmazza az **AnimalProperties** osztályt, amely a nyulak és rókák tulajdonságait definiálja. Ez az osztály segítségével könnyen testre szabható az állatok viselkedése a szimulációban.

### AnimalProperties osztály

*   **MaxFullness**

Meghatározza, hogy az állat (nyúl vagy róka) maximálisan mennyi jóllakottsági szinttel rendelkezhet. Ez az érték határozza meg, hogy mennyi táplálékot tud egyszerre tárolni.

*   **ReproductionFullness**

Meghatározza a szaporodási küszöböt, vagyis azt a jóllakottsági szintet, amelynél az állat képes szaporodni.

*   **HungerRate**

Meghatározza, hogy az állat milyen gyorsan éhezik el. Minden kör végén ennyi jóllakottsági szintet veszít.

*   **FoodValue**

Meghatározza, hogy az állat mennyi tápértéket kap egy-egy étkezés során. A nyulak esetében ez a fű tápértékét, a rókák esetében a nyúl tápértékét jelzi.