/*
    Parent class from whitch all the Subscribed* classes will inherit ID, Name and Topic
    
    Author: Jakub Smejkal (xsmejk28)
*/

using System;

namespace HomeControler.Objects
{
    [Serializable]
    public class SubscribedDevice
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
    }
}
