using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public class Action : MonoBehaviour
{

    public Text theList;
    public Button AddButton;
    public InputField inputText;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Load());
        StartCoroutine(Save());
        Button rbutton = AddButton.GetComponent<Button>();
        rbutton.onClick.AddListener(TaskOnClickAdd);
        Load();
    }

    void TaskOnClickAdd()
    {
        Save();
        Load();
    }

    IEnumerator Save()
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
        UnityWebRequest www = UnityWebRequest.Put("https://domain.se/filer/test2/test.txt", myData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }

    }

    IEnumerator Load()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://domain.se/filer/test2/test.txt");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            theList.text = www.downloadHandler.text;
        }
    }

}