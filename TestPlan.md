# Tesztelési terv

### **Simulator Tesztelési Terv**

Ez a tesztelési terv bemutatja a **Simulator** könyvtárhoz kapcsolódó tesztelési stratégiát és a már elkészített tesztek alapján történő vizsgálat céljait. A cél, hogy a nyulak, rókák és fű viselkedésének, valamint a szimuláció stabilitásának minden szempontját alaposan teszteljük, biztosítva, hogy a kód helyesen működjön különböző szituációkban.

* * *

## **Célok**

A tesztelés fő célja a **Simulator** könyvtárban található különböző komponensek (nyulak, rókák, fű) helyes működésének ellenőrzése. A tesztelés kiterjed az alábbiakra:

1. **Kezdeti állapotok ellenőrzése**: Nyulak és rókák megfelelő elhelyezése a szimuláció kezdetén.
2. **Entitások viselkedése**: Mozgás, táplálkozás, éhezés és szaporodás helyes működése.
3. **Szimuláció dinamikája**: Populációk egyensúlya, kihalások és szimuláció eredményei.
4. **Eredménykezelés**: A szimuláció végén megjelenő nyertes entitás (nyulak, rókák vagy fű) meghatározása.
5. **Fű regenerálódása**: A fű növekedésének és regenerálódásának ellenőrzése.

* * *

## **Tesztelési Típusok**

1. **Egységtesztek (Unit Tests)**
2. Minden funkciót külön-külön tesztelünk, hogy ellenőrizzük a helyes működést.
3. **Integrációs tesztek**
4. Teszteljük a különböző komponensek interakcióit (nyulak, rókák, fű) a szimuláció során.

* * *

## **Tesztelési Eszközök**

*   **MSTest**: Az MSTest keretrendszert használjuk a tesztek írásához és futtatásához.
*   **JetBrains Rider / Visual Studio**: A fejlesztési környezet és a tesztek futtatására szolgáló eszköz.

* * *

## **Tesztelési Kiterjedés**

1. **Entitások kezdeti elhelyezése**
    *   **Cél**: Győződjünk meg arról, hogy a nyulak és rókák a megfelelő mennyiségben és helyen kerülnek elhelyezésre a rácson.
    *   **Kapcsolódó tesztek**:
        *   `TestRabbitAndFoxInitialization()`
2. **Mozgás és viselkedés**
    *   **Cél**: Ellenőrizzük, hogy a nyulak és rókák helyesen mozognak a rácson, táplálkoznak és szaporodnak.
    *   **Kapcsolódó tesztek**:
        *   `TestNextTurnUpdatesGrid()`
        *   `TestValidMovement()`
3. **Szaporodás**
    *   **Cél**: Bizonyosodjunk meg arról, hogy a nyulak és rókák megfelelően szaporodnak, amikor elérik a szaporodási küszöböt.
    *   **Kapcsolódó tesztek**:
        *   `TestRabbitReproduction()`
        *   `TestFoxReproduction()`
4. **Éhezés és kihalás**
    *   **Cél**: Teszteljük, hogy a nyulak és rókák helyesen éheznek el, ha nem találnak elég táplálékot, és ennek következtében elpusztulnak.
    *   **Kapcsolódó tesztek**:
        *   `TestRabbitStarvation()`
        *   `TestFoxStarvation()`
5. **Fű növekedése**
    *   **Cél**: Ellenőrizzük, hogy a fű megfelelően regenerálódik több körön keresztül, ha nincs lelegelve.
    *   **Kapcsolódó tesztek**:
        *   `TestGrassRegrowth()`
        *   `TestGrassRegrowthMultipleTurns()`
6. **Szimulációs eredmények**
    *   **Cél**: Ellenőrizzük, hogy a szimuláció végén helyesen jelenik meg, melyik entitás dominál (nyulak, rókák vagy fű).
    *   **Kapcsolódó teszt**:
        *   `TestSimulationResult()`

* * *

## **Tesztelési Ütemezés**

1. **Kezdeti tesztelés**:
    *   A szimulációs komponensek helyes működésének ellenőrzése.
    *   Tesztek: `TestRabbitAndFoxInitialization()`, `TestNextTurnUpdatesGrid()`, `TestValidMovement()`
2. **Viselkedési tesztek**:
    *   Ellenőrizzük az entitások mozgását, táplálkozását és szaporodását.
    *   Tesztek: `TestRabbitReproduction()`, `TestFoxReproduction()`, `TestRabbitStarvation()`, `TestFoxStarvation()`
3. **Populáció dinamikájának tesztelése**:
    *   Hosszabb távú tesztek a populációk fennmaradásának ellenőrzésére.
    *   Tesztek: `TestSimulationResult()`
4. **Eredmények ellenőrzése**:
    *   Biztosítsuk, hogy a megfelelő eredmények jelenjenek meg a szimuláció végén.
    *   Teszt: `TestSimulationResult()`

* * *

## **Tesztelési Kritériumok**

*   **Sikeres tesztelés**:
    *   Minden teszt sikeresen lefut és az eredmények megfelelnek az elvárt viselkedésnek.
    *   A nyulak, rókák és fű megfelelően működnek, a szimuláció stabil.
*   **Sikertelen tesztelés**:
    *   Ha a tesztek közül bármelyik hibát jelez, vissza kell térni a forráskódhoz, és módosítani kell a hibás logikát.
    *   Különös figyelmet kell fordítani az éhezési és szaporodási tesztekre, mivel ezek kritikusak a szimuláció működése szempontjából.

* * *

## **Kapcsolódó tesztek és funkciók**

### **Entitások inicializálása**:

*   **TestRabbitAndFoxInitialization()**
    *   Biztosítja, hogy a nyulak és rókák a megfelelő számú és pozíciójú mezőkön legyenek elhelyezve a szimuláció kezdetén.

### **Szimulációs körök**:

*   **TestNextTurnUpdatesGrid()**
    *   Ellenőrzi, hogy minden kör végén frissülnek az entitások és történnek változások a rácson.

### **Entitások mozgása**:

*   **TestValidMovement()**
    *   Ellenőrzi, hogy a nyulak és rókák helyesen mozognak, nem lépnek ki a rácsból, és csak szomszédos mezőkre mozdulnak.

### **Szaporodás**:

*   **TestRabbitReproduction()** és **TestFoxReproduction()**
    *   Ellenőrzi, hogy a nyulak és rókák a megfelelő szinten képesek-e szaporodni.

### **Éhezés**:

*   **TestRabbitStarvation()** és **TestFoxStarvation()**
    *   Biztosítja, hogy az entitások éheznek és elpusztulnak, ha nem találnak táplálékot.

### **Fű növekedése**:

*   **TestGrassRegrowth()** és **TestGrassRegrowthMultipleTurns()**
    *   Teszteli, hogy a fű megfelelően növekszik és regenerálódik, ha nincs nyúl, amely lelegelné.

* * *

## **Tesztelési jelentés**

Minden teszt eredményei egy tesztelési jelentésben lesznek összegyűjtve. A jelentés tartalmazza a sikeres és sikertelen teszteket, valamint a sikertelen tesztek esetében a hibák részleteit és azok kijavítására irányuló javaslatokat.

* * *

Ez a **Simulator Tesztelési Terv** biztosítja, hogy a szimuláció minden komponense helyesen működjön, és stabil maradjon különböző forgatókönyvekben. A terv célja, hogy az összes fontos funkció alapos tesztelésen essen át, és a kód megfelelő minőségű és hibamentes legyen.

##