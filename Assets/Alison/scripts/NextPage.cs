using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPage : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;

    public void onClickNextChapter()
    {
        RectTransform content = scrollbar.transform.GetChild(0).GetComponent<RectTransform>();
        float currentPosition = content.anchoredPosition.x;
        float nextSnapPosition = Mathf.RoundToInt(currentPosition / 500f) * 500f + 500f;
        content.anchoredPosition = new Vector2(nextSnapPosition, content.anchoredPosition.y);
        float newValue = nextSnapPosition / (content.rect.width - scrollbar.GetComponent<RectTransform>().rect.width);
        scrollbar.value = Mathf.Clamp01(newValue);
    }
}
