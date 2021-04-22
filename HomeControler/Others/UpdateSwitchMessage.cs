/*
    Simple class for sending message about switch update

    Author: Jakub Smejkal, xsmejk28
*/

using HomeControler.Objects;

namespace HomeControler.Others
{
    class UpdateSwitchMessage
    {
        public SubscribedSwitch subscribedSwitch { get; set; }
        public string value { get; set; }
    }
}
