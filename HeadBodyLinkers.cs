namespace ConsoleAppLink;

using ConsoleAppLink.NameManager;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class Foothold
{
    public static bool Debug = true;
    public static bool Doing = true;
    private static Dictionary<string, HashSet<string>> CMap = new Dictionary<string, HashSet<string>>();
    private static HashSet<string> Location = new HashSet<string>();
    private static HashSet<string> Locations = new HashSet<string>();
    public static string[] Standard = new string[4]{ "/", "robots.txt", "sitemap.xml" , "siteindex.xml" };
    public static string DefaultApplication = "robots";
    public static string DefaultScheme = "https";
    public static HttpClient? SharedClient;
    public static Uri? Base;

    public static HttpClient Client ( string [ ] Elements)
    {
        Base = ElementalUri ( Elements , true );
        if ( Debug )
        {
            Console.WriteLine ( Base );
        }
        return SharedClient = new HttpClient ( new HttpClientHandler ( )
        {
            AllowAutoRedirect = true ,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        } )
        {
            BaseAddress = Base ,
            DefaultRequestVersion = HttpVersion.Version30 ,
            DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower
        };
    }

    public static void ReadXml( string standardFile )
    {
        Uri uri = new Uri (Base!, standardFile);
        if ( IsLinkedFile ( uri ) )
        {
            string fileInfo = LocalLinkInfo(uri);
            using ( TextReader? textReader = new StreamReader ( fileInfo ) )
            {
                using ( XmlReader xmlReader = XmlReader.Create ( textReader ) )
                {
                    while ( xmlReader.Read ( ) )
                    {
                        try
                        {
                            XmlReader xmlreader = xmlReader;
                            string name = xmlReader.Name;
                            string value = xmlReader.Value;
                            if ( CMap.ContainsKey ( name ) )
                            {
                                CMap [ name ].Add ( value );
                            }
                            else
                            {
                                AName Name = new AName()
                                {
                                    Id = ATeam.ANameCollection.Length,
                                    Name = name
                                };
                                ATeam.ANameCollection.Add ( Name );
                                foreach ( string result in string.Concat ( name.Select ( ( x , i ) => char.IsUpper ( x ) && i != 0 ? $" {x}" : x.ToString ( ) ) ).Split ( ' ' ) )
                                {
                                    Name.Id = ATeam.ANameCollection.Length;
                                    Name.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase ( result );
                                    ATeam.ANameCollection.Add ( Name );
                                }
                                CMap.Add ( name , new HashSet<string> ( ) { value } );
                            }
                            string[ ] results = Regex.Matches ( value , @"[\p{L}\p{N}]+" ).Cast<Match> ( ).Select ( x => x.Value ).ToArray ( );
                            foreach ( string result in results )
                            {
                                string[] resultors = string.Concat ( result.Select ( ( x , i ) => char.IsUpper ( x ) && i != 0 ? $" {x}" : x.ToString ( ) ) ).Split ( ' ' );
                                foreach ( string resulted in resultors )
                                {
                                    AName Name = new AName()
                                    {
                                        Id = ATeam.ANameCollection.Length,
                                        Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase ( resulted.Trim ( '/' ) )
                                    };
                                    ATeam.ANameCollection.Add ( Name );
                                }
                            }
                        } catch { }

                    }
                }
            }
        }
    }

    public static void ReadJson(string standardFile )
    {
        Uri uri = new Uri (Base!, standardFile);
        if ( IsLinkedFile ( uri ) )
        {
            string fileInfo = LocalLinkInfo(uri);
            using ( TextReader? textReader = new StreamReader ( fileInfo ) )
            {
                Utf8JsonReader jsonReader = new Utf8JsonReader ( Encoding.UTF8.GetBytes(textReader.ReadToEnd()).AsSpan<byte>() );

            }
        }
    }

    public static async Task Linkers ( )
    {
        foreach ( string standardFile in Standard )
        {
            try
            {
                Uri uri = new Uri (Base!, standardFile);
                if( IsLinkedFile (uri))
                {
                    if ( Debug )
                    {
                        Console.WriteLine ( uri );
                    }
                    string fileInfo = LocalLinkInfo(uri);
                    try
                    {
                        ReadXml ( standardFile );
                    } catch {
                        try
                        {
                            ReadJson ( standardFile );
                            // TODO JsonReader
                        } catch {
                            AUserAgent userAgent = new AUserAgent ( )
                            {
                                Id = ATeam.AUserAgentCollection.Length,
                                UserAgent = "*"
                            };
                            ATeam.AUserAgentCollection.Add ( userAgent  );
                            using ( TextReader? textReader = new StreamReader ( fileInfo ) )
                            {
                                string? line = null;
                                while((line = textReader.ReadLine()) is not null)
                                {
                                    string[] keyValue = line.Split(':');
                                    if(keyValue.Length == 2)
                                    {
                                        string Key = keyValue[0].Trim().ToUpper();
                                        string Value = keyValue[1].Trim();
                                        ALink link = new ALink ( )
                                        {
                                            Id = ATeam.ALinkCollection.Length ,
                                            Link = new Uri ( Value ) ,
                                            Allow = false ,
                                            Disallow = false ,
                                            Sitemap = false ,
                                            Robots = true
                                        };
                                        switch (Key)
                                        {
                                        default:
                                            break;
                                        case "ALLOW":
                                            link.Allow = true;
                                            ATeam.ALinkCollection.Add ( link );
                                            ATeam.AMemberCollection.Add ( new AMember ( )
                                            {
                                                Id = ATeam.AMemberCollection.Length ,
                                                Link = link.Id ,
                                                UserAgent = userAgent.Id
                                            } );
                                            break;
                                        case "DISALLOW":
                                            link.Disallow = true;
                                            ATeam.ALinkCollection.Add ( link );
                                            ATeam.AMemberCollection.Add ( new AMember ( )
                                            {
                                                Id = ATeam.AMemberCollection.Length ,
                                                Link = link.Id ,
                                                UserAgent = userAgent.Id
                                            } );
                                            break;
                                        case "USER-AGENT":
                                            ATeam.AUserAgentCollection.Add ( userAgent = new AUserAgent ( )
                                            {
                                                Id = ATeam.AUserAgentCollection.Length ,
                                                UserAgent = Value
                                            } );
                                            break;
                                        case "SITEMAP":
                                            link.Sitemap = true;
                                            ATeam.ALinkCollection.Add ( link );
                                            ATeam.AMemberCollection.Add ( new AMember ( )
                                            {
                                                Id = ATeam.AMemberCollection.Length ,
                                                Link = link.Id ,
                                                UserAgent = userAgent.Id
                                            } );
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if ( Debug )
                    {
                        Console.WriteLine ( uri );
                    }
                    string response = await LinkedFile ( HttpMethod.Options , uri );
                }
            } catch { }
        }
    }

    private static Uri ElementalUri ( string [ ] Elements , bool Reverse = true )
    {
        try
        {
            if ( Reverse )
            {
                Array.Reverse<string> ( Elements );
            }
            string url = String.Join ( $"{Program.ElementSeparator}" , Elements );
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Path = $"{Program.PathSeparator}";
            uriBuilder.Host = url.ToString ( );
            uriBuilder.Scheme = DefaultScheme;
            return uriBuilder.Uri;
        } catch { }
        return null;
    }

    public static string LocalLinkInfo(Uri uri)
    {
        string fileName = $"{uri.Authority}_{Path.GetFileName(uri.LocalPath)}";
        string filePath = Path.Combine ( Environment.CurrentDirectory, Path.GetExtension(uri.LocalPath).TrimStart('.') );
        string fileInfo = Path.Combine(filePath, fileName);
        return fileInfo;
    }

    public static bool IsLinkedFile(Uri uri)
    {
        string fileInfo = LocalLinkInfo(uri);
        if ( File.Exists ( fileInfo ) )
        {
            return true;
        }
        return false;
    }

    public static async Task<string> LinkedFile ( HttpMethod method , Uri uri )
    {
        try
        {
            object message = (object) await SendMethodAsync ( method , uri );
            if ( message is not null)
            {
                (dynamic, Uri) content;
                try
                {
                    content = ((byte[], Uri) ) message;
                   
                }
                catch
                {
                    content = ( (object, Uri) ) message;
                }
                try
                {
                    uri = ( Uri ) content.Item2;
                } catch { }
                if ( uri is not null )
                {
                    string fileInfo = LocalLinkInfo(uri);
                    if ( Debug )
                    {
                        Console.WriteLine ( fileInfo );
                    }
                    if ( IsLinkedFile ( uri ) )
                    {
                        return File.ReadAllText ( fileInfo );
                    }
                    else if ( content.Item1 is not null )
                    {
                        Directory.CreateDirectory ( Path.GetDirectoryName ( fileInfo )! );
                        using ( var stream = new MemoryStream ( ( byte [ ] ) content.Item1 ) )
                        {
                            using ( var outputFile = new FileStream ( fileInfo , new FileStreamOptions ( )
                            {
                                BufferSize = 1023 ,
                                Mode = FileMode.Create ,
                                Access = FileAccess.ReadWrite ,
                                Options = FileOptions.WriteThrough
                            } ) )
                            {
                                stream.WriteTo ( outputFile );
                            }
                        }
                    }
                }
            }
        } catch { }
        return string.Empty;
    }

    private static async Task<object> SendMethodAsync ( HttpMethod method , Uri uri )
    {
        try
        {
            HttpRequestMessage request = new (method, uri);
            request.Version = HttpVersion.Version30;
            using ( HttpResponseMessage response = await SharedClient!.GetAsync ( uri ) )
            {
                if ( response.IsSuccessStatusCode )
                {
                    response.EnsureSuccessStatusCode ( );
                    if ( Debug )
                    {
                        foreach ( KeyValuePair<string , IEnumerable<string>> header in response.Headers )
                        {
                            Console.WriteLine ( $"{header.Key.ToString ( )}: {header.Value.ToString ( )}" );
                        }
                    }
                    if ( method == HttpMethod.Options || method == HttpMethod.Head )
                    {
                        // TODO Handle Robots, Link, Allow
                        try
                        {
                            string[] Link = ( string [ ] ) response.Headers.GetValues ( "Link" );
                            if ( Link.Length > 0 )
                            {
                                string? linkHeaderFirstElement = String.Join("", Link).ToString ( )!;
                                string[] linkHeaderParameters = linkHeaderFirstElement.Split ( $"{Program.DelegateBinder}" ); // Splitting intra-semicolon
                                if( linkHeaderParameters.Length > 0)
                                {
                                    string? linkHeaderFirstParameter = linkHeaderParameters[0].ToString ( )!;
                                    Console.WriteLine ( linkHeaderFirstParameter.ToString() );
                                    string linkHeader = linkHeaderFirstParameter.TrimStart ( Program.LeastBinder ).TrimEnd ( Program.GreatBinder ); // Get those off and that up >,<
                                    Console.WriteLine ( linkHeader.ToString ( ) );
                                    object link = (object) await SendMethodAsync ( HttpMethod.Get , new Uri(Base!, linkHeader));
                                    if(link is not null)
                                    {
                                        return (( ( (byte [ ], Uri) ) link ).Item1, uri);
                                    }
                                }
                            }
                            else
                            {
                                double Difference = response.Headers.CacheControl!.MaxAge!.Value.TotalNanoseconds - response.Headers.Age!.Value.TotalNanoseconds;
                                while ( Difference > 1 )
                                {
                                    //Thread.Sleep ( ( int ) ( Difference / (720 ^ 2) ) );
                                }
                                (byte[], Uri) send =  ((byte [ ], Uri) ) (object) await SendMethodAsync ( HttpMethod.Get , uri );
                                return (( byte [ ] ) send.Item1, uri);
                            }
                        } catch { }
                    }
                    return (await response.Content.ReadAsByteArrayAsync ( ), uri);
                }
            }
        } catch ( Exception exc )
        {
            if ( Debug )
            {
                Console.WriteLine ( exc );
            }
        }
        return null!;
    }
}
