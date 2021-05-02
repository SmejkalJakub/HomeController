# Prototype application for my BP for smart homes

This app serves as a showcase of Smart home application for my BP.
It is just a prototype, the final application would be done in either MAUI framework or other multiplatform soulution.

The WPF framework was used for simplicity reasons and because of the delay of MAUI framework.

For testing purposes you can use settings (VPN access needed):

- 192.168.1.100
- 192.168.1.100
- SmartHomedb
- messages
- uiDesktopApp
- passForUiDesktopApp

To Import data with the button on the Dashboard view just put the 'HomeControllerData' folder onto your Desktop. After the settings setup and import you should restart the application and you will get all the Dashboards with all the controls.

If you use Export All Data button, all the data will be exported to the folder on your desktop 'HomeControllerData'

## Installation
To install the app onto your Windows, o to the Installation folder and run the ``setup.exe``.

After a brief installation, the app should start or you can find it as HomeController with windows search.
If you have a VPN access you should start it and then copy the HomeControllerData folder to the Desktop and then after setting up the Broker you can import the layouts into the dashboard.


## Known bugs

- If you want to import the data from desktop folder you will need to set the MQTT broker first and then after import restart the application. After that all will be imported just fine.

- Data from the database might take a while to load.

- All the layouts are named with numbers and cant be renamed (Default layout is set as "Default").