using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    Button buttonHost;
    Button buttonJoin;
    InputField textHost;

	// Use this for initialization
	void Start ()
	{
	    buttonHost = transform.Find("Panel/ButtonHost").GetComponent<Button>();
        buttonJoin = transform.Find("Panel/ButtonJoin").GetComponent<Button>();
	    textHost = transform.Find("Panel/TextHost").GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonHostClick()
    {
        var host = textHost.text;
        int port = 7777;
        if (host.Contains(":"))
        {
            port = System.Convert.ToInt32(host.Split(':')[1]);
            host = host.Split(':')[0];
        }
        /*NetworkSystem.Current.Host = host;
        NetworkSystem.Current.Port = port;
        NetworkSystem.Current.SetupHost((msg) =>
        {

        });*/
        GameSystem.Current.HostGame(port);
        gameObject.SetActive(false);
        MainGUI.Current.GameGUI.SetActive(true);
    }

    public void ButtonJoinClick()
    {
        var host = textHost.text;
        int port = 7777;
        if (host.Contains(":"))
        {
            port = System.Convert.ToInt32(host.Split(':')[1]);
            host = host.Split(':')[0];
        }
        /*NetworkSystem.Current.Host = host;
        NetworkSystem.Current.Port = port;
        NetworkSystem.Current.SetupHost((msg) =>
        {

        });*/
        GameSystem.Current.JoinGame(host, port);
    }
}
