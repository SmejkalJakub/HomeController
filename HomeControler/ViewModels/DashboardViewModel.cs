/*
    ViewModel for Dashboard View

    Author: Jakub Smejkal
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HomeControler.Objects;
using HomeControler.Others;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Windows;
using System.IO;

namespace HomeControler.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {

        /// <summary>
        /// Dictionary that gets the backhround file by the name of layout 
        /// </summary>
        private Dictionary<string, string> _BackgroundSources;

        public Dictionary<string, string> BackgroundSources
        {
            get
            {
                return _BackgroundSources;
            }
            set
            {
                _BackgroundSources = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Camera image position
        /// </summary>
        private Thickness _ImagePosition;
        public Thickness ImagePosition
        {
            get
            {
                return _ImagePosition;
            }
            set
            {
                _ImagePosition = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _TextBoxVisibility;
        public Visibility TextBoxVisibility
        {
            get
            {
                return _TextBoxVisibility;
            }
            set
            {
                _TextBoxVisibility = value;
                RaisePropertyChanged("TextBoxVisibility");
            }
        }


        private Thickness _TextBoxPosition;
        public Thickness TextBoxPosition
        {
            get
            {
                return _TextBoxPosition;
            }
            set
            {
                _TextBoxPosition = value;
                RaisePropertyChanged();
            }
        }

        private Dictionary<int, string> _ButtonNames;
        public Dictionary<int, string> ButtonNames
        {
            get
            {
                return _ButtonNames;
            }
            set
            {
                _ButtonNames = value;
                RaisePropertyChanged();
            }
        }

        private Dictionary<int, Thickness> _ButtonPositions;
        public Dictionary<int, Thickness> ButtonPositions
        {
            get
            {
                return _ButtonPositions;
            }
            set
            {
                _ButtonPositions = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// Dictionary that identifies the label controls
        /// </summary>
        private Dictionary<Guid, string> _Labels;
        public Dictionary<Guid, string> Labels
        {
            get 
            { 
                return _Labels; 
            }
            set 
            {
                _Labels = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Dictionary that identifies switch controls
        /// </summary>
        private Dictionary<Guid, BitmapImage> _Switches;
        public Dictionary<Guid, BitmapImage> Switches
        {
            get
            {
                return _Switches;
            }
            set
            {
                _Switches = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Current background source
        /// </summary>
        private string _BackgroundSource;
        public string BackgroundSource
        {
            get
            {
                return _BackgroundSource;
            }
            set
            {
                _BackgroundSource = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Node setting OnIcon
        /// </summary>
        private BitmapImage _OnIcon;
        public BitmapImage OnIcon
        {
            get
            {
                return _OnIcon;
            }
            set
            {
                _OnIcon = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Node setting OffIcon
        /// </summary>
        private BitmapImage _OffIcon;
        public BitmapImage OffIcon
        {
            get
            {
                return _OffIcon;
            }
            set
            {
                _OffIcon = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Image that was last recieved for the hovered camera
        /// </summary>
        private BitmapImage _CameraLastImage;
        public BitmapImage CameraLastImage
        {
            get
            {
                return _CameraLastImage;
            }
            set
            {
                _CameraLastImage = value;
                RaisePropertyChanged();
            }
        }

        private int lastButtonIndex = 0;

        private SubscribedCamera currentCamera;

        // Commands definition
        public ICommand SelectOnIconCommand => new RelayCommand(selectOnIcon);
        public ICommand SelectOffIconCommand => new RelayCommand(selectOffIcon);
        public ICommand SwitchClickedCommand => new RelayCommand<string>(switchClicked);
        public ICommand ChangeLayoutCommand => new RelayCommand<string>(changeLayout);
        public ICommand ChangeLayoutImageButton => new RelayCommand(changeLayoutImage);
        public ICommand AddNewLayoutCommand => new RelayCommand(addNewLayout);
        public ICommand ShowToRenameCommand => new RelayCommand<string>(showToRename);
        public ICommand ExportAllDataCommand => new RelayCommand(exportAllData);
        public ICommand ImportDataCommand => new RelayCommand(importData);
        public ICommand AddLabelCommand => new RelayCommand(SendAddLabelMessage);
        public ICommand AddSwitchCommand => new RelayCommand(SendAddSwitchMessage);
        public ICommand AddCameraCommand => new RelayCommand(SendAddCameraMessage);

        // Current layout selected
        private string currentLayout;

        /// <summary>
        /// Data from the database that will be shown in the 
        /// </summary>
        private ObservableCollection<DatabaseData> databaseData;

        public DashboardViewModel()
        {
            Messenger.Default.Register<UpdateLabelMessage>(this, "ChangeLabelValue", ChangeLabelValue);
            Messenger.Default.Register<UpdateSwitchMessage>(this, "ChangeSwitchValue", ChangeSwithcValue);
            Messenger.Default.Register<SubscribedCamera>(this, "updateCameraImage", ChangeCameraImage);

            Messenger.Default.Register<SubscribedSwitch>(this, "switchInfo", SetIconValues);

            Messenger.Default.Register<SubscribedSwitch>(this, "updateIcons", UpdateSwitchIcons);

            Messenger.Default.Register<SubscribedCamera>(this, "cameraMouseOver", CameraMouseOver);
            Messenger.Default.Register<string>(this, "cameraMouseOverLeave", CameraMouseOverLeave);

            Messenger.Default.Register<ReloadFromDatabaseMessage>(this, "getLastDatabaseValue", LoadLastDatabaseData);
            Messenger.Default.Register<string>(this, "loadLayout", loadLayout);

            Messenger.Default.Register<string>(this, "saveObjects", saveBackgrounds);

            databaseData = App.HomeControllerModelProperty.GetDatabaseData("All", "");

            Labels = new Dictionary<Guid, string>();
            Switches = new Dictionary<Guid, BitmapImage>();

            BackgroundSources = new Dictionary<string, string>();


            if(File.Exists(@"backgrounds.bin"))
            {
                string serializationFile = @"backgrounds.bin";

                //deserialize
                using (Stream stream = File.Open(serializationFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    Dictionary <string, string> BackgroundSourcesUpdated = new Dictionary<string, string>();
                    BackgroundSources.Clear();
                    BackgroundSourcesUpdated = (Dictionary<string, string>)bformatter.Deserialize(stream);

                    foreach(KeyValuePair<string, string> keyValuePair in BackgroundSourcesUpdated)
                    {
                        BackgroundSources.Add(keyValuePair.Key, Directory.GetCurrentDirectory() + @"\images\" + keyValuePair.Value.Split('\\')[keyValuePair.Value.Split('\\').Length - 1]);
                    }
                    BackgroundSource = BackgroundSources["Default"];
                }
            }

            _ButtonNames = new Dictionary<int, string>();
            _ButtonNames[0] = "Default";

            _ButtonPositions = new Dictionary<int, Thickness>();
            _ButtonPositions[0] = new Thickness(10, 10, 0, 0);

            _TextBoxVisibility = Visibility.Hidden;
            _TextBoxPosition = new Thickness(80, 40, 0, 0);
            currentLayout = "Default";

        }

        /// <summary>
        /// This function will send the trigger to add the label control to the view
        /// </summary>
        void SendAddLabelMessage()
        {
            Messenger.Default.Send(null as object, "addLabel");
        }

        /// <summary>
        /// This function will send the trigger to add the switch control to the view
        /// </summary>
        void SendAddSwitchMessage()
        {
            Messenger.Default.Send(null as object, "addSwitch");
        }

        /// <summary>
        /// This function will send the trigger to add the camera control to the view
        /// </summary>
        void SendAddCameraMessage()
        {
            Messenger.Default.Send(null as object, "addCamera");
        }

        /// <summary>
        /// This function will export all the layout data onto the Desktop to the HomeControllerData folder so it can be distributed
        /// </summary>
        void exportAllData()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if(!Directory.Exists(path + @"\HomeControllerData"))
            {
                Directory.CreateDirectory(path + @"\HomeControllerData");
            }

            string sourceFile = @"labels.bin";
            string destinationFile = path + @"\HomeControllerData\labels.bin";
            File.Copy(sourceFile, destinationFile, true);

            sourceFile = @"switches.bin";
            destinationFile = path + @"\HomeControllerData\switches.bin";
            File.Copy(sourceFile, destinationFile, true);

            sourceFile = @"cameras.bin";
            destinationFile = path + @"\HomeControllerData\cameras.bin";
            File.Copy(sourceFile, destinationFile, true);

            sourceFile = @"backgrounds.bin";
            destinationFile = path + @"\HomeControllerData\backgrounds.bin";
            File.Copy(sourceFile, destinationFile, true);

            DirectoryInfo dir = new DirectoryInfo("layouts");

            FileInfo[] files = dir.GetFiles();

            if(!Directory.Exists(path + @"\HomeControllerData\layouts"))
            {
                Directory.CreateDirectory(path + @"\HomeControllerData\layouts");
            }
            foreach (FileInfo file in files)
            {
                sourceFile = @"layouts\" + file.Name;
                destinationFile = path + @"\HomeControllerData\layouts\" + file.Name;
                File.Copy(sourceFile, destinationFile, true);
            }

            dir = new DirectoryInfo("images");

            files = dir.GetFiles();

            if (!Directory.Exists(path + @"\HomeControllerData\images"))
            {
                Directory.CreateDirectory(path + @"\HomeControllerData\images");
            }
            foreach (FileInfo file in files)
            {
                sourceFile = @"images\" + file.Name;
                destinationFile = path + @"\HomeControllerData\images\" + file.Name;
                File.Copy(sourceFile, destinationFile, true);
            }
        }

        /// <summary>
        /// This function will take care about importing the layout data. The data should be stored on the desktop in a HomeControllerData folder
        /// </summary>
        void importData()
        {
            if (Settings.Default.mqttBroker == "")
            {
                MessageBox.Show("Please update settings before importing data");
                return;
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string destinationFile = @"labels.bin";
            string sourceFile = path + @"\HomeControllerData\labels.bin";
            File.Copy(sourceFile, destinationFile, true);

            destinationFile = @"switches.bin";
            sourceFile = path + @"\HomeControllerData\switches.bin";
            File.Copy(sourceFile, destinationFile, true);

            destinationFile = @"cameras.bin";
            sourceFile = path + @"\HomeControllerData\cameras.bin";
            File.Copy(sourceFile, destinationFile, true);

            destinationFile = @"backgrounds.bin";
            sourceFile = path + @"\HomeControllerData\backgrounds.bin";
            File.Copy(sourceFile, destinationFile, true);

            DirectoryInfo dir = new DirectoryInfo(path + @"\HomeControllerData\layouts");

            FileInfo[] files = dir.GetFiles();

            if (!Directory.Exists(@"layouts"))
            {
                Directory.CreateDirectory(@"layouts");
            }
            foreach (FileInfo file in files)
            {
                destinationFile = @"layouts\" + file.Name;
                sourceFile = path + @"\HomeControllerData\layouts\" + file.Name;
                File.Copy(sourceFile, destinationFile, true);
            }

            dir = new DirectoryInfo(path + @"\HomeControllerData\images");
            
            files = dir.GetFiles();
            if (!Directory.Exists(@"images"))
            {
                Directory.CreateDirectory(@"images");
            }
            foreach (FileInfo file in files)
            {
                destinationFile = @"images\" + file.Name;
                sourceFile = path + @"\HomeControllerData\images\" + file.Name;
                File.Copy(sourceFile, destinationFile, true);
            }

            MessageBox.Show("Please restart the app");
            Application.Current.Shutdown();

        }

        /// <summary>
        /// This function will load data from the database about all the elements so they will show the value after app reset
        /// </summary>
        /// <param name="elements">Elements whitch data should be loaded</param>
        void LoadLastDatabaseData(ReloadFromDatabaseMessage elements)
        {
            if(databaseData != null)
            {
                foreach(SubscribedLabel subscribedLabel in elements.subscribedLabels)
                {
                    DatabaseData databaseEntry = databaseData.Where(x => x.Topic == subscribedLabel.Topic).OrderByDescending(d => d.Message_recieved).FirstOrDefault();
                    if(databaseEntry != null)
                    {
                        ChangeLabelValue(new UpdateLabelMessage { subscribedLabel = subscribedLabel, value = databaseEntry.Value });
                    }
                }
            }
        }

        /// <summary>
        /// This function will save all the backgrounds so they can be reloaded after the app reset
        /// </summary>
        /// <param name="value"></param>
        private void saveBackgrounds(string value)
        {
            string serializationFile = @"backgrounds.bin";

            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, BackgroundSources);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexString"></param>
        private void showToRename(string indexString)
        {
            int index = Convert.ToInt32(indexString);
            _TextBoxVisibility = Visibility.Hidden;
            _TextBoxPosition = new Thickness(ButtonPositions[index].Left, ButtonPositions[index].Top + 30, ButtonPositions[index].Right, ButtonPositions[index].Bottom);
        }

        /// <summary>
        /// This function will load layout from the data folder
        /// </summary>
        /// <param name="name">The layout name</param>
        private void loadLayout(string name)
        {
            lastButtonIndex++;
            _ButtonNames[lastButtonIndex] = lastButtonIndex.ToString();
            _ButtonPositions[lastButtonIndex] = new Thickness(_ButtonPositions[lastButtonIndex - 1].Left + 85, _ButtonPositions[lastButtonIndex - 1].Top, _ButtonPositions[lastButtonIndex - 1].Right, _ButtonPositions[lastButtonIndex - 1].Bottom);
            Messenger.Default.Send(lastButtonIndex, "addNewLayoutButton");
        }

        /// <summary>
        /// This function will take care about adding the new laoyut
        /// </summary>
        private void addNewLayout()
        {
            lastButtonIndex++;
            _ButtonNames[lastButtonIndex] = lastButtonIndex.ToString();
            _ButtonPositions[lastButtonIndex] = new Thickness(_ButtonPositions[lastButtonIndex - 1].Left + 85, _ButtonPositions[lastButtonIndex - 1].Top, _ButtonPositions[lastButtonIndex - 1].Right, _ButtonPositions[lastButtonIndex - 1].Bottom);

            BackgroundSources[lastButtonIndex.ToString()] = "";

            Messenger.Default.Send(lastButtonIndex, "addNewLayoutButton");

        }

        /// <summary>
        /// Function that will show the last recieved image next to the hovered camera element
        /// </summary>
        /// <param name="cameraObject"Camera element that is hovered over></param>
        void CameraMouseOver(SubscribedCamera cameraObject)
        {
            if(cameraObject.lastImage != null)
            {
                currentCamera = cameraObject;

                ImagePosition = new Thickness(cameraObject.Camera.Margin.Left + 300, cameraObject.Camera.Margin.Top + 300, cameraObject.Camera.Margin.Right, cameraObject.Camera.Margin.Bottom);

                CameraLastImage = cameraObject.lastImage;
            }
        }

        /// <summary>
        /// This function will change the layout to be shown on the screen
        /// </summary>
        /// <param name="layoutName">Name of the layout that should be shown</param>
        void changeLayout(string layoutName)
        {
            currentLayout = layoutName;
            if(BackgroundSources.ContainsKey(layoutName))
            {
                BackgroundSource = BackgroundSources[layoutName];
            }
            else
            {
                BackgroundSources[layoutName] = "";
            }
            Messenger.Default.Send(layoutName, "reloadGrid");
        }

        /// <summary>
        /// This function will take care about changing the current layout background
        /// </summary>
        void changeLayoutImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                if(!Directory.Exists(Directory.GetCurrentDirectory() + @"\images"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\images");
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\images\" + op.SafeFileName))
                {
                    File.Copy(op.FileName, Directory.GetCurrentDirectory() + @"\images\" + op.SafeFileName);

                }
                setBackground(Directory.GetCurrentDirectory() + @"\images\" + op.SafeFileName);
                BackgroundSources[currentLayout] = Directory.GetCurrentDirectory() + @"\images\" + op.SafeFileName;
            }
        }

        /// <summary>
        /// When the camera hover is done hide the image
        /// </summary>
        /// <param name="sender"></param>
        void CameraMouseOverLeave(string sender)
        {
            currentCamera = null;
            CameraLastImage = null;
        }

        /// <summary>
        /// Send the signal that the switch was clicked
        /// </summary>
        /// <param name="name">Name of the clicked switch</param>
        private void switchClicked(string name)
        {
            Messenger.Default.Send(name, "sendSwitchState");
        }

        /// <summary>
        /// Set current layout background image
        /// </summary>
        /// <param name="imageUri">URI of the image that should be set as background</param>
        private void setBackground(string imageUri)
        {
            BackgroundSource = imageUri;
        }

        /// <summary>
        /// This function will update the message shown in the label control
        /// </summary>
        /// <param name="message">Message that should be showed in the label</param>
        private void ChangeLabelValue(UpdateLabelMessage message)
        {
            Labels[message.subscribedLabel.Id] = message.subscribedLabel.Prefix + message.value + message.subscribedLabel.Postfix;
            RaisePropertyChanged("Labels");
        }

        /// <summary>
        /// This function will update the switch value
        /// </summary>
        /// <param name="message">Message recieved that will update the switch control</param>
        private void ChangeSwithcValue(UpdateSwitchMessage message)
        {
            Switches[message.subscribedSwitch.Id] = new BitmapImage(new Uri(message.value));
            RaisePropertyChanged("Switches");
        }

        /// <summary>
        /// This function will update the camera image but only if some camera is hovered
        /// </summary>
        /// <param name="cameraMessage">The message with the camera image</param>
        void ChangeCameraImage(SubscribedCamera cameraMessage)
        {
            if(currentCamera == cameraMessage)
            {
                CameraLastImage = currentCamera.lastImage;
            }
        }

        /// <summary>
        /// Select the selected switch on icon
        /// </summary>
        private void selectOnIcon()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\images"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\images");
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\images\" + op.SafeFileName))
                {
                    File.Copy(op.FileName, Directory.GetCurrentDirectory() + @"\images\" + op.SafeFileName);
                }
                BitmapImage image = new BitmapImage(new Uri(op.FileName));
                OnIcon = image;
            }
        }

        /// <summary>
        /// Select the selected swtch off icon
        /// </summary>
        private void selectOffIcon()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\images"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\images");
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\images\" + op.SafeFileName))
                {
                    File.Copy(op.FileName, Directory.GetCurrentDirectory() + @"\images\" + op.SafeFileName);
                }
                BitmapImage image = new BitmapImage(new Uri(op.FileName));
                OffIcon = image;
            }
        }

        /// <summary>
        /// This function will load the data about icons for the switch
        /// </summary>
        /// <param name="subSwitch">Switch whitch icons should be set</param>
        private void SetIconValues(SubscribedSwitch subSwitch)
        {
            if(subSwitch.OnIconUri != null && subSwitch.OffIconUri != null)
            {
                OnIcon = new BitmapImage(new Uri(subSwitch.OnIconUri));
                OffIcon = new BitmapImage(new Uri(subSwitch.OffIconUri));
            }
        }

        /// <summary>
        /// This function will update the data about icons for the switch
        /// </summary>
        /// <param name="subSwtich">Switch whitch icons should be updated</param>
        private void UpdateSwitchIcons(SubscribedSwitch subSwtich)
        {
            if(OnIcon != null && OffIcon != null)
            {
                subSwtich.OnIconUri = Directory.GetCurrentDirectory() + @"\images\" + (OnIcon.UriSource.AbsolutePath.Split('/')[OnIcon.UriSource.AbsolutePath.Split('/').Length - 1]);
                subSwtich.OffIconUri = Directory.GetCurrentDirectory() + @"\images\" + (OffIcon.UriSource.AbsolutePath.Split('/')[OnIcon.UriSource.AbsolutePath.Split('/').Length - 1]);

                Messenger.Default.Send(subSwtich.UniqueName, "getSwitchAndChangeValue");
            }
            else
            {
                MessageBox.Show("Please choose both icons");
            }
        }
    }
}
