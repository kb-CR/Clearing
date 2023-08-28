using ConsoleAppLink;
using ConsoleAppLink.NameManager;

using System.Data;

internal class Program
{
    public static bool Debug = true;
    public static bool Doing = true;
    public static bool Entering = true;
    // String.Empty parses Link Header Alternates
    public static char ArgumentSeparator = ' ';
    public static char ElementSeparator = '.';
    public static char GreatBinder = '>';
    public static char LeastBinder = '<';
    public static char DelegateBinder = ';';
    public static char PathSeparator = '/';
    public static char CueingStatement = '¿';
    public static char QueuingStatement = '?';

    public static KeyPosition AKeyRow = new KeyPosition();

    private static async Task Main ( string [ ] args )
    {
        Console.Title = "{ \"KDRCA\" : { \"Key\" : { \"Dat\" : [ Row, Col ] }, \"App\" } } KDRCA by CaalRoe (A StealWhool Product)";
        while ( Doing )
        {
            AKeyRow.CueQue ( );
            await AKeyRow.Do ( );
        }
    }
}