using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Service;

namespace YambApp.Tests
{
    [TestFixture]
    public class SumRulesTests
    {
        private ISumRules sumRules;

        [SetUp]
        public void Setup()
        {
            sumRules = new SumRules();
        }

        [Test]
        public void SumDices_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 1, 2, 3, 4, 5 };
            int sum = sumRules.SumDices(dices);
            Assert.AreEqual(15, sum);
        }

        [Test]
        public void SumOnes_ShouldSumOnes()
        {
            List<int> dices = new List<int> { 1, 1, 2, 3, 4 };
            int sumOnes = sumRules.SumOnes(dices);
            Assert.AreEqual(2, sumOnes);
        }

        [Test]
        public void SumTwos_ShouldSumTwos()
        {
            List<int> dices = new List<int> { 1, 2, 2, 3, 4 };
            int sumTwos = sumRules.SumTwos(dices);
            Assert.AreEqual(4, sumTwos);
        }

        [Test]
        public void SumThrees_ShouldSumThrees()
        {
            List<int> dices = new List<int> { 1, 2, 3, 3, 4 };
            int sumThrees = sumRules.SumThrees(dices);
            Assert.AreEqual(6, sumThrees);
        }

        [Test]
        public void SumFours_ShouldSumFours()
        {
            List<int> dices = new List<int> { 1, 2, 4, 4, 4 };
            int sumFours = sumRules.SumFours(dices);
            Assert.AreEqual(12, sumFours);
        }

        [Test]
        public void SumFives_ShouldSumFives()
        {
            List<int> dices = new List<int> { 1, 5, 5, 5, 5 };
            int sumFives = sumRules.SumFives(dices);
            Assert.AreEqual(20, sumFives);
        }

        [Test]
        public void SumSixes_ShouldSumSixes()
        {
            List<int> dices = new List<int> { 6, 6, 6, 6, 6 };
            int sumSixes = sumRules.SumSixes(dices);
            Assert.AreEqual(30, sumSixes);
        }

        [Test]
        public void SumYamb_WithBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 2, 2, 2, 2, 2 };
            int sumYamb = sumRules.SumYamb(dices, true);
            Assert.AreEqual(60, sumYamb);
        }

        [Test]
        public void SumYamb_WithoutBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 3, 3, 3, 3, 3 };
            int sumYamb = sumRules.SumYamb(dices, false);
            Assert.AreEqual(15, sumYamb);
        }


        [Test]
        public void SumPocker_WithBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 2, 2, 2, 2, 2 };
            int sumYamb = sumRules.SumPocker(dices, true);
            Assert.AreEqual(48, sumYamb);
        }

        [Test]
        public void SumPocker_WithoutBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 3, 3, 3, 3, 3 };
            int sumYamb = sumRules.SumPocker(dices, false);
            Assert.AreEqual(12, sumYamb);
        }


        [Test]
        public void SumFullHouse_WithBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 2, 2, 2, 1, 1 };
            int sumYamb = sumRules.SumFullHouse(dices, true);
            Assert.AreEqual(38, sumYamb);
        }

        [Test]
        public void SumFullHouse_WithoutBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 3, 3, 3, 6, 6 };
            int sumYamb = sumRules.SumFullHouse(dices, false);
            Assert.AreEqual(21, sumYamb);
        }



        [Test]
        public void SumSmallStraight_WithBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 1, 2, 3, 4, 5 };
            int sumYamb = sumRules.SumStraight(dices, true);
            Assert.AreEqual(45, sumYamb);
        }

        [Test]
        public void SumSmallStraight_WithoutBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 1, 2, 3, 4, 5 };
            int sumYamb = sumRules.SumStraight(dices, false);
            Assert.AreEqual(15, sumYamb);
        }

        [Test]
        public void SumBigStraight_WithBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> {2, 3, 4, 5, 6 };
            int sumYamb = sumRules.SumStraight(dices, true);
            Assert.AreEqual(50, sumYamb);
        }

        [Test]
        public void SumBigStraight_WithoutBonus_ShouldCalculateSum()
        {
            List<int> dices = new List<int> { 2, 3, 4, 5, 6 };
            int sumYamb = sumRules.SumStraight(dices, false);
            Assert.AreEqual(20, sumYamb);
        }


    }
}
