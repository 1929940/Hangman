using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace HangMan_
{
    /// <summary>
    /// Logika interakcji dla klasy WordPoolManager.xaml
    /// </summary>
    public partial class WordPoolManager : Window
    {
        ObservableCollection<String> PassableList;
        public WordPoolManager(ObservableCollection<String> list)
        {
            InitializeComponent();
            Listbox.ItemsSource = list;
            PassableList = list;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string tmp = txtBox.Text.ToLower().Title();
            PassableList.Add(tmp);
            txtBox.Text = String.Empty;
            Listbox.ScrollIntoView(PassableList[PassableList.Count() - 1]);

            using (StreamWriter write = new StreamWriter(@"..\..\Resources\wordPoolData.txt", true))
            {
            write.WriteLine(tmp);
            }
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            PassableList.Remove((string)Listbox.SelectedItem);
            Listbox.SelectedIndex = 0;
            using (StreamWriter write = new StreamWriter(@"..\..\Resources\wordPoolData.txt"))
            {
                foreach (var item in PassableList)
                {
                    write.WriteLine(item);
                }
            }


        }

        private void TxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddBtn_Click(this, e);
            }
        }       
    }
}
