using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using YambApp.Model;
using YambApp.Service;

namespace YambApp.Tests
{
    [TestFixture]
    public class ScoreTests

    {
        IScore score;
        Mock<IDataGridWrapper> dataGridWrapper;

        [SetUp]
        public void SetUp()
        {
            dataGridWrapper= new Mock<IDataGridWrapper>();
            score= new Score(dataGridWrapper.Object);

            
        }
        [Test]
        [RequiresThread(ApartmentState.STA)]
        public void Score_ShouldCalculate()
        {
            YambCategory category = new YambCategory();
            DataGrid scoreGrid = new DataGrid();
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
                            new YambCategory("Ones","1","2","",""),
                            new YambCategory("Twos","","","",""),
                            new YambCategory("Threes","","","",""),
                            new YambCategory("Fours","","","",""),
                            new YambCategory("Fives","","","",""),
                            new YambCategory("Sixes","","","",""),
                            new YambCategory("Max","10","10","10","10"),
                            new YambCategory("Min","5","5","5","5"),
                            new YambCategory("Straight","","","",""),
                            new YambCategory("Full House","","","",""),
                            new YambCategory("Poker","","","",""),
                            new YambCategory("Yamb","","","","")
            };
            scoreGrid.CanUserAddRows = false;
            scoreGrid.ItemsSource = values;

            TextBlock tb1 = new TextBlock();
            tb1.Text = "1";
            TextBlock tb2 = new TextBlock();
            tb2.Text = "2";
            TextBlock tb3 = new TextBlock();
            tb3.Text = "";
            TextBlock tb4 = new TextBlock();
            tb4.Text = "";
            TextBlock tb5 = new TextBlock();
            tb5.Text = "";
            TextBlock tb6 = new TextBlock();
            tb6.Text = "";
            TextBlock tb7 = new TextBlock();
            tb7.Text = "";
            TextBlock tb8 = new TextBlock();
            tb8.Text = "";
            TextBlock tb21 = new TextBlock();
            tb21.Text = "";
            TextBlock tb22 = new TextBlock();
            tb22.Text = "";
            TextBlock tb23 = new TextBlock();
            tb23.Text = "";
            TextBlock tb24 = new TextBlock();
            tb24.Text = "";
            TextBlock tb9 = new TextBlock();
            tb9.Text = "";
            TextBlock tb10 = new TextBlock();
            tb10.Text = "";
            TextBlock tb11 = new TextBlock();
            tb11.Text = "";
            TextBlock tb12 = new TextBlock();
            tb12.Text = "";
            TextBlock tb13 = new TextBlock();
            tb13.Text = "";
            TextBlock tb14 = new TextBlock();
            tb14.Text = "";
            TextBlock tb15 = new TextBlock();
            tb15.Text = "";
            TextBlock tb16 = new TextBlock();
            tb16.Text = "";
            TextBlock tb17 = new TextBlock();
            tb17.Text = "";
            TextBlock tb18 = new TextBlock();
            tb18.Text = "";
            TextBlock tb19 = new TextBlock();
            tb19.Text = "";
            TextBlock tb20 = new TextBlock();
            tb20.Text = "";
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
            tb33.Text = "";
            TextBlock tb34 = new TextBlock();
            tb34.Text = "";
            TextBlock tb35 = new TextBlock();
            tb35.Text = "";
            TextBlock tb36 = new TextBlock();
            tb36.Text = "";

            TextBlock tb37 = new TextBlock();
            tb37.Text = "";
            TextBlock tb38 = new TextBlock();
            tb38.Text = "";
            TextBlock tb39 = new TextBlock();
            tb39.Text = "";
            TextBlock tb40 = new TextBlock();
            tb40.Text = "";

            TextBlock tb41 = new TextBlock();
            tb41.Text = "";
            TextBlock tb42 = new TextBlock();
            tb42.Text = "";
            TextBlock tb43 = new TextBlock();
            tb43.Text = "";
            TextBlock tb44 = new TextBlock();
            tb44.Text = "";

            TextBlock tb45 = new TextBlock();
            tb45.Text = "";
            TextBlock tb46 = new TextBlock();
            tb46.Text = "";
            TextBlock tb47 = new TextBlock();
            tb47.Text = "";
            TextBlock tb48 = new TextBlock();
            tb48.Text = "";


            dataGridWrapper.SetupSequence(x => x.GetCellContent(scoreGrid, It.IsAny<int>(), It.IsAny<int>())).Returns(tb1).Returns(tb2).Returns(tb3).Returns(tb4).Returns(tb5).Returns(tb6).Returns(tb7).Returns(tb8).Returns(tb9).Returns(tb10).Returns(tb11).Returns(tb12).Returns(tb13).Returns(tb14).Returns(tb15).Returns(tb16).Returns(tb17).Returns(tb18).Returns(tb19).Returns(tb20).Returns(tb21).Returns(tb22).Returns(tb23).Returns(tb24).Returns(tb25).Returns(tb25).Returns(tb29).Returns(tb1).Returns(tb26).Returns(tb26).Returns(tb30).Returns(tb2).Returns(tb27).Returns(tb27).Returns(tb31).Returns(tb3).Returns(tb28).Returns(tb28).Returns(tb32).Returns(tb4).Returns(tb29).Returns(tb30).Returns(tb31).Returns(tb32).Returns(tb33).Returns(tb34).Returns(tb35).Returns(tb36).Returns(tb37).Returns(tb38).Returns(tb39).Returns(tb40).Returns(tb41).Returns(tb42).Returns(tb43).Returns(tb44).Returns(tb45).Returns(tb46).Returns(tb47).Returns(tb48);


            int result = score.ScoreValue(scoreGrid);

            Assert.AreEqual(18, result);

           
        }
    }
}
