using System.Collections.Generic;
using UnityEngine;
using UnityGoogleDrive;

public class DriveSync : MonoBehaviour
{
    private GoogleDriveFiles.GetRequest request;
    private string result;
    private string fileId = string.Empty;

   

    public void GetFile()
    {
        request = GoogleDriveFiles.Get(fileId);
        request.Fields = new List<string> { "name, size, createdTime" };
        request.Send().OnDone += BuildResultString;
    }

    public void BuildResultString(UnityGoogleDrive.Data.File file)
    {
        result = string.Format("Name: {0} Size: {1:0.00}MB Created: {2:dd.MM.yyyy HH:MM:ss}",
            file.Name,
            file.Size * .000001f,
            file.CreatedTime);
    }
}