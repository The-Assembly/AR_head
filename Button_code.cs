using System;
using Vuforia;
using System.IO;
using System.Net;
using UnityEngine;
using System.Threading;
using System.Collections;
using System.Net.Sockets;
using System.Collections.Generic;

/* 
Please make sure you follow the comments that are labelled as IMPORTANT, to minimize 
compilation errors */

public class ButtonControl : MonoBehaviour
{      //IMPORTANT: change "UIRasp" to the name of your Script
    bool socketReady = false;              // global variables are setup here
    TcpClient mySocket;
    public NetworkStream theStream;
    StreamWriter theWriter;
    public String Host = "10.4.36.166";     //IMPORTANT: change the IP-Address to fit your
                                            //Raspberry Pi's IP-Address
    public Int32 Port = 50001;

    void Start()
    {
        setupSocket();
    }

    public void UpBtn()
    {
        Debug.Log("Toggle Servo Up Status");
        writeSocket("Up");

    }

    public void DownBtn()
    {
        Debug.Log("Toggle Servo Down Status");
        writeSocket("Down");
    }

    public void RightBtn()
    {
        Debug.Log("Toggle Servo Right Status");
        writeSocket("Right");
    }

    public void LeftBtn()
    {
        Debug.Log("Toggle Servo Left Status");
        writeSocket("Left");
    }

    public void setupSocket()
    {                            // Socket setup here
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error:" + e);                // catch any exceptions
        }
    }

    public void writeSocket(string theLine)
    {            // function to write data out
        if (!socketReady)
            return;
        String tmpString = theLine;
        theWriter.Write(tmpString);
        theWriter.Flush();
    }

    public void closeSocket()
    {                            // function to close the socket
        if (!socketReady)
            return;
        writeSocket("close");
        theWriter.Close();
        mySocket.Close();
        socketReady = false;
    }

    public void maintainConnection()
    {                    // function to maintain the connection 
        if (!theStream.CanRead)
        {
            setupSocket();
        }
    }
} // END OF CODE
