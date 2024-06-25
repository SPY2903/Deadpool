using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemyHealthBar : MonoBehaviour
{
    private Camera _mainCam;
    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _mainCam.transform.position);
    }
}
