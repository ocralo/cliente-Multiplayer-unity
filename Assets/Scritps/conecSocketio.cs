using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using socket.io;

public class conecSocketio : MonoBehaviour
{
    public GameObject text, player;
    public Socket socket;
    public string json;
    public User user;
    // Start is called before the first frame update
    void Start()
    {
        var serverUrl = "http://localhost:7001";
        socket = Socket.Connect(serverUrl);

        // receive "news" event
        socket.On("user2", (string data) =>
        {
            Debug.Log(data);
            text.GetComponent<Text>().text = data;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (socket.IsConnected)
        {
            user.position = player.transform.position;
            json = JsonUtility.ToJson(user);
            socket.EmitJson("user"+user.id, json);

            socket.On("user1", (string data) =>
            {
                Debug.Log(data);
                text.GetComponent<Text>().text = data;
            });
        }
        else
        {

        }
    }

    [Serializable]
    public class User
    {
        public string playerName;
        public Vector3 position;
        public int life;
        public int id;
    }

}
