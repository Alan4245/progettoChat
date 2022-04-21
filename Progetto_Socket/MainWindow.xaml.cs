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
        public MainWindow()
        {
            try
            {
                InitializeComponent(); //inizializzazione componenti

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); //istanzio la socket

                IPAddress local = IPAddress.Any; //istanzio l'indirizzo locale
                IPEndPoint endpoint = new IPEndPoint(local.MapToIPv4(), 65000); //istanzio un endpoint locale con porta 65000

                lblRx.Content = "Ricezione messaggi attiva";

                socket.Bind(endpoint); //associo la socket ad un endpoint locale

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
                IPAddress remote = IPAddress.Parse(txtIP.Text); //ottengo l'ip remoto
                IPEndPoint rempote_endpoint = new IPEndPoint(remote, int.Parse(txtPorta.Text)); //ottengo il remote endpoint
                byte[] mex = Encoding.UTF8.GetBytes(txtMex.Text); //codifico il messaggio
                socket.SendTo(mex, rempote_endpoint); //inoltro il messaggio al remote endpoint

                lstBox.Items.Add("TU" + ": " + txtMex.Text); //MIGLIORIA: mi trascrivo il messaggio inviato nella listbox

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

                lstBox.Items.Add(from + ": " + messaggio);
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            lstBox.Items.Clear();
        }

        private void btnCreaContatto_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                IPAddress remote = IPAddress.Parse(txtIP.Text);
                IPEndPoint rempote_endpoint = new IPEndPoint(remote, int.Parse(txtPorta.Text));
                string nominativo = txtNominativo.Text;
                Contatto nuovoContatto = new Contatto(rempote_endpoint, nominativo);
                lstContatti.Items.Add(nuovoContatto);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Si è verificato un errore: " + ex.Message);
            }

        }
    }
}
