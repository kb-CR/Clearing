namespace ConsoleAppLink.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WebSearchAction : PotentialAction
{
    public new string Target;
    public new string Query;

    public WebSearchAction ( ) { }

    public WebSearchAction ( string target , string query )
    {
        Target = target;
        Query = query;
    }
}