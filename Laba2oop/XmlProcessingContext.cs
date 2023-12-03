namespace Laba2oop
{
    public class XmlProcessingContext
    {
        private IXmlProcessor _xmlProcessor;

        public XmlProcessingContext(IXmlProcessor xmlProcessor)
        {
            _xmlProcessor = xmlProcessor ?? throw new ArgumentNullException(nameof(xmlProcessor));
        }

        public void SetStrategy(IXmlProcessor xmlProcessor)
        {
            _xmlProcessor = xmlProcessor ?? throw new ArgumentNullException(nameof(xmlProcessor));
        }

        public string ProcessXml(string xmlFilePath, string searchQuery, string searchType)
        {
            if (_xmlProcessor == null)
            {
                throw new InvalidOperationException("XML Processor is not set.");
            }
            return _xmlProcessor.ProcessXml(xmlFilePath, searchQuery, searchType);
        }
    }
}
