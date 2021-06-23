using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YourMom
{
    /// <summary>
    /// Interaction logic for BudgetDetail.xaml
    /// </summary>
    public partial class BudgetDetail : Window
    {
        public BudgetDetail()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DynamicColumnChart.Series = new SeriesCollection();
            //((DefaultTooltip)DynamicColumnChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
            //DynamicColumnChart.Series.Add(new LineSeries()
            //{ Values = new ChartValues<double> { 1, 2, 3, 4, 5, 6, 7, 8 } });

        }

        private void chart123_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void DynamicColumnChart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //DynamicColumnChart.Series = new SeriesCollection();
            //((DefaultTooltip)DynamicColumnChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
            //DynamicColumnChart.Series.Add(new LineSeries()
            //{ Values = new ChartValues<double> { 1, 2, 3, 4 } });

            //List<KeyValuePair<double, double>> Power = new List<KeyValuePair<double, double>>();
            //List<double> lol = new List<double>();
            //lol.Add(1);
            //lol.Add(2);
            //lol.Add(3);
            //lol.Add(4);

            ////Setting data for line chart
            //DynamicColumnChart.DataContext = lol;

        //    DynamicColumnChart.AxisY.Clear();
        //    DynamicColumnChart.AxisY.Add(
        //new Axis
        //{
        //    MinValue = 0
        //});

            DynamicColumnChart.Series.Add(new LineSeries()
            { Values = new ChartValues<double> { 90, 90, 90, 90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90 }, 
                LineSmoothness = 0,
                PointGeometry = null,
                PointGeometrySize = 0,
                Title = "Dota2" });
            DynamicColumnChart.Series.Add(new LineSeries()
            { Values = new ChartValues<double> { 0, 0, 0, 3, 30,30,30,30,30 }, LineSmoothness = 0, PointGeometry = null,
                PointGeometrySize = 0,
            });

           
        }
    }
}
