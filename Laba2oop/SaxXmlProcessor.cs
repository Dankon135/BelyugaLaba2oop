using System;
using System.Xml;
using System.Text;

namespace Laba2oop
{
    public class SaxXmlProcessor : IXmlProcessor
    {
        public string ProcessXml(string xmlFilePath, string xslFilePath, string searchQuery, string searchType)
        {
            StringBuilder result = new StringBuilder();
            bool isCorrectNode = false;
            bool insideBook = false;

            try
            {
                using (XmlReader reader = XmlReader.Create(xmlFilePath))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (reader.Name.Equals("book"))
                                {
                                    insideBook = true; 
                                    isCorrectNode = false; 
                                }
                                else if (insideBook && reader.Name.Equals(searchType))
                                {
                                    isCorrectNode = true;
                                }
                                break;

                            case XmlNodeType.EndElement:
                                if (reader.Name.Equals("book"))
                                {
                                    insideBook = false; 
                                }
                                break;

                            case XmlNodeType.Text:
                                if (isCorrectNode && reader.Value.Contains(searchQuery))
                                {
                                    result.AppendLine($"Знайдено {searchType}: {reader.Value}");
                                    isCorrectNode = false; 
                                }
                                break;
                        }
                    }
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
