using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public GameObject Player;
    public GameObject Projectile;
    public GameObject Head;
    public GameObject Dummy;
    public GameObject Target;
    public GameObject SmokePuff;

    private Player player;
    private CameraController camCon;
    private ThrowController throwCon;

    private bool canThrow = true;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetComponent<Player>();
        camCon = Player.GetComponent<CameraController>();
        throwCon = Player.GetComponent<ThrowController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Return head to the player;
            ReturnHead();
            canThrow = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ReadyThrow();
        }

        if (Input.GetMouseButtonUp(1))
        {
            ThrowHead();
            canThrow = false;
        }
    }

    private void ThrowHead()
    {
        Projectile.SetActive(true);
        player.animator.SetBool("isThrow", true);
        if (canThrow == true)
        {
            StartCoroutine(throwCon.SimulateProjectile());
        }
        Dummy.SetActive(false);
        Target.SetActive(false);
        player.animator.SetBool("isThrowIdle", false);
    }

    private void ReadyThrow()
    {
        player.animator.SetBool("isThrowIdle", true);
        if (canThrow == true)
        {
            Head.SetActive(false);
            Dummy.SetActive(true);
            Target.SetActive(true);
            Projectile.SetActive(false);
        }
    }

    private void ReturnHead()
    {
        Head.SetActive(true);
        GameObject returnEffect = Instantiate(SmokePuff, Head.transform);
        Destroy(returnEffect, 1);
        Projectile.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
