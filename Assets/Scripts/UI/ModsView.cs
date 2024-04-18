using UnityEngine;

public class ModsView : MonoBehaviour
{
    [SerializeField] GameObject conterCardPrefab;
    [SerializeField] GameObject content;

    private void Awake()
    {
        GameEvents.current.onModsDataLoaded += UpdateContent;
    }

    private void OnDestroy()
    {
        GameEvents.current.onModsDataLoaded -= UpdateContent;
    }

    private void UpdateContent(ModData[] data)
    {
        for (int i = 0; i < data.Length; i++) 
        {
            ModDataSO modDataSO = ModDataSO.CreateInstance(data[i]);
            Instantiate(conterCardPrefab, content.transform).GetComponent<ContentCard>().Init(modDataSO);

        }

    }
}
