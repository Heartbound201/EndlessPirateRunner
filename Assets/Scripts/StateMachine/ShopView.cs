using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopView : View
{
    public UnityAction OnMenuClicked;

    public Player player;
    public GameObject buyItemPrefab;
    public Transform buyItemPanel;
    public List<PlayerShipPrototype> availableShips;

    private readonly List<UIShipCard> _shipCards = new List<UIShipCard>();

    public void FillShopItems()
    {
        // fill every ship in the game
        foreach (PlayerShipPrototype shipPrototype in availableShips)
        {
            // instantiate ui obj
            GameObject item = Instantiate(buyItemPrefab, buyItemPanel);
            UIShipCard uiShipCard = item.GetComponent<UIShipCard>();
            uiShipCard.image.sprite = shipPrototype.sprite;
            uiShipCard.playerShipPrototype = shipPrototype;
            uiShipCard.buyButton.GetComponentInChildren<Text>().text = shipPrototype.cost + " Gold";
            // assign on click events
            uiShipCard.useButton.onClick.AddListener(() =>
            {
                player.currentPlayerShip = uiShipCard.playerShipPrototype;
                player.Save();
                UpdateShipUpgradeButtons();
            });
            uiShipCard.buyButton.onClick.AddListener(() =>
            {
                if (player.gold.Value < shipPrototype.cost) return;
                player.gold.Value -= shipPrototype.cost;
                player.availableShips.Add(shipPrototype);
                player.Save();
                UpdateShipUpgradeButtons();
            });
            _shipCards.Add(uiShipCard);
        }
        UpdateShipUpgradeButtons();
    }

    private void UpdateShipUpgradeButtons()
    {
        // disable 'useButton' is this ship is currently in use. enable the rest
        // show only necessary buttons
        foreach (UIShipCard buyItem in _shipCards)
        {
            buyItem.useButton.interactable = true;
            buyItem.buyButton.interactable = true;
            if (player.availableShips.Contains(buyItem.playerShipPrototype))
            {
                buyItem.useButton.gameObject.SetActive(true);
                buyItem.buyButton.gameObject.SetActive(false);
                if (player.currentPlayerShip == buyItem.playerShipPrototype)
                {
                    buyItem.useButton.interactable = false;
                }
            }
            else
            {
                buyItem.useButton.gameObject.SetActive(false);
                buyItem.buyButton.gameObject.SetActive(true);
                if (player.gold.Value < buyItem.playerShipPrototype.cost)
                {
                    buyItem.buyButton.interactable = false;
                }
            }
        }
    }

    public void ClearShopItems()
    {
        foreach (UIShipCard item in _shipCards)
        {
            Destroy(item.gameObject);
        }

        _shipCards.Clear();
    }

    public void MenuClick()
    {
        OnMenuClicked?.Invoke();
    }
}