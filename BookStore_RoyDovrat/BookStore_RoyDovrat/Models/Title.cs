using System.Xml.Serialization;

namespace BookStore_RoyDovrat.Models
{
    public class Title
    {
        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
