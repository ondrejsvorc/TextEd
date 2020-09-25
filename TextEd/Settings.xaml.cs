using System.Collections;
using System.Windows;
using System.Windows.Media;

namespace TextEd
{
    public partial class Settings : Window
    {
        private IEnumerable cbFontsItemSource;
        private IEnumerable cbFontSizesItemSource;

        public Settings(IEnumerable cbFontsItemSource, IEnumerable cbFontSizesItemSource)
        {
            this.cbFontsItemSource = cbFontsItemSource;
            this.cbFontSizesItemSource = cbFontSizesItemSource;

            InitializeComponent();

            //AdditionalConfig();
            //GetComponentStates();
        }

        private void BackgroundColor(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            SolidColorBrush background = new SolidColorBrush((System.Windows.Media.Color)backgroundColor.SelectedColor);

            ((MainWindow)Application.Current.MainWindow).scrollViewer.Background = background;
        }

        private void MenuColor(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            SolidColorBrush menuBackground = new SolidColorBrush((System.Windows.Media.Color)menuColor.SelectedColor);

            ((MainWindow)Application.Current.MainWindow).menu.Background = menuBackground;

        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //void AdditionalConfig()
        //{
        //    cmbFonts.ItemsSource = cbFontsItemSource;
        //    cmbFontSize.ItemsSource = cbFontSizesItemSource;
        //}

        //void GetComponentStates()
        //{
        //    cmbFonts.SelectedIndex = Properties.Settings.Default.fontSelected;                              // DOESN'T WORK
        //    cmbFontSize.SelectedIndex = Properties.Settings.Default.fontSizeSelected;                       // DOESN'T WORK
        //}

        //void SetComponentStates()
        //{   
        //    Properties.Settings.Default.fontSelected = Convert.ToInt32(cmbFonts.SelectedIndex);             // DOESN'T WORK
        //    Properties.Settings.Default.fontSizeSelected = Convert.ToInt32(cmbFontSize.SelectedIndex);      // DOESN'T WORK

        //    Properties.Settings.Default.Save();
        //}
    }
}
