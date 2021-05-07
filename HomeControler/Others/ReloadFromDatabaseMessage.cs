/*
    Custom message class that is used to retrieve data from database for each device
    
    Author: Jakub Smejkal (xsmejk28)
*/

using HomeControler.Objects;
using System.Collections.Generic;

namespace HomeControler.Others
{
    public class ReloadFromDatabaseMessage
    {
        public List<SubscribedLabel> subscribedLabels { get; set; }
        public List<SubscribedSwitch> subscribedSwitches { get; set; }
        public List <SubscribedCamera> subscribedCameras { get; set; }
    }
}
