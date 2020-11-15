﻿using System.Windows;

namespace WpfApp1
{
    public partial class CashOrderWindow : Window
    {
        private int IdOrder { get; }

        private decimal Sum { get; }

        private readonly ActionsOrders actionsOrders = new ActionsOrders();

        public CashOrderWindow(int idOrder, decimal sum)
        {
            InitializeComponent();
            IdOrder = idOrder;
            Sum = sum;
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            TotalSum.Content = "Итоговая сумма заказа - " + Sum + " руб.";

            ListDrinks.ItemsSource = ListDishes.ItemsSource = actionsOrders.OutputOrdering_dishes(IdOrder);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}