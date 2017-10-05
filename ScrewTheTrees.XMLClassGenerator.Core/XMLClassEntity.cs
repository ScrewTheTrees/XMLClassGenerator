

using System.Collections.Generic;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class XmlClassEntity
    {
        public string ID { get; }
        public string Name { get; }
        public string Handler { get; }
        public int Size { get; }

        public string directory = "";

        public XmlClassEntity parentClass = null;
        public List<XmlClassEntity> childrenClasses = new List<XmlClassEntity>();


        public XmlClassEntity(string id, string name, string handler, int size)
        {
            ID = id;
            Name = name;
            Handler = handler;
            Size = size;
        }

        /**
         * Calculates the relative directory this class is gonna be in the file/import structure.
         **/
        public void CalculateDirectory()
        {
            XmlClassEntity parent = parentClass;

            while (parent != null)
            {
                directory = (parent.Name.Substring(1, parent.Name.Length - 1))+ "\\" + directory;

                parent = parent.parentClass;
            }

        }
        public void AddToParent()
        {
            if (parentClass != null)
            {
                if (!parentClass.childrenClasses.Contains(this))
                {
                    parentClass.childrenClasses.Add(this);
                }
            }
        }
    }
}
