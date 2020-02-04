using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using socket.io;

public class conecSocketio : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        var serverUrl = "http://localhost:7001";
        var socket = Socket.Connect(serverUrl);

        // receive "news" event
        socket.On("news", (string data) =>
        {
            Debug.Log(data);
            text.GetComponent<Text>().text = data;
            /* 
            // Emit raw string data
            socket.Emit("my other event", "{ my: data }");
            */

            // Emit json-formatted string data
            socket.EmitJson("my other event", @"{ ""my"": ""data"" }");
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
