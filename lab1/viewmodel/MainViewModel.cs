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


        private bool _isSaveButtonVisible;

        public double Segment1X1
        {
            get { return _segment1X1; }
            set
            {
                _segment1X1 = value;
                OnPropertyChanged();
                Calculate();
            }
        }
        public double Segment1X2
        {
            get { return _segment1X2; }
            set
            {
                _segment1X2 = value;
                OnPropertyChanged();
                Calculate();
            }
        }
        public double Segment1Y1
        {
            get { return _segment1Y1; }
            set
            {
                _segment1Y1 = value;
                OnPropertyChanged();
                Calculate();
            }
        }
        public double Segment1Y2
        {
            get { return _segment1Y2; }
            set
            {
                _segment1Y2 = value;
                OnPropertyChanged();
                Calculate();
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
                Calculate();
            }
        }
        public double Segment2Y1
        {
            get { return _segment2Y1; }
            set
            {
                _segment2Y1 = value;
                OnPropertyChanged();
                Calculate();
            }
        }
        public double Segment2Y2
        {
            get { return _segment2Y2; }
            set
            {
                _segment2Y2 = value;
                OnPropertyChanged();
                Calculate();
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


        public bool IsSaveButtonVisible
        {
            get { return _isSaveButtonVisible; }
            set
            {
                _isSaveButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        // Команда для обработки нажатия на кнопку "Рассчитать"
        public ICommand CalculateCommand { get; private set; }
        public ICommand LoadDataFromFileCommand { get; private set; }
        public ICommand SaveInFileCommand { get; private set; }
        public ICommand ShowStartupInfoCommand { get; }

        public static void ShowStartupInfo()
        {
            MessageBoxResult result = MessageBox.Show(
                "Добро пожаловать! Это информация при запуске программы." +
                "\nРабота №1. Алгоритмы и структуры данных\nПервая лабораторная работа предназначена для приобретения практического опыта в создании простейшего приложения с использованием языка программирования С#.\n" +
                "\nЗадание 16 варианта: Для заданных отрезков на плоскости определить, пересекаются ли они. Найти координаты точки пересечения. " +
                "\n\nПоказывать ли данное сообщение в будущем при запуске программы ?" +
                "\n\nЕсли вы указали нет, то сможете найти инфомацию в разделе Информация о разработчике",
                "Программу разработал Харисов Ильяс Ренатович, 424 группа",
                MessageBoxButton.YesNo);

            // Если нажата кнопка "ОК", ничего не делаем
            if (result == MessageBoxResult.OK)
            {
                SettingsManager.SaveShowStartupMessageSetting(true);
            }
            // Если нажата кнопка "Не показывать", сохраняем настройку
            else if (result == MessageBoxResult.Cancel)
            {
                // Сохраняем настройку, чтобы больше не показывать всплывающее окно
                SettingsManager.SaveShowStartupMessageSetting(false);
            }
        }

        public MainViewModel()
        {

            IsSaveButtonVisible = false;
            StatusOfLoadFile = "Файл не загружен";
            _intersectionResultX = "";
            _intersectionResultY = "";
            _intersectionResult = "";
            _statusOfLoadFile = "";

            // Создаем команду и передаем в нее метод, который будет вызываться при выполнении команды
            CalculateCommand = new RelayCommand(Calculate);
            LoadDataFromFileCommand = new RelayCommand(LoadDataFromFile);
            SaveInFileCommand = new RelayCommand(SaveInFile);
            ShowStartupInfoCommand = new RelayCommand(ShowStartupInfo);
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
                    MyData? data = parser.Parse(filePath);

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
                        IsSaveButtonVisible = true;
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


        // Метод, разчитывающий пересечение отрезков
        private void Calculate()
        {
            CalculateIntersection(Segment1X1, Segment1Y1, Segment1X2, Segment1Y2, Segment2X1, Segment2Y1, Segment2X2, Segment2Y2);
        }


        private void CalculateIntersection(double segment1X1, double segment1Y1, double segment1X2, double segment1Y2, double segment2X1, double segment2Y1, double segment2X2, double segment2Y2)
        {
            try
            {
                IsSaveButtonVisible = true;
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
                Console.WriteLine(exeption);
                MessageBox.Show($"Произошла непредвиденная ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void SaveInFile()
        {
            // Диалог сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt"; // Устанавливаем фильтр для файлов

            if (saveFileDialog.ShowDialog() == true)
            {
                // Получение пути к выбранному файлу
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine($"Отрезок 1 с координатами начала: X1: {Segment1X1}; Y1: {Segment1Y1}");
                        writer.WriteLine($"И конца: X2: {Segment1X2}; Y2: {Segment1Y2}");
                        writer.WriteLine($"И Отрезок 2 с координатами начала: X1: {Segment2X1}; Y1: {Segment2Y1}");
                        writer.WriteLine($"И конца: X2: {Segment2X2}; Y2: {Segment2Y2}");
                        writer.WriteLine(IntersectionResult);
                        if(IntersectionResult == "Отрезки пересекаются в точке:")
                            writer.Write($"X: {IntersectionResultX}; Y: {IntersectionResultY}");
                    }

                    MessageBox.Show("Результат сохранен успешно", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
