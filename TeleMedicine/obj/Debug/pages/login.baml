   M S B A M L   `   `   `  ����G  CTeleMedicine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:TeleMedicine.pagesTeleMedicine.pages  5          TeleMedicine.pages.login   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:TeleMedicine.pages     5      $	��668��5      $	��884��6,   
���   �.��5      � $"��-11.826,-4.445,-4.434,-1.478q�6   ��5	   
   �� 5
        �� ImageSource$  bg.jpg��6    j� Stretch$ UniformToFill=�6.   .+�5   
   �� $$��311.84,133.022,311.811,175.859q�6   ���	#7FD6C9C9 �� StrokeDashCap$ Round=�6O   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         RenderingBias$ Quality=�6#      Color$ 	#FFF0EBEB��6;      
BlurRadius$ 1Q�6M      	Direction$	 300Q�6\      ShadowDepth$ 3Q�6l   5      �� 5   
   -   5        	 ��Name$	 image��5      $��61.461��6   $��406.982,169.67,415.016,0q�6/   $	��Top=�6Q   $��13.png��6i   �� 5   
   -   5        
 ��Name$
 userbox��5      $%��350.024,321.234,350.236,322.689q�6#    �� TextWrapping$
 Wrap=�6L   $	��200��6`   $��30��6l   $��14.667(�6x    �� CharacterCasing$ Lower=�6�   6�   G� 5   
   -   5         G�Name$ passbox��5      $��30��6'   $��350.024,0,350.236,260.098q�63   $��Bottom=�6V   $	��200��6q   $��Segoe UI Light)�6}   $��14.667(�6�   6�   �� 5   
   -   5         ��Name$ loginbutton��5      $��login��6&   $��27��66   $��392.692,0,386.38,218.264q�6B   $��Bottom=�6d   ���	#7FE9F0EF���	#FF8AB0C1���	#FF0C6615$��13.333(�6�   6�   �� 5   
   -   5        $	 image1��5      $
��Left=�6    $��355.024,0,0,81.402q�6;   $��30��6W   $��FAQ.png��6b    J� RenderTransformOrigin$ -0.086,0.571&�6s   $��30��6�   $��Bottom=�6�    � Cursor$
 Hand��6�   6�   �� 5   
   -   5        $	 image2��5      $
��Left=�6    $��354.502,0,0,123.578q�6;   $��30��6X   $��drug.png��6c   $��30��6u   $��Bottom=�6�   $
 Hand��6�   �� 5   
   -   5         ��Name$ label��5      $��Pharmacy��6   $��388.89,0,391.236,115.056q�62   $��Segoe UI Semibold)�6T   $��33��6s   $��Bottom=�6   $
 Hand��6�   �� 5   
   -   5        $ label1��5      $ ��Frequently Asked Questions��6    $��388.024,0,348.236,74.402q�6E   $��Segoe UI Semibold)�6g   $��33��6�   $��Bottom=�6�   $
 Hand��6�   6�   �� 5   
   -	   5        $ label2��5      $��Username��6    $
��Left=�63   $��25.077��6N   $��353.024,291.157,0,0q�6^   $	��Top=�6{   $��81.482��6�   �� 5   
   -
   5        $ label2_Copy��5      $��Password��6%   $
��Left=�68   $��25.077��6S   $��353.024,0,0,289.186q�6c   $��Bottom=�6�   $��81.482��6�   �� 5   
   -   5        $ label3��5      $
��Tele��6    $��33.991��6/   $��382.628,233.521,0,0q�6?   $	��Top=�6\   $��Britannic Bold)�6t   $��21.333(�6�   $
��Left=�6�   $��51.614��6�   �� 5   
   -   5        $ label4��5      $��Medicine��6    $��31.947��63   $��422.982,233.609,379.362,0q�6C   $	��Top=�6f   ���	#FFAE0000$��21.333(�6�   $��Britannic Bold)�6�   �� 5   
   -   5        $ 	failedLab��5      $��Incorrect login details��6#   $��25.786��6E   $��393.024,0,348.236,183.478q�6U   $��Bottom=�6x   ���	#FFD83C3C$��Segoe UI Light)�6�   $y�Hidden=�6�   5      5      