﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class AddDishWindow : Window
    {
        private readonly ActionsOrders actionsOrders = new ActionsOrders();

        private int IdOrder { get; }

        public AddDishWindow(int idOrder)
        {
            InitializeComponent();

            IdOrder = idOrder;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxTypesDishes.ItemsSource = actionsOrders.FillingComboBoxTypesDishes();
            ComboBoxDishes.ItemsSource = actionsOrders.FillingComboBoxDishes();

            ComboBoxTypesDishes.SelectedIndex += 1;
        }

        private void ComboBoxTypesDishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxDishes.ItemsSource = actionsOrders.OutputByTypesDishes((int)ComboBoxTypesDishes.SelectedValue);

            ComboBoxDishes.SelectedIndex += 1;
        }

        private void ButtonСonfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new CafeEntities();
                var selectPriceDish = db.Dishes.Where(dish => dish.ID == (int)ComboBoxDishes.SelectedValue).Select(price => price.Price).FirstOrDefault();

                Dictionary<string, int> infoDishInOrder = new Dictionary<string, int>
                {
                     { "dish", (int)ComboBoxDishes.SelectedValue },
                     { "countDish", Convert.ToInt32(TextBoxCountDishes.Text) },
                     { "idOrder", IdOrder }
                };

                actionsOrders.AddDishOrder(infoDishInOrder, selectPriceDish);

                Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Ошибка при добавлении блюда или напитка в заказ.", "Ошибка! Некорректный ввод!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
