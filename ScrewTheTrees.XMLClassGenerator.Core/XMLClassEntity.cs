

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class XMLClassEntity
    {
        public string id;
        public string name;
        public string handler;
        public int size;

        public XMLClassEntity parentClass = null;
        public XMLClassEntity[] childrenClasses = null;


        public XMLClassEntity(string id, string name, string handler, int size)
        {
            this.id = id;
            this.name = name;
            this.handler = handler;
            this.size = size;
        }

    }
}
