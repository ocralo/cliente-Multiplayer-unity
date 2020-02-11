using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using socket.io;

public class conecSocketio : MonoBehaviour
{
    public GameObject text, player, player2;
    public Socket socket;
    public string json;
    public User user;
    public User user2;
    // Start is called before the first frame update
    void Start()
    {
        var serverUrl = "http://localhost:7001";
        socket = Socket.Connect(serverUrl);

        /* // receive "news" event
        socket.On("user2", (string data) =>
        {
            Debug.Log(data);
            text.GetComponent<Text>().text = data;
        }); */

    }

    void OnGUI()
    {
        Event e = Event.current;

        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.W.ToString())))
        {
            Debug.Log("Detected key code: " + e.keyCode);
            if (socket.IsConnected)
            {
                user.position = player.transform.position;
                json = JsonUtility.ToJson(user);
                socket.EmitJson("user" + user.id, json);
                Debug.Log(user2.id);
                socket.On("user" + user2.id, (string data) =>
                  {
                      Debug.Log("entre");
                      Debug.Log(data.Replace("\"", ""));
                      text.GetComponent<Text>().text = data;
                      user2 = JsonUtility.FromJson<User>(data.Replace("\"", "").Replace("'", "\""));
                      player2.transform.position = user2.position;
                  });
            }
            else
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        /* if (socket.IsConnected)
        {
            user.position = player.transform.position;
            json = JsonUtility.ToJson(user);
            socket.EmitJson("user" + user.id, json);
            Debug.Log(user2.id);
            socket.On("user" + user2.id, (string data) =>
              {
                  Debug.Log("entre");
                  Debug.Log(data.Replace("\"", ""));
                  text.GetComponent<Text>().text = data;
                  user2 = JsonUtility.FromJson<User>(data.Replace("\"", "").Replace("'", "\""));
                  player2.transform.position = user2.position;
              });
        }
        else
        {

        } */
    }

    [Serializable]
    public class User
    {
        public string playerName;
        public Vector3 position;
        public int life;
        public float id;
    }

    [Serializable]
    public class User2
    {
        public string playerName;
        public string position;
        public string life;
        public string id;
    }
}
