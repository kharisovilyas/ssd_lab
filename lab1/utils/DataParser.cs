using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.utils
{
    public class DataParser
    {
        public MyData? Parse(string filePath)
        {
            try
            {
                // Чтение всего содержимого файла в строку
                string json = File.ReadAllText(filePath);
                if (json != null)
                {
                    MyData? data = JsonConvert.DeserializeObject<MyData>(json);
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при парсинге файла: {ex.Message}");
                return null;
            }
        }
    }
    public class MyData
    {
        public double segment1x1 { get; set; }
        public double segment1y1 { get; set; }
        public double segment1x2 { get; set; }
        public double segment1y2 { get; set; }
        public double segment2x1 { get; set; }
        public double segment2y1 { get; set; }
        public double segment2x2 { get; set; }
        public double segment2y2 { get; set; }
    }
}
