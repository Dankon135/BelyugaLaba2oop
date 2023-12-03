using System;
using System.Xml.Xsl;

namespace Laba2oop
{
    public class XmlTransformer
    {
        public static void TransformXmlToHtml(string xmlFilePath, string xsltFilePath, string htmlOutputFilePath)
        {
            try
            {
                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(xsltFilePath);
                transform.Transform(xmlFilePath, htmlOutputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при трансформації XML: {ex.Message}");
            }
        }
    }
}
