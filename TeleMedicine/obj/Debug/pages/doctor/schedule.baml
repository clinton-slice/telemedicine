   M S B A M L   `   `   `  ����G  CTeleMedicine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:TeleMedicine.pages.doctorTeleMedicine.pages.doctor  5      (    "TeleMedicine.pages.doctor.schedule   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:TeleMedicine.pages.doctor     5      $	��685��5   7   $	��675��6C   
�������.��5
      � .+�5   
   �� -   5          ��Name$  label��5      $��	SCHEDULES��6   $��Right=�63   $
��33.5��6O   $��0,27.51,31.902,0q�6]   $	��Top=�6w   $��115.164��6�   $��Segoe UI Light)�6�   $��18.667(�6�   $��ExtraCondensed&�6�   $
��Bold"�6�   
���   ���6�   Z� 6  &   System.Windows.Controls.Calendar  5   
   -   5          Name$ calendar��5      $��0,124.07,0,0q�6%   $
��Left=�6;   $��175.766��6V   $	��Top=�6g   $��211.215��6      SelectionMode$ MultipleRange=�6�      DisplayMode$
 Year=�6�   �� 5   
   $��200.875��6   $��242.232,88.622,31.902,0q�6%   ���	#FFD8CFCF$	��Top=�6Y    �� RadiusY$ 15.747��6q    �� RadiusX$ 15.747��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
datepicker��5      $��25��6)   $��0,169.32,100.098,0q�65   $	��Top=�6Q   $��Right=�6i   $��215.646��6�      DisplayDate$ 
2015-11-07��6�   �� 5   
   -   5         ��Name$ 	listView0��5      $"��11.816,313.698,19.408,22.334q�6&   	 �� View	 5      �� 5      .P�5      �� $��
Patient ID��6S   
 �� Width$
 80��6g    �� DisplayMemberBinding 6%   �� *patient_code+6g   �� 5      $��Patient��6S   $	
 180��6d    6%   �� *patient_name+6d   �� 5      $��Attended��6O   $
 65��6a    6%   �� *
attended+6a   �� 5      $��Schedule Details��6O   $	
 155��6i    6%   �� *
schedule+6i   �� 5      $��Date Created��6N   $	
 155��6d    6%   �� *	created+6d   5      ��5      �� 5       �� 
TargetType" ��6   .�5      -   5        5      5      �� 5   
   -   5        $  label1��5      $��New Schedule��6    $��26.587��67   $��265.864,94.53,321.652,0q�6G   $	��Top=�6h   $��Segoe UI Semibold)�6�   $��13.333(�6�   �� 5    
   -   5         ��Name$ button��5       $
��Save��6!   $��Right=�60   $��29.54��6L   $��0,251.095,230.077,0q�6[   $	��Top=�6x   $��85.667��6�   6�   �� 5!   
   -   5         ��Name$ 	comboBox0��5!      $��Right=�6&   $��26.017��6B   $��0,132.025,100.098,0q�6R   $	��Top=�6o   $��215.646��6�   �� 5"   
   -	   5        $  label2��5"      $��Patient��6    $
��Left=�62   $��29.54��6M   $��280.635,131.456,0,0q�6\   $	��Top=�6y   $��59.081��6�   �� 5#   
   -
   5        $  label3��5#      $
��Date��6    $
��Left=�6/   $��29.54��6J   $��280.635,169.32,0,0q�6Y   $	��Top=�6u   $��59.081��6�   �� 5$   
   -   5        $  label4��5$      $
��Time��6    $
��Left=�6/   $��28.033��6J   $��280.635,208.291,0,0q�6Z   $	��Top=�6w   $��39.879��6�   �� 5%   
   -   5        $ 	comboBox1��5%      $��Right=�6&   $��26��6B   $��0,208.291,268.444,0q�6N   $	��Top=�6k   $��43��6�   $ �� IsSynchronizedWithCurrentItem$ FalseY�6�    J� IsHitTestVisible . ��.  � UseLayoutRounding .   �� MaxDropDownHeight$	 150��6   J� RenderTransformOrigin$ 0.852,0.681&�6#  �� 5&   
   -   5        $ 	comboBox2��5&      $��Right=�6&   $��26��6B   $��0,208.291,212.455,0q�6N   $	��Top=�6k   $��43��6�   $ FalseY�6�    . ��.  .  $	 150��6  �� 5'   
   -   5        $ 	comboBox3��5'      $��Right=�6&   $��26��6B   $��0,208.291,156.346,0q�6N   $	��Top=�6k   $��45��6�   $ FalseY�6�    . ��.  .  $	 150��6  �� 5(   
   -   5        $  label5��5(      $��:��6    $��Right=�6,   $��31.955��6H   $��0,203.83,243.456,0q�6X   $	��Top=�6t   $��25.109��6�   $
��Bold"�6�   $��Segoe UI Semibold)�6�   $��16(�6�   �� 5)   
   -   5        $  label6��5)      $��Schedule History��6    $
��Left=�6;   $��26.587��6V   $��11.816,94.53,0,0q�6f   $	��Top=�6�   $��118.163��6�   $��13.333(�6�   $��Segoe UI Light)�6�   �� 5*   
   -   5         ��Name$ image��5*      $��Right=�6   $
��33.5��6;   $��0,31.098,155.011,0q�6I   $	��Top=�6e   $��28.419��6}   $��schedule.png��6�   5,      5-      