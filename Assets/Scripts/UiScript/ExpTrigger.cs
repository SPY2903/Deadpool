using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpTrigger : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] private GameObject panelGamePlay;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private LevelDetail levelDetail;
    private Animator anim;
    [System.NonSerialized]
    public bool isCollected;
    // Start is called before the first frame update
    void Start()
    {
        anim = panelGamePlay.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Bonus");
            playerData.exp += 20;
            Destroy(gameObject, .5f);
            isCollected = true;
            levelDetail.UpdateExpTriggers(index);
        }
    }
}
