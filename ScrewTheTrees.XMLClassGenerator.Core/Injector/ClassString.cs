﻿using System.Collections.Generic;


namespace ScrewTheTrees.XMLClassGenerator.Core.Injector
{
    public class ClassString : IClassElement
    {

        string Text;

        public ClassString(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Injects this Include into a string List
        /// </summary>
        /// <param name="list">List to Inject into.</param>
        /// <returns>Returns whenever it was successfully injected or not.</returns>
        public bool InjectIntoList(List<string> list)
        {
            list.Add(string.Format("{0}", Text));

            return true;
        }

    }
}