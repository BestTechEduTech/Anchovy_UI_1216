using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Default_UI : MonoBehaviour
    
{
    public GameObject Panel;
    public Text Label;
    public Text content_Label;    
    public string assignText;    
    
    public void OnDefault()
    {
        Label.text = "멸치";
        content_Label.text = "멸치(anchovy)는 멸치과의 바닷물고기이다. 정어리의 일종으로, 사람들의 이용뿐만 아니라 먹이 사슬에서도 중요한 물고기이다. 그만큼 수많은 물고기종류들중 가장 개체수가 많은편인 물고기중 하나이다.";
    }
}
