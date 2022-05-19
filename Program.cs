// See https://aka.ms/new-console-template for more information

while (true)
{
    try
    {
        PlayTheGame();
        Console.WriteLine("Spelet har slutförts, tryck på valfri tangent för att börja om.");
        Console.ReadKey();
    }
    catch (ExitProgram)
    {
        Console.WriteLine("Du valde att avsluta programmet");
        break;
    }
    catch (GameError ex)
    {
        Console.WriteLine("Felmeddelande från spelet: ");
        Console.WriteLine(ex.Message);
        Console.WriteLine("Tryck på en tangent för att börja om spelet");
        Console.ReadKey();
    }
    catch (TaskException task)
    {
        Console.WriteLine("Nu väntar vi i catchen istället");
        await task.task;
    }
    catch (Exception ex)
    {
        Console.Clear();
        Console.WriteLine("Nånting gick riktigt illa, vi har loggat felet: ");
        Console.WriteLine(ex.Message);
        Console.WriteLine();
        Console.WriteLine("Vi avslutar därför programmet");
        // Log();
        break;
    }
    Console.Clear();
}

void PlayTheGame()
{
    var game = Menu();

    Console.Write("Du har valt att spela: ");
    Console.ForegroundColor = game.TitleColor;
    Console.Write(game.Title);
    Console.ResetColor();
    Console.Write(", Nice va");
    game.Run();
}

static int GetSelectedGameNumber()
{
    while (true)
    {
        var spelNummer = Console.ReadLine();
        Console.Clear();
        int spelNummerParsed;
        if (int.TryParse(spelNummer, out spelNummerParsed))
            return spelNummerParsed;

        Console.WriteLine();
        Console.WriteLine("Felaktigt val, försök ange ett nummer: ");
    }
}

Game Menu()
{
    Console.WriteLine("Vilket spel vill du spela?");
    Console.WriteLine("1. Gissa numret");
    Console.WriteLine("2. Skriv ord baklänges");
    Console.WriteLine("3. Väntespelet");
    Console.WriteLine("0. Avsluta programmet");
    Console.WriteLine("Skriv in siffran för spelet du vill spela: ");
    return ChooseGameFromNumber();
}

Game ChooseGameFromNumber()
{
    int spelNummer = GetSelectedGameNumber();
    switch (spelNummer)
    {
        case 1: return new GissaNummerGame();
        case 2: return new SkrivOrdBaklänges();
        case 3: return new VänteSpelet();
        case 0: throw new ExitProgram();
        default: throw new GameError("Vi har inget spel på det där numret, välj ett annat");
    }
}


class TaskException : Exception
{
    public Task task { get; private set; }
    public TaskException(Task task)
    {
        this.task = task;
    }
}
class GameError : Exception
{
    public GameError(string? message) : base(message)
    {
    }
}


class ExitProgram : Exception
{

}

public interface Game
{
    string Author { get; }
    string Title { get; }
    ConsoleColor TitleColor { get; }
    void Run();
}

class GissaNummerGame : Game
{
    public string Author { get; } = "lollen";
    public string Title { get; } = "Gissa spelet 2022 deluxe beta";
    public ConsoleColor TitleColor { get; } = ConsoleColor.DarkCyan;

    public void Run()
    {
        Console.WriteLine("Spelet är inte byggt ännu.");
    }
}
class SkrivOrdBaklänges : Game
{
    public string Author { get; } = "trollen";
    public string Title { get; } = "Skriv order baklänges eller så får katten stryk";
    public ConsoleColor TitleColor { get; } = ConsoleColor.DarkRed;

    public void Run()
    {
        Console.WriteLine("Spelet är inte byggt ännu.");
        throw new OutOfMemoryException();
    }

}

class VänteSpelet : Game
{
    public string Author => "trololololo";

    public string Title => "Väntespelet tar en stund...";

    public ConsoleColor TitleColor => ConsoleColor.DarkYellow;

    public void Run()
    {
        RunGame().GetAwaiter().GetResult();
    }
    public async Task RunGame()
    {
        await Blah();
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"{i}");
            Thread.Sleep(1000);
        }
        await Blah();
        Console.WriteLine("Tredje gången smällt");
        var blahaja = Blah();
        throw new TaskException(blahaja);
    }
    async Task Blah()
    {
        await Task.Run(() =>
        {
            Console.WriteLine("Börjar vänta 5 sekunder");
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine("Klar");

        });
    }
}

