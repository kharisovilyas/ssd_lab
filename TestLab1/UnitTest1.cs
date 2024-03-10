using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab1.model;
using lab1.utils;
using Moq;
using lab1.viewmodel;

namespace TestLab1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAreCrossing()
        {
            // Создаем два отрезка
            Segment segment1 = new Segment(0, 0, 2, 2);
            Segment segment2 = new Segment(0, 2, 2, 0);

            // Проверяем, что отрезки пересекаются
            bool result = segment1.AreCrossing(segment2);

            // Ожидаем, что метод вернет true
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestNotAreCrossing()
        {
            // Создаем два отрезка, которые не пересекаются
            Segment segment1 = new Segment(0, 0, 2, 2);
            Segment segment2 = new Segment(3, 3, 5, 5);

            // Проверяем, что отрезки не пересекаются
            bool result = segment1.AreCrossing(segment2);

            // Ожидаем, что метод вернет false
            Assert.IsFalse(result);
        }
    }
    [TestClass]
    public class DataParserTests
    {
        [TestMethod]
        public void TestParseValidFile()
        {
            // Создаем объект класса DataParser
            DataParser parser = new DataParser();

            // Создаем временный файл с валидными данными JSON
            string filePath = Path.GetTempFileName();
            string jsonData = "{\"segment1x1\":1,\"segment1y1\":2,\"segment1x2\":3,\"segment1y2\":4,\"segment2x1\":5,\"segment2y1\":6,\"segment2x2\":7,\"segment2y2\":8}";
            File.WriteAllText(filePath, jsonData);

            // Вызываем метод Parse для временного файла
            MyData result = parser.Parse(filePath);

            // Проверяем, что результат не равен null
            Assert.IsNotNull(result);

            // Проверяем, что значения свойств объекта MyData соответствуют ожидаемым
            Assert.AreEqual(1, result.segment1x1);
            Assert.AreEqual(2, result.segment1y1);
            Assert.AreEqual(3, result.segment1x2);
            Assert.AreEqual(4, result.segment1y2);
            Assert.AreEqual(5, result.segment2x1);
            Assert.AreEqual(6, result.segment2y1);
            Assert.AreEqual(7, result.segment2x2);
            Assert.AreEqual(8, result.segment2y2);

            // Удаляем временный файл
            File.Delete(filePath);
        }

        [TestMethod]
        public void TestParseInvalidFile()
        {
            // Создаем объект класса DataParser
            DataParser parser = new DataParser();

            // Создаем временный файл с невалидными данными
            string filePath = Path.GetTempFileName();
            string invalidData = "invalid JSON data";
            File.WriteAllText(filePath, invalidData);

            // Вызываем метод Parse для временного файла
            MyData result = parser.Parse(filePath);

            // Проверяем, что результат равен null, так как парсинг не удался
            Assert.IsNull(result);

            // Удаляем временный файл
            File.Delete(filePath);
        }
    }
    
}