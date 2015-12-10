using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.IO
{
    public static class IOHelper
    {
        private static HashSet<char> invalidFileNameChars = new HashSet<char>(
            Path.GetInvalidFileNameChars()
        );
        private static HashSet<char> invalidDirChars = new HashSet<char>(
            Path.GetInvalidPathChars()
        );

        public static void EnsurePathExists(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path == "")
                throw new ArgumentException("The given path may not be the empty string.");

            if (path.Any(x => invalidDirChars.Contains(x)))
                throw new ArgumentException("The given path contains invalid characters.");

            string[] segments = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            path = "";
            foreach(string segment in segments)
            {
                path += segment;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path += Path.DirectorySeparatorChar;
            }
        }
        public static string[] GetDirectories(string path)
        {
            EnsurePathExists(path);
            return Directory.GetDirectories(path);
        }
        public static string[] GetFiles(string path)
        {
            EnsurePathExists(path);
            return Directory.GetFiles(path);
        }

        public static bool TryReadInto<T>(string file, T target)
            where T : class
        {
            if (!File.Exists(file))
                return false;

            JsonSerializer serializer = JsonSerializer.CreateDefault();
            serializer.MissingMemberHandling = MissingMemberHandling.Error;
            bool hasError = false;
            serializer.Error += (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e) =>
            {
                hasError = true;
            };
            serializer.Populate(
                new JsonTextReader(new StreamReader(file)),
                target
            );
            return !hasError;
        }
    }
}
