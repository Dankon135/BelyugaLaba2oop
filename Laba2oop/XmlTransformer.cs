using System;
using System.Xml.Xsl;

namespace Laba2oop
{
    public class XmlTransformer
    {
        public static string TransformXmlToHtml(string xmlFilePath, string xsltFilePath, string htmlOutputFilePath)
        {
            try
            {
                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(xsltFilePath);
                transform.Transform(xmlFilePath, htmlOutputFilePath);
                return "XML was successfully transformed to HTML.";
            }
            catch (Exception ex)
            {
                return $"Error during XML transformation: {ex.Message}";
            }
        }
    }
}
