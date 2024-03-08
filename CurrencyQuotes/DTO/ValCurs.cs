using System.Xml.Serialization;

namespace DTO;

[XmlRoot(ElementName="ValCurs")]
public class ValCurs { 

    [XmlElement(ElementName="Valute")] 
    public List<Valute> Valute { get; init; }

    [XmlAttribute(AttributeName="Date")] 
    public string Date { get; init; }

    [XmlAttribute(AttributeName="name")] 
    public string Name { get; init; }

    [XmlText] 
    public string Text { get; init; }
}