using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp02_TreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://docs.microsoft.com/pl-pl/dotnet/desktop-wpf/data/data-binding-overview
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region On Loaded

        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /// get every logical drive on the machine 
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // create a new item for it
                // tworzymy nowy element dla każdego dysku logicznego
                var item = new TreeViewItem()
                {
                 // set the header
                 Header = drive,

                // and path
                Tag = drive
                };

                
               // add a dummy item 
               // dodajemy sztuczny element o wartości null aby element dał się rozwinąć
               item.Items.Add(null);

                // Listen out for item being expanded
                // odczytujemy foldery i pliki
                item.Expanded += Folder_Expanded;

                // Add it to the main tree view
                // Dodanie pozycji do TreeView, 
                // jedyne powiązanie z obiektem utworzonym w XAML po x:Name 
                FolderView.Items.Add(item);
            }
        }
       
        #endregion

        #region Folder Expandend
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {

            #region Initial Check

            
            var item = (TreeViewItem)sender;

           /// if the item only containst the dummy data 
           if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // clear dummy data
               item.Items.Clear();

            // get the fullPath
            var fullPath = (string)item.Tag;
            #endregion

            #region Get Folders - pobieramy listę folderów
            // Create the Blank List for directories
            var directories = new List<string>();

            try
            {
                // try to get directories from the folder
                // ignoring any issues doing so
                var dirs = Directory.GetDirectories(fullPath);

                if(dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }

            }
            catch (Exception)
            {
                          
            }

            // for each directory ...
            directories.ForEach(directoryPath =>
            {
                // Create Directory item
                var subItem = new TreeViewItem()
                {
                    // set header as folder name    
                    // Header = Path.GetDirectoryName(directoryPath),
                    Header = GetFileFolderName(directoryPath),
                    // set TAG as full path
                    Tag = directoryPath

                };

                // Add dummy item so we can expand folder
                subItem.Items.Add(null);

                // Handling expanding
                subItem.Expanded += Folder_Expanded;

                // Add this Item do the parent
                item.Items.Add(subItem);

            });
            #endregion

            #region Get Files - pobieramy listę plików

            // Create the Blank List for files
            var files = new List<string>();

            try
            {
                // try to get files from the folder
                // ignoring any issues doing so
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                {
                    files.AddRange(fs);
                }

            }
            catch (Exception)
            {

            }

            // for each file ...
            files.ForEach(filePath =>
            {
                // Create file item
                var subItem = new TreeViewItem()
                {
                    // set header as folder name    
                    // Header = Path.GetDirectoryName(directoryPath),
                    Header = GetFileFolderName(filePath),
                    // set TAG as full path
                    Tag = filePath

                };

                // Add this Item do the parent
                item.Items.Add(subItem);

            });

            #endregion

        }
        #endregion


        #region Helpers
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
            var normalizedPath = path.Replace('/','\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            // jeżeli nie znajdujemy backslash'a zwracamy pierwotną ścieżkę
            if (lastIndex == 0)
                return path;     
            
            return path.Substring(lastIndex+1);
                       
        }
        #endregion

    }
}
