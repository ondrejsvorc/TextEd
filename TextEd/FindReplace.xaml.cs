using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace TextEd
{
    public partial class FindReplace : Window
    {
        public TextRange textRange;
        public RichTextBox rich;

        public FindReplace(TextRange textRange, RichTextBox rich)
        {
            InitializeComponent();

            this.textRange = textRange;
            this.rich = rich;
        }

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            switch (((MenuItem)sender).Name)
            {
                case "replace":
                    find.IsChecked = false;
                    replace.IsChecked = true;

                    lbFind.Content = "Replace:";
                    lbReplace.Content = "Replace with:";
                    btnConfirm.Content = "Replace";

                    btnConfirm.Visibility = lbReplace.Visibility = txtBoxReplaceWith.Visibility = Visibility.Visible;
                    break;

                case "find":
                    replace.IsChecked = false;
                    find.IsChecked = true;

                    btnConfirm.Visibility = lbReplace.Visibility = txtBoxReplaceWith.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void MouseEnterToBold(object sender, MouseEventArgs e)
        {
            ((MenuItem)sender).FontWeight = FontWeights.Bold;
        }

        private void MouseLeaveToNormal(object sender, MouseEventArgs e)
        {
            ((MenuItem)sender).FontWeight = FontWeights.Normal;
        }

        // yield return returs object of IEnumerable type, each element one at a time
        // *yield break: stops
        private static IEnumerable<TextRange> ConvertWordsToTextRanges(FlowDocument doc)               
        {
            TextPointer pointer = doc.ContentStart;
            while (pointer != null)                                                                    // While there is some more text
            {
                if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = pointer.GetTextInRun(LogicalDirection.Forward);
                    MatchCollection matches = Regex.Matches(textRun, @"\w+");

                    foreach (Match match in matches)
                    {
                        int startIndex = match.Index;                                   // First char
                        int length = match.Length;                                      // The lenght
                        TextPointer start = pointer.GetPositionAtOffset(startIndex);    // Gets to the first char
                        TextPointer end = start.GetPositionAtOffset(length);            // Gets to the last char
                        yield return new TextRange(start, end);                         // Selects the match
                    }
                }

                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);     // Checks if there is more text
            }
        }
        
        private void RegexNotCaseSensitive()
        {
            if (Regex.IsMatch(textRange.Text.ToLower(), @"\b" + txtBoxFind.Text.ToLower() + @"\b")) //word break regex
            {
                IEnumerable<TextRange> wordRanges = ConvertWordsToTextRanges(rich.Document);
                foreach (TextRange wordRange in wordRanges)
                {
                    if (wordRange.Text.ToLower() == txtBoxFind.Text.ToLower())
                    {
                        wordRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Peru);
                    }
                }
            }
        }

        private void RegexCaseSensitive()
        {
            if (Regex.IsMatch(textRange.Text, @"\b" + txtBoxFind.Text + @"\b")) //word break regex
            {
                IEnumerable<TextRange> wordRanges = ConvertWordsToTextRanges(rich.Document);
                foreach (TextRange wordRange in wordRanges)
                {
                    if (wordRange.Text == txtBoxFind.Text)
                    {
                        wordRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Peru);
                    }
                }
            }
        }

        private void Replace(object sender, RoutedEventArgs e)
        {
            IEnumerable<TextRange> wordRanges = ConvertWordsToTextRanges(rich.Document);

            foreach (TextRange wordRange in wordRanges)
            {
                if (wordRange.GetPropertyValue(TextElement.BackgroundProperty) == Brushes.Peru)
                {
                    wordRange.Text = txtBoxReplaceWith.Text;
                }
            }
        }

        private void Highlight(object sender, TextChangedEventArgs e)
        {
            if ((bool)checkBoxCaseSensitive.IsChecked)
            {
                RegexCaseSensitive();
            }
            else
            {
                RegexNotCaseSensitive();
            }

            if (txtBoxFind.Text == string.Empty)
            {
                Unhighlight();
            }
        }

        private void Unhighlight()
        {
            IEnumerable<TextRange> wordRanges = ConvertWordsToTextRanges(rich.Document);

            foreach (TextRange wordRange in wordRanges)
            {
                if (wordRange.GetPropertyValue(TextElement.BackgroundProperty) == Brushes.Peru)
                {
                    wordRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);
                }
            }
        }

        private void WindowIsClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Unhighlight();
        }
    }
}
