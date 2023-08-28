namespace ConsoleAppLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ListExtensions
{
    public static bool Contains ( this List<object> Container , string Contents , StringComparison Comparer )
    {
        try
        {
            foreach ( string Content in Container )
            {
                if ( Content?.IndexOf ( Contents , Comparer ) >= 0 )
                {
                    return true;
                }
            }
        } catch { }
        return false;
    }
}