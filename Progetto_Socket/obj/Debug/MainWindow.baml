   M S B A M L   `   `   `  ����J  FProgetto_Socket, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Progetto_SocketProgetto_Socket  5   	        Progetto_Socket.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Progetto_Socket     5   	     � Title$  
MainWindow��5   	   $	��450��6   $
��1109��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ lstBox��5
      $
��Left=�6"   $	��315��6=   $��	40,16,0,0q�6J   $	��Top=�6]   $	��275��6u   $��Disabled=�6�   �� 5   
   $��Inserire indirizzo IP��6   $
��Left=�60   $��895,109,0,0q�6K   $	��Top=�6`   $	��185��6x   �� 5   
   $��Inserire messaggio��6   $
��Left=�6-   $��350,101,0,0q�6H   $	��Top=�6]   $	��185��6u   �� 5   
   $��Inserire porta��6   $
��Left=�6)   $��895,176,0,0q�6D   $	��Top=�6Y   $	��185��6q   �� 5   
   -   5         ��Name$ txtIP��5      $
��Left=�6!   $��23��6<   $��895,140,0,0q�6H    �� TextWrapping$
 Wrap=�6]   $�� ��6q   $	��Top=�6y   $	��185��6�   �� 5   
   -   5        $ txtPorta��5      $
��Left=�6$   $��23��6?   $��895,207,0,0q�6K   $
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��185��6�   �� 5   
   -   5        $ txtMex��5      $
��Left=�6"   $��64��6=   $��350,132,0,0q�6I   $
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��185��6�   �� 5   
   -   5         ��Name$ btnSend��5      $��INVIA��6"   $
��Left=�62   $��350,214,0,0q�6M   $	��Top=�6b   $	��185��6z   6�   �� 5   
   -   5         ��Name$ lblRx��5      $�� ��6   $
��Left=�6*   $��
320,10,0,0q�6E   $	��Top=�6Y   $	��185��6q   �� 5   
   -   5        $ btnClear��5      $��CANCELLA CHAT��6#   $
��Left=�6;   $��
80,345,0,0q�6V   $	��Top=�6j   $	��185��6�   6�   �� 5   
   -   5        $ btnBroadcast��5      $��INVIA A TUTTI I CONTATTI��6'   $
��Left=�6J   $��350,245,0,0q�6e   $	��Top=�6z   $	��185��6�   6�   �� 5   
   $��Inserire nominativo��6   $
��Left=�6.   $��
895,48,0,0q�6I   $	��Top=�6]   $	��185��6u   �� 5   
   $��      CREA UN CONTATTO��6   $
��Left=�61   $��
895,17,0,0q�6L   $	��Top=�6`   $	��185��6x   �� 5   
   -	   5        $ txtNominativo��5      $
��Left=�6)   $��23��6D   $��
895,79,0,0q�6P   $
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $	��185��6�   �� 5   
   $��       INVIA UN MESSAGGIO��6   $
��Left=�64   $��
350,70,0,0q�6O   $	��Top=�6c   $	��185��6{   �� 5   
   -
   5        $ btnCreaContatto��5      $��CREA CONTATTO��6*   $
��Left=�6B   $��895,245,0,0q�6]   $	��Top=�6r   $	��185��6�   6�   �� 5   
   $!��      SELEZIONA IL CONTATTO��6   $
��Left=�66   $��
640,17,0,0q�6Q   $	��Top=�6e   $	��185��6}   �� 5   
   -   5        $ lstContatti��5      $
��Left=�6'   $	��210��6B   $��
640,70,0,0q�6O   $	��Top=�6c   $	��185��6{   6�   5      5      