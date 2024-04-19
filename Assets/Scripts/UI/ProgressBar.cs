using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image progressBarImage;
    [SerializeField] TMP_Text progressBarText;
    [SerializeField] bool autoDisable = false;

    public void UpdateValue(float value)
    {
        if (progressBarText != null) 
            progressBarText.text = value.ToString("P0");
        if (progressBarImage != null )
            progressBarImage.fillAmount = value;

        if (autoDisable && value == 1f)
            gameObject.SetActive(false);
    }

}
