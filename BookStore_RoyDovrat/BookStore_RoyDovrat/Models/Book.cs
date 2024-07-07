using System.Xml.Serialization;

namespace BookStore_RoyDovrat.Models
{
    public class Book
    {
        [XmlAttribute("category")]
        public string Category { get; set; }

        [XmlAttribute("cover")]
        public string Cover { get; set; }

        [XmlElement("isbn")]
        public ulong ISBN { get; set; }

        [XmlElement("title")]
        public Title Title { get; set; }

        [XmlElement("author")]
        public List<string> Authors { get; set; } = new List<string>();

        [XmlElement("year")]
        public uint Year { get; set; }

        [XmlElement("price")]
        public double Price { get; set; }
    }
}
