   M S B A M L   `   `   `  ���� H  DHomeControler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HomeControler.ViewsHomeControler.Views  -clr-namespace:HomeControlerHomeControler  *    $HomeControler.Views.DatabaseDataView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:HomeControler.Views     +topclr-namespace:HomeControler     .��� 
�������.+��� $��Filter��$��Right=�$	��Top=�$��29��$��37��$��
0,61,175,0q��� -     ��Name$  	filterBox��$
��Left=�$��1223,67,0,0q� �� TextWrapping$ NoWrap=�$	��Top=�$	��146��$	��146������  �� Path$ 	FilterBox��� $��Column��$��Right=�$	��Top=�$��29��$��54��$��
0,86,175,0q��� $
��Left=�$��1223,90,0,0q�$	��Top=�$	��146������ $ Filters� �� SelectedItem �� $ SelectedFilter�&   System.Windows.Controls.DataGrid  -   $��dataGridCustomers��   AutoGenerateColumns$ False��$��Center=�$	��Top=�   CanUserResizeRows$ False��$
��1340��$	��698��$��	0,146,0,0q����� $ DatabaseDataCollection�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	MessageId��   
IsReadOnly$
 True��1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� $ 	MessageId�  $ Topic��$
 True��	 �� $ Topic�  $ Value��$
 True��	 �� $ Value�  $
 Room��$
 True��	 �� $
 Room�  $ Message Recieved��$
 True��	 �� $ Message_recieved��� -   $��btnloaddata��$��	Load Data��$��25��$��Right=�$��
0,116,29,0q�$	��Top=�$	��100������ *LoadDataCommand+