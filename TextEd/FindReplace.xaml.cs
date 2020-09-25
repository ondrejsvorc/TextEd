using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace TextEd
{
    public partial class FindReplace : Window
    {
        private TextRange range;
        private RichTextBox rtb;

        public FindReplace(TextRange textRange, RichTextBox rich)
        {
            InitializeComponent();

            range = textRange;
            rtb = rich;
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

        static IEnumerable<TextRange> ConvertWordsToTextRanges(FlowDocument doc)
        {
            TextPointer pointer = doc.ContentStart;
            while (pointer != null)                                                     // while there is some more text
            {
                if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = pointer.GetTextInRun(LogicalDirection.Forward);
                    MatchCollection matches = Regex.Matches(textRun, @"\w+");

                    foreach (Match match in matches)
                    {
                        int startIndex = match.Index;                                   // first char
                        int length = match.Length;                                      // the lenght
                        TextPointer start = pointer.GetPositionAtOffset(startIndex);    // gets to the first char
                        TextPointer end = start.GetPositionAtOffset(length);            // gets to the last char
                        yield return new TextRange(start, end);                         // selects the match
                    }
                }
                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);     // checks if there is more text
            }
        }
        
        void RegexNotCaseSensitive()
        {
            if (Regex.IsMatch(range.Text.ToLower(), @"\b" + txtBoxFind.Text.ToLower() + @"\b"))     //word break regex
            {
                IEnumerable<TextRange> wordRanges = ConvertWordsToTextRanges(rtb.Document);
                foreach (TextRange wordRange in wordRanges)
                {
                    if (wordRange.Text.ToLower() == txtBoxFind.Text.ToLower())
                    {
                        wordRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Peru);
                    }
                }
            }
        }

        void RegexCaseSensitive()
        {
            if (Regex.IsMatch(range.Text, @"\b" + txtBoxFind.Text + @"\b"))                         //word break regex
            {
                IEnumerable<TextRange> wordRanges = ConvertWordsToTextRanges(rtb.Document);
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
            IEnumerable<TextRange> wordRanges = ConvertWordsToTextRanges(rtb.Document);

            foreach (TextRange wordRange in wordRanges)
            {
                if (wordRange.GetPropertyValue(TextElement.BackgroundProperty) == Brushes.Peru)
                {
                    wordRange.Text = txtBoxReplaceWith.Text;
                }
            }
            Unhighlight();                                              // once replaced, unhighlight
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

        void Unhighlight()
        {
            IEnumerable<TextRange> wordRanges = ConvertWordsToTextRanges(rtb.Document);

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
