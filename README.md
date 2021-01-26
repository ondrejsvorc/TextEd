# TextEd v1.0

## Download [here](http://www.mediafire.com/file/k8loihmh13i38uh/file).

TextEd is free a desktop application used for editing text; completely developed using C# and XAML under WPF framework.

The application competed in [ITnetwork summer contest 2020](https://www.itnetwork.cz/csharp/wpf/zdrojove-kody/texted-textovy-procesor).

## Libraries

| Library  | Used for |
| ------------- | ------------- |
| System | Converting to double |
| System.IO | Opening external files and saving a RichTextBox content to a file |
| System.Windows.Controls.Primitives | Commanding ToggleButton |
| System.Windows.Documents  | Saving TextRanges and detecting TextSelection |
| System.Windows.Input;  | Capturing mouse input |
| System.Text.RegularExpressions | Counting words |
| System.Collections.Generic | Uploading int values of font sizes into a ComboBox |
| System.Threading | Keeping a loading screen up for a second |
| System.Windows.Media;  | Accessing fonts and coloring |
| System.Windows.Controls  | Commanding various WPF controls |
| Microsoft.Win32  | Opening dialogs to save or open a file (OpenFileDialog  SaveFileDialog) |
| GemBox.Document | Saving RichTextBox content into various file formats, such as PDF, RTF, HTML, JPG, PNG... |



## To-Do List

| Task  | Status |
| ------------- | ------------- |
| Design Home page using ToolBarTray  | FINISHED  |
| Make Bold, italic, underlined work | FINISHED  |
| Make Copy, Cut work  | FINISHED  |
| Make Alignment work | FINISHED  |
| Upload Fonts to ComboBox and make them work  | FINISHED  |
| Upload FontSizes to ComboBox and make them work | FINISHED  |
| Make ToolBar items bold/normal - MouseEnter + MouseLeave event  | FINISHED  |
| Take care of 3 ToolBarTrays using IF-statements  | FINISHED  |
| Make Export as txt work | FINISHED  |
| Make Export as pdf work  | FINISHED  |
| Make Cursor start its position in richTextBox when program begins  | FINISHED  |
| Make New and New window menuItems work (re-open the app and just open new instance)  | FINISHED  |
| Make a Loading Screen for the app  | FINISHED  |
| Add Words Counter in the lower left corner  | FINISHED  |
| Keep the exported as pdf text formatted  | FINISHED  |
| Make the Tab do its job  | FINISHED  |
| Make .txed file extension open the app  | FINISHED  |
| Add Make Selected Text All Capital + All Small + First Letter Big   | FINISHED  |
| Add About section and design it | FINISHED  |
| Make Print menuItem display a print dialog  | FINISHED  |
| Add Background Highlighter | FINISHED  |
| Add a Color Picker for foreground and for background (WPF Extension Toolkit)  | FINISHED  |
| Add Superscript and Subscript  | FINISHED  |
| Design a Find and Replace menu  | FINISHED  |
| Design a Zoom feature  | FINISHED  |
| When somebody scrolls, more of the page appears  | FINISHED  |
| Make Open function work  | FINISHED  |
| Find / Replace - highlight the found text = make Find function work and unhighlight the text when closed or txtbox is empty = highlight only one word at a time  | FINISHED  |
| Find / Replace - highlight the found text and replace it = make Replace function work | FINISHED  |
| Add Bullet Points feature | FINISHED  |
| Add Numbering feature  | FINISHED  |

| Task  | Status |
| ------------- | ------------- |
| Add Insert Date feature + menu with formats of date  | PENDING  |
| Add Insert Image feature (drag image + put image where the cursor is)  | PENDING |
| Design Insert Symbol window (add symbols) - and make it work (insert the clicked symbol)  | PENDING |
| Make it so when you Print, the format is correct and everything is where it's supposed to be  | PENDING |
| Design the Settings menu and think of customizable features + make it WORK  | PENDING |
| Add Zoom feature (+when user holds CTRL and SCROLLS)  | PENDING |
| Design Insert page using ToolBarTray | PENDING |
| Add Open External Document function -  web browser, pdf, image would open next to page so the user doesn't have to open any other app  | PENDING |


# Bugfixes

| Fixed bugs  |
| ------------- | 
| Application doesn't appear properly on laptops   | 
| Margins in toolbars (hard or impossible to click) |

# Appearance

![](../master/TextEd/TextEd.png)

## Video
[![](https://img.youtube.com/vi/JsOziaOux7g/0.jpg)](https://youtu.be/JsOziaOux7g)



