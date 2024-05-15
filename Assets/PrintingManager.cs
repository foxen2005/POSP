using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sfs2X.Entities.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;

public class PrintingManager : MonoBehaviour
{
    string path = null;
    
    void Start()
    {

        //arreglar que el archivo al abrirse no se muestra, esta direccion no se consigue
         //path = Application.dataPath + "/Resources/Ticket.pdf";  

         // path = Application.persistentDataPath + "/Resources/Ticket.pdf";
    }

    public void GenerateFile(string textRecibe, string numFile) {
        //File.Delete(Application.persistentDataPath + " bolet.pdf");


     /*   if (File.Exists(Application.dataPath + " bolet.pdf"))
            File.Delete(Application.dataPath + " bolet.pdf");*/
        //using (var fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
        using (var fileStream = new FileStream(Application.persistentDataPath + " bolet " + numFile + ".pdf", FileMode.CreateNew , FileAccess.Write ))

        {
            var document = new Document(PageSize.A7, 10f, 10f, 10f, 0f);
            var writer = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            document.NewPage();

            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
          
            //Paragraph p = new Paragraph(string.Format("Ticket Id : {0}",12345 )); //iSFSObject.GetUtfString("TICKET_ID"
            Paragraph p = new Paragraph(string.Format("BOLETA ELECTRONICA: "+numFile+"\n\n")); //iSFSObject.GetUtfString("TICKET_ID"
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);

            //p = new Paragraph(string.Format("Bet Number : {0}     BetAmount : {1}", 1, 100));
            p = new Paragraph(string.Format(textRecibe));
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);

            


            document.Close();
            writer.Close();

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = Application.persistentDataPath + " bolet " + numFile + ".pdf";

           // process.StartInfo.Verb = "print";

            process.Start();
        }

        /* StreamWriter writeri = new StreamWriter("/idbfs/Ticket.pdf", false);
         writeri.WriteLine(string.Format(textRecibe));
         writeri.Close();*/


        //StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine(string.Format("Ticket Id : {0}",iSFSObject.GetUtfString("TICKET_ID")));
        //var betting = iSFSObject.GetSFSArray("BET_DETAILS");
        //for (int i = 0; i< betting.Count;i++)
        //    writer.WriteLine(string.Format("Bet Number : {0}     BetAmount : {1}", betting.GetSFSObject(i).GetUtfString("BET_NUM"), betting.GetSFSObject(i).GetDouble("BET_AMOUNT")));
        //writer.Close();

        // PrintFiles();
    }

  /* public void PrintFiles()
    {
        Debug.Log(path);
        if (path == null)
            return;

        if (File.Exists(path))
        {
            Debug.Log("file found");
            //var startInfo = new System.Diagnostics.ProcessStartInfo(path);
            //int i = 0;
            //foreach (string verb in startInfo.Verbs)
            //{
            //    // Display the possible verbs.
            //    Debug.Log(string.Format("  {0}. {1}", i.ToString(), verb));
            //    i++;
            //}
        }
        else
        {
            Debug.Log("file not found");
            return;
        }

        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        process.StartInfo.UseShellExecute = true;
        process.StartInfo.FileName = path;

      

       // process.StartInfo.Verb = "print";

        
        process.Start();
        //process.WaitForExit();

    }*/
}
