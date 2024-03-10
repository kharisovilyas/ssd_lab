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
            // ������� ��� �������
            Segment segment1 = new Segment(0, 0, 2, 2);
            Segment segment2 = new Segment(0, 2, 2, 0);

            // ���������, ��� ������� ������������
            bool result = segment1.AreCrossing(segment2);

            // �������, ��� ����� ������ true
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestNotAreCrossing()
        {
            // ������� ��� �������, ������� �� ������������
            Segment segment1 = new Segment(0, 0, 2, 2);
            Segment segment2 = new Segment(3, 3, 5, 5);

            // ���������, ��� ������� �� ������������
            bool result = segment1.AreCrossing(segment2);

            // �������, ��� ����� ������ false
            Assert.IsFalse(result);
        }
    }
    [TestClass]
    public class DataParserTests
    {
        [TestMethod]
        public void TestParseValidFile()
        {
            // ������� ������ ������ DataParser
            DataParser parser = new DataParser();

            // ������� ��������� ���� � ��������� ������� JSON
            string filePath = Path.GetTempFileName();
            string jsonData = "{\"segment1x1\":1,\"segment1y1\":2,\"segment1x2\":3,\"segment1y2\":4,\"segment2x1\":5,\"segment2y1\":6,\"segment2x2\":7,\"segment2y2\":8}";
            File.WriteAllText(filePath, jsonData);

            // �������� ����� Parse ��� ���������� �����
            MyData result = parser.Parse(filePath);

            // ���������, ��� ��������� �� ����� null
            Assert.IsNotNull(result);

            // ���������, ��� �������� ������� ������� MyData ������������� ���������
            Assert.AreEqual(1, result.segment1x1);
            Assert.AreEqual(2, result.segment1y1);
            Assert.AreEqual(3, result.segment1x2);
            Assert.AreEqual(4, result.segment1y2);
            Assert.AreEqual(5, result.segment2x1);
            Assert.AreEqual(6, result.segment2y1);
            Assert.AreEqual(7, result.segment2x2);
            Assert.AreEqual(8, result.segment2y2);

            // ������� ��������� ����
            File.Delete(filePath);
        }

        [TestMethod]
        public void TestParseInvalidFile()
        {
            // ������� ������ ������ DataParser
            DataParser parser = new DataParser();

            // ������� ��������� ���� � ����������� �������
            string filePath = Path.GetTempFileName();
            string invalidData = "invalid JSON data";
            File.WriteAllText(filePath, invalidData);

            // �������� ����� Parse ��� ���������� �����
            MyData result = parser.Parse(filePath);

            // ���������, ��� ��������� ����� null, ��� ��� ������� �� ������
            Assert.IsNull(result);

            // ������� ��������� ����
            File.Delete(filePath);
        }
    }
    
}