using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentCard : MonoBehaviour
{
    [SerializeField] Image preview;
    [SerializeField] TMP_Text text;
    [SerializeField] Button downloadBtn;
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

    private void Download()
    {

    }

}