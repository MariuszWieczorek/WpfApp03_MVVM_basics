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
    /// https://www.youtube.com/watch?v=U2ZvZwDZmJU
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
        
        #endregion

    }
}
