// See https://aka.ms/new-console-template for more information

using Promise;

bool isPress = false;
Log("end");
while (true)
{
    if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.F)
    {
        isPress = true;
    }
    TickManager.Inst.Update(0.016f);
    Thread.Sleep(16);
}

async void Log(string msg)
{
    var result = await Test();
    Console.Write(msg + $"{result}");
}

async Promise.Promise<int> Test()
{
    Console.WriteLine("11");
    await Promise.Promise.Delay(1);
    Console.WriteLine("22");
    await Promise.Promise.WaitUtil(() => isPress);
    Console.WriteLine("33");
    return 1;
}



