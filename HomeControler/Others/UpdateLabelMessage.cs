/*
    Simple class for sending message about label update

    Author: Jakub Smejkal, xsmejk28
*/

using HomeControler.Objects;

namespace HomeControler.Others
{
    public class UpdateLabelMessage
    {
        public SubscribedLabel subscribedLabel { get; set; }
        public string value { get; set; }
    }
}
