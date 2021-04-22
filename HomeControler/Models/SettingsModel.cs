﻿/*
    Model class that will store all the settings data
    
    Author: Jakub Smejkal(xsmejk28)
*/

namespace HomeControler.Models
{
    public class SettingsModel
    {
        public string BrokerIpAddress { get; set; }
        public string DatabaseIpAddress { get; set; }
        public string DatabaseUserName { get; set; }
        public string DatabaseUserPassword { get; set; }
        public string DatabaseTableName { get; set; }
        public string DatabaseName { get; set; }

    }
}
