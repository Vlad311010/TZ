using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image progressBarImage;
    [SerializeField] TMP_Text progressBarText;

    public void UpdateValue(float value)
    {
        progressBarText.text = value.ToString("P0");
        progressBarImage.fillAmount = value;
    }

}
