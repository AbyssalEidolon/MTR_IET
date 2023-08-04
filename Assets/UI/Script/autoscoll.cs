using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class autoscoll : MonoBehaviour
{
    public float scrollSpeed = 10.0f;

    private RectTransform panelRectTransform;

    void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float newPosition = panelRectTransform.anchoredPosition.y - (scrollSpeed * Time.deltaTime);
        float maxPosition = Mathf.Max(panelRectTransform.sizeDelta.y - GetComponentInParent<RectTransform>().sizeDelta.y, 0);

        if (newPosition < -maxPosition)
        {
            newPosition = -maxPosition;
        }

        panelRectTransform.anchoredPosition = new Vector2(panelRectTransform.anchoredPosition.x, newPosition);
    }
}
