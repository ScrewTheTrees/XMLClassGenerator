using System.Collections.Generic;
using System.Xml;

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class XMLHandler
    {
        public XmlDocument docu = new XmlDocument();
        public List<XMLClassEntity> eList = new List<XMLClassEntity>();
        
        public XMLHandler(string document)
        {
            docu.Load(document);
        }
    }
}
