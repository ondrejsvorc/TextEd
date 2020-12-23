using System;
using Microsoft.Win32;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using GemBox.Document;
using System.Security.Principal;
using System.Management;
using System.Diagnostics;
using System.Reflection;
using Color = System.Windows.Media.Color;
using Inline = System.Windows.Documents.Inline;
using Paragraph = System.Windows.Documents.Paragraph;


namespace TextEd
{
    public partial class MainWindow : Window
    {
        object obj;
        TextSelection sel;

        public MainWindow()
        {
            InitializeComponent();

            GetComponentStates();
            Config();

            LoadFonts();
            LoadFontSizes();
            LoadZoomOptions();
        }

        void Config()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");                          // To make GemBox library work

            page1.AcceptsTab = true;                                               // To make TAB key work 
            page1.SpellCheck.IsEnabled = true;                                     // Checks spelling
            page1.Focus();                                                         // Cursor starts in the richtextbox
            page1.BorderThickness = new Thickness(0);                              // No border

            cmbFonts.SelectedItem = 0;                                             // Starts from none

            cmbFontSize.SelectedIndex = 4;                                         // Fontsize when loaded: 16

            cbZoom.SelectedIndex = 90;                                             // Starts at 100%

            foregroundColor.ShowDropDownButton = false;
            backgroundColor.ShowDropDownButton = false;

            try
            {
                foregroundColor.SelectedColor = Color.FromRgb(0, 0, 0);            // Black color as foreground for text
                backgroundColor.SelectedColor = Color.FromRgb(255, 255, 255);      // White color as background for text
            }
            catch { }
        }

        // WORKS FINE!!! :) JUST DESIGN SETTINGS MENU AND ADD THE COMPONENTS
        void GetComponentStates()
        {
            //checkBoxTest.IsChecked = Properties.Settings.Default.test;
        }

        void SetComponentStates()
        {
            //Properties.Settings.Default.test = (bool)checkBoxTest.IsChecked;
            //Properties.Settings.Default.Save();
        }

        void LoadFonts()
        {
            cmbFonts.ItemsSource = Fonts.SystemFontFamilies;
        }

        void LoadFontSizes()
        {
            List<int> fontSizes = new List<int>();

            for (int i = 8; i < 122; i += 2)             // Load fontsizes: 8-120 
            {                                            // Pattern: 8, 10, 12...
                fontSizes.Add(i);
            }

            cmbFontSize.ItemsSource = fontSizes;
        }

        void LoadZoomOptions()
        {
            for (int i = 10; i < 501; i++)
            {
                cbZoom.Items.Add(i + "%");
            }
        }

        void ShowHome()
        {
            homeTray.Visibility = Visibility.Visible;
        }

        void HideHome()
        {
            homeTray.Visibility = Visibility.Collapsed;
        }

        void ShowInsert()
        {
            insertTray.Visibility = Visibility.Visible;
        }

        void HideInsert()
        {
            insertTray.Visibility = Visibility.Collapsed;
        }

        void ShowFormat()
        {
            formatTray.Visibility = Visibility.Visible;
        }

        void HideFormat()
        {
            formatTray.Visibility = Visibility.Collapsed;
        }

        private void MouseEnterToBold(object sender, MouseEventArgs e)
        {
            ((MenuItem)sender).FontWeight = FontWeights.Bold;
        }

        private void MouseLeaveToNormal(object sender, MouseEventArgs e)
        {
            ((MenuItem)sender).FontWeight = FontWeights.Normal;
        }

        private void RichTxtBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            sel = page1.Selection;

            obj = sel.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = obj.ToString();

            obj = sel.GetPropertyValue(Inline.FontWeightProperty);
            bold.IsChecked = (obj != DependencyProperty.UnsetValue) && (obj.Equals(FontWeights.Bold));

            obj = sel.GetPropertyValue(Inline.FontStyleProperty);
            italic.IsChecked = (obj != DependencyProperty.UnsetValue) && (obj.Equals(FontStyles.Italic));

