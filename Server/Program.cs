using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string ManagerIpAddress="172.16.52.125";
            string ManagerPortAddress="10000";
            DataReceive dr = new DataReceive("9000");
            string ipaddress=dr.ipaddress;
            string data = dr.getData();
            Console.WriteLine(data);
            DataSend ds = new DataSend(ManagerIpAddress,ManagerPortAddress);
            ds.sendData(ipaddress);
            ds.sendData(data);
        }
    }
}
