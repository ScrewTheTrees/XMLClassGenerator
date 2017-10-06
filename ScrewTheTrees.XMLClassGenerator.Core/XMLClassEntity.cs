

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

        public List<string> Includes = new List<string>();
        public List<string> Header = new List<string>();
        public List<string> Body = new List<string>();

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

        public void GenerateIncludes()
        {
            Includes.Clear();
            if (ParentClass != null)
                Includes.Add(string.Format("#include \"{0}{1}.h\"", ParentClass.Directory.Replace('\\', '/'), ParentClass.Name));
        }
        public void GenerateIncludes(List<string> includes, bool generateParent = false) 
        {
            Includes.Clear();
            if (generateParent == true && ParentClass != null)
                Includes.Add(string.Format("#include \"{0}{1}.h\"", ParentClass.Directory.Replace('\\', '/'), ParentClass.Name));
            Includes.AddRange(includes);
        }

        public void GenerateHeader()
        {
            Header.Clear();
            Header.Add("/*");
            Header.Add(string.Format("Name: {0}", Name));
            if (ParentClass != null)
            {
                Header.Add(string.Format("Parent: {0}", ParentClass.Name));
                Header.Add(string.Format("ID: {0}", ID));
                Header.Add(string.Format("Size: {0} (+{1})", Size, Size-ParentClass.Size));
            }
            else
            {
                Header.Add(string.Format("ID: {0}", ID));
                Header.Add(string.Format("Size: {0}", Size));
            }
            Header.Add(string.Format("Handler: {0}", Handler));
            Header.Add("*/");
        }
        public void GenerateHeader(List<string> header, bool generateBase = false)
        {
            Header.Clear();
            Header.Add("/*");
            if (generateBase == true)
            {
                GenerateHeader();       //Generate normal header
                Header.Remove("*/");    //Remove the endtag so that more lines can be added.
            }

            Header.AddRange(header);
            Header.Add("*/");
        }

        public void GenerateBody()
        {
            Body.Clear();
            if (ParentClass != null)
                Body.Add(string.Format("class {0} : {1} {{", Name, ParentClass.Name));
            else Body.Add(string.Format("class {0} {{", Name));



            Body.Add("};");
        }



        /// <summary>
        /// Compares the order of XmlClassEntitites based on how far nested they are in the file system.
        /// Generally to make sure the base classes and their directories are always generated before the inheriting classes.
        /// </summary>
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
