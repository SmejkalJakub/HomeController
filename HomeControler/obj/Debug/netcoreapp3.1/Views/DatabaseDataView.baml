   M S B A M L   `   `   `  ����H  DHomeControler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HomeControler.ViewsHomeControler.Views  5      -clr-namespace:HomeControlerHomeControler  5      *    $HomeControler.Views.DatabaseDataView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	      @d2http://schemas.microsoft.com/expression/blend/2008     5
      3local!clr-namespace:HomeControler.Views     5      +topclr-namespace:HomeControler     5      .��5      � 
�������.+�5   
   �� $��Filter��6   $��Right=�6!   $	��Top=�6=   $��29��6U   $��37��6a   $��
0,61,175,0q�6l   �� 5   
   -   5          ��Name$  	filterBox��5      $
��Left=�6%   $��1223,67,0,0q�6@    �� TextWrapping$ NoWrap=�6u   $	��Top=�6�   $	��146��6�   $	��146��6�   ��6U   ��  �� Path$ 	FilterBox�6�   �� 5   
   $��Column��6   $��Right=�6!   $	��Top=�6=   $��29��6U   $��54��6a   $��
0,86,175,0q�6l   �� 5   
   $
��Left=�6   $��1223,90,0,0q�6.   $	��Top=�6C   $	��146��6[   ��5      �� $ Filters� �� SelectedItem 5      �� $ SelectedFilter�&   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGridCustomers��5   d      AutoGenerateColumns$ False��6   $��Center=�6/   $	��Top=�6L      CanUserResizeRows$ False��6�   $
��1340��6�   $	��698��6�   $��	0,146,0,0q�6�   ��6}   �� $ DatabaseDataCollection�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	MessageId��6H      
IsReadOnly$
 True��6[   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� $ 	MessageId�6[     5      $ Topic��6D   $
 True��6S   	 6%   �� $ Topic�6S     5      $ Value��6D   $
 True��6S   	 6%   �� $ Value�6S     5      $
 Room��6C   $
 True��6Q   	 6%   �� $
 Room�6Q     5      $ Message Recieved��6O   $
 True��6i   	 6%   �� $ Message_recieved�6i   5!      �� 5"   
   -   5        $��btnloaddata��5"   a   $��	Load Data��6   $��25��6%   $��Right=�61   $��
0,116,29,0q�6M   $	��Top=�6t   $	��100��6�   ��6�   �� *LoadDataCommand+5#      5$      