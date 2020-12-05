using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FieldLabelsScript : MonoBehaviour
{
    [SerializeField]
    private Transform container;

    [SerializeField]
    private GameObject labelPrefab;

    [SerializeField]
    private Canvas uiCanvas;

    private void OnEnable()
    {
        EventManager.OnClearDots += ClearDots;
    }

    private void OnDisable()
    {
        EventManager.OnClearDots -= ClearDots;
    }

    void ClearDots(List<DotScript> list, Color color, int score)
    {
        Vector2 worldPos = Vector2.zero;
        foreach (var d in list)
        {
            worldPos.x += d.transform.position.x;
            worldPos.y += d.transform.position.y;
        }
        worldPos /= list.Count;

        var screenPoint = RectTransformUtility.WorldToScreenPoint(uiCanvas.worldCamera, worldPos);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(uiCanvas.GetComponent<RectTransform>(), screenPoint, uiCanvas.worldCamera, out localPoint);

        var label = Instantiate(labelPrefab, container) as GameObject;
        var tmp = label.GetComponentInChildren<TextMeshProUGUI>();

        label.transform.localPosition = localPoint;

        tmp.text = score.ToString();
        tmp.color = color;

        Destroy(label, 1.0f);
    }
}
