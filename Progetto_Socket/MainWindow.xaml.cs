﻿using System;
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

        Socket cipolla;
        DispatcherTimer dTimer;
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                cipolla = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress local = IPAddress.Any;
                IPEndPoint endpoint = new IPEndPoint(local.MapToIPv4(), 65000);

                lblRx.Content = "Ricezione messaggi attiva";

                cipolla.Bind(endpoint);


                dTimer = new DispatcherTimer();

                dTimer.Tick += new EventHandler(aggiornamento_dTimer);
                dTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
                dTimer.Start();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IPAddress remote = IPAddress.Parse(txtIP.Text);
                IPEndPoint rempote_endpoint = new IPEndPoint(remote, int.Parse(txtPorta.Text));
                byte[] mex = Encoding.UTF8.GetBytes(txtMex.Text);
                cipolla.SendTo(mex, rempote_endpoint);

                lstBox.Items.Add("TU" + ": " + txtMex.Text);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void aggiornamento_dTimer(object sender, EventArgs e)
        {

            int nBytes = 0;

            if((nBytes = cipolla.Available) > 0)
            {
                //ricezione dei caratteri in attesa
                byte[] buffer = new byte[nBytes];

                EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                nBytes = cipolla.ReceiveFrom(buffer, ref remoteEndPoint);
                string from = ((IPEndPoint)remoteEndPoint).Address.ToString();
                string messaggio = Encoding.UTF8.GetString(buffer, 0, nBytes);

                lstBox.Items.Add(from + ": " + messaggio);
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            lstBox.Items.Clear();
        }
    }
}