/*
    Simple class for sending messages with Messenger that will allow ViewModels to communicate

    Author: Jakub Smejkal, xsmejk28
*/

namespace HomeControler.Others
{
    public class SimpleMessage
    {
        public SimpleMessage()
        {
        }

        /// <summary>
        /// Enum to specify the type of Message
        /// </summary>
        public enum MessageType
        {
            SettingsUpdated,
            ConnectedToMQTT,
            MQTTTimeout
        }

        public MessageType Type { get; set; }

        public string Message { get; set; }
    }
}
