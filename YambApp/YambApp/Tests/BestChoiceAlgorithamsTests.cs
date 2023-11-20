using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FsCheck;
using Microsoft.FSharp.Core;
using Moq;
using NUnit.Framework;
using YambApp.Model;
using YambApp.Service;
using YambApp.Model;
using System.Collections;

namespace YambApp.Tests
{
    [TestFixture]
    public class BestChoiceAlgorithamsTests
    {
        IBestChoiceAlgorithams bestChoiceAlgorithms;

        private Mock<IDataGridWrapper> dataGridWrapper=new Mock<IDataGridWrapper>();
        private SumRules sumRules = new SumRules();



        [SetUp]
        public void Setup()
        {
            bestChoiceAlgorithms = new BestChoiceAlgorithams(dataGridWrapper.Object, sumRules);
            
        }




        [Test]
        [RequiresThread(ApartmentState.STA)]
        public void AllFreeFields_ReturnsCorrectFreeFields()
        {
            
          
           YambCategory category = new YambCategory();
           DataGrid  scoreGrid = new DataGrid();
           scoreGrid.DataContext = category;
           // Set up the DataGrid and perform other UI-related operations here
            

            // Define column headers           
            DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = " ",
                    Binding = new Binding("CategoryName"),
                    // Use the column header as the binding path
                };

                scoreGrid.Columns.Add(column);
            DataGridTextColumn column1 = new DataGridTextColumn
                {
                    Header = "Down",
                    Binding = new Binding("Down") // Use the column header as the binding path
                };

                scoreGrid.Columns.Add(column1);
            DataGridTextColumn column2 = new DataGridTextColumn
                {
                    Header = "Free",
                    Binding = new Binding("Free") // Use the column header as the binding path
                };

                scoreGrid.Columns.Add(column2);
            DataGridTextColumn column3 = new DataGridTextColumn
                {
                    Header = "Up",
                    Binding = new Binding("Up") // Use the column header as the binding path
                };

                scoreGrid.Columns.Add(column3);
            DataGridTextColumn column4 = new DataGridTextColumn
                {
                    Header = "Lock",
                    Binding = new Binding("Lock") // Use the column header as the binding path
                };

                scoreGrid.Columns.Add(column4);


            List<YambCategory> values = new List<YambCategory>
            {
                            new YambCategory("Ones","1","1","",""),
                            new YambCategory("Twos","2","2","2","2"),
                            new YambCategory("Threes","2","2","2","2"),
                            new YambCategory("Fours","3","3","3","3"),
                            new YambCategory("Fives","4","4","4","4"),
                            new YambCategory("Sixes","5","5","5","5"),
                            new YambCategory("Max","10","10","10","10"),
                            new YambCategory("Min","5","5","5","5"),
                            new YambCategory("Straight","45","45","45","45"),
                            new YambCategory("Full House","6","6","6","6"),
                            new YambCategory("Poker","53","55","75","55"),
                            new YambCategory("Yamb","2","","1","1")
            };
            scoreGrid.CanUserAddRows = false;
            scoreGrid.ItemsSource = values;

            TextBlock tb1 = new TextBlock();
            tb1.Text = "1";
            TextBlock tb2 = new TextBlock();
            tb2.Text = "1";
            TextBlock tb3 = new TextBlock();
            tb3.Text = "";
            TextBlock tb4 = new TextBlock();
            tb4.Text = "";
            TextBlock tb5 = new TextBlock();
            tb5.Text = "2";
            TextBlock tb6 = new TextBlock();
            tb6.Text = "2";
            TextBlock tb7 = new TextBlock();
            tb7.Text = "2";
            TextBlock tb8 = new TextBlock();
            tb8.Text = "2";
            TextBlock tb21 = new TextBlock();
            tb21.Text = "2";
            TextBlock tb22 = new TextBlock();
            tb22.Text = "2";
            TextBlock tb23 = new TextBlock();
            tb23.Text = "2";
            TextBlock tb24 = new TextBlock();
            tb24.Text = "2";
            TextBlock tb9 = new TextBlock();
            tb9.Text = "3";
            TextBlock tb10 = new TextBlock();
            tb10.Text = "3";
            TextBlock tb11 = new TextBlock();
            tb11.Text = "3";
            TextBlock tb12 = new TextBlock();
            tb12.Text = "3";
            TextBlock tb13 = new TextBlock();
            tb13.Text = "4";
            TextBlock tb14 = new TextBlock();
            tb14.Text = "4";
            TextBlock tb15 = new TextBlock();
            tb15.Text = "4";
            TextBlock tb16 = new TextBlock();
            tb16.Text = "4";
            TextBlock tb17 = new TextBlock();
            tb17.Text = "5";
            TextBlock tb18 = new TextBlock();
            tb18.Text = "5";
            TextBlock tb19 = new TextBlock();
            tb19.Text = "5";
            TextBlock tb20 = new TextBlock();
            tb20.Text = "5";
            TextBlock tb25 = new TextBlock();
            tb25.Text = "10";
            TextBlock tb26 = new TextBlock();
            tb26.Text = "10";
            TextBlock tb27 = new TextBlock();
            tb27.Text = "10";
            TextBlock tb28 = new TextBlock();
            tb28.Text = "10";
            TextBlock tb29 = new TextBlock();
            tb29.Text = "5";
            TextBlock tb30 = new TextBlock();
            tb30.Text = "5";
            TextBlock tb31 = new TextBlock();
            tb31.Text = "5";
            TextBlock tb32 = new TextBlock();
            tb32.Text = "5";

