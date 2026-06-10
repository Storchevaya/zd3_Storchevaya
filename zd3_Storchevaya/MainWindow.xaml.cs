using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace zd3_Storchevaya
{
    public partial class MainWindow : Window
    {
        // Коллекция 1: List для хранения объектов
        private List<RoadWork> roadWorksList;

        // Коллекция 2: List для отображения
        private List<string> displayList;

        public MainWindow()
        {
            InitializeComponent();

            roadWorksList = new List<RoadWork>();
            displayList = new List<string>();
        }

        // Обновление списка на экране
        private void UpdateDisplay()
        {
            lstRoadWorks.Items.Clear();
            foreach (var work in roadWorksList)
            {
                lstRoadWorks.Items.Add(work.GetInfo());
            }
        }

        // Проверка ввода

        private bool ValidateBasicInput()
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Введите местоположение!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtLocation.Focus();
                return false;
            }

            if (!double.TryParse(txtWidth.Text, out double width) || width <= 0)
            {
                MessageBox.Show("Введите корректную ширину (положительное число)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtWidth.Focus();
                return false;
            }

            if (!double.TryParse(txtLength.Text, out double length) || length <= 0)
            {
                MessageBox.Show("Введите корректную длину (положительное число)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtLength.Focus();
                return false;
            }

            if (!double.TryParse(txtMass.Text, out double mass) || mass <= 0)
            {
                MessageBox.Show("Введите корректную массу покрытия (положительное число)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMass.Focus();
                return false;
            }

            return true;
        }

        //проверка ввода коэффицента
        private bool ValidateReinforcedInput()
        {
            if (!int.TryParse(txtP.Text, out int p) || p < 1 || p > 10)
            {
                MessageBox.Show("Введите коэффициент P от 1 до 10!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtP.Focus();
                return false;
            }

            return true;
        }

        // Добавление

        // метод
        public void AddRoadWork(RoadWork work)
        {
            if (work != null)
            {
                roadWorksList.Add(work);
                UpdateDisplay();
            }
        }

        // перегрузка 1 - обычная дорога
        public void AddRoadWork(double width, double length, double mass,
                                string location, DateTime startDate)
        {
            RoadWork work = new RoadWork(width, length, mass, location, startDate);
            AddRoadWork(work);
        }

        // перегрузка 2 - усиленная дорога
        public void AddRoadWork(double width, double length, double mass,
                                string location, DateTime startDate,
                                int p, string weather, string contractor)
        {
            ReinforcedRoadWork work = new ReinforcedRoadWork(width, length, mass,
                                                              location, startDate,
                                                              p, weather, contractor);
            AddRoadWork(work);
        }

        // Удаление

        // метод
        public void RemoveRoadWork(RoadWork work)
        {
            if (work != null)
            {
                roadWorksList.Remove(work);
                UpdateDisplay();
            }
        }

        // Перегрузка 1 - по индексу
        public void RemoveRoadWork(int index)
        {
            if (index >= 0 && index < roadWorksList.Count)
            {
                roadWorksList.RemoveAt(index);
                UpdateDisplay();
            }
        }

        // Перегрузка 2 - по местоположению
        public void RemoveRoadWork(string location)
        {
            var work = roadWorksList.FirstOrDefault(w => w.Location == location);
            if (work != null)
            {
                roadWorksList.Remove(work);
                UpdateDisplay();
            }
        }

        // LINQ метод

        // группировка по типу (Обычные / Усиленные)
        public void ShowStatistics()
        {
            if (roadWorksList.Count == 0)
            {
                MessageBox.Show("Нет объектов для статистики! Добавьте хотя бы один объект.", "Статистика", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Использование LINQ
            var grouped = roadWorksList.GroupBy(w => w is ReinforcedRoadWork ? "Усиленные" : "Обычные");

            string result = "СТАТИСТИКА\n";

            foreach (var group in grouped)
            {
                result += $"📁 {group.Key}:\n";
                result += $"   • Количество: {group.Count()} объектов\n";
                result += $"   • Среднее качество: {group.Average(w => w.GetQuality()):F2} тонн\n";
                result += $"   • Максимальное качество: {group.Max(w => w.GetQuality()):F2} тонн\n";
                result += $"   • Минимальное качество: {group.Min(w => w.GetQuality()):F2} тонн\n\n";
            }

            result += "═══════════════════════════════════════════════════\n";
            result += $"📊 Всего объектов: {roadWorksList.Count}";

            MessageBox.Show(result, "Статистика", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // обрабочики кнопок

        private void ChkIsReinforced_Checked(object sender, RoutedEventArgs e)
        {
            panelReinforced.Visibility = Visibility.Visible;
        }

        private void ChkIsReinforced_Unchecked(object sender, RoutedEventArgs e)
        {
            panelReinforced.Visibility = Visibility.Collapsed;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateBasicInput()) return;

                double width = double.Parse(txtWidth.Text);
                double length = double.Parse(txtLength.Text);
                double mass = double.Parse(txtMass.Text);
                string location = txtLocation.Text;
                DateTime startDate = dpStartDate.SelectedDate ?? DateTime.Today;

                if (chkIsReinforced.IsChecked == true)
                {
                    if (!ValidateReinforcedInput()) return;

                    int p = int.Parse(txtP.Text);
                    string weather = txtWeather.Text;
                    string contractor = txtContractor.Text;

                    //перегрузка 2 - добавление
                    AddRoadWork(width, length, mass, location, startDate, p, weather, contractor);
                    txtStatus.Text = "✓ Усиленная дорога добавлена";
                }
                else
                {
                    //перегрузка 1 - добавление
                    AddRoadWork(width, length, mass, location, startDate);
                    txtStatus.Text = "✓ Обычная дорога добавлена";
                }

                txtLocation.Text = "";
                txtWidth.Text = "10";
                txtLength.Text = "100";
                txtMass.Text = "200";
                txtP.Text = "5";
                txtWeather.Text = "";
                txtContractor.Text = "";
                chkIsReinforced.IsChecked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtStatus.Text = "Ошибка при добавлении";
            }
        }

        //кнопка удаления
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstRoadWorks.SelectedIndex >= 0)
            {
                //перегрузка 1 - удаление
                RemoveRoadWork(lstRoadWorks.SelectedIndex);
                txtStatus.Text = "Объект удален";
            }
            else
            {
                MessageBox.Show("Выберите объект для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtStatus.Text = "Не выбран объект";
            }
        }

        //кнопка удаления по местоположению
        private void BtnDeleteByLocation_Click(object sender, RoutedEventArgs e)
        {
            string location = txtDeleteLocation.Text.Trim();

            if (string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Введите местоположение для удаления!", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // пергрузка 2 - удаление
            RemoveRoadWork(location);

            txtStatus.Text = $"Удалены объекты с местоположением: {location}";
            txtDeleteLocation.Text = "";
        }

        private void BtnGroup_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics();
        }
    }
}