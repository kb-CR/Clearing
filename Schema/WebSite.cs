namespace ConsoleAppLink.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WebSite
{
    public string URL;
    public PotentialAction PotentialAction;

    public WebSite() { }

    public WebSite(string url, PotentialAction potentialaction)
    {
        URL = url;
        PotentialAction = potentialaction;
    }
}

/*
{
    "@context": "https://schema.org",
    "@type": "WebSite",
    "url": "http://example.com/",
    "potentialAction": {
      "@type": "SearchAction",
      "target": "http://example.com/search?&q={query}",
      "query": "required"
    }
}
*/