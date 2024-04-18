using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButton : MonoBehaviour, IPointerClickHandler
{
    TabBar tabBar;

    [SerializeField] Image icon;
    [SerializeField] TMP_Text label;

    private void Awake()
    {
        tabBar = GetComponentInParent<TabBar>();
    }

    public void SetColor(Color color)
    {
        icon.color = color;
        label.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabBar.OnSelect(this);
    }
}
