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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearDots(List<DotScript> list, Color color, int score)
    {
        var d = list[0];

        var screenPoint = RectTransformUtility.WorldToScreenPoint(uiCanvas.worldCamera, d.transform.position);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(uiCanvas.GetComponent<RectTransform>(), screenPoint, uiCanvas.worldCamera, out localPoint);

        var label = Instantiate(labelPrefab, container) as GameObject;
        label.transform.localPosition = localPoint;

        var tmp  = label.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = score.ToString();
        tmp.color = color;

        Destroy(label, 1.0f);
    }
}
