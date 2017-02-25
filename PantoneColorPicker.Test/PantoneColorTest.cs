using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PantoneColorPicker.Models;
using System.Linq;

namespace PantoneColorPicker.Test
{
    [TestClass]
    public class PantoneColorTest
    {
        [TestMethod]
        public void StaticConstructorTest()
        {
            PantoneColor color = PantoneColor.FindColor("");
        }

        [TestMethod]
        public void FindClosestColor_WhereYellow_Test()
        {
            PantoneColor color = PantoneColor.FindColor("250, 230, 40");

            Assert.AreEqual("102 U: R255, G236, B45", color.ToString());
            Assert.AreEqual("102 U", color.Name);
            Assert.AreEqual(color.Name, PantoneColor.PantoneCatalog.Where(u => u.Name == "102 U").FirstOrDefault().Name);
        }

        [TestMethod]
        public void FindClosestColor_Where102U_Test()
        {
            PantoneColor color = PantoneColor.FindColor("102 U");

            Assert.AreEqual("102 U: R255, G236, B45", color.ToString());
            Assert.AreEqual("102 U", color.Name);
            Assert.AreEqual(color.Name, PantoneColor.PantoneCatalog.Where(u => u.Name == "102 U").FirstOrDefault().Name);
        }

        [TestMethod]
        public void FindClosestColor_ColorModified_Test()
        {
            PantoneColor color = PantoneColor.FindColor("102 U");
            color.Name = "Hellow";
            color.Hex = "hexzofeoof";
            color.RGB.B = 0;

            // Rest if deep copy worked properly
            var color2 = PantoneColor.FindColor("102 U");
            Assert.IsNotNull(color2);

            Assert.AreNotEqual(color.Hex, color2.Hex);
            Assert.AreNotEqual(color.RGB.B, color2.RGB.B);


        }
    }
}
