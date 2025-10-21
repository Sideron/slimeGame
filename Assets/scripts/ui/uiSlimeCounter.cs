using UnityEngine;
using TMPro;

public class uiSlimeCounter : MonoBehaviour
{
    public int amount = 0;
    private TextMeshProUGUI textcounter;
    void Start()
    {
        textcounter = GetComponentInChildren<TextMeshProUGUI>();
        setAmount(amount);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setAmount(int nAmount)
    {
        amount = nAmount;
        textcounter.text = "x"+amount;
    }
}
