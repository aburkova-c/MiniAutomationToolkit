namespace MiniAutomationToolkit.Core.Simulations;

public class LongOperationSimulator
{
    public string LongOperation()
    {
        Thread.Sleep(2000);
        return "Done";
    }

    public async Task<string> LongOperationAsync()
    {
        await Task.Delay(2000);
        return "Done";
    }
}