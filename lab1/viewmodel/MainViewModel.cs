using lab1.model;
using lab1.utils;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace lab1.viewmodel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private double _segment1X1;
        private double _segment1Y1;
        private double _segment1X2;
        private double _segment1Y2;

        private double _segment2X1;
        private double _segment2Y1;
        private double _segment2X2;
        private double _segment2Y2;

        private string _intersectionResult;
        private string _intersectionResultX;
        private string _intersectionResultY;
        private string _statusOfLoadFile;

        public double Segment1X1
        {
            get { return _segment1X1; }
            set
            {
                _segment1X1 = value;
                OnPropertyChanged();
                CalculateClick();
            }
        }
        public double Segment1X2
        {
            get { return _segment1X2; }
            set
            {
                _segment1X2 = value;
                OnPropertyChanged();
                CalculateClick();
            }
        }
        public double Segment1Y1
        {
            get { return _segment1Y1; }
            set
            {
                _segment1Y1 = value;
                OnPropertyChanged();
                CalculateClick();
            }
        }
        public double Segment1Y2
        {
            get { return _segment1Y2; }
            set
            {
                _segment1Y2 = value;
                OnPropertyChanged();
                CalculateClick();
            }

        }
        public double Segment2X1
        {
            get { return _segment2X1; }
            set
            {
                _segment2X1 = value;
                OnPropertyChanged();
            }
        }
        public double Segment2X2
        {
            get { return _segment2X2; }
            set
            {
                _segment2X2 = value;
                OnPropertyChanged();
                CalculateClick();
            }
        }
        public double Segment2Y1
        {
            get { return _segment2Y1; }
            set
            {
                _segment2Y1 = value;
                OnPropertyChanged();
                CalculateClick();
            }
        }
        public double Segment2Y2
        {
            get { return _segment2Y2; }
            set
            {
                _segment2Y2 = value;
                OnPropertyChanged();
                CalculateClick();
            }
        }

        public string StatusOfLoadFile
        {
            get { return _statusOfLoadFile; }
            set
            {
                _statusOfLoadFile = value;
                OnPropertyChanged();
            }
        }

        public string IntersectionResult
        {
            get { return _intersectionResult; }
            set
            {
                _intersectionResult = value;
                OnPropertyChanged();
            }
        }

        public string IntersectionResultX
        {
            get { return _intersectionResultX; }
            set
            {
                _intersectionResultX = value;
                OnPropertyChanged();
            }
        }

        public string IntersectionResultY
        {
            get { return _intersectionResultY; }
            set
            {
                _intersectionResultY = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        // Команда для обработки нажатия на кнопку "Рассчитать"
        public ICommand CalculateClickCommand { get; private set; }

        public ICommand LoadDataFromFileCommand { get; private set; }

        public MainViewModel()
        {
            StatusOfLoadFile = "Файл не загружен";
            // Создаем команду и передаем в нее метод, который будет вызываться при выполнении команды
            CalculateClickCommand = new RelayCommand(CalculateClick);
            LoadDataFromFileCommand = new RelayCommand(LoadDataFromFile);
        }

        private void LoadDataFromFile()
        {
            // Диалог выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt"; // Устанавливаем фильтр для файлов

            if (openFileDialog.ShowDialog() == true)
            {
                // Получение пути к выбранному файлу
                string filePath = openFileDialog.FileName;

                if (Path.GetExtension(filePath) != ".txt")
                {
                    // Если выбран неподходящий файл, выводим предупреждение
                    MessageBox.Show("Допустимы только файлы с расширением .txt", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Завершаем метод
                }

                try
                {
                    DataParser parser = new DataParser();
                    MyData data = parser.Parse(filePath);

                    if (data != null)
                    {
                        // Присваиваем значения из файла свойствам ViewModel
                        Segment1X1 = data.segment1x1;
                        Segment1Y1 = data.segment1y1;
                        Segment1X2 = data.segment1x2;
                        Segment1Y2 = data.segment1y2;
                        Segment2X1 = data.segment2x1;
                        Segment2Y1 = data.segment2y1;
                        Segment2X2 = data.segment2x2;
                        Segment2Y2 = data.segment2y2;

                        StatusOfLoadFile = "Файл загружен успешно";
                        // Вызываем метод расчета пересечения
                        CalculateIntersection(Segment1X1, Segment1Y1, Segment1X2, Segment1Y2, Segment2X1, Segment2Y1, Segment2X2, Segment2Y2);
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка при чтении файла: неверный формат", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        StatusOfLoadFile = "Файл не загружен";
                    }
                }
                catch (Exception ex)
                {
                    StatusOfLoadFile = "Файл не загружен";
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        // Метод, который будет вызываться при нажатии на кнопку
        private void CalculateClick()
        {
            // Ваша логика обработки нажатия на кнопку "Рассчитать"
            CalculateIntersection(Segment1X1, Segment1Y1, Segment1X2, Segment1Y2, Segment2X1, Segment2Y1, Segment2X2, Segment2Y2);
        }


        private void CalculateIntersection(double segment1X1, double segment1Y1, double segment1X2, double segment1Y2, double segment2X1, double segment2Y1, double segment2X2, double segment2Y2)
        {
            try
            {
                // Создание отрезков и расчет пересечения
                Segment segment1 = new Segment(segment1X1, segment1Y1, segment1X2, segment1Y2);
                Segment segment2 = new Segment(segment2X1, segment2Y1, segment2X2, segment2Y2);

                if (segment1.AreCrossing(segment2))
                {
                    IntersectionResult = "Отрезки пересекаются в точке:";
                    IntersectionResultX = $"X: {segment1.CrossingX}";
                    IntersectionResultY = $"Y: {segment1.CrossingY}";
                }
                else
                {
                    IntersectionResult = "Отрезки не пересекаются";
                    IntersectionResultX = "";
                    IntersectionResultY = "";
                }
            }
            catch (Exception exeption)
            {
                // Обработка исключений (например, вывод ошибки)
            }
        }
    }
}
