namespace ConsoleAppLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

public class DynamicXML : IXmlSerializable
{
    private readonly Dictionary<string, object> Identifiers;
    private readonly Dictionary<string, object> Attributes;
    private readonly Dictionary<string, object> Elements;
    private readonly Dictionary<string, object> Names;
    

    public DynamicXML ( )
    {
        Attributes = new Dictionary<string , object> ( );
    }

    public XmlSchema GetSchema ( )
    {
        return null;
    }

    public void ReadXml ( XmlReader reader )
    {
        if ( reader.HasAttributes )
        {
            XmlReader subtree;
            while ( (subtree = reader.ReadSubtree()) is not null )
            {
                var key = reader.LocalName;
                var value = reader.Value;

                Attributes.Add ( key , value );
            }

            // back to the owner of attributes
            reader.MoveToElement ( );
        }

        reader.ReadStartElement ( );
    }

    public void WriteXml ( XmlWriter writer )
    {
        foreach ( var attribute in Attributes )
        {
            writer.WriteStartAttribute ( attribute.Key );

            writer.WriteValue ( attribute.Value );

            writer.WriteEndAttribute ( );
        }
    }

    // implementation ostring?Dictionary<string, object> comes here
}