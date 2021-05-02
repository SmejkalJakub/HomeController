# Prototype application for my BP for smart homes

This app serves as a showcase of Smart home application for my BP.
It is just a prototype, the final application would be done in either MAUI framework or other multiplatform soulution.

The WPF framework was used for simplicity reasons and because of the delay of MAUI framework.

For testing puposes you can use settings (VPN access needed):

- 192.168.1.100
- 192.168.1.100
- SmartHomedb
- messages
- uiDesktopApp
- passForUiDesktopApp

To Import data with the button on the Dashboard view just put the 'HomeControllerData' folder onto your Desktop. After the settings setup and import you should restart the application and you will get all the Dashboards with all the controls.

If you use Export All Data button, all the data will be exported to the folder on your desktop 'HomeControllerData'

## Known bugs

- If you want to import the data from desktop folder you will need to set the MQTT broker first and then after import restart the application. After that all will be imported just fine.

- Data from the database might take a while to load.

- All the layouts are named with numbers and cant be renamed (Default layout is set as "Default").