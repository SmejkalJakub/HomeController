/*
    Code behind for the NodeSettings

    Author: Jakub Smejkal (xsmejk28)
*/

using GalaSoft.MvvmLight.Messaging;
using HomeControler.Objects;
using HomeControler.Others;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HomeControler
{
    /// <summary>
    /// Interaction logic for nodeSettings.xaml
    /// </summary>
    public partial class NodeSettings : UserControl
    {
        SubscribedLabel currentLabel;
        SubscribedSwitch currentSwitch;
        SubscribedCamera currentCamera;

        public NodeSettings()
        {
            InitializeComponent();

            Messenger.Default.Register<SubscribedLabel>(this, "topic", GetValues);
            Messenger.Default.Register<SubscribedSwitch>(this, "topic", GetValues);
            Messenger.Default.Register<SubscribedCamera>(this, "topic", GetValues);
        }

        /// <summary>
        /// Load the selected element data and show it in the view
        /// </summary>
        /// <param name="node">Element that was selected</param>
        private void GetValues(object node)
        {
            switch (node.GetType().Name)
            {
                case "SubscribedLabel":
                    currentLabel = (SubscribedLabel) node;

                    topicBox.Text = currentLabel.Topic;
                    valueBox.Visibility = Visibility.Visible;
                    valueLabel.Visibility = Visibility.Visible;

                    postfixLabel.Visibility = Visibility.Visible;
                    postfixBox.Visibility = Visibility.Visible;

                    ClickableCheckbox.Visibility = Visibility.Hidden;

                    OnIcon.Visibility = Visibility.Hidden;
                    OffIcon.Visibility = Visibility.Hidden;
                    OnButton.Visibility = Visibility.Hidden;
                    OffButton.Visibility = Visibility.Hidden;

                    valueBox.Text = currentLabel.Prefix;
                    postfixBox.Text = currentLabel.Postfix;


                    break;
                case "SubscribedSwitch":
                    currentSwitch = (SubscribedSwitch)node;

                    topicBox.Text = currentSwitch.Topic;
                    valueBox.Visibility = Visibility.Hidden;
                    valueLabel.Visibility = Visibility.Hidden;

                    postfixLabel.Visibility = Visibility.Hidden;
                    postfixBox.Visibility = Visibility.Hidden;

                    ClickableCheckbox.Visibility = Visibility.Visible;
                    ClickableCheckbox.IsChecked = currentSwitch.Clickable;


                    OnIcon.Visibility = Visibility.Visible;
                    OffIcon.Visibility = Visibility.Visible;
                    OnButton.Visibility = Visibility.Visible;
                    OffButton.Visibility = Visibility.Visible;


                    Messenger.Default.Send(currentSwitch, "switchInfo");

                    break;
                case "SubscribedCamera":
                    currentCamera = (SubscribedCamera)node;

                    topicBox.Text = currentCamera.Topic;
                    valueBox.Visibility = Visibility.Hidden;
                    valueLabel.Visibility = Visibility.Hidden;

                    ClickableCheckbox.Visibility = Visibility.Hidden;

                    postfixLabel.Visibility = Visibility.Hidden;
                    postfixBox.Visibility = Visibility.Hidden;

                    OnIcon.Visibility = Visibility.Hidden;
                    OffIcon.Visibility = Visibility.Hidden;
                    OnButton.Visibility = Visibility.Hidden;
                    OffButton.Visibility = Visibility.Hidden;

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Confirm button callback that will send updated data to the view model to be saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(currentSwitch != null)
            {
                currentSwitch.Topic = topicBox.Text;
                currentSwitch.Clickable = Convert.ToBoolean(ClickableCheckbox.IsChecked);

                Messenger.Default.Send(currentSwitch, "updateIcons");
                Messenger.Default.Send((currentSwitch as object), "updateObject");
            }
            else if(currentLabel != null)
            {
                currentLabel.Topic = topicBox.Text;
                currentLabel.Prefix = valueBox.Text;
                currentLabel.Postfix = postfixBox.Text;


                Messenger.Default.Send((currentLabel as object), "updateObject");
            }
            else if (currentCamera != null)
            {
                currentCamera.Topic = topicBox.Text;

                Messenger.Default.Send((currentCamera as object), "updateObject");
            }
            Messenger.Default.Send(new SimpleMessage { Type = SimpleMessage.MessageType.SettingsUpdated });


            currentLabel = null;
            currentSwitch = null;
            currentCamera = null;
        }

        /// <summary>
        /// Delete button callback to delete element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send(currentLabel, "labelDelete");
        }
    }
}
