using UnityEngine;

public class slimeRow : MonoBehaviour
{
    public uiSlimeCounter[] counters;
    public Transform selectFrame;
    public int index = 0;

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
        selectFrame.position = Vector2.Lerp(selectFrame.position,counters[index].transform.position,0.1f);
    }
}
