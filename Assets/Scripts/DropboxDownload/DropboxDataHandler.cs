using Plugins.Dropbox;
using System;
using System.Collections;
using System.IO;
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

    public static DropboxDataHandler current;

    [SerializeField] ProgressBar loadScreen;
    [SerializeField] string modsDataRelativePath = "";
    [SerializeField] string fileToDownload; 

    private ModsDataContainer modsDataContainer;

    private void Awake()
    {
        current = this;
        Directory.CreateDirectory(Application.persistentDataPath + "/mods/files");
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        var task = DropboxHelper.Initialize();
        yield return new WaitUntil(() => task.IsCompleted);
        yield return DropboxHelper.DownloadAndSaveFile(modsDataRelativePath, (p) => loadScreen.UpdateValue(p));
        LoadModsData();
    }

    public void LoadModsData()
    {
        string modsDataJson = File.ReadAllText(Application.persistentDataPath + "/" + modsDataRelativePath);
        modsDataContainer = JsonUtility.FromJson<ModsDataContainer>(modsDataJson);
        Debug.Log("Mod data loaded");
        GameEvents.current.ModsDataLoaded(modsDataContainer.mods);
        GameEvents.current.CategoriesLoaded(modsDataContainer.categories);
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
            yield return DropboxHelper.DownloadAndSaveFile(relativePath, p => { });
            StartCoroutine(LoadModPreviewImageCoroutine(relativePath, callback));
        }
    }


    /*[ContextMenu("DownloadFile")]
    public void DownloadFile()
    {
        StartCoroutine(DropboxHelper.DownloadAndSaveFile(fileToDownload));
    }*/


}
