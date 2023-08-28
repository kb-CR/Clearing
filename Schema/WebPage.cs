namespace ConsoleAppLink.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WebPage : DynamicJson
{
    public static Dictionary<string, Type> names = new Dictionary<string, Type>()
    {
        ["breadcrumb"] = typeof(string),
        ["lastReviewed"] = typeof(int),
        ["mainContentOfPage"] = typeof(string),
        ["primaryImageOfPage"] = typeof(string),
        ["relatedLink"] = typeof(string),
        ["reviewedBy"] = typeof(string),
        ["significantLink"] = typeof(string),
        ["speakable"] = typeof(string),
        ["specialty"] = typeof(string)
    };
}

public class DynamicJson : DynamicObject
{

}

/*
{
    "@context": "https://schema.org",
    "@type": "WebPage",
    "name": "Lecture 12: Graphs, networks, incidence matrices",
    "description": "These video lectures of Professor Gilbert Strang teaching 18.06 were recorded in Fall 1999 and do not correspond precisely to the current  edition of the textbook.",
    "publisher": {
        "@type": "CollegeOrUniversity",
        "name": "MIT OpenCourseWare"
    },
    "license": "http://creativecommons.org/licenses/by-nc-sa/3.0/us/deed.en_US"
}
*/