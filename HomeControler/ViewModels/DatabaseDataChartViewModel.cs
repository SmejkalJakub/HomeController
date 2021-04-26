/*
    ViewModel for Database Data Chart View

    Author: Jakub Smejkal
*/


using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HomeControler.ViewModels
{
    public class DateModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }

    public class DatabaseDataChartViewModel : ViewModelBase
    {
        public ICommand GenerateGraphCommand => new RelayCommand(generateGraph);

        /// <summary>
        /// All the topics loaded from the database
        /// </summary>
        private ObservableCollection<string> _topics;

        public ObservableCollection<string> Topics
        {
            get { return _topics; }
            set { _topics = value; }
        }

        /// <summary>
        /// Topic that was selected with the dropdown
        /// </summary>
        private string _selectedTopic;

        public string SelectedTopic
        {
            get { return _selectedTopic; }
            set { _selectedTopic = value; }
        }

        public Func<double, string> Formatter { get; set; }
        public SeriesCollection Data { get; set; }

        public DatabaseDataChartViewModel()
        {
            ObservableCollection<DatabaseData> databaseData;

            databaseData = App.HomeControllerModelProperty.GetDatabaseData("All", "");

            Topics = new ObservableCollection<string>();


            if(databaseData == null)
            {
                SelectedTopic = "PLEASE CONNECT TO DATABASE IN SETTINGS AND RESTART APP";
                return;
            }
            foreach (DatabaseData data in databaseData)
            {
                if(!Topics.Contains(data.Topic))
                {
                    Topics.Add(data.Topic);
                }
            }
            
            SelectedTopic = Topics[0];

            databaseData = App.HomeControllerModelProperty.GetDatabaseData("Topic", SelectedTopic);

            var dayConfig = Mappers.Xy<DateModel>()
                .X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)
                .Y(dayModel => dayModel.Value);

            List<DateModel> allData = new List<DateModel>();

            foreach (DatabaseData data in databaseData)
            {
                data.Value = data.Value.Replace('.', ',');
                allData.Add(new DateModel
                {
                    DateTime = data.Message_recieved,
                    Value = Convert.ToDouble(data.Value)
                });
            }

            Data = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Values = new ChartValues<DateModel>(allData)
                }
            };

            Formatter = value => new DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Generate graph for the selected topic
        /// </summary>
        private void generateGraph()
        {
            ObservableCollection<DatabaseData> databaseData;

            databaseData = App.HomeControllerModelProperty.GetDatabaseData("Topic", SelectedTopic);

            var dayConfig = Mappers.Xy<DateModel>()
                .X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)
                .Y(dayModel => dayModel.Value);

            List<DateModel> allData = new List<DateModel>();

            foreach (DatabaseData data in databaseData)
            {
                data.Value = data.Value.Replace('.', ',');
                allData.Add(new DateModel
                {
                    DateTime = data.Message_recieved,
                    Value = Convert.ToDouble(data.Value)
                });
            }

            Data = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Values = new ChartValues<DateModel>(allData)
                }
            };
            RaisePropertyChanged("Data");

            Formatter = value => new DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("dd/MM/yyyy HH:mm");
        }
    }
}
