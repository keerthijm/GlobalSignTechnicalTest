using System;
using System.IO;
using System.Reflection;

namespace GlobalSignTechnicalTest
{
    internal class FileUtility
    {      
        /// <summary>
        /// Finds the file given the file name.
        /// </summary>
        /// <param name="fileName">The name of the file to find.</param>
        /// <returns>The full path to the file.</returns>
        /// <remarks>The FindFile method is a no-op if the fileName
        /// argument represents a full path to a valid file. Otherwise,
        /// it looks in the same directory as the executing assembly.</remarks>
        internal static string FindFile(string fileName)
        {
            string foundFile = fileName;
            if (!File.Exists(foundFile))
            {
                foundFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Path.GetFileName(fileName));
            }

            if (!File.Exists(foundFile))
            {
                throw new FileNotFoundException("Could not find the specified file.", fileName);
            }

            return foundFile;
        }    
    }
}