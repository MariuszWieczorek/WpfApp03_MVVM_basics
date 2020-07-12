using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

/* Konwertery wartości są bardzo często używane przy wiązaniu danych. Oto kilka podstawowych przykładów:
 * 
 * Chciałbyś zaznaczyć CheckBox na podstawie wartości zmiennej, ale ta wartość jest tekstem np. "tak" lub "nie", a nie wartością typu Boolean (true/false).
 * Masz wartość liczbową, ale chciałbyś pokazać wartość zero w jednych przypadkach, a liczby dodatnie w pozostałych
 * Masz wielkość pliku wyrażoną w bajtach, ale chciałbyś ją pokazać w bajtach, kilobajtach, megabajtach lub gigabajtach, w zależności od jej wielkości
 * Na przykład chcesz ustawić zaznaczenie kontrolki CheckBox na podstawie zmiennej typu Boolean, 
 * ale na odwrót - to znaczy chciałbyś, aby CheckBox był zaznaczony, gdy zmienna jest równa "false",
 * a niezaznaczony, gdy zmienna jest równa "true".
 * Mógłbyś nawet wykorzystać konwerter do wygenerowania obrazka i ustawienia go w ImageSource w zależności od wartości zmiennej.
 * Na przykład sygnał zielony dla wartości "true" i czerwony dla "false". Możliwości są praktycznie nieograniczone!
 * 
 * Te niewielkie klasy, które implementują interfejs IValueConverter, będą działać jak pośrednicy i tłumaczyć wartości między źródłem a miejscem docelowym.
 * We jakichkolwiek przypadkach, gdy potrzebujesz dokonać transformacji danych w jedną lub w drugą stronę - najprawdopodobniej potrzebujesz konwertera.
 * 
 * Jak wspomniano wcześniej - konwerter wartości WPF wymaga zaimplementowania interfejsu IValueConverter lub alternatywnie interfejsu IMultiValueConverter
 * Oba interfejsy wymagają tylko zaimplementowania dwóch metod: Convert() i ConvertBack().
 * Jak sama nazwa wskazuje, te metody będą używane do konwersji wartości do docelowego formatu oraz w drugą stronę - do formatu źródłowego.
 * 
 */

namespace WpfApp02_TreeView
{
    /// <summary>
    /// Converts a full path to a specific image type of drive, folder or file
    /// </summary>
    [ValueConversion(typeof(string),typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        // konwertujemy string to image
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // możemy w tym miejscu zwrócić bezpośrednio ścieżkę do obrazka, tak jak byśmy wpisali ją w kodzie XAML
            // return "Images/drive.png";


            var path = (string)value;
            if (path == null)
                return null;

            
            // domyślnie zakładamy, że węzeł w drzewie katalogów jest plikiem
            var image = "Images/file.png";

            // za pomocą metody GetFileFolderName wyodrębniamy ze ścieżki nazwę pliku lub katalogu
            // jeżeli jest ona pusta to zakłądamy, że mamy do czynienia z napędem
            // if the name is blank we presume it's drive as we cannot have a blank file or a folder name
            // dalej po przez sprawdzenie atrybutu pliku sprawdzamy czy jest to katalog
            // var name = MainWindow.GetFileFolderName(path);
            string name = "";
                        
            
            if (string.IsNullOrEmpty(name))
                image = "Images/drive.png";
            else if(new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory) )
                image = "Images/folder-closed.png";

            return new BitmapImage(new Uri($"pack://application:,,,/{image}")); 
            
        }

        // odwrotnie z image do string, w tym przypadku nie korzystamy z niej, więc zostawiamy domyślną implementację
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
