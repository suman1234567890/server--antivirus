using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

class DataSend
{
    string ipaddress = null;
    string port = null;
    public void sendData(string text)
    {
        while (Static_Lock.lockCondition == true) ;
        Static_Lock.lockCondition = true;
        IPAddress host = IPAddress.Parse(ipaddress);
        IPEndPoint hostep = new IPEndPoint(host, Convert.ToInt32(port));
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        try
        {
            sock.Connect(hostep);
        }
        catch (SocketException e)
        {
            Console.WriteLine("Problem connecting to host");
            Console.WriteLine(e.ToString());
            sock.Close();
            return;
        }

        try
        {
            sock.Send(Encoding.ASCII.GetBytes(text));
        }
        catch (SocketException e)
        {
            Console.WriteLine("Problem sending data");
            Console.WriteLine(e.ToString());
            sock.Close();
            return;
        }
        try
        {
            byte[] s = new byte[10];

            sock.Receive(s);
            Console.WriteLine(Encoding.ASCII.GetString(s));
        }
        catch(SocketException e)
        {
            Console.WriteLine("Problem with receiving");
        }
        sock.Close();
        Static_Lock.lockCondition = false;
    }
    public DataSend(string ipaddress,string port)
    {
        this.ipaddress = ipaddress;
        this.port = port;
    }
    public void Acknw()
    {
        IPAddress host = IPAddress.Parse(ipaddress);
        IPEndPoint hostep = new IPEndPoint(host, Convert.ToInt32(port));
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        while (Static_Lock.lockCondition == true) ;
        Static_Lock.lockCondition = true;
        try
        {
            sock.Connect(hostep);
        }
        catch (SocketException e)
        {
            Console.WriteLine("Problem connecting to host");
            Console.WriteLine(e.ToString());
            sock.Close();
            return;
        }

        try
        {
            sock.Send(Encoding.ASCII.GetBytes("Data Received"));
        }
        catch (SocketException e)
        {
            Console.WriteLine("Problem sending data");
            Console.WriteLine(e.ToString());
            sock.Close();
            return;
        }
        try
        {
            byte[] s = new byte[10];

            sock.Receive(s);
            Console.WriteLine(Encoding.ASCII.GetString(s));
        }
        catch (SocketException e)
        {
            Console.WriteLine("Problem with receiving");
        }
        sock.Close();
        Static_Lock.lockCondition = false;
    }
}

