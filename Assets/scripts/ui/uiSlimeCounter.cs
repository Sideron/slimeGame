using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class uiSlimeCounter : MonoBehaviour
{
    public Color slimeColor;
    public Sprite slimeIconSprite;
    public int amount = 0;
    private TextMeshProUGUI textcounter;
    private Image slimeIcon;
    void Start()
    {
        slimeIcon = GetComponentInChildren<Image>();
        textcounter = GetComponentInChildren<TextMeshProUGUI>();
        setAmount(amount);
        slimeIcon.color = slimeColor;
        slimeIcon.sprite = slimeIconSprite;
    }
    public void setAmount(int nAmount)
    {
        amount = nAmount;
        if (textcounter == null) { return; }
        textcounter.text = "x"+amount;
    }
}
