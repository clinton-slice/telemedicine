   M S B A M L   `   `   `  ����G  CTeleMedicine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:TeleMedicine.pages.doctorTeleMedicine.pages.doctor  5   	   ,    &TeleMedicine.pages.doctor.searchWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:TeleMedicine.pages.doctor     5   	   $	��400��5      $	��500��6'     � WindowStartupLocation$  CenterScreen=�63    � Topmost . $	��500��6g   $	��400��6v    � 
ResizeMode$ NoResize=�6�    � Icon$ /;component/telemed.ico��6�    � WindowStyle$ 
ToolWindow=�6�   .��5      � .+�5	   
   �� -   5         ��Name$ label��5	      $��Searching database��6   $��28��6<   $��184,4,193,0q�6H   $	��Top=�6]   �� 5
   
   -   5         ��Name$ listView��5
      $��
10,35,10,7q�6%    �� View 5      �� 5      .P�5      �� $��Fullname��6O    �� Width$	 200��6a   	 �� DisplayMemberBinding	 6%   �� *
fullname+6a   �� 5      $��Match��6L   $	 120��6[   	 6%   �� *match+6[   �� 5      $��Content��6N   $	 130��6_   	 6%   �� *	content+6_   5      ��5      �� 5      
 �� 
TargetType"
 ��6   .�5      -   5        5      5      5      5      