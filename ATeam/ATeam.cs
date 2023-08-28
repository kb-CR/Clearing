
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class ATeam
{
    public static ACollection ALinkCollection = new ACollection();
    public static ACollection ANameCollection = new ACollection();
    public static ACollection AUserAgentCollection = new ACollection();
    public static ACollection AContentCollection = new ACollection();
    public static ACollection AMemberCollection = new ACollection();

    public static ACollection[] Collections = new ACollection[]
    {
        ATeam.ALinkCollection,
        ATeam.ANameCollection,
        ATeam.AUserAgentCollection,
        ATeam.AContentCollection,
        ATeam.AMemberCollection
    };
}
