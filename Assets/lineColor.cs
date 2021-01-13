using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class lineColor : MonoBehaviour,
     IPointerEnterHandler, IPointerExitHandler
{
    public GameObject line;
    public LineRenderer lineRenderer;
    private Color initColor = new Color(25,121,255);

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("it works!!");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
