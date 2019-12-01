using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (name.Contains("Torch") && collision.transform.name.Contains("Projectile"))
        {
            StartCoroutine(douseFlame());
        }

        if (name.Contains("Exit") && collision.transform.name.Contains("Player"))
        {
            collision.gameObject.GetComponent<Player>().showEnd();
        }

        if (name.Contains("Door") && collision.transform.name.Contains("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().testUnlockDoor())
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator douseFlame()
    {
        transform.Find("Flame").gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        transform.Find("Flame").gameObject.SetActive(true);
    }
}
