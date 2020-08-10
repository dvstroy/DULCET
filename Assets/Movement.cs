using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Movement : MonoBehaviour
{
    public KeyCode forward, backward, left, right, jump, music;
    public float jumpForce;
    public float runSpeed;
    public bool isGrounded;
    public bool musicMenu = false;
    public CanvasGroup menu;
    public AudioClip bSound;
    public AudioClip aSound;
    public AudioClip dSound;
    public AudioClip fSound;
    public AudioSource audioSource;

    //Animator anim;
    Rigidbody rb;
    Vector3 wantedDirection, camDirection;
    bool wantedJump;
    Vector3 debugEulerAngles;
    Vector3 resetPos = Vector3.zero;
    float cameraRotationY = 0;
    bool cursorVisible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        Cursor.visible = cursorVisible;
        Cursor.lockState = CursorLockMode.Locked;
        isGrounded = true;
    }

    private void Update()
    {
        //R Resets player to 000
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        //Alt + R resets the entire scene to it's default state
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Cursor.visible = !cursorVisible;
        //    cursorVisible = !cursorVisible;
        //}

        if (Input.GetKey(music))
        {
            musicMenu = true;
            Debug.Log("Music ON");
            menu.alpha = (1);
        }
        else
        {
            musicMenu = false;
            Debug.Log("Music OFF");
            menu.alpha = (0);
        }

        //Setting up vars for movement
        var forwardIntent = 0;
        var horizontalIntent = 0;

        //Taking inputs
        //Movement Keys

        if (Input.GetKey(forward))
        {
            if (musicMenu == false)
            {
                forwardIntent += 1;
            }
        }
        if (Input.GetKey(backward))
        {
            if (musicMenu == false)
            {
                forwardIntent -= 1;
            }

        }
        if (Input.GetKey(left))
        {
            if (musicMenu == false)
            {
                horizontalIntent -= 1;
            }
        }
        if (Input.GetKey(right))
        {
            if (musicMenu == false)
            {
                horizontalIntent += 1;
            }

        }
        //Storing the direction the player wants to go in
        wantedDirection = new Vector3(horizontalIntent, 0, forwardIntent).normalized;

        //Taking jump input
        if (Input.GetKeyDown(jump) && isGrounded)
        {
            wantedJump = true;
        }

    }

    void FixedUpdate()
    {
        //This if checks if the player is moving
        if (wantedDirection.magnitude == 0)
        {
            //anim.SetBool("isRunning", false);
        }
        else
        {
            //The code that references where the camera is and how that should effect the player
            rb.transform.forward = Quaternion.Euler(0, cameraRotationY, 0) * wantedDirection;
            //The code that actually moves the palyer
            rb.MovePosition(rb.position + rb.transform.forward * runSpeed * Time.deltaTime);
           // anim.SetBool("isRunning", true);
        }


        if (wantedJump)
        {
            //Makes the player jump
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
           // anim.SetTrigger("jump");
            wantedJump = false;
        }

    }

    //For the camera script to tell the player which way the camera is facing, purely on the Y rotaitonal axis
    public void SetCameraForward(Vector3 forward)
    {
        cameraRotationY = Quaternion.FromToRotation(Vector3.forward, new Vector3(forward.x, 0, forward.z)).eulerAngles.y;
    }

    //For other scripts to set the player to be grounded
    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
        //anim.SetBool("inAir", !grounded);
    }

    void Reset()
    {
        transform.position = resetPos;
        rb.velocity = new Vector3(0, 0, 0);
    }

    public void UnlockCursor()
    {
        Cursor.visible = !cursorVisible;
        cursorVisible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LockCursor()
    {
        Cursor.visible = !cursorVisible;
        cursorVisible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MusicMenu()
    {
        if (Input.GetKeyDown(forward))
        {
            if (musicMenu == true)
            {
                PlayB();
            }
        }
        if (Input.GetKeyDown(backward))
        {
            if (musicMenu == true)
            {
                PlayF();
            }
        }
        if (Input.GetKeyDown(left))
        {
            if (musicMenu == true)
            {
                PlayA();
            }
        }
        if (Input.GetKeyDown(right))
        {
            if (musicMenu == true)
            {
                PlayD();
            }
        }
    }

    public void PlayB()
    {
        audioSource.PlayOneShot (bSound, 1f);
        Debug.Log("PLAY B");
    }
    public void PlayD()
    {
        audioSource.PlayOneShot(dSound, 1f);
        Debug.Log("PLAY D");
    }
    public void PlayF()
    {
        audioSource.PlayOneShot(fSound, 1f);
        Debug.Log("PLAY F");
    }
    public void PlayA()
    {
        audioSource.PlayOneShot(aSound, 1f);
        Debug.Log("PLAY A");
    }
}