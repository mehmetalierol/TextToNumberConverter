using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextToNumber.Business;

namespace TextToNumber.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Test0()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("sıfır");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "0");
        }

        [TestMethod]
        public void Test1()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("bir");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "1");
        }

        [TestMethod]
        public void Test10()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("on");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "10");
        }

        [TestMethod]
        public void Test19()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("ondokuz");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "19");
        }

        [TestMethod]
        public void Test1_9()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("on dokuz");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "19");
        }

        [TestMethod]
        public void Test97()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("doksanyedi");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "97");
        }

        [TestMethod]
        public void Test100()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("yüz");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "100");
        }

        [TestMethod]
        public void Test101()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("yüzbir");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "101");
        }

        [TestMethod]
        public void Test110()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("yüzon");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "110");
        }

        [TestMethod]
        public void Test111()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("yüzonbir");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "111");
        }

        [TestMethod]
        public void Test200()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("iki yüz");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "200");
        }

        [TestMethod]
        public void Test201()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("iki yüzbir");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "201");
        }

        [TestMethod]
        public void Test271()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("iki yüzyetmiş bir");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "271");
        }

        [TestMethod]
        public void Test888()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("sekiz yüz seksen sekiz");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "888");
        }

        [TestMethod]
        public void Test1000()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("bin");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "1000");
        }

        [TestMethod]
        public void Test1005()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("binbeş");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "1005");
        }

        [TestMethod]
        public void Test1060()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("bin altmış");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "1060");
        }

        [TestMethod]
        public void Test1900()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("bin dokuzyüz");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "1900");
        }

        [TestMethod]
        public void Test1888()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("binsekiz yüz seksen sekiz");

            //Assert
            Assert.AreEqual(result.Replace(" ", ""), "1888");
        }

        [TestMethod]
        public void TestCase01()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("yüz bin lira kredi kullanmak istiyorum");

            //Assert
            Assert.AreEqual(result.Replace("  ", ""), "100000 lira kredi kullanmak istiyorum");
        }

        [TestMethod]
        public void TestCase02()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("Bugün Yirmi sekiz yaşına girdim");

            //Assert
            Assert.AreEqual(result.Replace("  ", ""), "Bugün 28 yaşına girdim");
        }

        [TestMethod]
        public void TestCase03()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("Elli altı bin lira kredi alıp üç yılda geri ödeyeceğim");

            //Assert
            Assert.AreEqual(result.Replace("  ", ""), "56000 lira kredi alıp 3 yılda geri ödeyeceğim");
        }

        [TestMethod]
        public void TestCase04()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("Seksen yedi bin iki yüz on altı lira borcum var");

            //Assert
            Assert.AreEqual(result.Replace("  ", ""), "87216 lira borcum var");
        }

        [TestMethod]
        public void TestCase05()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("Bin yirmi dört lira eksiğim kaldı");

            //Assert
            Assert.AreEqual(result.Replace("  ", ""), "1024 lira eksiğim kaldı");
        }

        [TestMethod]
        public void TestCase06()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("Yarın saat onaltıda geleceğim");

            //Assert
            Assert.AreEqual(result.Replace("  ", ""), "Yarın saat 16 da geleceğim");
        }

        [TestMethod]
        public void TestCase07()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("Dokuzyüzelli beş lira fiyatı var");

            //Assert
            Assert.AreEqual(result.Replace("  ", ""), "955 lira fiyatı var");
        }

        [TestMethod]
        public void TestCase08()
        {
            //Arrange
            var Converter = new TextConverter();

            //Act
            var result = Converter.DoConvertion("Benim 1 milyon 85 bin lira param var");

            //Assert
            Assert.AreEqual(result.Replace("  ", ""), "Benim 1085000 lira param var");
        }
    }
}