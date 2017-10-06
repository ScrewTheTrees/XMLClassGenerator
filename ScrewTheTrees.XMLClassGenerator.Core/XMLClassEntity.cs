

using System.Collections.Generic;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    public class XmlClassEntity
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
        public List<string> Fields = new List<string>();

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

        /// <summary>
        /// Generate the include strings at the top of the document, other top of the document stuff can technically be added here aswell.
        /// </summary>
        /// <param name="includes">List (Nullable) of includes you want to add, the strings are parsed and will be written as is (no #include and etc...)</param>
        /// <param name="generateParent">Default true, Should the include for this Class parent be included (if not null)?</param>
        public void GenerateIncludes(List<string> includes = null, bool generateParent = true)
        {
            Includes.Clear();
            if (generateParent == true && ParentClass != null)
                Includes.Add(string.Format("#include \"{0}{1}.h\"", ParentClass.Directory.Replace('\\', '/'), ParentClass.Name));
            if (includes != null)
                Includes.AddRange(includes);
        }

        public void GenerateHeader(List<string> header = null, bool generateBase = true)
        {
            Header.Clear();
            Header.Add("/*");
            if (generateBase == true)
            {
                Header.Add(string.Format("Name: {0}", Name));
                if (ParentClass != null)
                {
                    Header.Add(string.Format("ID: {0}", ID));
                    Header.Add(string.Format("Size: {0} (0x{1})", Size, Size.ToString("X")));
                    Header.Add(string.Format("SizeDifference: {0} (0x{1})", Size - ParentClass.Size, (Size - ParentClass.Size).ToString("X")));
                    Header.Add(string.Format("ParentSize: {0} (0x{1})", ParentClass.Size, ParentClass.Size.ToString("X")));
                    Header.Add(string.Format("Parent: {0}", ParentClass.Name));
                }
                else
                {
                    Header.Add(string.Format("ID: {0}", ID));
                    Header.Add(string.Format("Size: {0} (0x{1})", Size, Size.ToString("X")));
                }
                Header.Add(string.Format("Handler: {0}", Handler));
            }
            if (header != null)
                Header.AddRange(header);
            Header.Add("*/");
        }

        /// <summary>
        /// Please generate "Fields" before generating the body, as this function uses the Fields
        /// This function accepts null lists that you dont have to make a new list just to add afterFields
        /// </summary>
        /// <param name="beforeFields">Lines to add before the fields get filled in.</param>
        /// <param name="afterFields">Lines to add after the fields and before };</param>
        public void GenerateBody(List<string> beforeFields = null, List<string> afterFields = null)
        {
            Body.Clear();
            if (ParentClass != null)
                Body.Add(string.Format("class {0} : {1} {{", Name, ParentClass.Name));
            else Body.Add(string.Format("class {0} {{", Name));

            if (beforeFields != null)
                Body.AddRange(beforeFields);

            Body.Add("public:");
            Body.AddRange(Fields);

            if (afterFields != null)
                Body.AddRange(afterFields);

            Body.Add("};");
        }

        public void GenerateFields()
        {
            int start = 0;
            if (ParentClass != null)
                start = ParentClass.Size;

            int increment = 4;
            string varType = "int";

            for (int i = start; i < Size; i += increment)
            {
                string hexValue = i.ToString("X");
                Fields.Add(string.Format("  {0} field_{1};",varType ,hexValue));
            }
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
