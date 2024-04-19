using System.Collections;
using UnityEngine;

public class InternetConnectionCheck : MonoBehaviour
{
    [SerializeField] float interval;

    private void Awake()
    {
        StartCoroutine(ChechConnection());
    }

    private IEnumerator ChechConnection()
    {
        yield return new WaitForSeconds(interval);
        if (Application.internetReachability == NetworkReachability.NotReachable)
            GameEvents.current.InternetConnectionLost();

        StartCoroutine(ChechConnection());
    }
}
