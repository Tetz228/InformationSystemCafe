﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class AddOrderWindow : Window
    {
        private Order order;

        public AddOrderWindow()
        {
            InitializeComponent();
        }

        public AddOrderWindow(Order order)
        {
            InitializeComponent();

            this.order = order;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ActionsOrders actionsOrders = new ActionsOrders();

            if (order == null)
            {
                ComboBoxTables.ItemsSource = actionsOrders.FillingComboBoxTables();
                ComboBoxStatusOrders.ItemsSource = actionsOrders.FillingComboBoxStatusOrders();

                using (var db = new CafeEntities())
                {
                    var statusOrder = db.Status_orders.Where(status => status.Name == "Принят").FirstOrDefault();

                    ComboBoxTables.SelectedIndex += 1;
                    ComboBoxStatusOrders.SelectedValue = statusOrder.ID;
                    ComboBoxStatusOrders.IsEnabled = false;
                }
            }
            else
            {
                ComboBoxTables.ItemsSource = actionsOrders.FillingComboBoxTables();
                ComboBoxStatusOrders.ItemsSource = actionsOrders.FillingComboBoxStatusOrders();

                ComboBoxTables.SelectedValue = order.Table.Table_number;
                ComboBoxStatusOrders.SelectedValue = order.Status_orders.ID;
                TextBoxCountPeople.Text = order.Count_person.ToString();

                ComboBoxTables.IsEnabled = false;
                TextBoxCountPeople.IsEnabled = false;
            }
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            ActionsOrders actionsOrders = new ActionsOrders();

            if (order == null)
            {
                try
                {
                    Dictionary<string, int> infoOrder = new Dictionary<string, int>();
                    infoOrder.Add("table", (int)ComboBoxTables.SelectedValue);
                    infoOrder.Add("status", (int)ComboBoxStatusOrders.SelectedValue);
                    infoOrder.Add("people", Convert.ToInt32(TextBoxCountPeople.Text));

                    actionsOrders.AddOrder(infoOrder, out int idOrder);

                    ListDishesAndDrinksInOrderWindow orderDetailsWindow = new ListDishesAndDrinksInOrderWindow(idOrder);

                    Close();

                    orderDetailsWindow.ShowDialog();

                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка при добавлении заказа.", "Ошибка! Некорректный ввод!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                actionsOrders.UpdateStatusOrder(order.ID, (int)ComboBoxStatusOrders.SelectedValue);

                Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}