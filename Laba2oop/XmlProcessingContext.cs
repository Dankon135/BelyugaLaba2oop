namespace Laba2oop
{
    public class XmlProcessingContext
    {
        private IXmlProcessor _xmlProcessor;

        public XmlProcessingContext(IXmlProcessor xmlProcessor)
        {
            _xmlProcessor = xmlProcessor;
        }

        public void SetStrategy(IXmlProcessor xmlProcessor)
        {
            _xmlProcessor = xmlProcessor;
        }

        public string ProcessXml(string xmlFilePath, string xslFilePath, string searchQuery, string searchType)
        {
            return _xmlProcessor.ProcessXml(xmlFilePath, xslFilePath, searchQuery, searchType);
        }
    }
}
