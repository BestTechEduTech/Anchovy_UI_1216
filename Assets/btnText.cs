using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnText : MonoBehaviour
{
    // Start is called before the first frame update
    private string btn_Text;

    void Start()
    {
        btn_Text = gameObject.name;       
        gameObject.GetComponentInChildren<Text>().text = btn_Text;
    }

}
