   M S B A M L   `   `   `  ���� H  DHomeControler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HomeControler.ViewsHomeControler.Views  &     HomeControler.Views.SettingsView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:HomeControler.Views     .��� 
����ح�.+��� $��30,30,30,30q����#ECECEC.+���   �� Orientation$  
Horizontal=�.+��� $��MQTT Server Address���� $	��150��$	��300��$��1q�
������ $
��Bold"�$��20��$��Center=�����  �� Path$+ %SettingsModelProperty.BrokerIpAddress� �� Current �� Source# Z �� $	��150��$	��300��$��Connect������ *ConnectToServerCommand+�� ���� *	MQTTState+���� *	TextColor+�� $  
Horizontal=�.+��� $��Database Address���� $	��150��$	��300��$��1q�
������ $
��Bold"�$��20��$��Center=����� $- 'SettingsModelProperty.DatabaseIpAddress�# Z �� $  
Horizontal=�.+��� $��Database Name���� $	��150��$	��300��$��1q�
������ $
��Bold"�$��20��$��Center=����� $( "SettingsModelProperty.DatabaseName�# Z �� $  
Horizontal=�.+��� $��Database Table Name���� $	��150��$	��300��$��1q�
������ $
��Bold"�$��20��$��Center=����� $- 'SettingsModelProperty.DatabaseTableName�# Z �� $  
Horizontal=�.+��� $��Database User Name���� $	��150��$	��300��$��1q�
������ $
��Bold"�$��20��$��Center=����� $, &SettingsModelProperty.DatabaseUserName�# Z �� $  
Horizontal=�.+��� $��Database Password���� $	��150��$	��300��$��1q�
������ $
��Bold"�$��20��$��Center=����� $0 *SettingsModelProperty.DatabaseUserPassword�# Z �� $  
Horizontal=�.+��� $	��150��$	��300��$��Connect������ *ConnectToDbCommand+�� ���� *DbConnectionState+���� *DbTextColor+