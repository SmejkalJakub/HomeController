   M S B A M L   `   `   `  ����H  DHomeControler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HomeControler.ViewsHomeControler.Views  5      &     HomeControler.Views.SettingsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	      @d2http://schemas.microsoft.com/expression/blend/2008     5
      3local!clr-namespace:HomeControler.Views     5      .��5      � 
����ح�.+�5   
   �� $��30,30,30,30q�6   ���#ECECEC.+�5      ��   �� Orientation$  
Horizontal=�6   .+�5      �� $��MQTT Server Address��6   �� 5      $	��150��6   $	��300��6)   $��1q�68   
������ $
��Bold"�6e   $��20��6w   $��Center=�6�   ��6�   ��  �� Path$+ %SettingsModelProperty.BrokerIpAddress� �� Current �� Source# Z �� 5      $	��150��6   $	��300��6(   $��Connect��67   ��6I   �� *ConnectToServerCommand+�� 5      ��6   �� *	MQTTState+��66   �� *	TextColor+5      �� 5      $  
Horizontal=�6   .+�5      �� $��Database Address��6   �� 5      $	��150��6   $	��300��6)   $��1q�68   
������ $
��Bold"�6e   $��20��6w   $��Center=�6�   ��6�   �� $- 'SettingsModelProperty.DatabaseIpAddress�# Z 5      �� 5      $  
Horizontal=�6   .+�5      �� $��Database Name��6   �� 5      $	��150��6   $	��300��6)   $��1q�68   
������ $
��Bold"�6e   $��20��6w   $��Center=�6�   ��6�   �� $( "SettingsModelProperty.DatabaseName�# Z 5      �� 5       $  
Horizontal=�6   .+�5!      �� $��Database Table Name��6   �� 5"      $	��150��6   $	��300��6%   $��1q�64   
������ $
��Bold"�6a   $��20��6s   $��Center=�6   ��6�   �� $- 'SettingsModelProperty.DatabaseTableName�# Z 5$      �� 5%      $  
Horizontal=�6   .+�5&      �� $��Database User Name��6   �� 5'      $	��150��6   $	��300��6)   $��1q�68   
������ $
��Bold"�6e   $��20��6w   $��Center=�6�   ��6�   �� $, &SettingsModelProperty.DatabaseUserName�# Z 5)      �� 5*      $  
Horizontal=�6   .+�5+      �� $��Database Password��6   �� 5,      $	��150��6   $	��300��6)   $��1q�68   
������ $
��Bold"�6e   $��20��6w   $��Center=�6�   ��6�   �� $0 *SettingsModelProperty.DatabaseUserPassword�# Z 5.      �� 5/      $  
Horizontal=�6   .+�50      �� $	��150��6   $	��300��6(   $��Connect��67   ��6I   �� *ConnectToDbCommand+�� 51      ��6   �� *DbConnectionState+��6>   �� *DbTextColor+52      53      55      56      