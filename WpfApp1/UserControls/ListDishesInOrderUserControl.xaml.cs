﻿using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp1.Сook;

namespace WpfApp1
{
    public partial class ListDishesInOrderUserControl : System.Windows.Controls.UserControl
    {
        private readonly ActionsOrders actionsOrders;

        private int IdOrder { get; }
        private string PostName { get; }

        public ListDishesInOrderUserControl(int idOrder, string postName)
        {
            InitializeComponent();

            actionsOrders = new ActionsOrders();
            IdOrder = idOrder;
            PostName = postName;

            if (postName == "Повар" || postName == "Администратор")
                DataGridTextColumnStatusDish.Visibility = Visibility.Visible;

            UploadOrderingDishes();
        }

        public void UploadOrderingDishes()
        {
            DataGridOrderingDishes.ItemsSource = actionsOrders.OutputOrdering_dishes(IdOrder);
        }

        private void DataGridOrderingDishes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridOrderingDishes.SelectedItem = null;
        }

        public void ConfirmOrder()
        {
            if (DataGridOrderingDishes.Items.Count != 0)
            {
                if (PostName == "Официант")
                    actionsOrders.CalculationAndAddSumOrder(IdOrder);

                Window.GetWindow(this).Close();
            }
            else
                System.Windows.Forms.MessageBox.Show("Ошибка! В заказе отсутствуют блюда!", "Нет блюд в заказе", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void AddDish()
        {
            AddDishWindow addDishAndDrinkWindow = new AddDishWindow(IdOrder);
            addDishAndDrinkWindow.ShowDialog();

            UploadOrderingDishes();
        }

        private void DataGridOrderingDishes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PostName == "Повар")
            {
                if (DataGridOrderingDishes.SelectedItem != null)
                {
                    Ordering_dishes ordering_Dishes = DataGridOrderingDishes.SelectedItem as Ordering_dishes;

                    EditStatusDishWindow editStatusDishWindow = new EditStatusDishWindow(ordering_Dishes);
                    editStatusDishWindow.ShowDialog();

                    UploadOrderingDishes();
                }
            }
        }
    }
}
