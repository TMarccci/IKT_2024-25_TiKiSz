namespace Simulator_Lib;

public class Result
{
    private int RabbitEnd { get; set; }
    private int FoxEnd { get; set; }
    private int GrassEnd { get; set; }

    public Result()
    {
        RabbitEnd = 0;
        FoxEnd = 0;
        GrassEnd = 0;   
    }

    private void BumpRabbitEnd() => RabbitEnd++;
    private void BumpFoxEnd() => FoxEnd++;
    private void BumpGrassEnd() => GrassEnd++;

    public void DetermineResult(Grid grid)
    {
        int rabCount = grid.CountThis(Grid.Entities.Rabbit);
        int foxCount = grid.CountThis(Grid.Entities.Fox);
        int grassCount = grid.CountThis(Grid.Entities.Grass);

        if (rabCount > grassCount && rabCount > grassCount) BumpRabbitEnd();
        else if (foxCount > grassCount && foxCount > rabCount) BumpFoxEnd();
        else if (grassCount > foxCount && grassCount > rabCount) BumpGrassEnd();
    }
    
    public override string ToString() => $"\nAt the end:\nRabbit: {RabbitEnd}, Fox: {FoxEnd}, Extinction: {GrassEnd}";
}