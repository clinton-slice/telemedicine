   M S B A M L   `   `   `  ����G  CTeleMedicine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:TeleMedicine.pages.doctorTeleMedicine.pages.doctor  5      (    "TeleMedicine.pages.doctor.messages   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:TeleMedicine.pages.doctor     5      $	��685��5   6   $	��675��6B   
�������  � 	Resources  5	         	myLBStyle �� 
TargetType �� 	Resources �� Color  
myLBStyle2��%\  &	        &	 �     �� 5
   
   " ��6"    5      ���� 5      $ 	LightGray��5      (��       5   "   * SystemColors.HighlightBrushKey+)5      5      5      �� 5   
   " ��6#    5      ���� 5      $ Transparent��5      (��       5   "   * SystemColors.HighlightBrushKey+)5      5      5      5      .��5      � ��5   
   �� 5       �� ImageSource$ 13.png��6    j� Stretch$
 None=�6.    �� Opacity$	 0.1Q�6=   .+�5   
   �� -   5         ��Name$ label��5      $��MESSAGES��6   $��Right=�62   $
��33.5��6N   $��0,31.098,40.365,0q�6\   $	��Top=�6w   $��116.05��6�   $��Segoe UI Light)�6�   $��18.667(�6�   $��ExtraCondensed&�6�   $
��Bold"�6�   
���   ���6�   Z� 6  �� 5   
   -   5         ��Name$ image��5      $��Right=�6   $
��33.5��6;   $��0,31.098,157.415,0q�6I   $	��Top=�6e   $��28.419��6}   $��message.png��6�   �� 5   
   -   5        	 ��Name$	 conversationlist��5      #��[  6,   $
��Left=�6\   $��12.092,179.242,0,18.5q�6w   $��198.874��6�   ���	#FFE6E0E05!      �� 5#   
   -   5        $	 bodyBox��5#      #��[ 6#   $��232,127.742,31,138q�6T   ���	#FFDADDE26p   �� 5$   
   $	��112��6   $��228,0,31,10q�6!   $��Bottom=�66   �� 5%   
   -   5        
 ��Name$
 	rectFrame��5%      $��26.242��6'   $��232,96.5,31,0q�67   ���	#FFE6E9EA$	��Top=�6a    �� RadiusY$	 7.5��6y    �� RadiusX$	 7.5��6�   ��5&      �� 5'       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5(      �  � Color$ 	#FFF3F3F3��6#    � Offset$ 0Q�65   � 5)      $ 	#FFF8FBDA��6#   $ 1Q�65   5*      5,      �� 5-   
   -   5        $ label1��5-      $!��Conversations with patients��6    $
��Left=�6F   $��27.742��6a   $��13.5,150.5,0,0q�6q   $	��Top=�6�   $��192.966��6�   $��13.333(�6�   $
��Bold"�6�   $��Segoe UI Light)�6�   &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�6�   �� 5.   
   -   5         ��Name$ addConversation��5.      $��New Conversation��6*   $
��Left=�6E   $��27.742��6`   $��18,98.258,0,0q�6p   $	��Top=�6�   $	��140��6�   $��Segoe UI Light)�6�   $ Display=�6�   ���	#FFC4D6D8��6�   Z� 6  �� 5/   
   -   5         ��Name$	 rtb��5/      $��75��6#   $��232,0,31,51.81q�6/   $��Bottom=�6G   $ Display=�6b   $��Segoe UI Light)�6�   . �50      /� 51      �� 52   
   -	   5        $ sendBut��52      $
��Send��6"   $��Right=�61   $��26��6M   $��0,0,34.365,18.638q�6Y   $��Bottom=�6t   $��58.02��6�   $ Display=�6�   ��6�   Z� �� 53   
   -
   5        $ threadLabel��53      $��Thread started by: You��6%   $��25.242��6F   $��234,96.5,188.834,0q�6V   $	��Top=�6r   $ Display=�6�   �� 54   
   -   5        $ messagecountLabel��54      $��Message Count: 1��6+   $��Right=�6F   $��25.242��6b   $��0,96.5,43,0q�6r   $	��Top=�6�   $��130.834��6�   $ Display=�6�   �� 55   
   -   5        $ 	delButton��55      $
��Left=�6$   $��25��6?   $��172,98.258,0,0q�6K   $	��Top=�6c   $��25��6{    � ToolTip$ Delete a conversation��6�    � Cursor$
 Hand��6�   ��6�   Z� ��56      �� 57      $ file_delete.png��6   $ Uniform=�6;   59      5;      5<      