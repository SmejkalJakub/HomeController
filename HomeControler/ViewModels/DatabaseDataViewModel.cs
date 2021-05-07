/*
    ViewModel for Database Data View

    Author: Jakub Smejkal (xsmejk28)
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HomeControler.ViewModels
{

    public class DatabaseData
    {
        int messageId;
        string topic;
        string message_value;
        string room;
        DateTime message_recieved;

        /// <summary>
        /// Message ID property
        /// </summary>
        public int MessageId
        {
            get { return messageId; }
            set { messageId = value; }
        }

        /// <summary>
        /// Message topic property
        /// </summary>
        public string Topic
        {
            get { return topic; }
            set { topic = value; }
        }

        /// <summary>
        /// Message value property
        /// </summary>
        public string Value
        {
            get { return message_value; }
            set { message_value = value; }
        }

        /// <summary>
        /// Device Room property
        /// </summary>
        public string Room
        {
            get { return room; }
            set { room = value; }
        }

        /// <summary>
        /// Message date time recieved 
        /// </summary>
        public DateTime Message_recieved
        {
            get { return message_recieved; }
            set { message_recieved = value; }
        }
    }
    public class DatabaseDataViewModel : ViewModelBase
    {
        private ObservableCollection<string> _filters;

        public ICommand LoadDataCommand => new RelayCommand(LoadData);

        /// <summary>
        /// Database data that should be displayed in the table
        /// </summary>
        private ObservableCollection<DatabaseData> _databaseData;
        public ObservableCollection<DatabaseData> DatabaseDataCollection
        {
            get 
            { 
                return _databaseData; 
            }
            set 
            { 
                _databaseData = value; 
            }
        }

        /// <summary>
        /// List of filters
        /// </summary>
        public ObservableCollection<string> Filters
        {
            get 
            { 
                return _filters; 
            }
            set 
            { 
                _filters = value; 
            }
        }

        /// <summary>
        /// Currently selected filter
        /// </summary>
        private string _selectedFilter;

        public string SelectedFilter
        {
            get 
            { 
                return _selectedFilter; 
            }
            set 
            { 
                _selectedFilter = value; 
            }
        }

        /// <summary>
        /// Filter box string
        /// </summary>
        private string _filterBox;
        public string FilterBox
        {
            get 
            { 
                return _filterBox; 
            }
            set 
            { 
                _filterBox = value; 
            }
        }

        public DatabaseDataViewModel()
        {
            DatabaseDataCollection = new ObservableCollection<DatabaseData>();
            Filters = new ObservableCollection<string>()
            {
                "All",
                "Topic",
                "Value",
                "Room",
                "message_recieved"
            };
            SelectedFilter = Filters[0];
        }

        /// <summary>
        /// Load data from the database based on model
        /// </summary>
        private void LoadData()
        {
            DatabaseDataCollection.Clear();

            DatabaseDataCollection = App.HomeControllerModelProperty.GetDatabaseData(SelectedFilter, FilterBox);
            RaisePropertyChanged("DatabaseDataCollection");
        }
    }
}
