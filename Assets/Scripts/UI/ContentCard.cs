using Plugins.Dropbox;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContentCard : MonoBehaviour
{
    public string Title => modData.title;
    public string Category => modData.category;

    [SerializeField] Image preview;
    [SerializeField] TMP_Text text;
    [SerializeField] Button downloadBtn;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] SpinnerScript spinner;

    private static NativeShare nativeShare;
    private ModDataSO modData;
    private string textTemplate = "<b>{0}<b>\r\n<size=36>{1}</size>";



    public void Init(ModDataSO modData)
    {
        if (nativeShare == null)
            nativeShare = new NativeShare();

        this.modData = modData;
        DropboxDataHandler.current.LoadModPreviewImage(modData.previewPath.TrimStart('/'), SetPreview);
        SetText();
    }

    private void SetPreview(Sprite sprite)
    {
        preview.sprite = sprite;
        Destroy(spinner.gameObject);
    }

    private void SetText()
    {
        text.text = string.Format(textTemplate, modData.title, modData.description);
    }

    private IEnumerator DownloadCoroutine()
    {
        downloadBtn.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(true);

        nativeShare.Clear();
        DropboxHelper.onFailedToDownload += FailedToDownloadPopup;

        Task task = DropboxHelper.DownloadAndSaveFile(modData.filePath.TrimStart('/'), (p) => { progressBar.UpdateValue(p); });
        yield return new WaitUntil(() => task.IsCompleted);

        DropboxHelper.onFailedToDownload -= FailedToDownloadPopup;
        if (File.Exists(Application.persistentDataPath + modData.filePath)) 
        {
            nativeShare.AddFile(Application.persistentDataPath + modData.filePath);
            nativeShare.Share();
        }

        downloadBtn.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(false);

    }

    private void FailedToDownloadPopup()
    {
        GameEvents.current.DownloadFail();
    }

    public void Donwload()
    {
        StartCoroutine(DownloadCoroutine());
    }
}