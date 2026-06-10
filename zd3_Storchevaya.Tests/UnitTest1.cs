using Microsoft.VisualStudio.TestTools.UnitTesting;
using zd3_Storchevaya;
using System;

namespace zd3_Storchevaya.Tests
{
    [TestClass]
    public class RoadWorkTests
    {
        // ТЕСТЫ БАЗОВОГО КЛАССА
        // ТЕСТ 1: Проверка формулы качества Q
        [TestMethod]
        public void Test_GetQuality_NormalRoad_ReturnsCorrectValue()
        {
            // Arrange (подготовка)
            RoadWork road = new RoadWork(10, 100, 200, "Москва", DateTime.Now);

            // Act (действие)
            double result = road.GetQuality();

            // Assert (проверка)
            Assert.AreEqual(200, result, 0.001);
        }

        // ТЕСТ 2: Проверка конструктора (все поля заполняются)
        [TestMethod]
        public void Test_Constructor_RoadWork_SetsAllProperties()
        {
            // Arrange
            DateTime testDate = new DateTime(2024, 5, 15);

            // Act
            RoadWork road = new RoadWork(15, 50, 300, "Санкт-Петербург", testDate);

            // Assert
            Assert.AreEqual(15, road.Width);
            Assert.AreEqual(50, road.Length);
            Assert.AreEqual(300, road.MassPerSquareMeter);
            Assert.AreEqual("Санкт-Петербург", road.Location);
            Assert.AreEqual(testDate, road.StartDate);
        }

        // ТЕСТ 3: Проверка метода GetInfo (не пустая строка)
        [TestMethod]
        public void Test_GetInfo_RoadWork_ReturnsNotEmptyString()
        {
            // Arrange
            RoadWork road = new RoadWork(10, 100, 200, "Москва", DateTime.Now);

            // Act
            string info = road.GetInfo();

            // Assert
            Assert.IsNotNull(info);
            Assert.IsTrue(info.Length > 0);
        }

        // ТЕСТ 4: Проверка, что GetInfo содержит Q
        [TestMethod]
        public void Test_GetInfo_RoadWork_ContainsQuality()
        {
            // Arrange
            RoadWork road = new RoadWork(10, 100, 200, "Москва", DateTime.Now);

            // Act
            string info = road.GetInfo();

            // Assert
            StringAssert.Contains(info, "Q =");
        }

        //ТЕСТЫ КЛАССА-ПОТОМКА

        // ТЕСТ 5: P от 5 до 8 → Qp = Q * 1.1
        [TestMethod]
        public void Test_GetQuality_ReinforcedRoad_P6_ReturnsQtimes1_1()
        {
            // Arrange
            ReinforcedRoadWork road = new ReinforcedRoadWork(10, 100, 200, "Москва",
                                                              DateTime.Now, 6, "Солнечно", "СтройГрупп");

            // Act
            double result = road.GetQuality();

            // Assert: 200 * 1.1 = 220
            Assert.AreEqual(220, result, 0.001);
        }

        // ТЕСТ 6: P = 3 → Qp = Q * 1.6
        [TestMethod]
        public void Test_GetQuality_ReinforcedRoad_P3_ReturnsQtimes1_6()
        {
            // Arrange
            ReinforcedRoadWork road = new ReinforcedRoadWork(10, 100, 200, "Москва",
                                                              DateTime.Now, 3, "Дождь", "СтройГрупп");

            // Act
            double result = road.GetQuality();

            // Assert: 200 * 1.6 = 320
            Assert.AreEqual(320, result, 0.001);
        }

        // ТЕСТ 7: P = 4 → Qp = Q * 1.6
        [TestMethod]
        public void Test_GetQuality_ReinforcedRoad_P4_ReturnsQtimes1_6()
        {
            // Arrange
            ReinforcedRoadWork road = new ReinforcedRoadWork(10, 100, 200, "Москва",
                                                              DateTime.Now, 4, "Туман", "СтройГрупп");

            // Act
            double result = road.GetQuality();

            // Assert: 200 * 1.6 = 320
            Assert.AreEqual(320, result, 0.001);
        }

