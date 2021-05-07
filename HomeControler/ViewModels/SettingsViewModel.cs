/*
    ViewModel for Settings View

    Author: Jakub Smejkal (xsmejk28)
*/
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HomeControler.Others;
using MySql.Data.MySqlClient;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeControler.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        // Commands definition
        public ICommand ConnectToServerCommand => new RelayCommand(ConnectToServer);
        public ICommand ConnectToDbCommand => new RelayCommand(ConnectToDatabase);

        /// <summary>
        /// State of connection to the mqtt broker
        /// </summary>
        private string _mqttState;
        public string MQTTState
        {
            get
            {
                return _mqttState;
            }
            set
            {
                _mqttState = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// State of connection to the database
        /// </summary>
        private string _dbConnectionState;
        public string DbConnectionState
        {
            get
            {
                return _dbConnectionState;
            }
            set
            {
                _dbConnectionState = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Color of the state text
        /// </summary>
        private Brush _dbTextColor;
        public Brush DbTextColor
        {
            get
            {
                return _dbTextColor;
            }
            set
            {
                _dbTextColor = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// MQTT state text color
        /// </summary>
        private Brush _textColor;
        public Brush TextColor
        {
            get
            {
                return _textColor;
            }
            set
            {
                _textColor = value;
                RaisePropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            MQTTState = "Not connected";
            TextColor = Brushes.Black;

            DbConnectionState = "Not connected";
            DbTextColor = Brushes.Black;


            if(Settings.Default.databaseAddress != null)
            {
                App.SettingsModelProperty.DatabaseIpAddress = Settings.Default.databaseAddress;
            }
            if (Settings.Default.databaseName != null)
            {
                App.SettingsModelProperty.DatabaseName = Settings.Default.databaseName;
            }
            if (Settings.Default.databaseTableName != null)
            {
                App.SettingsModelProperty.DatabaseTableName = Settings.Default.databaseTableName;
            }
            if (Settings.Default.databaseUserName != null)
            {
                App.SettingsModelProperty.DatabaseUserName = Settings.Default.databaseUserName;
            }
            if (Settings.Default.databaseTablePasswd != null)
            {
                App.SettingsModelProperty.DatabaseUserPassword = Settings.Default.databaseTablePasswd;
            }


            Messenger.Default.Register<SimpleMessage>(this, ConsumeMessage);
            Messenger.Default.Register<string>(this, "settingsBroker", getBrokerAddress);

        }

        /// <summary>
        /// Connect to MQTT server button pressed
        /// </summary>
        private void ConnectToServer()
        {
            MQTTState = "Connecting...";
            TextColor = Brushes.Black;
            Messenger.Default.Send(App.SettingsModelProperty);
        }

        /// <summary>
        /// Connect to the database button pressed
        /// </summary>
        private void ConnectToDatabase()
        {
            DbConnectionState = "Connecting...";
            DbTextColor = Brushes.Black;

            MySqlConnection conn = new MySqlConnection("Server=" + App.SettingsModelProperty.DatabaseIpAddress + ";userid=" + App.SettingsModelProperty.DatabaseUserName + ";password=" + App.SettingsModelProperty.DatabaseUserPassword + ";Database=" + App.SettingsModelProperty.DatabaseName);

            try
            {
                conn.Open();

                MySqlCommand cmd;
                cmd = new MySqlCommand("SELECT * FROM " + App.SettingsModelProperty.DatabaseTableName, conn);

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DbConnectionState = "Database Connection Succesful";
                DbTextColor = Brushes.Green;

                Settings.Default.databaseAddress = App.SettingsModelProperty.DatabaseIpAddress;
                Settings.Default.databaseTableName = App.SettingsModelProperty.DatabaseTableName;
                Settings.Default.databaseUserName = App.SettingsModelProperty.DatabaseUserName;
                Settings.Default.databaseName = App.SettingsModelProperty.DatabaseName;
                Settings.Default.databaseTablePasswd = App.SettingsModelProperty.DatabaseUserPassword;
                Settings.Default.Save();
            }

            catch (MySqlException ex)
            {
                DbConnectionState = "Database Connecting Error, check your configuration...";
                DbTextColor = Brushes.Red;
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Get broker address from settings
        /// </summary>
        /// <param name="mqttBroker"></param>
        private void getBrokerAddress(string mqttBroker)
        {
            App.SettingsModelProperty.BrokerIpAddress = mqttBroker;
        }

        private void ConsumeMessage(SimpleMessage message)
        {
            switch (message.Type)
            {
                case SimpleMessage.MessageType.MQTTTimeout:
                    MQTTState = "Connecting Failed";
                    TextColor = Brushes.Red; 
                    break;
                case SimpleMessage.MessageType.ConnectedToMQTT:
                    MQTTState = "Connected";
                    TextColor = Brushes.Green;
                    Settings.Default.mqttBroker = App.SettingsModelProperty.BrokerIpAddress;
                    Settings.Default.Save();
                    break;
            }
        }
    }
}
