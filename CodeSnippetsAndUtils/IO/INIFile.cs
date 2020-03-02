using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using SysPath = System.IO.Path;

namespace CodeSnippetsAndUtils.IO
{
    public partial class INIFile
    {
        private IDictionary<string, INISection> Sections { get; set; } = new Dictionary<string, INISection>();

        /// <summary>
        /// The INI file's path and file name.
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Specifies the characters used to identify a comment in the INI file. The default identifiers are '<c>;</c>' and '<c>#</c>'.
        /// </summary>
        public HashSet<char> CommentIdentifiers { get; set; } = new HashSet<char>(new[] {';', '#'});

        public ICollection<string> Keys
        {
            get
            {
                ICollection<string> allKeys = new Collection<string>();
                foreach (var key in Sections.Keys)
                {
                    allKeys.Concat(Sections[key].Keys);
                }
                return allKeys;
            }
        }

        public ICollection<string> Values
        {
            get
            {
                ICollection<string> allVals = new Collection<string>();
                foreach (var key in Sections.Keys)
                {
                    allVals.Concat(Sections[key].Values);
                }
                return allVals;
            }
        }

        public INIFile()
        {
            var dict = new Dictionary<string, string>();
        }

        public T? GetValue<T>(string key, string section) where T : struct
        {
            //Convert.ChangeType()
            return default;
        }

        public void SetValue(string key, string value, string section)
        {

        }

        public INISection GetSectionDict(string section)
        {
            //return Sections[]
            return null;
        }
    }

    public partial class INIFile
    {
        public class INISection
        {
            public string this[string key]
            {
                get => kvPairs[key];
            }

            private readonly IDictionary<string, string> kvPairs = new Dictionary<string, string>();
            public ICollection<string> Keys => kvPairs.Keys;
            public ICollection<string> Values => kvPairs.Values;
        }
    }
}
