   M S B A M L   `   `   `  ����J  FLoginManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:LoginManagement.CommonLoginManagement.Common  5      #    LoginManagement.View.DataView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:LoginManagement.Common     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      .+�5   
   &   System.Windows.Controls.DataGrid  $��0��6   $��15(�6J      RowHeaderWidth$ 27Q�6X      AutoGenerateColumns .  ��6    �� *DataModel.Courses+ � 	Resources 5         
errorStyle �� 
TargetType �� Property �� Value �� Triggers P� Property	 P� Value
 �� RelativeSource �� Path��%  &	        �� 5      " ��6+   .�5      ��  � ��$ -2q�60    5      P� 5       �@=�HasError	 . . �5      ��  � ��
 �  ��6;   �� 5       �@�ToolTip 68   �� 
 �� *Self+$&  (Validation.Errors)/ErrorContent�5      5      5         RowValidationRules 5      1   +LoginManagement.Common.CourseValidationRule  5      ,  &System.Windows.Controls.ValidationRule   ValidationStep$ CommittedValue=�6-   !   RowValidationErrorTemplate 5      �� 5      .R�5      � $��	0,-2,0,-2q�6    � ToolTip 6.   �� 
 �� *FindAncestor+ �� AncestorType)  #System.Windows.Controls.DataGridRow"  $&  (Validation.Errors)/ErrorContent�.+�5!      A� $��0��6"   
���  ��#��z��6A   #��z��6d   �� 5"      $��!��6$   #��z��6-   $
��Bold"�6S   
�������$��Center=�6x   5#      5$         Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn   Header$ Course Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *Name+ �� TargetNullValue$ (enter a course name)��  5(      $ 	Course ID��6%      EditingElementStyle# [  6�    68   �� *ID+ �� ValidatesOnExceptions .  �� NotifyOnValidationError . 6�     5)      $ Course StartDate��6%   # [  5*   %    5)   ?   �� *	StartDate+ .  .  �� StringFormat$ d��5*   %     5+      $ Course EndDate��6%   # [  5,   %    5+   =   �� *	EndDate+ .  . $ d��5,   %   5.      �� 5/   
   -   5        $��RelativeSource��5/   "   $��1��6    �� Orientation$ 
Horizontal=�68   .+�51      �� ��6   �� 
 ��  �� Mode$ FindAncestor=�" �� �� AncestorLevel$ 1��$
 Name��� 53      $
��Self��6   $	��100��6$   ��60   �� 
 �� $
 Self=�$ Width�54      55      56      