using System.Buffers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CategoryFilterBtn : MonoBehaviour, IPointerClickHandler
{
    public string CategoryName => categoryName;

    Image btnImage;

    [SerializeField] TMP_Text text;
    [SerializeField] Sprite filterEnabledSprite;
    [SerializeField] Sprite filterDisabledSprite;

    private SearchBarScript searchBar;
    private string categoryName;
    private bool filterEnabled = false;

    public void Init(SearchBarScript searchBar, string category)
    {
        btnImage = GetComponent<Image>();
        this.searchBar = searchBar;
        categoryName = category;
        text.text = category;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (filterEnabled)
            searchBar.RemoveFilter(categoryName);
        else
            searchBar.AddFilter(categoryName);
        filterEnabled = !filterEnabled;
        btnImage.sprite = filterEnabled ? filterEnabledSprite : filterDisabledSprite;
    }


}