            TextBlock tb33 = new TextBlock();
            tb33.Text = "45";
            TextBlock tb34 = new TextBlock();
            tb34.Text = "45";
            TextBlock tb35 = new TextBlock();
            tb35.Text = "45";
            TextBlock tb36 = new TextBlock();
            tb36.Text = "45";

            TextBlock tb37 = new TextBlock();
            tb37.Text = "6";
            TextBlock tb38 = new TextBlock();
            tb38.Text = "6";
            TextBlock tb39 = new TextBlock();
            tb39.Text = "6";
            TextBlock tb40 = new TextBlock();
            tb40.Text = "6";

            TextBlock tb41 = new TextBlock();
            tb41.Text = "53";
            TextBlock tb42 = new TextBlock();
            tb42.Text = "55";
            TextBlock tb43 = new TextBlock();
            tb43.Text = "75";
            TextBlock tb44 = new TextBlock();
            tb44.Text = "55";

            TextBlock tb45 = new TextBlock();
            tb45.Text = "2";
            TextBlock tb46 = new TextBlock();
            tb46.Text = "";
            TextBlock tb47 = new TextBlock();
            tb47.Text = "1";
            TextBlock tb48 = new TextBlock();
            tb48.Text = "1";




            dataGridWrapper.SetupSequence(x => x.GetCellContent(scoreGrid, It.IsAny<int>(), It.IsAny<int>())).Returns(tb1).Returns(tb2).Returns(tb3).Returns(tb7).Returns(tb4).Returns(tb5).Returns(tb6).Returns(tb7).Returns(tb8).Returns(tb9).Returns(tb10).Returns(tb11).Returns(tb12).Returns(tb13).Returns(tb14).Returns(tb15).Returns(tb16).Returns(tb17).Returns(tb18).Returns(tb19).Returns(tb20).Returns(tb21).Returns(tb22).Returns(tb23).Returns(tb24).Returns(tb25).Returns(tb26).Returns(tb27).Returns(tb28).Returns(tb29).Returns(tb30).Returns(tb31).Returns(tb32).Returns(tb33).Returns(tb34).Returns(tb35).Returns(tb36).Returns(tb37).Returns(tb38).Returns(tb39).Returns(tb40).Returns(tb41).Returns(tb42).Returns(tb43).Returns(tb44).Returns(tb45).Returns(tb46).Returns(tb47).Returns(tb48);
            // Act
            Dictionary<int, List<int>> result = bestChoiceAlgorithms.AllFreeFields(scoreGrid, 1);

            Dictionary<int, List<int>> expectedValues = new Dictionary<int, List<int>> {
                { 0, new List<int>(){3,4 }},
                { 1, new List<int>(){ }},
                { 2, new List<int>(){ }},
                { 3, new List<int>(){ }},
                { 4, new List<int>(){ }},
                { 5, new List<int>(){ }},
                { 6, new List<int>(){ }},
                { 7, new List<int>(){ }},
                { 8, new List<int>(){ }},
                { 9, new List<int>(){ }},
                { 10, new List<int>(){ }},
                { 11, new List<int>(){2} },
            };

            // Assert
            // Define expected free fields and compare with the result
            Assert.AreEqual(expectedValues, result);
        }

        [Test]

        public void BestChoice_ReturnsCorrectFreeFields()
        {
            Dictionary<int, List<int>> freeFieldIndexes = new Dictionary<int, List<int>> {
                { 0, new List<int>(){1,2,4 }},
                { 1, new List<int>(){ 2}},
                { 2, new List<int>(){ 2}},
                { 3, new List<int>(){ 2}},
                { 4, new List<int>(){ 2}},
                { 5, new List<int>(){ 2}},
                { 6, new List<int>(){ 2}},
                { 7, new List<int>(){ 2}},
                { 8, new List<int>(){ 2}},
                { 9, new List<int>(){ 2}},
                { 10, new List<int>(){2}},
                { 11, new List<int>(){2,3} },
            };

            List<int> dices = new List<int>() { 1,1,1,1,1 };
            dices.Sort();

            bool gameWithBonus = false;

         

            Dictionary<Dictionary<int, int>, int> result = bestChoiceAlgorithms.BestChoice(freeFieldIndexes, dices, gameWithBonus);

            Dictionary<Dictionary<int, int>, int> expectedValues = new Dictionary<Dictionary<int, int>, int> {
                {new Dictionary<int, int> {{0,1}},5 },
                {new Dictionary<int, int> {{0,2}},5 },
                {new Dictionary<int, int> {{0,4}},5 },
                {new Dictionary<int, int> {{1,2}},0 },
                {new Dictionary<int, int> {{2,2}},0 },
                {new Dictionary<int, int> {{3,2}},0 },
                {new Dictionary<int, int> {{4,2}},0 },
                {new Dictionary<int, int> {{5,2}},0 },
                {new Dictionary<int, int> {{6,2}},5 },
                {new Dictionary<int, int> {{7,2}},5 },
                {new Dictionary<int, int> {{8,2}},0 },
                {new Dictionary<int, int> {{9,2}},0 },
                {new Dictionary<int, int> {{10,2}},4 },
                {new Dictionary<int, int> {{11,2}},5 },
                {new Dictionary<int, int> {{11,3}},5 },

            };

            Console.WriteLine();
            Console.WriteLine();
            foreach (var j in expectedValues)
            {
                Console.WriteLine();
                foreach (var ind in j.Key)
                {
                    
                    Console.Write("Row:" + ind.Key + " " + "Column:" + ind.Value + " " + "Value:" + j.Value);
                    Console.WriteLine();
                }
            }

            DictionaryComparer dictionaryComparer= new DictionaryComparer();
           bool areEqual = dictionaryComparer.AreDictionariesEqual(result, expectedValues);
            


            Assert.IsTrue(areEqual);
        }
      
                

        
    }
}
