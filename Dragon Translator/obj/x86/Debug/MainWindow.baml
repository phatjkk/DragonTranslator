   M S B A M L   `   `   `  ����L  HDragon Translator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Dragon_TranslatorDragon_Translator  5      "    Dragon_Translator.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      1localclr-namespace:Dragon_Translator     5      U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � Title$  Dragon Translator V2.0��5	   	    � Icon$ icon_2Y9_icon.ico��6(   $��253.553��6A   $��525.888��6R    � 
ResizeMode$ NoResize=�6b    � WindowStartupLocation$ CenterScreen=�6x    � WindowStyle$
 None=�6�   .��5
      � ���#34495e.+�5   
   � $��50��6   $	��Top=�6   ���#2c3e50.+�5      �� $��Center=�5      $��Center=�5      $��
10,6,478,6q�6"   $��38��66   $��38��6B    �� StretchDirection$ DownOnly=�6M   $
��Fill=�6i    J� RenderTransformOrigin$ 
0.464,0.25&�6x   $��icon_2Y9_icon.ico��6�   5      �� 5      $��Dragon Transtlator v2.0��6   $��Center=�67   $��Center=�6T   $��20(�6o   $��Segoe UI Semilight)�6}   
�������6�   �� 5      -   5        $��×��5      $��Center=�6    $��55��6=   $��486,-11,0,6q�6I   $��Bottom=�6^   $��40��6y   
�������$��36(�6�   6�   �� 5      -   5        $��-��5      $��Center=�6    $��55��6=   $��446,-11,40,6q�6I   $��Bottom=�6_   $��40��6z   
�������$��36(�6�   6�   5      �� 5   
   $2��,Quick Translate (Double click for translate)��5      $��10,55,244,10q�5      .��5      � .+�5      �� -   5        $��	list_lang��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Choose language��5      $��72��5      $��	10,0,0,91q�5       �� SelectedValuePath$ Content��5       &  "MaterialDesignFloatingHintComboBox#��[  5      
�������$��16(�6R   $��46��6`   ��5      �� *#!LongListToTestComboVirtualization+.@�5       �� .��6#   	English6,   �� 5!      .��6#   
Vietnamese6/   �� 5"      .��6#   Chinese - Simplified69   �� 5#      .��6#   	Spanish6,   �� 5$      .��6#   French6+   �� 5%      .��6#   	Russian6,   �� 5&      .��6#   
Japanese6-   5'      �� 5(      -   5        $��_hotkey��5(      $ Set hotkey (optional)��5)      	   
HelperText$'	 !Set hotkey for translate document��5*      $��
10,51,0,42q�6^   
�������$��16(�6�    % !MaterialDesignFloatingHintTextBox#��[ 5+      $��Center=�5,      $��44��6   �� 5-      -   5        $��_runws��5-      $��Run when window start��6)   $��Center=�5.      $��10,112,83,7q�5/      $��Segoe UI Semibold)�6>   
�������6]   �� 50      -   5        $��	btn_start��50      $��Start��6*   $
��Left=�6:   $��28��6U   $��176,109,0,0q�6a   $	��Top=�6v   $��78��6�     MaterialDesignRaisedButton#���  6�   ���	#DDFFFFFF6�   51      52      �� 53   
   $��Scan Translate (OCR)��54      $��287,55,10,10q�55      .��56      � .+�57      )  #MaterialDesignThemes.Wpf.DialogHost    MaterialDesignDivider#���  6,   
   DialogContent
 58      � 59      $	��300��6   $	��150��6+   $��Center=�68   .+�5;      �� -   5        $��	_ocr_lang��5<   '   $��72��5=   %   $��112,10,15,104q�63   #��[  5?   %   ���	#FF2980B9$��12(�6x   $��36��6�   ��5>   %   �� *#!LongListToTestComboVirtualization+.@�5@   "   �� $��English��6/   �� 5A   "   $��
Vietnamese��6/   �� 5B   "   $��Chinese - Simplified��6/   �� 5C   "   $��Spanish��6/   �� 5D   "   $��French��6/   �� 5E   "   $��Russian��6/   �� 5F   "   $��Japanese��6/   5G      �� 5H      $��Scan language :��6$   $��28��6>   $��10,18,193,0q�6J   $	��Top=�6_   ���	#FF2980B9$
��Bold"�6�   �� 5J       �� Orientation$ 
Horizontal=�6)   $��Right=�6B   $��Bottom=�6^   $��15q�6y   .+�5K   "   �� -   5        $��_SaveSetting��5K   )      CloseDialogCommand#��Z 6=     MaterialDesignFlatButton#���  6   $��4q�6�   $��Center=�6�   .��6�   Save5M   #   �� 5N   "   -	   5        $��_CloseSetting��5N   )   #��Z 6>   #���  6�   $��4q�6�   $��Center=�6�   .��6�   Back5P   #   5Q      �� 5R      $��Scan Hotkey :��6$   $��28��6<   $��10,53,193,0q�6H   $	��Top=�6]   ���	#FF2980B9$
��Bold"�6�   �� 5S      -
   5        $��
ocr_hotkey��5S   &   $
��Left=�68   $��28��6S   $��
112,53,0,0q�6_    �� TextWrapping$
 Wrap=�6s   $��F4��6�   $	��Top=�6�   $	��173��6�   ���	#FF2980B9$
��Bold"�6�   5T      .��5V      � .+�5W      �� -   5         $  MaterialDesignRaisedAccentButton#��[ 5X       � ToolTip$. (Scan and translate text on window screen��5Y      $��10,10,10,95q�6M   .��6b   Scan5[      �� 5\      -   5         " MaterialDesignRaisedDarkButton#��[ 5]      $- 'Choose image file to scan and translate��5^      $��10,47,10,58q�67   .��6c   Select File...5`      �� 5a      -   5           OpenDialogCommand#��Z 5b       * &MaterialDesignFloatingActionMiniButton#��[ 5c      $
��Left=�5d      $ Settings��5e      $��
166,90,5,5q�6   
�������$
��Auto��6=   ���	#FF673AB7.��5f      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Settings=�5g      $��21��5h      $��21��5i      5j      �� 5k      -   5        #��[ 5l      $ Translator toolkit��5m      $��10,95,104,10q�6"   ���	#FFECF0F1���	#FF34495E.��6}   
Translator5p      �� 5q      -   5        #��[ 5r      $
��Left=�5s      $, &MaterialDesignFloatingActionMiniButton��5t      $��
121,90,0,5q�64   $
��Auto��6H   .��5u        $ Information=�5v      $��24��5w      $��24��5x      5y      5z      5{      5|      5}      5~      5      