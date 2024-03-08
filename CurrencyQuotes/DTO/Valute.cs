using System.Xml.Serialization;

namespace DTO;

[XmlRoot(ElementName="Valute")]
public class Valute { 

    [XmlElement(ElementName="NumCode")] 
    public int NumCode { get; init; }

    [XmlElement(ElementName="CharCode")] 
    public string CharCode { get; init; }

    [XmlElement(ElementName="Nominal")] 
    public int Nominal { get; init; } 

    [XmlElement(ElementName="Name")] 
    public string Name { get; init; }

    [XmlElement(ElementName="Value")] 
    public string Value { get; init; } 

    [XmlElement(ElementName="VunitRate")] 
    public string VunitRate { get; init; }

    [XmlAttribute(AttributeName="ID")] 
    public string ID { get; init; }

    [XmlText] 
    public string Text { get; init; }
}