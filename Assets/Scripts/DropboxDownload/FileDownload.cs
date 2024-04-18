using Plugins.Dropbox;
using System.Collections;
using UnityEngine;

public class FileDownload : MonoBehaviour
{
    [SerializeField] string relativePath;

    private void Awake()
    {
       StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        var task = DropboxHelper.Initialize();
        yield return new WaitUntil(() => task.IsCompleted);
    }




    [ContextMenu("DownloadFile")]
    public async void DownloadFile()
    {
        await DropboxHelper.DownloadAndSaveFile(relativePath);
    }
}
