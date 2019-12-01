using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 0.5f;
    private bool grounded = false;
    private bool aiming = false;
    public CameraController camController;
    public GameObject UI;
    public GameObject Exit;

    public int GemsCollected = 0;

    private Rigidbody rb;
    public Animator animator;

    private float timeTaken;
    private bool gameover = false;

    public int keys = 0;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        timeTaken += Time.deltaTime;

        if (animator.GetBool("isThrow") == true)
        {
            animator.SetBool("isThrow", false);
        }

        if (grounded && !gameover)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            //velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            if (!aiming)
            {
                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }

            if (velocityChange == Vector3.zero)
            {
                animator.SetBool("isWalking", false);
            } else
            {
                animator.SetBool("isWalking", true);
            }

            // Player rotation using the Horizontal Axis
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * speed/2, 0));

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        // We apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void collectGem()
    {
        GemsCollected += 1;
        testEndGame();
    }

    public void testEndGame()
    {
        if (GemsCollected >= 5)
        {
            Debug.Log("Get to the portal!");
           Exit.SetActive(true);
        }
    }

    public void showEnd()
    {
        UI.transform.Find("EndGamePanel").gameObject.SetActive(true);
        UI.transform.Find("EndGamePanel").Find("TimeText").GetComponent<Text>().text = ((int)(timeTaken / 60)) + "Mins " + ((int)(timeTaken % 60)) + "Secs";
        gameover = true;
        transform.Find("skel_body").gameObject.SetActive(false);
    }

    public void resetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void updateKeys(int change)
    {
        keys += change;
        UI.transform.Find("Keys").Find("Text").GetComponent<Text>().text = keys.ToString();
    }

    public bool testUnlockDoor()
    {
        if (keys >= 1)
        {
            updateKeys(-1);
            return true;
        }
        else
        {
            return false;
        }
    }
}
