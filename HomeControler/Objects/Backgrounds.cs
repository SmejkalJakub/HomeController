/*
    Serializable class that will be created for each camera in the dashboard
    
    Author: Jakub Smejkal (xsmejk28)
*/

using System;

namespace HomeControler.Objects
{
    [Serializable]

    public class Backgrounds
    {
        string backgroundUri { get; set; }
    }
}
