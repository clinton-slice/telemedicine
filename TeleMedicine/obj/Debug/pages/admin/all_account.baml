   M S B A M L   `   `   `  ����G  CTeleMedicine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TeleMedicine.pages.adminTeleMedicine.pages.admin  5      *    $TeleMedicine.pages.admin.all_account   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:TeleMedicine.pages.admin     5      $	��685��5   7   $	��675��6C   .��5
      � 
�������.+�5   
   �� -   5          ��Name$  listView��5      $��16.5,87,16,53q�6%   ���	#FFF7F8F9��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value$ Stretch=�6C   5       �� View 5      �� 5      .P�5      �� $��Photo��6%    �� Width$ 95��65    �� CellTemplate 5      �� 5      .R�5   "   ��  �� CornerRadius$ 2,2,2,2��6)   $��50��6@   $��50��6K   ���	#FFEAE6E6$��3,3,3,3q�6�   ��6W   Z� ._�5   &   �� $��Center=�6,   $��Center=�6I   $
��Auto��6d   $
��Auto��6q   $��UniformToFill=�6�   $��2,2,2,2q�6�   ��6   �� *photo+6�   5   #   5      5"      �� 5#      $��Fullname��6P   $
 200 ��6b    �� DisplayMemberBinding 6&   �� *
fullname+6b   �� 5$      $��Username��6O   $	 105��6a    6%   �� *
username+6a   �� 5%      $��Account type��6O   $	 110��6e    6%   �� *
category+6e   �� 5'      $ 50��6%    5)      �� 5*      .R�5+   "   �� -   5        $��25��5+   \   $��25��6g   ��6?   �� ��6s   Z� ��5,   &   �� 5-   *   	 �� ImageSource$	 icons_password.png��65   5/   #   50      53      �� 56      $ 50��6%    58      �� 59      .R�5:   "   �� -   5        $��25��5:   ]   $��25��6h   ��6@   �� ��6t   Z� ��5;   &   �� 5<   *   $	 file_delete.png��65   5>   #   5?      5B      5F      5H      �� 5I   
   -   5        
 ��Name$
 label��5I      $��ALL ACCOUNTS��6   $��Right=�66   $
��33.5��6R   $��0,23.271,19,0q�6`   $	��Top=�6w   $	��144��6�   $��Segoe UI Light)�6�   $��18.667(�6�   $��ExtraCondensed&�6�   $
��Bold"�6�   
���   ���6�   Z� 6  �� 5J   
   -   5        $
 label1��5J      $��Filter with fullname��6    $
��Left=�6?   $
��29.5��6Z   $��109.5,0,0,11.5q�6h   $��Bottom=�6�   $	��138��6�   $��14.667(�6�   $��Segoe UI Light)�6�   �� 5K   
   -   5         ��Name$ filtercomboBox��5K      $��25��6+   $��271.5,0,175,11.5q�67   $��Bottom=�6Q   6l   �� 5L   
   -   5        $
 resultLabel��5L      $��Result: 0 found��6%   $
��Left=�6?   $��25.229��6Z   $��16.5,56.771,0,0q�6j   $	��Top=�6�   $��169.5��6�   �� 5M   
   -   5         ��Name$ image��5M      $��Right=�6   $
��33.5��6;   $��0,26.598,165.915,0q�6I   $	��Top=�6e   $��28.419��6}   $��members.png��6�   5O      5P      