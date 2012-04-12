using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
class DataReceive
{
    string text;
    public string ipaddress;
    
    public DataReceive(String port)
    {
        while (Static_Lock.lockCondition == true) ;
        Static_Lock.lockCondition = true;
        IPEndPoint ip = new IPEndPoint(IPAddress.Any, Convert.ToInt32(port));
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        Console.WriteLine("Binding--Data");
        socket.Bind(ip);
        Console.WriteLine("Bind successful");
        socket.Listen(10);
        //Console.WriteLine("Waiting for a client...//Data Receive called");
        

        Socket client = socket.Accept();
        
        
        IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
        ipaddress = clientep.Address.ToString();
        //Console.WriteLine("Connected with {0} at port {1}", clientep.Address, clientep.Port);
        byte[] data = new byte[100];
        int receivedDataLength = client.Receive(data);
        text= Encoding.ASCII.GetString(data, 0, receivedDataLength);
        try
        {

            client.Send(Encoding.ASCII.GetBytes("Received"));
           // Console.WriteLine("Acknowledgement sending");
        }
        catch (SocketException e)
        {
            Console.WriteLine("Problem with receiving");
        }
        
        //Console.WriteLine("Disconnected from {0}+1st", clientep.Address);
        client.Close();
        socket.Close();
        Console.WriteLine("Binding--Data--close");
        Static_Lock.lockCondition = false;
        //Console.WriteLine("Disconnected from {0}+2nd", clientep.Address);
    }
    public string getData()
    {
        return text;
    }
}