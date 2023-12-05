using System;
using System.Xml.Linq;
using System.Linq;
using System.Text;

namespace Laba2oop
{
    public class LinqXmlProcessor : IXmlProcessor
    {
        public string ProcessXml(string xmlFilePath, string xslFilePath, string searchQuery, string searchType)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                XDocument doc = XDocument.Load(xmlFilePath);

                IEnumerable<XElement> query = null;

                // Формування запиту залежно від searchType
                switch (searchType.ToLower())
                {
                    case "title":
                        query = doc.Descendants("book")
                                   .Where(book => book.Element("title") != null &&
                                                  book.Element("title").Value.Contains(searchQuery));
                        break;
                    case "author":
                        query = doc.Descendants("book")
                                   .Where(book => book.Element("author") != null &&
                                                  book.Element("author").Value.Contains(searchQuery));
                        break;
                    case "year":
                        query = doc.Descendants("book")
                                   .Where(book => book.Descendants("publicationQualities")
                                                      .Elements("year").Any() &&
                                                  book.Descendants("publicationQualities")
                                                      .Elements("year").First().Value.Contains(searchQuery));
                        break;

                    case "language":
                        query = doc.Descendants("language")
                                   .Where(book => book.Descendants("publicationQualities")
                                                      .Elements("language").Any() &&
                                                  book.Descendants("publicationQualities")
                                                      .Elements("language").First().Value.Contains(searchQuery));
                       break;
                    
                    case "genre":
                        query = doc.Descendants("genre")
                                   .Where(book => book.Descendants("publicationQualities")
                                                      .Elements("genre").Any() &&
                                                  book.Descendants("publicationQualities")
                                                      .Elements("genre").First().Value.Contains(searchQuery));
                        break;
                }

                // Збір і виведення результатів
                if (query != null && query.Any())
                {
                    foreach (var element in query)
                    {
                        result.AppendLine(element.ToString());
                    }
                }
                else
                {
                    result.AppendLine("За вашим запитом нічого не знайдено.");
                }
            }
            catch (Exception ex)
            {
                result.AppendLine($"Помилка при обробці XML: {ex.Message}");
            }

            return result.ToString();
        }
    }
}
