using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Rigidbody newrd;
    private Rigidbody rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        ////////////Debug.Log()
    }

    // Update is called once per frame
    void Update()
    {
    }
}
