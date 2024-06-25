using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttack : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            MinionController minion = other.GetComponent<MinionController>();
            minion.HealthBar.SetActive(true);
            minion.beHit = true;
            minion.dameTake = playerController.CrtDetail.dame;
        }
        if (other.CompareTag("Enemy"))
        {
            BossController boss = other.GetComponent<BossController>();
            boss.beHit = true;
            boss.dameTake = playerController.CrtDetail.dame;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            //MinionController minion = other.GetComponent<MinionController>();
            //minion.beHit = false;
            //minion.dameTake = 0;
        }
        if (other.CompareTag("Enemy"))
        {
        }
    }

}
