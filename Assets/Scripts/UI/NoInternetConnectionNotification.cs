using UnityEngine;

public class NoInternetConnectionNotification : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.current.onInternetConnectionLost += ShowWindow;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.current.onInternetConnectionLost -= ShowWindow;
    }

    private void ShowWindow()
    {
        gameObject.SetActive(true);
    }
}
