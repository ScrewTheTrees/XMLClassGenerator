

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class XMLClassEntity
    {
        public string id;
        public string name;
        public string handler;
        public int size;

        public XMLClassEntity parentClass;
        public XMLClassEntity[] childrenClasses;
    }
}
