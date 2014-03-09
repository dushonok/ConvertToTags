using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConvertToTags
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        List<String> wordsToRemove;
        List<String> wordsToPair;
        Dictionary<String,String> synonyms;
        
        public MainWindow()
        {
            InitializeComponent();

            this.TagsTextBox.Text = "";
            this.InputTextBox.Text = "";

            wordsToRemove = new List<String>();
            wordsToRemove.Add("by");
            wordsToRemove.Add("with");
            wordsToRemove.Add("the");
            wordsToRemove.Add(",");
            wordsToRemove.Add("on");
            wordsToRemove.Add("etsy");
            wordsToRemove.Add("from");

            wordsToPair = new List<String>();
            wordsToPair.Add("golden tone");
            wordsToPair.Add("silver tone");
            wordsToPair.Add("button down");
            wordsToPair.Add("paint brush");
            wordsToPair.Add("Holt Renfrew");
            wordsToPair.Add("gilberte bertrand");

            synonyms = new Dictionary<String, String>();
            synonyms.Add("clipons", "earrings");
            synonyms.Add("anchor", "marine sea naval nautical");
            synonyms.Add("blouse", "shirt");
            synonyms.Add("shirt", "blouse");
        }


        private String convertTextToTags(String txt)
        {
            if (wordsToRemove == null || synonyms == null)
                return txt;

            String result = " " + txt.ToLower();

            foreach (String word in wordsToPair)
            {
                String resWord = word.Replace(' ', '-');
                result = result.Replace(word.ToLower(), resWord); ;
            }

            foreach (String word in wordsToRemove)
            {
                result = result.Replace(" " + word, " ");
            }


            string[] words = result.Split(' ');
            for (int i = 0; i < words.Length; ++i )
            {
                String word = words[i].Trim();
                if (!synonyms.ContainsKey(word))
                    continue;

                result += " ";
                result += synonyms[word];
            }

            String pattern = "[^\\S\\n]+";
            String replacement = " #";
            Regex rgx = new Regex(pattern);
            result = rgx.Replace(result, replacement);
            return result;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (this.TagsTextBox == null)
            {
                return;
            }
            this.TagsTextBox.Text = convertTextToTags(((TextBox)sender).Text);
        }
    }
}
