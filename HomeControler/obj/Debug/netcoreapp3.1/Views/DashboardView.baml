   M S B A M L   `   `   `  ����H  DHomeControler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HomeControler.ViewsHomeControler.Views  5      -clr-namespace:HomeControlerHomeControler  5      ;"clr-namespace:HomeControler.OthersHomeControler.Others  5      '    !HomeControler.Views.DashboardView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5	      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5
      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:HomeControler.Views     5      +topclr-namespace:HomeControler     5      5others"clr-namespace:HomeControler.Others     5      .��5      � -   5          �Name$  
mainCanvas��5      
������� � 	Resources 5   
   '  !HomeControler.Others.BindingProxy 	  Proxy   Data��%&   &	          5       60   �� 5       � ContextMenu 5   
   �� 5      .@�5      m� $��Label��6    m� Command 6*   ��  �� Source# [   �� Path$ Data.AddLabelCommand�m� 5      $��Switch��6    6+   �� # [  $ Data.AddSwitchCommand�m� 5      $��Camera��6    6+   �� # [  $ Data.AddCameraCommand�5      .+�5   
   �� -   5        $��imgPhoto��5      $��Center=�6    $��Center=�6=   ��6X   �� *BackgroundSource+�� 5    
   $��+��6   $	��Top=�6   $��Right=�65   $��22��6y   $��20��6�   $��	0,10,10,0q�6�   ��6Q   �� *AddNewLayoutCommand+6�       HomeControler.NodeSettings  5"   
   -   5          Name$ NodeSettings��5"      $��	600,0,0,0q�5#      $y�Hidden=�5$      
�������$��Stretch=�5&      $��Right=�5'      �� 5(   
   $��Change Home Plan��6   $��Bottom=�6,   $
��Left=�6G   $��	96,0,0,10q�6�   $��39��6�   $	��108��6�   ��6b   �� *ChangeLayoutImageButton+6�   �� 5)   
   -   5         ��Name$ 
saveButton��5)      $��Save Layout��6%   $��Bottom=�6;   $
��Left=�6V   $��	10,0,0,10q�6q   $��39��6�   $��74��6�   �� 5*   
   $��Export All Data��6   $��Bottom=�6+   $
��Left=�6F   $��
217,0,0,10q�6�   $��39��6�   $	��108��6�   ��6a   �� *ExportAllDataCommand+6�   �� 5+   
   $��Import Data��6   $��Bottom=�6'   $
��Left=�6B   $��
339,0,0,10q�6�   $��39��6�   $	��108��6�   ��6]   �� *ImportDataCommand+6�   �� 5.   
   $	��Top=�64   $
��Left=�6L   $��22��6�   $��74��6�   $��22��6�   $��74��6  ��6   �� *ButtonNames[0]+��6g   �� *ButtonPositions[0]+��6�   �� *ChangeLayoutCommand+��6�   �� *ButtonNames[0]+	 J� InputBindings	 5/      d� 50      
 d� Gesture$
 LeftDoubleClicka�6    �� CommandParameter$ 0��6a    �� Command 69   �� *ShowToRenameCommand+6a   52      �� 54   
   $
��TEST��6   $	��Top=�6   $
��Left=�66   $��22��6�   $��74��6�   $��22��6�    �� 	MaxLength$ 15��6�   $��74��6�   y�6Q   �� *TextBoxVisibility+��6z   �� *TextBoxPosition+6�   � 56   
   -   5        $  controlCanvas��56      58      �� 59   
   $	��300��6T   $	��300��6d   ��6   �� *CameraLastImage+��63   �� *ImagePosition+6d   5:      5;      