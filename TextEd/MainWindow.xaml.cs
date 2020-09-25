using System;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Media;
using GemBox.Document;
using System.Windows.Controls.Primitives;
using Color = System.Windows.Media.Color;
using Inline = System.Windows.Documents.Inline;
using Paragraph = System.Windows.Documents.Paragraph;
using Image = System.Windows.Controls.Image;

namespace TextEd
{
    public partial class MainWindow : Window
    {
        object obj;
        TextSelection sel;

        public MainWindow()
        {
            LoadingScreen();

            InitializeComponent();

            Config();

            LoadFonts();
            LoadFontSizes();
        }

        void LoadingScreen()
        {
            LoadingScreen loadingScreen = new LoadingScreen();

            loadingScreen.Show();
            Thread.Sleep(1000);                        // waits for one second 
            loadingScreen.Close();
        }

        void Config()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            rtb.AcceptsTab = true;                     // makes TAB key work 
            rtb.SpellCheck.IsEnabled = true;           // checks spelling
            rtb.Focus();                               // starts the app with a cursor in the richtextbox
            rtb.BorderThickness = new Thickness(0);    // no border

            cmbFonts.SelectedIndex = 0;                // starts from font: Arial
            cmbFontSize.SelectedIndex = 4;             // fontsize when loaded: 16

            foregroundColor.ShowDropDownButton = false;
            backgroundColor.ShowDropDownButton = false;

            try
            {
                foregroundColor.SelectedColor = Color.FromRgb(0, 0, 0);             // sets black color as foreground for text
                backgroundColor.SelectedColor = Color.FromArgb(0, 255, 255, 255);   // sets transparent as background for text
            }
            catch { }
        }

        void LoadFonts()
        {
            cmbFonts.ItemsSource = Fonts.SystemFontFamilies;
        }

