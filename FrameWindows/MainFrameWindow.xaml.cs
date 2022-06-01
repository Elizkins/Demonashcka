using DemoTest.Database;
using DemoTest.Pages;
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

namespace DemoTest
{
    /// <summary>
    /// Логика взаимодействия для MainFrameWindow.xaml
    /// </summary>
    public partial class MainFrameWindow : Window
    {
        public static ProductsEntities Context { get; set; }
        public MainFrameWindow()
        {
            InitializeComponent();

            Context = new ProductsEntities();

            MainFrame.Navigate(new ProductListPage());
        }
    }
}
