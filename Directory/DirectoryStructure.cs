using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A helper class to query informaction about directories
/// </summary>
namespace WpfApp02_TreeView
{
    public static class DirectoryStructure
    {
        /// <summary>
        /// Find the File Folder name from a full path
        /// Wyodrębniamy nazwę pliku lub katalogu z pełnej ścieżki
        /// </summary>
        /// <param name="path"> The full Path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {

            // if we have no path return empty
            // gdy pusta ścieżka zwracamy pusty string
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Make all slashes -> back slashes
            // zamieniamy wszystkie slashe na backslashe
            // \ jest znakiem ucieczki dlatego musimy wyszukać \\
            // pierwszy znak 
            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            // jeżeli nie znajdujemy backslash'a zwracamy pierwotną ścieżkę
            if (lastIndex == 0)
                return path;

            return path.Substring(lastIndex + 1);

        }
    
    }

    
}
