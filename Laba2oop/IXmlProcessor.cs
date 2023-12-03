using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2oop

{
    public interface IXmlProcessor
    {
        string ProcessXml(string xmlFilePath, string searchQuery, string searchType);
    }
}