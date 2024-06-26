using Plugins.Dropbox;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DropboxDataHandler : MonoBehaviour
{
    private class ModsDataContainer
    {
        public ModData[] mods;
        public string[] categories;
    }


    public ModData[] Mods => modsDataContainer.mods;
    public string[] Categories => modsDataContainer.categories;

    public static DropboxDataHandler current;

    [SerializeField] ProgressBar loadScreen;
    [SerializeField] string modsDataRelativePath = "";
    [SerializeField] string fileToDownload; 
    [SerializeField] GameObject reconnectBtn; 

    private ModsDataContainer modsDataContainer;

    private void Awake()
    {
        current = this;
        Directory.CreateDirectory(Application.persistentDataPath + "/mods/files");
        InitConnection();
    }

    public void InitConnection()
    {
        loadScreen.gameObject.SetActive(true);
        StartCoroutine(Init());
    }

    private void FaildToDownloadData()
    {
        Debug.Log("Failed to download data");
        StopAllCoroutines();
        loadScreen.gameObject.SetActive(true);
        reconnectBtn.SetActive(true);
    }

    private IEnumerator Init()
    {
        DropboxHelper.onFailedToDownload += FaildToDownloadData;

        var task = DropboxHelper.Initialize();
        yield return new WaitUntil(() => task.IsCompleted);
        task = DropboxHelper.DownloadAndSaveFile(modsDataRelativePath, (p) => loadScreen.UpdateValue(p));
        yield return new WaitUntil(() => task.IsCompleted);
        LoadModsData();

        DropboxHelper.onFailedToDownload -= FaildToDownloadData;
    }

    public void LoadModsData()
    {
        string modsDataJson = File.ReadAllText(Application.persistentDataPath + "/" + modsDataRelativePath);
        modsDataContainer = JsonUtility.FromJson<ModsDataContainer>(modsDataJson);
        Debug.Log("Aplication data loaded");
    }

    public void LoadModPreviewImage(string relativePath, Action<Sprite> callback)
    {
        StartCoroutine(LoadModPreviewImageCoroutine(relativePath, callback));
    }

    private IEnumerator LoadModPreviewImageCoroutine(string relativePath, Action<Sprite> callback)
    {
        if (File.Exists(Application.persistentDataPath + "/" + relativePath))
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(Application.persistentDataPath + "/" + relativePath);
            yield return request.SendWebRequest();
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(Vector2.zero, texture.Size()), new Vector2(0.5f, 0.0f), 1.0f);
            callback(sprite);
        }
        else
        {
            Task task = DropboxHelper.DownloadAndSaveFile(relativePath, p => { });
            yield return new WaitUntil(() => task.IsCompleted);
            StartCoroutine(LoadModPreviewImageCoroutine(relativePath, callback));
        }
    }

}
