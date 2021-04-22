/*
    Serializable class that will be created for each camera in the dashboard
    
    Author: Jakub Smejkal(xsmejk28)
*/

using HomeControler.Controls;
using System;
using System.Windows.Media.Imaging;

namespace HomeControler.Objects
{
    [Serializable]
    public class SubscribedCamera : SubscribedDevice
    {
        public string UniqueName { get; set; }

        [NonSerialized] public BitmapImage lastImage;

        [NonSerialized] public SimpleCamera Camera;
    }
}
