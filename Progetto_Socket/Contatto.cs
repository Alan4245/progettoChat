using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Progetto_Socket
{
    class Contatto
    {

        private IPEndPoint _endPoint;
        private string _nominativo;
        private List<string> _chat;

        public Contatto(IPEndPoint endPoint, string nominativo)
        {

            try
            {
                EndPoint = endPoint;
                Nominativo = nominativo;
                _chat = new List<string>();
            }catch(Exception ex)
            {
                throw new Exception("Impossibile creare il contatto: " + ex.Message);
            }

        }

        public IPEndPoint EndPoint
        {

            get
            {
                return _endPoint;
            }

            set
            {
                if (value != null)
                {
                    _endPoint = value;
                }
                else
                {
                    throw new Exception("Endpoint non valido!");
                }
            }

        }

        public string Nominativo
        {
            get
            {
                return _nominativo;
            }

            set
            {
                if(string.IsNullOrWhiteSpace(value) == false)
                {
                    _nominativo = value;
                }
                else
                {
                    throw new Exception("Nominativo non valido!");
                }
            }
        }

        public List<string> Chat
        {
            get
            {
                return _chat;
            }

            set
            {
                if(value != null)
                {
                    _chat = value;
                }
                else
                {
                    throw new Exception("Chat nulla!");
                }
            }
        }

        public void AggiungiMessaggio(string messaggio)
        {

            _chat.Add(messaggio);

        }

        public override string ToString()
        {
            return Nominativo;
        }

        public void CancellaChat()
        {
            List<string> nuovaChat = new List<string>();
            _chat = nuovaChat;
        }
    }
}