        void LoadFontSizes()
        {
            var fontSizes = new List<int>();

            for (int i = 8; i < 122; i += 2)             // loads font sizes: 8-120 
            {                                            // pattern: 8, 10, 12...
                fontSizes.Add(i);
            }

            cmbFontSize.ItemsSource = fontSizes;
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
            sel = rtb.Selection;

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
            if (((MenuItem)sender).Header.ToString() != "Home")             
            {
                if (((MenuItem)sender).Header.ToString() == "Insert")       
                {
                    HideFormat();
                    HideHome();
                    ShowInsert();
                }                                                           
                else
                {
                    HideHome();
                    HideInsert();    
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

        private void Cut(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(rtb.Selection.Text);                        // sets text of clipboard to the selexted one
            rtb.Selection.Text = string.Empty;                            // removes the selected text
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(rtb.Selection.Text);                        // sets text of clipboard to the selexted one = copies it
        }

        private void TxtFont(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFonts.SelectedItem != null)
            {
                rtb.Selection.ApplyPropertyValue(FontFamilyProperty, cmbFonts.SelectedItem);
            }
        }

        private void TxtFontSize(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rtb.Selection.ApplyPropertyValue(FontSizeProperty, Convert.ToDouble(((ComboBox)sender).SelectedItem));
            }
            catch { }
        }

        private void Alignment(object sender, RoutedEventArgs e)
        {
            sel = rtb.Selection;

            switch (((ToggleButton)sender).Name)
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
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

            dialog.Title = "Exporting...";
            dialog.CreatePrompt = true;

            switch (((MenuItem)sender).Header.ToString())                   // can also be: switch ((string)((MenuItem)sender).Header)
            {
                case "TextEd (.rtf)":
                    dialog.FileName = ".rtf";
                    dialog.Filter = "TextEd RTF (*.rtf)|*.rtf";

                    if ((bool)dialog.ShowDialog())
                    {
                        using (var stream = new MemoryStream())
                        {
                            textRange.Save(stream, DataFormats.Rtf);

                            stream.Position = 0;

                            DocumentModel.Load(stream, LoadOptions.RtfDefault).Save(dialog.FileName);
                        }
                    }
                    break;

                case ".pdf":
                    dialog.Filter = "PDF (*.pdf)|*.pdf|All files (*.*)|*.*";
                    dialog.FileName = ".pdf";

                    if ((bool)dialog.ShowDialog())
                    {
                        using (var stream = new MemoryStream())
                        {
                            textRange.Save(stream, DataFormats.Rtf);

                            stream.Position = 0;

                            DocumentModel.Load(stream, LoadOptions.RtfDefault).Save(dialog.FileName);
                        }
                    }
                    break;

                case ".docx":
                    dialog.Filter = "Microsoft Word Document (*.docx)|*.docx|All files (*.*)|*.*";
                    dialog.FileName = ".docx";

                    if ((bool)dialog.ShowDialog())
                    {
                        using (var stream = new MemoryStream())
                        {
                            textRange.Save(stream, DataFormats.Rtf);

                            stream.Position = 0;

                            DocumentModel.Load(stream, LoadOptions.RtfDefault).Save(dialog.FileName);
                        }
                    }
                    break;

                case ".html":
                    dialog.Filter = "HTML (*.html)|*.html|All files (*.*)|*.*";
                    dialog.FileName = ".html";

                    if ((bool)dialog.ShowDialog())
                    {
                        using (var stream = new MemoryStream())
                        {
                            textRange.Save(stream, DataFormats.Rtf);

                            stream.Position = 0;

                            DocumentModel.Load(stream, LoadOptions.RtfDefault).Save(dialog.FileName);
                        }
                    }
                    break;

                case ".jpg":
                    dialog.Filter = "JPG (*.jpg)|*.jpg*";
                    dialog.FileName = ".jpg";

                    if ((bool)dialog.ShowDialog())
                    {
                        using (var stream = new MemoryStream())
                        {
                            textRange.Save(stream, DataFormats.Rtf);

                            stream.Position = 0;

                            DocumentModel.Load(stream, LoadOptions.RtfDefault).Save(dialog.FileName);
                        }
                    }
                    break;

                case ".png":
                    dialog.Filter = "PNG (*.png)|*.png*";
                    dialog.FileName = ".png";

                    if ((bool)dialog.ShowDialog())
                    {
                        using (var stream = new MemoryStream())
                        {
                            textRange.Save(stream, DataFormats.Rtf);

                            stream.Position = 0;

                            DocumentModel.Load(stream, LoadOptions.RtfDefault).Save(dialog.FileName);
                        }
                    }
                    break;
            }
        }

        private void NewOrNewWindow(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();

            if (((MenuItem)sender).Header.ToString() == "New")
            {
                newWindow.Activate();
                this.Close();
            }
            else
            {
                newWindow.Activate();
                newWindow.Topmost = true;
            }
        }

        private void About(object sender, RoutedEventArgs e)
        {
            AboutSection aboutSection = new AboutSection();

            aboutSection.Show();
        }

        private void CountWords(object sender, TextChangedEventArgs e)
        {
            TextRange docContent = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

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
            sel = rtb.Selection;

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
                rtb.SpellCheck.IsEnabled = false;
                printDialog.PrintVisual(rtb as Visual, "");
            }
        }

        private void ForegroundColor(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            SolidColorBrush brushForeground = new SolidColorBrush((Color)foregroundColor.SelectedColor);

            rtb.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, brushForeground);
        }

        private void BackgroundColor(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            SolidColorBrush brushBackground = new SolidColorBrush((Color)backgroundColor.SelectedColor);

            rtb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, brushBackground);
        }

        private void Superscript(object sender, RoutedEventArgs e)
        {
            sel = rtb.Selection;

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
            sel = rtb.Selection;

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

        private void FindReplace(object sender, RoutedEventArgs e)
        {
            FindReplace findReplaceWindow = new FindReplace(new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd), rtb);

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
                rtb.SelectAll();                                                                            // selects all
                rtb.Selection.Load(new FileStream(openDialog.FileName, FileMode.Open), DataFormats.Rtf);    // replaces the selection with new content
            }
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings(cmbFonts.ItemsSource, cmbFontSize.ItemsSource);
            settingsWindow.Show();
        }

        private void BulletPoints(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleBullets.Execute(null, rtb);
        }

        private void Numbering(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleNumbering.Execute(null, rtb);
        }

    }
}
