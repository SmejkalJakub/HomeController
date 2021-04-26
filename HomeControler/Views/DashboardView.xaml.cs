/*
    Code behind for the DashboardView

    Author: Jakub Smejkal (xsmejk28)
*/

using GalaSoft.MvvmLight.Messaging;
using HomeControler.Controls;
using HomeControler.Objects;
using HomeControler.Others;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Xml;

namespace HomeControler.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        List<SubscribedLabel> subscribedLabels = new List<SubscribedLabel>();
        List<SubscribedSwitch> subscribedSwitches = new List<SubscribedSwitch>();
        List<SubscribedCamera> subscribedCameras = new List<SubscribedCamera>();

        private string currentGrid;

        public DashboardView()
        {
            InitializeComponent();

            Messenger.Default.Register<SimpleMessage>(this, ConsumeMessage);

            Messenger.Default.Register<SubscribedLabel>(this, "labelDelete", DeleteLabel);
           
            Messenger.Default.Register<object>(this, "addLabel", AddLabel);
            Messenger.Default.Register<object>(this, "addSwitch", AddSwitch);
            Messenger.Default.Register<object>(this, "addCamera", AddCamera);

            Messenger.Default.Register<string>(this, "reloadGrid", reloadGrid);
            Messenger.Default.Register<int>(this, "addNewLayoutButton", addNewLayoutButton);

            if (Directory.Exists(@"layouts"))
            {
                DirectoryInfo dir = new DirectoryInfo(@"layouts");

                foreach (var file in dir.GetFiles("*.*"))
                {
                    if (file.Name.Split('.')[0] == "default")
                        continue;
                    Messenger.Default.Send(file.Name, "loadLayout");
                }

                currentGrid = "default";
                reloadGrid("default");
            }
        }

        /// <summary>
        /// Add new layout (+) button callback that will add the new layout to the top of the dashboard screen
        /// </summary>
        /// <param name="index"></param>
        private void addNewLayoutButton(int index)
        {
            Button newLayoutButton = new Button();

            newLayoutButton.VerticalAlignment = VerticalAlignment.Top;
            newLayoutButton.HorizontalAlignment = HorizontalAlignment.Left;

            newLayoutButton.MaxHeight = 22;
            newLayoutButton.MaxWidth = 74;
            
            newLayoutButton.Height = 22;
            newLayoutButton.Width = 74;

            Binding contentBinding = new Binding("ButtonNames[" + index + "]");
            BindingOperations.SetBinding(newLayoutButton, ContentProperty, contentBinding);

            Binding marginBinding = new Binding("ButtonPositions[" + index + "]");
            BindingOperations.SetBinding(newLayoutButton, MarginProperty, marginBinding);

            Binding commandBinding = new Binding("ChangeLayoutCommand");
            BindingOperations.SetBinding(newLayoutButton, Button.CommandProperty, commandBinding);

            BindingOperations.SetBinding(newLayoutButton, Button.CommandParameterProperty, contentBinding);

            mainCanvas.Children.Add(newLayoutButton);
        }

        /// <summary>
        /// Function that will take care of deleting selected label control
        /// </summary>
        /// <param name="deletedLabel">Label that should be deleted</param>
        private void DeleteLabel(SubscribedLabel deletedLabel)
        {
            SubscribedLabel foundedLabel = subscribedLabels.Find(x => x.Label == deletedLabel.Label);
            foundedLabel = deletedLabel;

            controlCanvas.Children.Remove(foundedLabel.Label);
            subscribedLabels.Remove(foundedLabel);

            HideConfig();
        }

        /// <summary>
        /// Callback when some control element is double clicked that will show the settings for selected control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void object_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Storyboard sb = new Storyboard();
            ThicknessAnimation thicknessAnimation = new ThicknessAnimation();

            thicknessAnimation.SetValue(Storyboard.TargetNameProperty, "NodeSettings");
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(MarginProperty));

            thicknessAnimation.From = new Thickness(0, 0, -210, 0);
            thicknessAnimation.To = new Thickness(0, 0, 0, 0);
            thicknessAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            NodeSettings.Visibility = Visibility.Visible;

            Messenger.Default.Send(sender, "showConfig");

            sb.Children.Add(thicknessAnimation);
            sb.Begin(this);
        }

        private void ConsumeMessage(SimpleMessage message)
        {
            switch (message.Type)
            {
                case SimpleMessage.MessageType.SettingsUpdated:
                    HideConfig();
                    break;
            }
        }

        /// <summary>
        /// Simple function that will take care of hiding the node settings view
        /// </summary>
        private void HideConfig()
        {
            Storyboard sb = new Storyboard();
            ThicknessAnimation thicknessAnimation = new ThicknessAnimation();

            thicknessAnimation.SetValue(Storyboard.TargetNameProperty, "NodeSettings");
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(MarginProperty));

            thicknessAnimation.From = new Thickness(0, 0, 0, 0);
            thicknessAnimation.To = new Thickness(0, 0, -210, 0);
            thicknessAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            sb.Children.Add(thicknessAnimation);
            sb.Begin(this);
        }

        /// <summary>
        /// This function will take care of adding the new camera element
        /// </summary>
        /// <param name="sender"></param>
        private void AddCamera(object sender)
        {
            SimpleCamera simpleCamera = new SimpleCamera();

            Random RNG = new Random();
            int length = 16;
            var rString = "";
            for (var i = 0; i < length; i++)
            {
                rString += ((char)(RNG.Next(1, 26) + 64)).ToString().ToLower();
            }

            SubscribedCamera subscribedCamera = new SubscribedCamera
            {
                Id = Guid.NewGuid(),
                UniqueName = rString,
                Camera = simpleCamera,
                Topic = ""
            };

            simpleCamera.Name = subscribedCamera.UniqueName;
            Grid grid = subscribedCamera.Camera.Content as Grid;
            Image camera = grid.Children[0] as Image;

            camera.Name = subscribedCamera.UniqueName + "_image";

            subscribedCameras.Add(subscribedCamera);

            simpleCamera.MouseDoubleClick += object_MouseDoubleClick;

            simpleCamera.VerticalAlignment = VerticalAlignment.Center;
            simpleCamera.HorizontalAlignment = HorizontalAlignment.Center;

            simpleCamera.Margin = new Thickness(0, 0, 0, 0);

            controlCanvas.Children.Add(simpleCamera);
            Messenger.Default.Send(subscribedCamera, "cameraAdd");
        }
        
        /// <summary>
        /// This function will load camera object when the app is opened with already configured layout 
        /// </summary>
        /// <param name="simpleCamera">Camera element that should be loaded</param>
        void LoadCamera(SimpleCamera simpleCamera)
        {
            SimpleCamera cameraControl = new SimpleCamera();

            cameraControl.Margin = new Thickness(simpleCamera.Margin.Left, simpleCamera.Margin.Top, simpleCamera.Margin.Right, simpleCamera.Margin.Bottom);

            cameraControl.Name = simpleCamera.Name;


            Tuple<SimpleCamera, string> cameraData = new Tuple<SimpleCamera, string>(cameraControl, cameraControl.Name);
            Messenger.Default.Send(cameraData, "loadCamera");

            cameraControl.MouseDoubleClick += object_MouseDoubleClick;

            cameraControl.VerticalAlignment = VerticalAlignment.Center;
            cameraControl.HorizontalAlignment = HorizontalAlignment.Center;

            controlCanvas.Children.Add(cameraControl);
        }

        /// <summary>
        /// This function will take care of adding the new switch element
        /// </summary>
        /// <param name="sender"></param>
        private void AddSwitch(object sender)
        {
            SimpleSwitch simpleSwitchControl = new SimpleSwitch();

            Random RNG = new Random();
            int length = 16;
            var rString = "";
            for (var i = 0; i < length; i++)
            {
                rString += ((char)(RNG.Next(1, 26) + 64)).ToString().ToLower();
            }

            SubscribedSwitch subscribedSwitch = new SubscribedSwitch
            {
                Id = Guid.NewGuid(),
                UniqueName = rString,
                SimpleSwitch = simpleSwitchControl,
                Topic = "",
                Clickable = true
            };

            simpleSwitchControl.Name = subscribedSwitch.UniqueName;

            Grid grid = subscribedSwitch.SimpleSwitch.Content as Grid;
            CheckBox box = grid.Children[0] as CheckBox;

            Image icon = grid.Children[1] as Image;

            Binding commandBinding = new Binding("SwitchClickedCommand");
            BindingOperations.SetBinding(icon.InputBindings[0], InputBinding.CommandProperty, commandBinding);

            icon.InputBindings[0].CommandParameter = subscribedSwitch.UniqueName;

            Binding myBinding = new Binding("Switches[" + subscribedSwitch.Id + "]");
            BindingOperations.SetBinding(icon, Image.SourceProperty, myBinding);

            subscribedSwitches.Add(subscribedSwitch);

            simpleSwitchControl.MouseDoubleClick += object_MouseDoubleClick;

            simpleSwitchControl.VerticalAlignment = VerticalAlignment.Center;
            simpleSwitchControl.HorizontalAlignment = HorizontalAlignment.Center;

            simpleSwitchControl.Margin = new Thickness(0, 0, 0, 0);

            controlCanvas.Children.Add(simpleSwitchControl);
            Messenger.Default.Send(subscribedSwitch, "switchAdd");
        }

        /// <summary>
        /// This function will load switch object when the app is opened with already configured layout 
        /// </summary>
        /// <param name="simpleSwitch">The switch element that should be loaded</param>
        private void LoadSwitch(SimpleSwitch simpleSwitch)
        {
            SimpleSwitch switchControl = new SimpleSwitch();

            switchControl.Margin = new Thickness(simpleSwitch.Margin.Left, simpleSwitch.Margin.Top, simpleSwitch.Margin.Right, simpleSwitch.Margin.Bottom);

            switchControl.Name = simpleSwitch.Name;

            Tuple<SimpleSwitch, string> switchData = new Tuple<SimpleSwitch, string>(switchControl, switchControl.Name);
            Messenger.Default.Send(switchData, "loadSwitch");

            switchControl.MouseDoubleClick += object_MouseDoubleClick;

            switchControl.VerticalAlignment = VerticalAlignment.Center;
            switchControl.HorizontalAlignment = HorizontalAlignment.Center;
            
            controlCanvas.Children.Add(switchControl);
        }

        /// <summary>
        /// This function will take care of adding the new label element
        /// </summary>
        /// <param name="sender"></param>
        private void AddLabel(object sender)
        {
            SimpleLabel labelControl = new SimpleLabel();

            Random RNG = new Random();
            int length = 16;
            var rString = "";
            for (var i = 0; i < length; i++)
            {
                rString += ((char)(RNG.Next(1, 26) + 64)).ToString().ToLower();
            }

            SubscribedLabel _subscribedLabel = new SubscribedLabel
            {
                Id = Guid.NewGuid(),
                UniqueName = rString,
                Label = labelControl,
                Topic = ""
            };

            Label label = labelControl.Label;

            label.Name = _subscribedLabel.UniqueName;

            Binding myBinding = new Binding("Labels[" + _subscribedLabel.Id + "]");
            label.SetBinding(ContentProperty, myBinding);

            Messenger.Default.Send(new UpdateLabelMessage { subscribedLabel = _subscribedLabel, value = "Waiting for first Value..." }, "ChangeLabelValue");

            labelControl.MouseDoubleClick += object_MouseDoubleClick;

            labelControl.VerticalAlignment = VerticalAlignment.Center;
            labelControl.HorizontalAlignment = HorizontalAlignment.Center;
            labelControl.Margin = new Thickness(0, 30, 0, 0);

            controlCanvas.Children.Add(labelControl);
            Messenger.Default.Send(_subscribedLabel, "labelAdd");
        }

        /// <summary>
        /// This function will load label object when the app is opened with already configured layout 
        /// </summary>
        /// <param name="simpleLabel">Label control that should be loaded</param>
        private void LoadLabel(SimpleLabel simpleLabel)
        {
            SimpleLabel labelControl = new SimpleLabel();

            labelControl.Margin = new Thickness(simpleLabel.Margin.Left, simpleLabel.Margin.Top, simpleLabel.Margin.Right, simpleLabel.Margin.Bottom);

            Label label = simpleLabel.Content as Label;
            labelControl.Label.Name = label.GetValue(NameProperty).ToString();

            Tuple<SimpleLabel, string> labelData = new Tuple<SimpleLabel, string>(labelControl, label.GetValue(NameProperty).ToString());
            Messenger.Default.Send(labelData, "loadLabel");

            labelControl.MouseDoubleClick += object_MouseDoubleClick;

            labelControl.VerticalAlignment = VerticalAlignment.Center;
            labelControl.HorizontalAlignment = HorizontalAlignment.Center;

            controlCanvas.Children.Add(labelControl);
        }

        /// <summary>
        /// Save button callback that will save current layout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder outstr = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            foreach(var child in controlCanvas.Children)
            {
                if(child.GetType().Name == "SimpleSwitch")
                {
                    SimpleSwitch simpleSwitch = child as SimpleSwitch;
                    Grid grid = simpleSwitch.Content as Grid;

                    Image icon = grid.Children[1] as Image;

                    icon.InputBindings[0].Command = null;
                }
            }

            XamlDesignerSerializationManager dsm = new XamlDesignerSerializationManager(XmlWriter.Create(outstr, settings));
            dsm.XamlWriterMode = XamlWriterMode.Expression;

            XamlWriter.Save(controlCanvas, dsm);
            string savedControls = outstr.ToString();

            if(!Directory.Exists(@"layouts"))
            {
                Directory.CreateDirectory(@"layouts");
            }
            File.WriteAllText(@"layouts/" + currentGrid + ".xaml", savedControls);

            foreach (var child in controlCanvas.Children)
            {
                if (child.GetType().Name == "SimpleSwitch")
                {
                    SimpleSwitch simpleSwitch = child as SimpleSwitch;

                    Grid grid = simpleSwitch.Content as Grid;

                    Image icon = grid.Children[1] as Image;

                    Binding commandBinding = new Binding("SwitchClickedCommand");
                    BindingOperations.SetBinding(icon.InputBindings[0], InputBinding.CommandProperty, commandBinding);
                }
            }

            Messenger.Default.Send("save", "saveObjects");
        }

        /// <summary>
        /// This function will load the whole grid from the file saved on disk
        /// </summary>
        /// <param name="gridName">Name of the grid that should be loaded</param>
        void reloadGrid(string gridName)
        {
            currentGrid = gridName;
            if(!File.Exists(@"layouts/" + gridName + ".xaml"))
            {
                controlCanvas.Children.Clear();
                return;
            }
            StreamReader sR = new StreamReader(@"layouts/" + gridName + ".xaml");
            string text = sR.ReadToEnd();
            sR.Close();

            StringReader stringReader = new StringReader(text);
            XmlReader xmlReader = XmlReader.Create(stringReader);

            Grid grid = (Grid)XamlReader.Load(xmlReader);

            controlCanvas.Children.Clear();

            foreach (FrameworkElement child in grid.Children)
            {
                if(child.GetType().ToString() == "HomeControler.Controls.SimpleLabel")
                {
                    SimpleLabel label = CloneFrameworkElement(child) as SimpleLabel;
                    LoadLabel(label);
                }
                else if(child.GetType().ToString() == "HomeControler.Controls.SimpleSwitch")
                {
                    SimpleSwitch simpleSwitch = CloneFrameworkElement(child) as SimpleSwitch;
                    LoadSwitch(simpleSwitch);
                }
                else if (child.GetType().ToString() == "HomeControler.Controls.SimpleCamera")
                {
                    SimpleCamera simpleCamera = CloneFrameworkElement(child) as SimpleCamera;
                    LoadCamera(simpleCamera);
                }
            }

            Messenger.Default.Send("reload", "getSubscribedDevices");
            Messenger.Default.Send("subscribe", "subscribeToTopics");
        }

        FrameworkElement CloneFrameworkElement(FrameworkElement originalElement)
        {
            string elementString = XamlWriter.Save(originalElement);

            StringReader stringReader = new StringReader(elementString);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            FrameworkElement clonedElement = (FrameworkElement)XamlReader.Load(xmlReader);

            return clonedElement;
        }
    }
}
