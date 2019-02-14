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

namespace HangMan_
{
    /// <summary>
    /// Logika interakcji dla klasy WordPoolManager.xaml
    /// </summary>
    public partial class WordPoolManager : Window
    {
        List<String> PassableList;
        public WordPoolManager(List<String> list)
        {
            InitializeComponent();
            Listbox.ItemsSource = list;
            PassableList = list;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            PassableList.Add(txtBox.Text.ToLower().Title());
            Listbox.Items.Refresh();
            txtBox.Text = String.Empty;
            Listbox.ScrollIntoView(PassableList[PassableList.Count() - 1]);
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            PassableList.Remove((string)Listbox.SelectedItem);
            Listbox.Items.Refresh();
            Listbox.SelectedIndex = 0;
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
