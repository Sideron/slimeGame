using UnityEngine;

public class slimeRow : MonoBehaviour
{
    private uiSlimeCounter[] counters;
    public Transform selectFrame;
    public GameObject slimeCounterPrefab;
    public int index = 0;
    bool isInit = false;
    public void init(GameObject[] slimes, int[] amounts)
    {
        Debug.Log(slimes.Length);
        Debug.Log(amounts.Length);
        counters = new uiSlimeCounter[slimes.Length];
        for (int i = 0; i < slimes.Length; i++)
        {
            GameObject nCounter = Instantiate(slimeCounterPrefab, selectFrame.position + Vector3.right*(i*100), Quaternion.identity);
            nCounter.transform.SetParent(this.transform);
            nCounter.transform.localScale = Vector3.one;
            uiSlimeCounter counterScript = nCounter.GetComponent<uiSlimeCounter>();
            SpriteRenderer sr = slimes[i].GetComponent<SpriteRenderer>();
            counterScript.slimeColor = sr.color;
            counterScript.slimeIconSprite = sr.sprite;
            counters[i] = counterScript;
        }
        isInit = true;
    }

    public void setIndex(int nIndex)
    {
        index = nIndex;
    }
    public void updateRow(int[] nList)
    {
        for (int i = 0; i < nList.Length; i++)
        {
            counters[i].setAmount(nList[i]);
        }
    }
    public void updateCounter(int pos, int value)
    {
        counters[pos].setAmount(value);
    }
    void Update()
    {
        if(!isInit) { return; }
        selectFrame.position = Vector2.Lerp(selectFrame.position,counters[index].transform.position,0.1f);
    }
}
