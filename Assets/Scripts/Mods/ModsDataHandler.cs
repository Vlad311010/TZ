using Plugins.Dropbox;
using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class ModsDataHandler : MonoBehaviour
{
    private class ModsDataContainer
    {
        public ModData[] mods;
        public string[] categories;
    }


    ModData[] Mods => modsDataContainer.mods;
    string[] Categories => modsDataContainer.categories;

    [SerializeField] string modsDataRelativePath = "";
    [SerializeField] GameObject loadScreen;

    private ModsDataContainer modsDataContainer;
    

    private void Awake()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        var task = DropboxHelper.Initialize();
        yield return new WaitUntil(() => task.IsCompleted);
        DownloadModsData();
    }

    private async void DownloadModsData()
    {
        await DropboxHelper.DownloadAndSaveFile(modsDataRelativePath);
        
    }

    private async void DownloadWrapper(IProgress<int> progress)
    {
        Task task = DropboxHelper.DownloadAndSaveFile(modsDataRelativePath);
        progress.Report((int)task.Status * 100);
    }

    public void LoadModsData()
    {
        string modsDataJson = File.ReadAllText(Application.persistentDataPath + "/" + modsDataRelativePath);
        modsDataContainer = JsonUtility.FromJson<ModsDataContainer>(modsDataJson);
        Debug.Log("Mod data loaded");
    }


}
