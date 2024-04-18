using UnityEngine;

public class SpinnerScript : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
