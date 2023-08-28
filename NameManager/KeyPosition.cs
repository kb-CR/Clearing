namespace ConsoleAppLink.NameManager;


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class KeyPosition
{
    private StringBuilder Builder = new StringBuilder();
    private ConsoleKeyInfo consoleKeyInfo;

    public void ReadKey( bool Intercept )
    {
        consoleKeyInfo = Console.ReadKey ( Intercept );
    }

    public void Build()
    {
        Builder = new StringBuilder ( );
    }

    public void Demolish()
    {
        Builder.Clear();
    }

    public void Clean()
    {
        Console.Clear ( );
    }

    public void CueQue()
    {
        Console.Write ( $"{Program.CueingStatement}{Program.QueuingStatement}" );
        Console.SetCursorPosition ( Console.CursorLeft - 1 , Console.CursorTop );
    }

    public async Task Do ()
    {
        while ( Program.Entering )
        {
            ReadKey ( true );
            await Switch ( consoleKeyInfo.Key );
        }
    }

    public async Task Arguments ()
    {
        string value = Builder.ToString().Trim();
        if( value.Length > 0)
        {
            string[] Arguments = value.Split ( $"{Program.ArgumentSeparator}" );
            if ( Arguments.Length >= 1 )
            {
                foreach ( string argument in Arguments )
                {
                    await Elements ( argument );
                }
            }
        }
    }

    public async Task Elements (string Argument)
    {
        Argument = Argument.Trim ( );
        string[] Elements = Argument.Split ( $"{Program.ElementSeparator}" );
        if ( Elements.Length >= 1 )
        {
            Foothold.Client ( Elements );
            await Foothold.Linkers ( );
        }
    }

    public async Task ReadEnter ()
    {
        Console.Write ( consoleKeyInfo.KeyChar );
        string value = Builder.ToString ( ).Trim();
        if ( value.Length > 0 )
        {
            // TODO Parse links different than non uri text.
            //Program.ADatCol.Add ( value );
            AName name = new AName()
            {
                Id = ATeam.ANameCollection.Length,
                Name = value
            };
            ATeam.ANameCollection.Add ( name );
            if ( Builder.ToString ( ) is not null )
            {
                await Arguments ( );
            }
        }
        Demolish ( );
        Clean ( );
        CueQue ( );
    }

    public string IgnoreCaseTrimStart(string value, string trim)
    {
        int Position = 0;
        char[] Glyphics = trim.ToCharArray ( );
        foreach ( char Glyph in Glyphics )
        {
            if (value.IndexOf( Glyph, StringComparison.CurrentCultureIgnoreCase ) == Position )
            {
                Position++;
                continue;
            }
            break;
        }
        return value.Substring(Position, value.Length - Position).Trim();
    }

    public void ReadTab()
    {
        string value = Builder.ToString().Trim();
        //TODO Implement 0 length, usage/time con neg.
        if( value.LongCount() >= 0 )
        {
            List<string> Names = new List<string>();
            foreach ( ACollection aCollection in ATeam.Collections )
            {
                aCollection.Reset ( );
                while ( aCollection.MoveNext())
                {
                    if ( aCollection.Value.ToString ( ).ToLower ( ).StartsWith ( value.ToLower ( ) ) )
                    {
                        Names.Add ( aCollection.Value!.ToString ( ) );
                    }
                }
            }
            if ( Names.Count >= 1 )
            {
                string Remainder = IgnoreCaseTrimStart(Names[0], Builder.ToString());
                Console.Write ( $"{Remainder}" );
                Builder.Append ( Remainder );
            }
        }
    }

    public void ReadBackspace()
    {
        if ( Console.CursorLeft > 1 )
        {
            Console.SetCursorPosition ( Console.CursorLeft - 1 , Console.CursorTop );
            Console.Write ( ' ' );
            Console.SetCursorPosition ( Console.CursorLeft - 1 , Console.CursorTop );
            if ( Builder.Length > 0 )
            {
                Builder.Remove ( Builder.Length - 1 , 1 );
            }
        }
    }

    public void ReadDefault()
    {
        Console.Write ( consoleKeyInfo.KeyChar );
        Builder.Append ( consoleKeyInfo.KeyChar );
    }

    public async Task Switch (ConsoleKey key)
    {
        switch ( key )
        {
        case ConsoleKey.Enter:
            {
                await ReadEnter ( );
                break;
            }
        case ConsoleKey.Tab:
            {
                ReadTab ( );
                break;
            }
            
        case ConsoleKey.Backspace:
            {
                ReadBackspace ( );
                break;
            }
        default:
            {
                ReadDefault ( );
                break;
            }
        }
    }
}
