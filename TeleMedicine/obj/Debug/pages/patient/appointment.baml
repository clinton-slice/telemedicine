   M S B A M L   `   `   `  ����G  CTeleMedicine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TeleMedicine.pages.patientTeleMedicine.pages.patient  5      ,    &TeleMedicine.pages.patient.appointment   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:TeleMedicine.pages.patient     5      $	��900��5   7   $	��525��6C   
�������.��5
      � .+�5   
   �� -   5          ��Name$  label��5      $��APPOINTMENTS��6   $��Right=�66   $
��33.5��6R   $��0,27.51,31.902,0q�6`   $	��Top=�6z   $��146.266��6�   $��Segoe UI Light)�6�   $��18.667(�6�   $��ExtraCondensed&�6�   $
��Bold"�6�   
���   ���6�   Z� 6  &   System.Windows.Controls.Calendar  5   
   -   5          Name$ calendar��5      $��0,124.07,0,0q�6%   $
��Left=�6;   $��175.766��6V   $	��Top=�6g   $��211.215��6      SelectionMode$ MultipleRange=�6�      DisplayMode$
 Year=�6�   �� 5   
   $��200.875��6   $��242.232,88.622,31.902,0q�6%   ���	#FFD8CFCF$	��Top=�6Y    �� RadiusY$ 15.747��6q    �� RadiusX$ 15.747��6�   �� 5   
   -   5         ��Name$ 	listView0��5      $"��11.816,313.698,19.408,22.334q�6&    �� View 5      �� 5      .P�5      �� $��Doctor��6R    �� Width$	 200��6b   	 �� DisplayMemberBinding	 6%   �� *doctor_name+6b   �� 5      $��Doctor's Specializations��6U   $	 200��6w   	 6%   �� *specialization+6w   �� 5      $��Attended��6O   $ 80��6a   	 6%   �� *
attended+6a   �� 5      $��Schedule Details��6O   $	 180��6i   	 6%   �� *
schedule+6i   �� 5      $��Date Created��6N   $	 180��6d   	 6%   �� *	created+6d   5      5      �� 5   
   -   5        $  label1��5      $��Today's Schedule��6    $��26.587��6;   $��265.864,92.262,321.652,0q�6K   $	��Top=�6m   $��Segoe UI Semibold)�6�   $��13.333(�6�   �� 5   
   -   5        $  label5��5      $��:��6    $��Right=�6,   $��31.955��6H   $��0,203.83,243.456,0q�6X   $	��Top=�6t   $��25.109��6�   $
��Bold"�6�   $��Segoe UI Semibold)�6�   $��16(�6�   �� 5   
   -   5        $  label6��5      $��Schedule History��6    $
��Left=�6;   $��26.587��6V   $��11.816,94.53,0,0q�6f   $	��Top=�6�   $��118.163��6�   $��13.333(�6�   $��Segoe UI Light)�6�   �� 5   
   -   5        
 ��Name$

 home��5      $
��Left=�6   $��32.728��69   $��17.874,28.954,0,0q�6I   $	��Top=�6d   $��34.609��6|    � Cursor$
 Hand��6�   $��back.png��6�   �� 5   
   -   5        $  
label_Copy��5      $��Back to dashboard��6$   $
��Left=�6@   $
��33.5��6[   $��48.109,26.724,0,0q�6i   $	��Top=�6�   $��158.357��6�   $��Segoe UI Light)�6�   $��18.667(�6�   $��ExtraCondensed&�6�   $
��Bold"�6�   
���   �$
 Hand��6a  ��6	  Z� 6a  �� 5   
   -	   5        $ 	listView1��5      $$��265.864,122.715,56.449,250.266q�6&   ���	#FFDDE0E4 5       �� 5!      .P�5"      �� $��Doctor��6R   $	 220��6b   	 6%   �� *doctor_name+6b   �� 5#      $��Attended��6O   $	 110��6a   	 6%   �� *
attended+6a   �� 5$      $��Schedule Details��6O   $	 210��6i   	 6%   �� *
schedule+6i   5%      5(      5*      5+      