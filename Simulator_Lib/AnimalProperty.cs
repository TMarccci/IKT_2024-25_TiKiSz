namespace Simulator_Lib;

public class AnimalProperties
{
    public int MaxFullness { get; set; }          // Az állat maximális jóllakottsága
    public int ReproductionFullness { get; set; }  // Jóllakottsági szint a szaporodáshoz
    public int HungerRate { get; set; }            // Mennyit veszít a jóllakottságból minden körben
    public int FoodValue { get; set; }             // Az étel tápértéke (nyulak esetében a fű, rókák esetében a nyúl)
}
