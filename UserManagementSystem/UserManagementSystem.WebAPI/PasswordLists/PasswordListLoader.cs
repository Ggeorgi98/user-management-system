using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace UserManagementSystem.WebAPI.PasswordLists
{
    public class PasswordListLoader
    {
        private const string Prefix = "UserManagementSystem.WebAPI.PasswordLists.";

        static PasswordListLoader()
        {
            Top100000Passwords = new Lazy<HashSet<string>>(() => LoadPasswordList("darkweb2017-top10000.txt"));
        }

        public static Lazy<HashSet<string>> Top100000Passwords { get; }

        public static HashSet<string> LoadPasswordList(/*string prefix, */string listName)
        {
            HashSet<string> hashset;

            var assembly = typeof(PasswordListLoader).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(Prefix + listName))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    hashset = new HashSet<string>(
                        GetLines(streamReader),
                        StringComparer.OrdinalIgnoreCase);
                }
            }

            return hashset;
        }

        private static IEnumerable<string> GetLines(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                yield return line;
            }
        }
    }
}
