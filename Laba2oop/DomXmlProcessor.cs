using System;
using System.Xml;
using System.Text;

namespace Laba2oop
{
    public class DomXmlProcessor : IXmlProcessor
    {
        public string ProcessXml(string xmlFilePath, string xslFilePath, string searchQuery, string searchType)
        {
            StringBuilder result = new StringBuilder();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(xmlFilePath);
                XmlNodeList nodes;

                
                switch (searchType.ToLower())
                {
                    case "title":
                        nodes = doc.DocumentElement.SelectNodes($"//book[title[contains(text(), '{searchQuery}')]]");
                        break;
                    case "author":
                        
                        nodes = doc.DocumentElement.SelectNodes($"//book[author[contains(text(), '{searchQuery}')]]");
                        break;
                    case "year":
                        
                        nodes = doc.DocumentElement.SelectNodes($"//book[publicationQualities/year[contains(text(), '{searchQuery}')]]");
                        break;
                    case "language":
                        
                        nodes = doc.DocumentElement.SelectNodes($"//book[publicationQualities/language[contains(text(), '{searchQuery}')]]");
                        break;
                    case "genre":
                        
                        nodes = doc.DocumentElement.SelectNodes($"//book[publicationQualities/genre[contains(text(), '{searchQuery}')]]");
                        break;

                    default:

                        return "Невизначений тип пошуку.";
                }

                
                foreach (XmlNode node in nodes)
                {
                    if (node != null)
                    {
                        result.AppendLine(node.OuterXml);
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
