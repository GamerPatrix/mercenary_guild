using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Reusable UI component used to represent a single item in the player's inventory.
/// The component displays the item icon, name, and count.
/// </summary>
[DisallowMultipleComponent]
public class ItemUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Image component that will display the item icon.")]
    private Image iconImage;

    [SerializeField]
    [Tooltip("Text component that displays the item name.")]
    private TMP_Text nameText;

    [SerializeField]
    [Tooltip("Text component that displays the item count.")]
    private TMP_Text countText;

    private mercenary_guild.sos.ItemSO itemSO;

    /// <summary>
    /// Sets the item data to be displayed by this UI component.
    /// </summary>
    /// <param name="item">The item to display.</param>
    /// <param name="count">The quantity of the item.</param>
    public void SetItem(mercenary_guild.sos.ItemSO item, int count)
    {
        this.itemSO = item;

        if (iconImage != null && item != null)
        {
            iconImage.sprite = item.UIsprite;
            iconImage.enabled = item.UIsprite != null;
        }

        if (nameText != null && item != null)
        {
            nameText.text = item.GetItemName();
        }

        if (countText != null)
        {
            countText.text = "x" + count;
        }
    }
}
