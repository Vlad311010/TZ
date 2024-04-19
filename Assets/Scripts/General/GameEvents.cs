using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5)]
public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action<ModData[]> onModsDataLoaded;
    public void ModsDataLoaded(ModData[] modsData)
    {
        if (onModsDataLoaded != null)
        {
            onModsDataLoaded(modsData);
        }
    }

    public event Action<string[]> onCategoriesLoaded;
    public void CategoriesLoaded(string[] categories)
    {
        if (onCategoriesLoaded != null)
        {
            onCategoriesLoaded(categories);
        }
    }

    public event Action<string, IEnumerable<string>> onModsViewFilterChange;
    public void ModsViewFilterChange(string titleFilter, IEnumerable<string> categoriesFilter)
    {
        if (onModsViewFilterChange != null)
        {
            onModsViewFilterChange(titleFilter, categoriesFilter);
        }
    }

    public event Action onDownloadFail;
    public void DownloadFail()
    {
        if (onDownloadFail != null)
        {
            onDownloadFail();
        }
    }

    public event Action onInternetConnectionLost;
    public void InternetConnectionLost()
    {
        if (onInternetConnectionLost != null)
        {
            onInternetConnectionLost();
        }
    }

}