            obj = sel.GetPropertyValue(Inline.TextDecorationsProperty);
            underline.IsChecked = (obj != DependencyProperty.UnsetValue) && (obj.Equals(TextDecorations.Underline));
        }

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).Header.ToString() != "Home")             // If it's not Home what's been clicked
            {
                if (((MenuItem)sender).Header.ToString() == "Insert")       // and not even insert,
                {
                    HideFormat();
                    HideHome();
                    ShowInsert();
                }                                                           // then it's format
                else
                {
                    HideHome();
                    HideInsert();       // TO-DO CHANGE THE LOGIC (TOOLBARTRAY1 VISIBILITY = TOOLBARTRAY2 VISIBILITY = FALSE)
                    ShowFormat();
                }
            }
            else
            {
                HideFormat();
                HideInsert();
                ShowHome();
            }
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(page1.Selection.Text);                        // Sets text of clipboard to the selexted one
            page1.Selection.Text = string.Empty;                            // Removes the selected text
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(page1.Selection.Text);                        // Sets text of clipboard to the selected one = copies it
        }

        private void TxtFont(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFonts.SelectedItem != null)
            {
                page1.Selection.ApplyPropertyValue(FontFamilyProperty, cmbFonts.SelectedItem);
            }
        }

        private void TxtFontSize(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                page1.Selection.ApplyPropertyValue(FontSizeProperty, Convert.ToDouble(((ComboBox)sender).SelectedItem));
            }
            catch { }
        }

        private void Alignment(object sender, RoutedEventArgs e)
        {
            // TO - DO - CHANGE THE LOGIC, IF POSSIBLE - ISCHECKED

            sel = page1.Selection;

            switch (((System.Windows.Controls.Primitives.ToggleButton)sender).Name)
            {
                case "alignmentLeft":
                    sel.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
                    alignmentCenter.IsChecked = alignmentRight.IsChecked = alignmentJustify.IsChecked = false;
                    break;

                case "alignmentCenter":
                    sel.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
                    alignmentLeft.IsChecked = alignmentRight.IsChecked = alignmentJustify.IsChecked = false;
                    break;

                case "alignmentRight":
                    sel.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
                    alignmentCenter.IsChecked = alignmentLeft.IsChecked = alignmentJustify.IsChecked = false;
                    break;

                case "alignmentJustify":
                    sel.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Justify);
                    alignmentCenter.IsChecked = alignmentLeft.IsChecked = alignmentRight.IsChecked = false;
                    break;
            }
        }

        private void ExportAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            TextRange textRange = new TextRange(page1.Document.ContentStart, page1.Document.ContentEnd);

            string menuItem = ((MenuItem)sender).Header.ToString();

            dialog.Title = "Exporting...";
            dialog.CreatePrompt = true;

            if (menuItem == "TextEd (.rtf)")
            {
                dialog.FileName = ".rtf";
                dialog.Filter = "TextEd RTF (*.rtf)|*.rtf";
            }
            else if (menuItem == ".pdf")
            {
                dialog.FileName = ".pdf";
                dialog.Filter = "PDF (*.pdf)|*.pdf|All files (*.*)|*.*";
            }
            else if (menuItem == ".docx")
            {
                dialog.FileName = ".docx";
                dialog.Filter = "Microsoft Word Document (*.docx)|*.docx|All files (*.*)|*.*";
            }
            else if (menuItem == ".html")
            {
                dialog.FileName = ".html";
                dialog.Filter = "HTML (*.html)|*.html|All files (*.*)|*.*";
            }
            else if (menuItem == ".jpg")
            {
                dialog.FileName = ".jpg";
                dialog.Filter = "JPG (*.jpg)|*.jpg*";
            }
            else if (menuItem == ".png")
            {
                dialog.FileName = ".png";
                dialog.Filter = "PNG (*.png)|*.png*";
            }

            if ((bool)dialog.ShowDialog())
            {
                using (var stream = new MemoryStream())
                {
                    textRange.Save(stream, DataFormats.Rtf);

                    stream.Position = 0;

                    DocumentModel.Load(stream, LoadOptions.RtfDefault).Save(dialog.FileName);
                }
            }
        }

        private void NewOrNewWindow(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();

            switch (((MenuItem)sender).Header)
            {
                case "New":
                    newWindow.Activate();
                    this.Close();
                    break;

                case "New window":
                    newWindow.Activate();
                    newWindow.Topmost = true;
                    break;
            }
        }

        private void About(object sender, RoutedEventArgs e)
        {
            AboutSection aboutSection = new AboutSection();
            aboutSection.Show();
        }

        private void CountWords(object sender, TextChangedEventArgs e)
        {
            TextRange docContent = new TextRange(page1.Document.ContentStart, page1.Document.ContentEnd);

            lbWordsCount.Content = Regex.Matches(docContent.Text, @"[\S]+").Count;  // \S --> Matches any character that is not whitespace 
                                                                                    // +  --> Without it, it would count only chars, but with it it counts word
        }

        private void AaMenuOpen(object sender, MouseButtonEventArgs e)
        {
            ContextMenu contextMenu = ((Image)sender).ContextMenu;

            contextMenu.IsOpen = true;

            e.Handled = true;   // don't take any further action - if this weren't there, it would close the contextmenu
        }

        private void AaOptions(object sender, RoutedEventArgs e)
        {
            sel = page1.Selection;

            switch (((MenuItem)sender).Header)
            {
                case "ALL CAPITAL":
                    sel.Text = sel.Text.ToUpper();
                    break;

                case "all small":
                    sel.Text = sel.Text.ToLower();
                    break;

                case "First letter capital":
                    sel.Text = char.ToUpper(sel.Text[0]) + sel.Text.Substring(1);
                    break;
            }
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if ((bool)printDialog.ShowDialog())
            {
                page1.SpellCheck.IsEnabled = false;
                printDialog.PrintDocument((((IDocumentPaginatorSource)page1.Document).DocumentPaginator), "printing as paginator");
            }
        }

        private void ForegroundColor(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            SolidColorBrush brushForeground = new SolidColorBrush((Color)foregroundColor.SelectedColor);

            page1.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, brushForeground);
        }

        private void BackgroundColor(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            SolidColorBrush brushBackground = new SolidColorBrush((Color)backgroundColor.SelectedColor);

            page1.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, brushBackground);
        }


        private void Superscript(object sender, RoutedEventArgs e)
        {
            sel = page1.Selection;

            if ((bool)subscript.IsChecked)
            {
                subscript.IsChecked = false;
            }

            sel.ApplyPropertyValue(Inline.BaselineAlignmentProperty, BaselineAlignment.Superscript);

            if (!(bool)subscript.IsChecked && !(bool)superscript.IsChecked)
            {
                sel.ApplyPropertyValue(Inline.BaselineAlignmentProperty, BaselineAlignment.Baseline);
            }
        }

        private void Subscript(object sender, RoutedEventArgs e)
        {
            sel = page1.Selection;

            if ((bool)superscript.IsChecked)
            {
                superscript.IsChecked = false;
            }

            sel.ApplyPropertyValue(Inline.BaselineAlignmentProperty, BaselineAlignment.Subscript);

            if (!(bool)subscript.IsChecked && !(bool)superscript.IsChecked)
            {
                sel.ApplyPropertyValue(Inline.BaselineAlignmentProperty, BaselineAlignment.Baseline);
            }
        }

        private void TxtToUrl(object sender, RoutedEventArgs e)
        {
            // TO-DO
            // Open the URL when holding CTRL + Click

            string url = page1.Selection.Text;

            if (url.StartsWith("www.") || url.StartsWith("https://") || url.StartsWith("http://"))
            {
                sel.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                sel.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Color.FromRgb(33, 124, 255)));

            }
        }

        private void FindReplace(object sender, RoutedEventArgs e)      // btnFindReplace click event
        {
            FindReplace findReplaceWindow = new FindReplace(new TextRange(page1.Document.ContentStart, page1.Document.ContentEnd), page1);

            findReplaceWindow.Show();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Title = "Open",
                Filter = "RTF (*.rtf)|*.rtf|All files (*.*)|*.*",
                FileName = ".rtf"
            };

            if ((bool)openDialog.ShowDialog())
            {
                page1.SelectAll();
                page1.Selection.Load(new FileStream(openDialog.FileName, FileMode.Open), DataFormats.Rtf);
            }
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.Show();
        }

        private void BulletPoints(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleBullets.Execute(null, page1);
        }

        private void Numbering(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleNumbering.Execute(null, page1);
        }
    }
}
