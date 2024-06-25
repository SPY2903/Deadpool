using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHit : MonoBehaviour
{
    [System.NonSerialized]
    public bool beHit= false;
    [System.NonSerialized]
    public int dameTake;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion Attack"))
        {
            beHit = true;
            Component[] arr = other.GetComponentsInParent<Transform>();
            dameTake = arr[arr.Length - 1].GetComponent<MinionController>().crtDetail.dame;
        }
        if(other.CompareTag("Enemy Attack"))
        {
            beHit = true;
            Component[] arr = other.GetComponentsInParent<Transform>();
            dameTake = arr[arr.Length - 1].GetComponent<BossController>().crtDetail.dame;
        }
    }

}
