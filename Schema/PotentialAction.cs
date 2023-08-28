namespace ConsoleAppLink.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PotentialAction
{
    public string Target;
    public string Query;

    public PotentialAction ( ) { }

    public PotentialAction ( string target , string query )
    {
        Target = target;
        Query = query;
    }
}