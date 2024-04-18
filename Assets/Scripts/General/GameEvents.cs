using System;
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

}
