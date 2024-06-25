using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [System.NonSerialized]
    public bool isPlayerTrigger = false;
    private PauseResume pr;

    private void Start()
    {
        pr = GetComponent<PauseResume>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTrigger = true;
            pr.Pause();
        }
    }

}
