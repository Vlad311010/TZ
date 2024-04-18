using UnityEngine;

public class TabBar : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color selectedColor;
    [SerializeField] GameObject pages;


    private TabButton[] tabs;
    private TabButton selectedTab;

    private void Awake()
    {
        tabs = GetComponentsInChildren<TabButton>(true);
        OnSelect(tabs[0]);
    }

    public void OnSelect(TabButton selectedTab)
    {
        ResetTabs();
        this.selectedTab = selectedTab;
        selectedTab.SetColor(selectedColor);
        int pageIdx = selectedTab.transform.GetSiblingIndex();

        Debug.Assert(pageIdx < pages.transform.childCount, "Wrong page index");
        pages.transform.GetChild(pageIdx).gameObject.SetActive(true);
    }

    private void ResetTabs()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetColor(defaultColor);
            pages.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
