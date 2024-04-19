using Plugins.Dropbox;
using System.Collections;
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
    
    private ModDataSO modData;

    private string textTemplate = "<b>{0}<b>\r\n<size=36>{1}</size>";



    public void Init(ModDataSO modData)
    {
        this.modData = modData;

        Debug.Log(modData.previewPath);
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
        yield return DropboxHelper.DownloadAndSaveFile(modData.filePath.TrimStart('/'), (p) => { progressBar.UpdateValue(p); } );
        downloadBtn.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(false);

    }

    public void Donwload()
    {
        StartCoroutine(DownloadCoroutine());
    }
}