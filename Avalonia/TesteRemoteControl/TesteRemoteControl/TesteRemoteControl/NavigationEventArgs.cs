using System;

namespace IpPortEventArgs
{
    public class NavigationEventArgs : EventArgs
    {
        public string IP { get; set; }
        public string Porta { get; set; }

        public NavigationEventArgs(string ip, string porta)
        {
            IP = ip;
            Porta = porta;
        }
    }

}
