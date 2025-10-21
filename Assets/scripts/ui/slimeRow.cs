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
    void Update()
    {
        selectFrame.position = Vector2.Lerp(selectFrame.position,counters[index].transform.position,0.1f);
    }
}
