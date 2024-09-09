using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Calendar
{
    public partial class MainWindow : Window
    {
        private int workingDaysInWeek = 5;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCountdownText();
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCountdownText();
        }

        private void WorkingDaysComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            workingDaysInWeek = int.Parse(((ComboBoxItem)workingDaysComboBox.SelectedItem).Content.ToString());
            UpdateCountdownText();
        }

        private void UpdateCountdownText()
        {
            if (startDatePicker.SelectedDate.HasValue && endDatePicker.SelectedDate.HasValue)
            {
                DateTime startDate = startDatePicker.SelectedDate.Value;
                DateTime endDate = endDatePicker.SelectedDate.Value;
                int workingDays = CountWorkingDays(startDate, endDate);
                int weekendDays = CountWeekendDays(startDate, endDate);
                int totalDays = (endDate - startDate).Days + 1;
                countdownText.Text = $"{workingDays}";
                weekendText.Text = $"{weekendDays}";
                totalDaysText.Text = $"{totalDays}";
            }
        }

        private int CountWorkingDays(DateTime startDate, DateTime endDate)
        {
            int workingDays = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (IsWorkingDay(date))
                {
                    workingDays++;
                }
            }
            return workingDays;
        }

        private int CountWeekendDays(DateTime startDate, DateTime endDate)
        {
            int weekendDays = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (!IsWorkingDay(date))
                {
                    weekendDays++;
                }
            }
            return weekendDays;
        }

        private bool IsWorkingDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Monday && workingDaysInWeek < 1)
            {
                return false;
            }
            if (date.DayOfWeek == DayOfWeek.Tuesday && workingDaysInWeek < 2)
            {
                return false;
            }
            if (date.DayOfWeek == DayOfWeek.Wednesday && workingDaysInWeek < 3)
            {
                return false;
            }
            if (date.DayOfWeek == DayOfWeek.Thursday && workingDaysInWeek < 4)
            {
                return false;
            }
            if (date.DayOfWeek == DayOfWeek.Friday && workingDaysInWeek < 5)
            {
                return false;
            }
            if (date.DayOfWeek == DayOfWeek.Saturday && workingDaysInWeek < 6)
            {
                return false;
            }
            if (date.DayOfWeek == DayOfWeek.Sunday && workingDaysInWeek < 7)
            {
                return false;
            }

            string[] russianHolidays = new string[] {
                "01-01", // Новый год
                "01-07", // Рождество
                "03-08", // Международный женский день
                "05-01", // Праздник весны и труда
                "05-09", // День Победы
                "06-12", // День России
                "11-04", // День народного единства
                "12-31" // Новый год
            };

            string dateStr = date.ToString("MM-dd");
            if (Array.IndexOf(russianHolidays, dateStr) != -1)
            {
                return false;
            }

            return true;
        }
    }
}