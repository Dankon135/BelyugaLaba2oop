using System;
using System.Xml;
using System.Text;
using System.Linq;

namespace Laba2oop
{
    public class DomXmlProcessor : IXmlProcessor
    {
        public string ProcessXml(string xmlFilePath, string searchQuery, string searchType)
        {
            StringBuilder result = new StringBuilder();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(xmlFilePath);
                XmlNodeList nodes;

                // Вибір пошукової стратегії
                switch (searchType.ToLower())
                {
                    case "title":
                    case "author":
                    case "year":
                        nodes = doc.DocumentElement.SelectNodes($"//book/{searchType}[contains(text(), '{searchQuery}')]");
                        break;
                    default:
                        return "Невизначений тип пошуку.";
                }

                // Збір результатів пошуку
                foreach (XmlNode node in nodes)
                {
                    if (node.ParentNode != null)
                    {
                        result.AppendLine(node.ParentNode.OuterXml);
                    }
                }

                return result.Length > 0 ? result.ToString() : "За вашим запитом нічого не знайдено.";
            }
            catch (Exception ex)
            {
                return $"Помилка при обробці XML: {ex.Message}";
            }
        }
    }
}
