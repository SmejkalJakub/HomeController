/*
    Simple class for sending message about camera update

    Author: Jakub Smejkal, xsmejk28
*/

using HomeControler.Objects;
using System.Windows.Media.Imaging;

namespace HomeControler.Others
{
    class UpdateCameraMessage
    {
        public SubscribedCamera subscribedCamera { get; set; }
        public BitmapImage value { get; set; }
    }
}
