/*
    Auto generated class updated by Jakub Smejkal(xsmejk28)
*/

using HomeControler.Models;
using System.Windows;

namespace HomeControler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Added property to access the database model
        /// </summary>
        private static HomeControllerModel _homeControllerModel = new HomeControllerModel();
        public static HomeControllerModel HomeControllerModelProperty
        {
            get
            {
                return _homeControllerModel;
            }
        }

        /// <summary>
        /// Added property to access the settings model
        /// </summary>
        private static SettingsModel _settingsModel = new SettingsModel();
        public static SettingsModel SettingsModelProperty
        {
            get
            {
                return _settingsModel;
            }
            set
            {
                _settingsModel = value;
            }
        }
    }
}
