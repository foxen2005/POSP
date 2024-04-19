using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;

public class Reader : MonoBehaviour
{


    ///
    public int numList, sizeList, sizeColum, numColum;
    
    public string[]  ID, Name_s, Role_s, Nick_s;
    public string[] ColumnHeaders;

    public static string[] ColHeaders;

    public string FileTitle;
    public string TextFile;

    //GUI
    public Text[] LineTitle;
    public Text fileTitle_tx, columTest1, columTest2, columTest3, columTest4, fila1, fila2, fila3, fila4;

    public string FilaData_tx;
    public string[] columnasData_tx;

    public int runText ,instaserNum = 0;

    public GameObject ColumnaMaster;
    GameObject[] InstaColumnas;
    public Transform ColumBlock;

  
    string filePath;

    void Start()
    {
       
        TextFile = File.ReadAllText(Application.streamingAssetsPath + "/JsonChallenge.json");
    

        StartCoroutine(Lector());
        InstaColumnas = new GameObject[sizeColum];
    }



    void FixedUpdate()
       {

        StartCoroutine(Lector());
       
        fileTitle_tx.text = FileTitle;

        if (instaserNum < sizeColum)
        {
            InstaColumnas[instaserNum] = (GameObject)Instantiate(ColumnaMaster, ColumBlock);
            InstaColumnas[instaserNum].name = "" + instaserNum;
            instaserNum++;
        }

        StopAllCoroutines();

    }

    public IEnumerator Lector()
    {
        
        var DATA = JSON.Parse(TextFile);

        FileTitle = DATA["Title"].Value;
        sizeColum = DATA["ColumnHeaders"].Count;
        sizeList = DATA["Data"].Count;

        yield return null;
    }



   
   
}


