using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using socket.io;

public class user2Socketio : MonoBehaviour
{

    //public GameObject text;
    public Socket socket;
    public User user;

    // Start is called before the first frame update
    void Start()
    {
        string serverUrl = "http://localhost:7001";
        socket = Socket.Connect(serverUrl);
    }
    // Update is called once per frame
    void Update()
    {
        if (socket.IsConnected)
        {
            socket.On("user2", (string data) =>
            {
                //text.GetComponent<Text>().text = data;
                user = JsonUtility.FromJson<User>(data);
                this.transform.position = user.position;
                Debug.Log(user);
            });
        }
    }


    [Serializable]
    public class User
    {
        public string playerName;
        public Vector3 position;
        public int life;
    }
}
