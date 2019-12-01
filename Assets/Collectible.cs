using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject particleEffect;

    private bool slowMoveUp = false;

    public GameObject collectedIndicator;

    private void Update()
    {
        if (slowMoveUp == true)
        {
            this.gameObject.transform.position += new Vector3(0, 0.04f, 0);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Player"))
        {
            if (this.name.Contains("Gem"))
            {
                collectedIndicator.SetActive(true);
                Destroy(this.GetComponent<BoxCollider>());
                if (particleEffect)
                {
                    GameObject effect = GameObject.Instantiate(particleEffect, transform);
                    effect.transform.parent = null;
                    slowMoveUp = true;
                    Destroy(effect, 2);
                }
                this.GetComponent<SimpleAnims>().RotationSpeed += 4;
                collision.gameObject.GetComponent<Player>().collectGem();
                Destroy(gameObject, 1);
            }

            if (this.name.Contains("Key"))
            {
                collision.gameObject.GetComponent<Player>().updateKeys(1);
                Destroy(this.GetComponent<BoxCollider>());
                this.GetComponent<SimpleAnims>().RotationSpeed += 8;
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
