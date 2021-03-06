/*
    Serializable class that will be created for each switch in the dashboard
    
    Author: Jakub Smejkal (xsmejk28)
*/

using HomeControler.Controls;
using System;

namespace HomeControler.Objects
{
    [Serializable]
    public class SubscribedSwitch : SubscribedDevice
    {

        [NonSerialized] public SimpleSwitch SimpleSwitch;
        public string UniqueName { get; set; }
        public string OnIconUri { get; set; }
        public string OffIconUri { get; set; }
        public bool State { get; set; }
        public bool Clickable { get; set; }
    }
}
