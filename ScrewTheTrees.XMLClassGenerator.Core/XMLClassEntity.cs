

using System.Collections.Generic;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class XmlClassEntity
    {
        public string ID;
        public string Handler;
        public string Name;
        public int Size;

        public string Directory = "";

        public XmlClassEntity ParentClass;
        public List<XmlClassEntity> ChildrenClasses = new List<XmlClassEntity>();

        public List<string> Includes { get; }


        public void CalculateDirectory()
        {
            XmlClassEntity parent = ParentClass;

            while (parent != null)
            {
                Directory = (parent.Name.Substring(1, parent.Name.Length - 1)) + @"\" + Directory;

                parent = parent.ParentClass;
            }
        }
        public void AddToParent()
        {
            if (ParentClass != null)
            {
                if (!ParentClass.ChildrenClasses.Contains(this))
                {
                    ParentClass.ChildrenClasses.Add(this);
                }
            }
        }

        public void ClearIncludes()
        {
            Includes.Clear();
        }

        public void CreateIncludes()
        {
            Includes.Add("#include \"" + ParentClass.Directory.Replace('\\', '/') + @"\" + ParentClass.Name + ".h\"");
        }

        public void CreateIncludes(List<string> includes) 
        {
            this.Includes.Add("#include \"" + Directory.Replace('\\','/') + ".h\"");

            if (includes != null)
                Includes.Add("#include \"" + ParentClass.Directory.Replace('\\', '/') + @"\" + ParentClass.Name + ".h\"");
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
                    int retval = dir1.Directory.Length.CompareTo(dir2.Directory.Length);

                    if (retval != 0)
                        return retval;
                    else
                        return dir1.Directory.CompareTo(dir2.Directory);
                }
            }
        }
    }
}
