

using System.Collections.Generic;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class XmlClassEntity
    {
        public string ID = "";
        public string Name = "";
        public string Handler = "";
        public int Size = 0;

        public string directory = "";

        public XmlClassEntity parentClass = null;
        public List<XmlClassEntity> childrenClasses = new List<XmlClassEntity>();


        public XmlClassEntity()
        {

        }

        /**
         * Calculates the relative directory this class is gonna be in the file/import structure.
         **/
        public void CalculateDirectory()
        {
            XmlClassEntity parent = parentClass;

            while (parent != null)
            {
                directory = (parent.Name.Substring(1, parent.Name.Length - 1))+ @"\" + directory;

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


        public static int CompareByDirectoryLength(XmlClassEntity dir1, XmlClassEntity dir2)
        {
            if (dir1 == null)
            {
                if (dir2 == null)
                    return 0;   //Well, shit
                else
                    return -1;  //Counter productive
            }
            else
            {
                if (dir2 == null)
                    return 1;   //Finally some progress
                else
                {
                    int retval = dir1.directory.Length.CompareTo(dir2.directory.Length);

                    if (retval != 0)
                        return retval;
                    else
                        return dir1.directory.CompareTo(dir2.directory);
                }
            }
        }
    }
}
