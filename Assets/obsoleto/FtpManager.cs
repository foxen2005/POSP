using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;
using System.Net;
using System;

public class FtpManager : MonoBehaviour
{
    private string ftpHost = "ftp://a1f51.us-west-1.sftpcloud.io:22";
    private string ftpUsuario = "unity001";
    private string ftpPassword = "wt8JtGvBjReekK8Ad4DrsUJAFBG1HFIz";


     string rutaLocalArchivo = "D:/files/test.txt";
     string rutaDestino = "/unity001";


    // Subir archivo al servidor FTP usando UnityWebRequest
    public IEnumerator SubirArchivo(string rutaLocalArchivo, string rutaDestino)
    {
        byte[] fileContents = File.ReadAllBytes(rutaLocalArchivo);
        UnityWebRequest request = UnityWebRequest.Put(ftpHost + "/" + rutaDestino, fileContents);
        request.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(ftpUsuario + ":" + ftpPassword)));
        request.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Subida completa");
        }
    }

    // Bajar archivo del servidor FTP usando UnityWebRequest
    public IEnumerator BajarArchivo(string rutaServidorArchivo, string rutaLocalDestino)
    {
        UnityWebRequest request = UnityWebRequest.Get(ftpHost + "/" + rutaServidorArchivo);
        request.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(ftpUsuario + ":" + ftpPassword)));
        request.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            File.WriteAllBytes(rutaLocalDestino, request.downloadHandler.data);
            Debug.Log("Descarga completa");
        }
    }

    private class AcceptAllCertificatesSignedWithASpecificKeyPublicKey : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            // Aquí deberías implementar la lógica de validación del certificado.
            // Para propósitos de prueba, se aceptan todos los certificados.
            return true;
        }
    }




    public void Enviar()
    {
        
       StartCoroutine(SubirArchivo("D:/files/test.txt", "/unity001"));

    }
}
