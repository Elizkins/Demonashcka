using DemoTest.Database;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        int _pageNumber = 1;
        int _pagesCount = 0;
        List<Product> _products;
        public ProductListPage()
        {
            InitializeComponent();

            SortCBox.ItemsSource = new List<string> { "Наименование(по возрастанию)", "Наименование(по убыванию)", "Номер цеха(по возрастанию)", "Номер цеха(по убыванию)", "Мин. стоимость(по возрастанию)", "Мин. стоимость(по убыванию)" };

            List<string> productTypes = new List<string>();
            productTypes.Add("Все");
            productTypes.AddRange(MainFrameWindow.Context.ProductTypes.Select(pt => pt.Title));
            FilterCBox.ItemsSource = productTypes;
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }
        private void SortSelectedBox(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
        private void FilterSelectedBox(object sender, SelectionChangedEventArgs e)
        {
            UpdateList(e.AddedItems[0] as string);
        }
        private void UpdateList(string filterText = null)
        {
            _products = MainFrameWindow.Context.Products.ToList();

            if(FilterCBox.SelectedIndex != 0)
            {
                _products = _products.Where(p => p.ProductType.Title == filterText).ToList();
            }

            _products = _products.Where(p => p.Title.ToUpper().Contains(SearchBox.Text.ToUpper()) ||
                                                                    (p.Description != null && p.Description.ToUpper().Contains(SearchBox.Text.ToUpper()))).ToList();

            switch (SortCBox.SelectedIndex)
            {
                case 0:
                    _products = _products.OrderBy(p => p.Title).ToList();
                    break;
                case 1:
                    _products = _products.OrderByDescending(p => p.Title).ToList();
                    break;
                case 2:
                    _products = _products.OrderBy(p => p.ProductionWorkshopNumber).ToList();
                    break;
                case 3:
                    _products = _products.OrderByDescending(p => p.ProductionWorkshopNumber).ToList();
                    break;
                case 4:
                    _products = _products.OrderBy(p => p.MinCostForAgent).ToList();
                    break;
                case 5:
                    _products = _products.OrderByDescending(p => p.MinCostForAgent).ToList();
                    break;
                default:
                    break;
            }

            ProductList.ItemsSource = _products.Take(20);

            _pageNumber = 1;
            _pagesCount = 0;

            PageSPanel.Children.Clear();
            PageSPanel.Children.Add(GetButton("<"));
            for (int i = 1; i * 20 <= _products.Count(); i++)
            {
                PageSPanel.Children.Add(GetButton($"{i}"));
                _pagesCount++;
            }
            PageSPanel.Children.Add(GetButton(">"));
        }

        private Button GetButton(string value)
        {
            Button button = new Button
            {
                Content = $"{value}",
                Background = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(0)
            };
            button.Click += PageButtonClick;
            return button;
        }

        private void PageButtonClick(object sender, RoutedEventArgs e)
        {
            string pageSymbol = (string)(sender as Button).Content;
            switch (pageSymbol)
            {
                case "<":
                    if (_pageNumber != 1)
                    {
                        _pageNumber--;
                        ProductList.ItemsSource = _products.Skip(20 * (_pageNumber - 1)).Take(20).ToList();
                    }
                    break;
                case ">":
                    if (_pageNumber != _pagesCount)
                    {
                        _pageNumber++;
                        ProductList.ItemsSource = _products.Skip(20 * (_pageNumber - 1)).Take(20).ToList();
                    }
                    break;
                default:
                    _pageNumber = Convert.ToInt32(pageSymbol);
                    ProductList.ItemsSource = _products.Skip(20 * (_pageNumber - 1)).Take(20).ToList();
                    break;
            }
        }

        private void OpenProductEditPage(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("dddd");
        }
    }
}
