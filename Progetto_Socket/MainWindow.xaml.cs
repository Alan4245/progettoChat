using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Progetto_Socket
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Socket socket; //creazione socket
        DispatcherTimer dTimer; //creazione dispatcher
        object semaforo;
        List<Contatto> contatti;
        Contatto contattoCorrente;
        public MainWindow()
        {
            try
            {
                InitializeComponent(); //inizializzazione componenti

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); //istanzio la socket
                IPAddress local = IPAddress.Any; //istanzio l'indirizzo locale
                IPEndPoint endpoint = new IPEndPoint(local.MapToIPv4(), 65000); //istanzio un endpoint locale con porta 65000
                socket.Bind(endpoint); //associo la socket ad un endpoint locale
                lblRx.Content = "Ricezione messaggi attiva";



                semaforo = new object();
                contatti = new List<Contatto>();

                dTimer = new DispatcherTimer(); //istanzio il dispatcher

                dTimer.Tick += new EventHandler(aggiornamento_dTimer); //setto i parametri del dispatcher
                dTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
                dTimer.Start(); //starto il dispatcher
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); //catturo eventuali errori e li visualizzo
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(lstContatti.SelectedIndex != -1)
                {
                    lock (semaforo)
                    {
                        lstBox.Items.Add("TU" + ": " + txtMex.Text); //MIGLIORIA: mi trascrivo il messaggio inviato nella listbox
                        contattoCorrente.AggiungiMessaggio("TU" + ": " + txtMex.Text);

                        byte[] mex = Encoding.UTF8.GetBytes(txtMex.Text); //codifico il messaggio
                        socket.SendTo(mex, contattoCorrente.EndPoint); //inoltro il messaggio al remote endpoint
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); //catturo eventuali errori e li visualizzo
            }
        }

        private void aggiornamento_dTimer(object sender, EventArgs e)
        {

            int nBytes = 0;

            if((nBytes = socket.Available) > 0) //verifico che io abbia ricevuto dei messaggi
            {
                //ricezione dei caratteri in attesa
                byte[] buffer = new byte[nBytes];

                EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                nBytes = socket.ReceiveFrom(buffer, ref remoteEndPoint);
                string from = ((IPEndPoint)remoteEndPoint).Address.ToString();
                string messaggio = Encoding.UTF8.GetString(buffer, 0, nBytes);

                foreach(Contatto c in contatti)
                {
                    if(c.EndPoint.ToString() == remoteEndPoint.ToString())
                    {
                        c.AggiungiMessaggio(c.Nominativo + ": " + messaggio);
                        AggiornaChat();
                    }
                }
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if(lstContatti.SelectedIndex != -1)
            {
                lock (semaforo)
                {
                    try
                    {
                        lstBox.Items.Clear();
                        string nominativo = lstContatti.SelectedItem.ToString();
                        foreach (Contatto c in contatti)
                        {
                            if (c.Nominativo == nominativo)
                            {
                                c.CancellaChat();
                            }
                        }
                    }catch(Exception ex)
                    {
                        MessageBox.Show("Si è verificato un errore: " + ex.Message);
                    }
                }
            }
        }

        private void btnCreaContatto_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                IPAddress remote = IPAddress.Parse(txtIP.Text);
                IPEndPoint rempote_endpoint = new IPEndPoint(remote, int.Parse(txtPorta.Text));
                string nominativo = txtNominativo.Text;
                Contatto nuovoContatto = new Contatto(rempote_endpoint, nominativo);
                if (contatti.Contains(nuovoContatto))
                {
                    throw new Exception("Contatto già presente!");
                }
                else
                {
                    contatti.Add(nuovoContatto);
                    lstContatti.Items.Add(nuovoContatto);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Si è verificato un errore: " + ex.Message);
            }

        }

        private void lstContatti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lstContatti.SelectedIndex != -1)
            {
                lock (semaforo)
                {
                    try
                    {
                        string nominativo = lstContatti.SelectedItem.ToString();
                        foreach (Contatto c in contatti)
                        {
                            if (c.Nominativo == nominativo)
                            {
                                contattoCorrente = c;
                            }
                        }

                        lstBox.Items.Clear();

                        if(contattoCorrente.Chat != null)
                        {
                            foreach (string messaggio in contattoCorrente.Chat)
                            {
                                lstBox.Items.Add(messaggio);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Si è verificato un errore: " + ex.Message);
                    }
                }
            }

        }

        private void btnBroadcast_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (lstContatti.SelectedIndex != -1)
                {
                    lock (semaforo)
                    {
                        byte[] mex = Encoding.UTF8.GetBytes(txtMex.Text); //codifico il messaggio
                        foreach (Contatto c in contatti)
                        {
                            c.AggiungiMessaggio("TU" + ": " + txtMex.Text);
                            
                            socket.SendTo(mex, c.EndPoint); //inoltro il messaggio al remote endpoint
                        }

                        AggiornaChat();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //catturo eventuali errori e li visualizzo
            }

        }

        public void AggiornaChat()
        {
            lstBox.Items.Clear();

            if (contattoCorrente != null && contattoCorrente.Chat != null)
            {
                foreach (string messaggio in contattoCorrente.Chat)
                {
                    lstBox.Items.Add(messaggio);
                }
            }
        }
    }
}
