using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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


namespace HangMan_
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        int counter = 5;
        string generatedWord;
        HashSet<char> guessedLetters = new HashSet<char>()
        {
            'a', 'e', 'i', 'o', 'u' , 'y'
        };
        bool btnFlag = true;

        ObservableCollection<String> wordPool;

        //List<String> wordPool = new List<string>()
        // {
        //    "Grzeszczyk",
        //    "Agata",
        //    "Kufel",
        //    "Listewnik",
        //    "Czarnecki",
        //    "Mostowski",
        //    "Dudkowska",
        //    "Topolski",
        //    "Berger",
        //    "Denisiuk",
        //    "Morawska",
        //    "Piotrowski",
        //    "Kuciapa"
        //};


        public MainWindow()
        {
            InitializeComponent();
            wordPool = ReadFile();
            generatedWord = GenerateWord();

            counterLabel.Content = counter;
            guessLabel.Content = GenerateDisplay();            

        }
        public ObservableCollection<String> ReadFile()
        {
            string line;
            ObservableCollection<String> tmp = new ObservableCollection<string>();
            StreamReader read = new StreamReader(@"..\..\Resources\wordPoolData.txt");
            while ((line = read.ReadLine()) != null)
            {
                tmp.Add(line);
            }
            return tmp;
        }

        internal String GenerateDisplay()
        {
            string displayedWord = "";
            bool flag;

                foreach (char letter in generatedWord)
                {
                    flag = true;
                    foreach (char guessedLetter in guessedLetters)
                    {
                        if (letter == guessedLetter)
                        {
                            displayedWord += guessedLetter;
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        displayedWord += '#';
                    }
                }
            return displayedWord.Title();
        }
        internal String GenerateWord()
        {
            Random random = new Random();
            if (wordPool.Count() == 0) return String.Empty; // prevents the app from crashing
            int randomIndex = random.Next(wordPool.Count());
            textBox.MaxLength = wordPool[randomIndex].Length;
            return wordPool[randomIndex].ToLower();
        }
        internal void Victory(bool flag)
        {
            if (!flag) return;

            if (MessageBox.Show("You have won. Do you want to play again?", "Play again?", MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                textBox.Text = string.Empty;
                Reset();
            }
            else
            {
                textBox.IsEnabled = false;
                ClickBTN.Content = "Restart";
                btnFlag = false;
            }
        }
        internal void Reset()
        {
            counter = 5;
            counterLabel.Content = counter;
            generatedWord = GenerateWord();

            guessedLetters = new HashSet<char>()
            {
            'a', 'e', 'i', 'o', 'u' , 'y'
            };

            guessLabel.Content = GenerateDisplay();
            ClickBTN.IsEnabled = true;
        }



        private void ClickBTN_Click(object sender, RoutedEventArgs e)
        {
            bool stopFlag = true;
            bool victoryFlag = true;
            if (btnFlag)
            {
                counterLabel.Content = --counter; 
                if (textBox.Text.Length == 1)
                {
                    guessedLetters.Add(Convert.ToChar(textBox.Text.ToLower()));
                }
                else if (textBox.Text.ToLower() == generatedWord)
                {
                    guessLabel.Content = textBox.Text.ToLower().Title();
                    Victory(true);
                    stopFlag = false;
                    victoryFlag = false;
                }                
                if (stopFlag) guessLabel.Content = GenerateDisplay(); 
                if ((string)guessLabel.Content.ToString().ToLower() == generatedWord)
                {
                    Victory(victoryFlag);
                }
                textBox.Text = string.Empty;
            }
            else
            {
                btnFlag = true;
                ClickBTN.Content = "Guess";
                Reset();
                textBox.IsEnabled = true;
            }
            if (counter == 0)
            {
                textBox.IsEnabled = false;
                ClickBTN.Content = "Restart";
                btnFlag = false;
            }
        }

        private void OnEnterEvent(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ClickBTN_Click(this, e);
            }

        }

        private void MenuItem_Click_Reset(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void MenuItem_Click_Quit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_WordPoolManager(object sender, RoutedEventArgs e)
        {
            WordPoolManager wpm = new WordPoolManager(wordPool);

            wpm.ShowDialog();
        }
    }
}
