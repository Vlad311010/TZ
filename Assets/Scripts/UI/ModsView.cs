using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModsView : MonoBehaviour
{
    [SerializeField] GameObject conterCardPrefab;
    [SerializeField] GameObject content;

    private List<ContentCard> cards;

    private void Awake()
    {
        GameEvents.current.onModsDataLoaded += CreateContent;
    }

    private void OnDestroy()
    {
        GameEvents.current.onModsDataLoaded -= CreateContent;
    }

    private void OnEnable()
    {
        GameEvents.current.onModsViewFilterChange += FilterContent;
    }

    private void OnDisable()
    {
        GameEvents.current.onModsViewFilterChange -= FilterContent;
    }

    private void CreateContent(ModData[] data)
    {
        if (cards != null)
        {
            foreach (var card in cards)
            {
                Destroy(card.gameObject);
            }
        }

        cards = new List<ContentCard>();
        for (int i = 0; i < data.Length; i++) 
        {
            ModDataSO modDataSO = ModDataSO.CreateInstance(data[i]);
            ContentCard newCard = Instantiate(conterCardPrefab, content.transform).GetComponent<ContentCard>();
            cards.Add(newCard);
            newCard.Init(modDataSO);
        }

        FilterContent("", new string[0]);
    }

    public void FilterContent(string titleFilter, IEnumerable<string> categoryFilter)
    {
        foreach (ContentCard c in cards)
            c.gameObject.SetActive(false);

        IEnumerable<ContentCard> cardsToShow = cards
            .Where(c =>
                (string.IsNullOrEmpty(titleFilter) || c.Title.Contains(titleFilter))
                && (categoryFilter.Count() == 0 || categoryFilter.Contains(c.Category))
            );

        foreach (ContentCard c in cardsToShow)
            c.gameObject.SetActive(true);
    }
}
