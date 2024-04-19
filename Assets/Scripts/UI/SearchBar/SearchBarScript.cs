using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchBarScript : MonoBehaviour
{

    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject categoriesContainer;
    [SerializeField] CategoryFilterBtn categoriesFilterBtnPrefab;

    private List<string> selectedFilters;

    private void Awake()
    {
        GameEvents.current.onCategoriesLoaded += UpdateCategories;
        inputField.onValueChanged.AddListener(FilterEvent);
    }

    private void OnDestroy()
    {
        GameEvents.current.onCategoriesLoaded -= UpdateCategories;
        inputField.onValueChanged.RemoveListener(FilterEvent);
    }

    private void UpdateCategories(string[] categories)
    {
        selectedFilters = new List<string>();
        for (int i = 0; i < categories.Length; i++)
        {
            Instantiate(categoriesFilterBtnPrefab, categoriesContainer.transform).Init(this, categories[i]);
        }
    }

    public void AddFilter(string category)
    {
        selectedFilters.Add(category);
        Debug.Log(string.Join(',', selectedFilters));
        GameEvents.current.ModsViewFilterChange(inputField.text, selectedFilters);
    }

    public void RemoveFilter(string category)
    {
        selectedFilters.Remove(category);
        Debug.Log(string.Join(',', selectedFilters));
        GameEvents.current.ModsViewFilterChange(inputField.text, selectedFilters);
    }

    private void FilterEvent(string searchBarText)
    {
        GameEvents.current.ModsViewFilterChange(searchBarText, selectedFilters);
    }

    public void ClearText()
    {
        inputField.text = string.Empty;
    }
}
