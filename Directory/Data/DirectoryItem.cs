namespace WpfApp02_TreeView
{
    /// <summary>
    /// Information about a directory such a drive, file or a folder
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }
        
        /// <summary>
        /// the absolutle path to this item
        /// </summary>
        public string FullPath { get; set; }
        
        /// <summary>
        /// the name of this directory item
        /// </summary>
        public string Name { get { return DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
