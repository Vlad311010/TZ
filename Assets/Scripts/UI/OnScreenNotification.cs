using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenNotification : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;
    [SerializeField] float stayTime;
    [SerializeField] float disappearTime;

    private Color defaultImageColor = Color.white;
    private Color imageAlphaZeroColor = new Color(1f, 1f, 1f, 0f);
    private Color defaultTextColor = Color.black;
    private Color TextAlphaZeroColor = new Color(0f, 0f, 0f, 0f);

    private void Awake()
    {
        GameEvents.current.onDownloadFail += ShowDownloadFailMessage;
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.current.onDownloadFail -= ShowDownloadFailMessage;
    }

    private void OnEnable()
    {
        SetColor(defaultImageColor, defaultTextColor);
        StartCoroutine(Hide());
    }

    private void ShowDownloadFailMessage()
    {
        gameObject.SetActive(true);
    }

    private void SetColor(Color imageColor, Color textColor)
    {
        image.color = imageColor;
        text.color = textColor;
    }


    private IEnumerator Hide(float timer = 0f)
    {
        timer += Time.deltaTime;
        yield return new WaitForEndOfFrame();
        if (timer > stayTime)
        {
            float lerpTime = (timer - stayTime) / disappearTime;
            SetColor(Color.Lerp(defaultImageColor, imageAlphaZeroColor, lerpTime), Color.Lerp(defaultTextColor, TextAlphaZeroColor, lerpTime));
        }

        if (timer < stayTime + disappearTime)
            StartCoroutine(Hide(timer));
        else
            gameObject.SetActive(false);
    }


}
