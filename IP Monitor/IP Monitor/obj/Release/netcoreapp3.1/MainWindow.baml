   M S B A M L   `   `   `  ���� E  AIP Monitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:IP_Monitor
IP_Monitor      IP_Monitor.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:IP_Monitor       � Title$  
IP Monitor��$	��450��$	��800�� � Icon$ /IP.ico��.��� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFD4D4D4�� � Offset$ 1Q�� $ White��$ 0Q� � RowDefinitions �� $��85.008 ���  � ColumnDefinitions �� $
��501* ��� $
��300* �.+��� $��1��$
��Left=�$��	10,17,0,0q�$��Select your CSV��$
��Wrap=�$	��Top=�$��16(���}� �� -    ��Name$
 Path��$��1��$��
10,43,55,0q�	 �� TextWrapping$
	 Wrap=�$	��Top=�$��Center=�$��25��
���   ��� -   
 ��Name$
 OpenFIle��$	��...��$��1��$��Right=�$��	0,43,10,0q�$	��Top=�$��25��$��40�����	#00707070���	#B200B9FF
���   � � 	Resources  �� 
TargetType �� Property �� Value��%9   '��       �� " ��.���  �@��CornerRadius$ 8���� -   $
 Start��$��Start��$��1��$��
75,0,75,10q����	#00707070���	#B200FF00
���   �$��30��$��Bottom=�$��1�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 10���� $��1��$
��Left=�$��	10,10,0,0q�$	��Log��$
��Wrap=�$	��Top=�$��16(�$��1����}� �� -    ��Name$ logBox��$��10,36,10,55q�$��1��$��1��
���   �. �/� .T�J� .*�.*��� $� ���� $
��Auto=�$��10,43,10,10q�$��2��.���� -    ��Name$ Machines���� $
��Left=�$��	10,17,0,0q�$��Machines��$
��Wrap=�$	��Top=�$��16(���}� 