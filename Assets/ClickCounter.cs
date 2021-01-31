using UnityEngine;

public class ClickCounter : MonoBehaviour
{
    public int clickCount { get; private set; } = 0;

    public void IncrementClickCount()
    {
        clickCount++;
        Debug.Log($"Clicks: {clickCount}");
    }
}
