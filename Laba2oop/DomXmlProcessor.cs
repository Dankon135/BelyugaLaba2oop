using System;
using System.Xml;
using System.Text;

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
                        nodes = doc.DocumentElement.SelectNodes($"//book[title[contains(text(), '{searchQuery}')]]");
                        break;
                    case "author":
                        // Ваш XPath запит для пошуку за автором
                        nodes = doc.DocumentElement.SelectNodes($"//book[author[contains(text(), '{searchQuery}')]]");
                        break;
                    case "year":
                        // Ваш XPath запит для пошуку за роком
                        nodes = doc.DocumentElement.SelectNodes($"//book[publicationQualities/year[contains(text(), '{searchQuery}')]]");
                        break;
                    default:
                        return "Невизначений тип пошуку.";
                }

                // Збір результатів пошуку
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
