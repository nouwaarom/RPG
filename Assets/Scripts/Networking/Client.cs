using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;


    public class Client : MonoBehaviour
    {
        static TcpClient _client;

        void Start()
        {
            Debug.Log("Welcome to the client program.");
            _client = new TcpClient();
            _client.Connect("127.0.0.1", 3460);
		}
	
		void update()
		{
            while (_client.Connected)
            {
            	recieve();
                send();
            }
        }
	
        static byte[] buffer = new byte[4096];

        static void send()
        {
            NetworkStream stream = _client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes("test");
            stream.Write(data, 0, data.Length);
        }


        static void recieve()
        {
            NetworkStream stream = _client.GetStream();
            int data = stream.Read(buffer, 0, 4096);
            string message = ("Server: " + Encoding.ASCII.GetString(buffer, 0, data));
			UnityEngine.Debug.Log(message);
        }
	
		void OnGUI()
		{
			
		}
    }



