using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour
{
    private Text tooltip;

    void Start()
    {
        tooltip = GetComponentInChildren<Text>();
    }

    public void GenerateContent(Item item)
    {
        string tooltip = string.Format("{0}\n{1}\n", item.title, item.description);
        this.tooltip.text = tooltip;
        gameObject.SetActive(true);
    }
}
