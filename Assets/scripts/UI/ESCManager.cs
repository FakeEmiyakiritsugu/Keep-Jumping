using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCManager : MonoBehaviour
{
    [Tooltip("放菜单")]
    public GameObject ESCMenu;//放菜单
    [Tooltip("切出菜单按键")]
    public KeyCode ESCInput = KeyCode.Escape;
    [Tooltip("是否暂停")]
    public bool isPaused { get; set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(ESCInput))
        {
            if
        }
    }
}
