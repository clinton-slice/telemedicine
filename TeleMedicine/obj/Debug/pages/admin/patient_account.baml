   M S B A M L   `   `   `  ����G  CTeleMedicine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TeleMedicine.pages.adminTeleMedicine.pages.admin  5      .    (TeleMedicine.pages.admin.patient_account   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:TeleMedicine.pages.admin     5      $	��685��5   7   $	��675��6C   .��5	      � 
�������.+�5
   
   �� -   5          ��Name$  listView��5
      $��16.5,87,16,53q�6%   ���	#FFF7F8F9��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value$ Stretch=�6C   5       �� View 5      �� 5      .P�5      �� $��Photo��6%    �� Width$ 95��65    �� CellTemplate 5      �� 5      .R�5   "   ��  �� CornerRadius$ 2,2,2,2��6)   $��50��6@   $��50��6K   ���	#FFEAE6E6$��3,3,3,3q�6�   ��6W   Z� ._�5   &   �� $��Center=�6,   $��Center=�6I   $
��Auto��6d   $
��Auto��6q   $��UniformToFill=�6�   $��2,2,2,2q�6�   ��6   �� *photo+6�   5   #   5      5       �� 5!      $��Fullname��6P   $	 300��6b    �� DisplayMemberBinding 6&   �� *
fullname+6b   �� 5"      $��Username��6O   $	 125��6a    6%   �� *
username+6a   �� 5$      $ 50��6%    5&      �� 5'      .R�5(   "   �� -   5        $��25��5(   \   $��25��6g   ��6?   �� ��6s   Z� ��5)   &   �� 5*   *   	 �� ImageSource$	 icons_password.png��65   5,   #   5-      50      �� 53      $ 50��6%    55      �� 56      .R�57   "   �� -   5        $��25��57   ]   $��25��6h   ��6@   �� ��6t   Z� ��58   &   �� 59   *   $	 file_delete.png��65   5;   #   5<      5@      5C      5E      �� 5F   
   -   5        
 ��Name$
 label��5F      $��PATIENT ACCOUNTS��6   $��Right=�6:   $
��33.5��6V   $��0,23.628,16.014,0q�6d   $	��Top=�6   $��187.279��6�   $��Segoe UI Light)�6�   $��18.667(�6�   $��ExtraCondensed&�6�   $
��Bold"�6�   
���   ���6  Z� 6  �� 5G   
   -   5         ��Name$ filtercomboBox��5G      $��25��6+   $��271.5,0,175,11.5q�67   $��Bottom=�6Q   6l   �� 5H   
   -   5        $
 label1��5H      $��Filter with fullname��6    $
��Left=�6?   $
��29.5��6Z   $��109.5,0,0,11.5q�6h   $��Bottom=�6�   $	��138��6�   $��14.667(�6�   $��Segoe UI Light)�6�   �� 5I   
   -   5        $
 resultLabel��5I      $��Result: 0 found��6%   $
��Left=�6?   $��25.229��6Z   $��16.5,56.771,0,0q�6j   $	��Top=�6�   $��169.5��6�   �� 5J   
   -   5         ��Name$ image��5J      $��Right=�6   $
��33.5��6;   $��0,26.667,203.752,0q�6I   $	��Top=�6e   $��28.419��6}   $��patient.png��6�   5L      5M      