   M S B A M L   `   `   `  ����E  AEntryPoint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:EntryPoint.ControlEntryPoint.Control  5      5clr-namespace:EntryPoint.CommonEntryPoint.Common  5      =#clr-namespace:EntryPoint.ConvertersEntryPoint.Converters  5      3clr-namespace:EntryPoint.ModelEntryPoint.Model  5	      .    (EntryPoint.Control.ProofreadingContainer   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:EntryPoint.Control     5      2commonclr-namespace:EntryPoint.Common     5      :
converters#clr-namespace:EntryPoint.Converters     5      0modelclr-namespace:EntryPoint.Model     5	        � 	Resources  5         BackgroundBrush �� Color  SelectedBrush  GrayTextBrush �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� X1	 �� Y1		 �� Y2
 �� StrokeDashArray	 �� X2 �� RelativeSource �� Mode �� AncestorType �� Path��%W  &	        &	 !     &	 B     '�� c     7  7   '�� �    �� 5   
   $ #F7F7FA��62   �� 5   
   $ #E3E3ED��60   �� 5   
   $ #6e6e91��60   �� 5   
   " ��6   .�5      ��  � ��$ 25��6'   �� 5       � ��$ 11(�6)   �� 5       � ��8   6+   �� 5       � ��8  6+   -   5        �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 5��6K   ._�5      �� $��Center=�6/   $
��Left=�5   /   $��3,0,15,0q�5   /   5      5      5!      5"      �� 5$   
   " ��6   .�5%      ��  � �� 5&      �� 5'      " ��6&   .R�5(      �� $ 0��6   $ 0��6&   $	 0��6-   $	
 1,2R�5)      $��1��5*      
������� 5(   4   ��  �� $ FindAncestor=�$ 	SeparatorL�$ ActualWidth�5+      5,      5.      5/      51      .��53      �  � DataContext 6   ��  �� $ FindAncestor=�$! local:ProofreadingContainerL� � ColumnDefinitions 54   
   �� 55      $	��200 �6   �� 56      $	��400 �6    � RowDefinitions 58   
   �� 59      $	��200 �6   .+�5;   
   �� #��[  6   ._�5<      �� .��5=      �� $
��13,0q�6   .+�5>      �� ��6#   �� $ ProofreadingGroups���5?      �� 5@       �� DataType(   "EntryPoint.Model.ProofreadingGroup"  6+   .R�5A   "   �� .+�5B   &   �� #��[ 5C   0   $��11(�5D   0   $��Medium"�5E   0   $	��3,0q�5F   0   ��5B   0   �� *
GroupTitle+5F   0   �� 5G   &   ��63   �� $ Items���5H   *   �� 5I   .   '   !EntryPoint.Model.ProofreadingItem"  6;   .R�5J   2   � .+�5K   6   �� ��6<   �� $ Title�
 � Tag 5L   <   �� *.+�� 5M   6   #��[ 5N   <   
���   ���5M   <   �� $ Title�y�5O   <   �� $ 
IsSelected�5   /EntryPoint.Converters.BoolToVisibilityConverter   Instance �� 	Converter# Z  5Q   <   �� *.+�� 5R   6   $��Right=�5S   @   $��Center=�5T   @   #��[ 5U   @   $	��3,0q�5V   @   ��5R   @   �� $ Contents.Count�5V   @   5W   3   5X   /   5Z   '   �� 5[   &   $��0,0,0,10q�60   5\   #   5]      5_      5`      5a      5b      �� 5c   
   $��1��6   .��5d      �� $
��13,0q�6   .+�5e      �� $��16(�5f      
���   �$��0,0,0,5q�5h      ��5e      �� $ SelectedItem.Title�5h      �� 5i      $
��Wrap=�5j      $��CharacterEllipsis=�5k      $��11(�5l      
���   ���5i      �� $ SelectedItem.Description�5m      �� 5n      ��6   �� $ SelectedItem.Contents���5o      �� 5p      *   $EntryPoint.Model.ProofreadingContent"  6'   .R�5q      �� .+�5r   "   �� $
��0,15q�6,   �� 5s   "   #��[ 5t   ,   $��0,0,0,10q�5u   ,   ��5s   ,   �� $ Subject�y�5v   ,   �� $ Subject�7   1EntryPoint.Converters.StringToVisibilityConverter   Instance# Z #   EntryPoint.Common.TextBlockEx  5w   "   $��12(�5x   5   $��3,0,0,0q�5y   5      FormattedText 5w   5   �� $ FormattedText�5y   5   5z      5{      5}      5~      5      5�      5�      