        // ТЕСТ 8: P = 9 → Qp = Q * 1.6
        [TestMethod]
        public void Test_GetQuality_ReinforcedRoad_P9_ReturnsQtimes1_6()
        {
            // Arrange
            ReinforcedRoadWork road = new ReinforcedRoadWork(10, 100, 200, "Москва",
                                                              DateTime.Now, 9, "Ветер", "СтройГрупп");

            // Act
            double result = road.GetQuality();

            // Assert: 200 * 1.6 = 320
            Assert.AreEqual(320, result, 0.001);
        }

        // ТЕСТ 9: P = 10 → Qp = Q * 1.6
        [TestMethod]
        public void Test_GetQuality_ReinforcedRoad_P10_ReturnsQtimes1_6()
        {
            // Arrange
            ReinforcedRoadWork road = new ReinforcedRoadWork(10, 100, 200, "Москва",
                                                              DateTime.Now, 10, "Ураган", "СтройГрупп");

            // Act
            double result = road.GetQuality();

            // Assert: 200 * 1.6 = 320
            Assert.AreEqual(320, result, 0.001);
        }

        // ТЕСТ 10: P = 1 → Qp = Q * 1.9
        [TestMethod]
        public void Test_GetQuality_ReinforcedRoad_P1_ReturnsQtimes1_9()
        {
            // Arrange
            ReinforcedRoadWork road = new ReinforcedRoadWork(10, 100, 200, "Москва",
                                                              DateTime.Now, 1, "Шторм", "СтройГрупп");

            // Act
            double result = road.GetQuality();

            // Assert: 200 * 1.9 = 380
            Assert.AreEqual(380, result, 0.001);
        }

        // ТЕСТ 11: P = 2 → Qp = Q * 1.9
        [TestMethod]
        public void Test_GetQuality_ReinforcedRoad_P2_ReturnsQtimes1_9()
        {
            // Arrange
            ReinforcedRoadWork road = new ReinforcedRoadWork(10, 100, 200, "Москва",
                                                              DateTime.Now, 2, "Снег", "СтройГрупп");

            // Act
            double result = road.GetQuality();

            // Assert: 200 * 1.9 = 380
            Assert.AreEqual(380, result, 0.001);
        }

        // ТЕСТ 12: Проверка конструктора усиленной дороги
        [TestMethod]
        public void Test_Constructor_ReinforcedRoadWork_SetsAllProperties()
        {
            // Arrange
            DateTime testDate = new DateTime(2024, 5, 15);

            // Act
            ReinforcedRoadWork road = new ReinforcedRoadWork(15, 50, 300, "Казань",
                                                              testDate, 7, "Облачно", "ДорСтрой");

            // Assert
            Assert.AreEqual(15, road.Width);
            Assert.AreEqual(50, road.Length);
            Assert.AreEqual(300, road.MassPerSquareMeter);
            Assert.AreEqual("Казань", road.Location);
            Assert.AreEqual(testDate, road.StartDate);
            Assert.AreEqual(7, road.P);
            Assert.AreEqual("Облачно", road.WeatherCondition);
            Assert.AreEqual("ДорСтрой", road.Contractor);
        }

        // ТЕСТ 13: Проверка GetInfo усиленной дороги
        [TestMethod]
        public void Test_GetInfo_ReinforcedRoad_ContainsQp()
        {
            // Arrange
            ReinforcedRoadWork road = new ReinforcedRoadWork(10, 100, 200, "Москва",
                                                              DateTime.Now, 6, "Солнечно", "СтройГрупп");

            // Act
            string info = road.GetInfo();

            // Assert
            StringAssert.Contains(info, "Qp=");
        }

        // ТЕСТ 14: Проверка GetInfo усиленной дороги содержит P
        [TestMethod]
        public void Test_GetInfo_ReinforcedRoad_ContainsP()
        {
            // Arrange
            ReinforcedRoadWork road = new ReinforcedRoadWork(10, 100, 200, "Москва",
                                                              DateTime.Now, 6, "Солнечно", "СтройГрупп");

            // Act
            string info = road.GetInfo();

            // Assert
            StringAssert.Contains(info, "P=6");
        }
    }
}