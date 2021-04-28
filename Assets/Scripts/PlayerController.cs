using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float distance = 3f;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private AnimationClip anim;
    [SerializeField] private AudioClip[] moveSounds = new AudioClip[5];
    [SerializeField] private ParticleSystem[] ps;
    [SerializeField] private GameObject[] ragDollsBoxColliders;
    [SerializeField] private GameObject[] ragDollsCapsuleColliders;
    [SerializeField] private GameObject ragDollsSphereColliders;
    [SerializeField] private Timer timer;

    private Animator animator;
    private CharacterController cc;    
    private AudioSource moveAudiosource;
    private AudioSource cameraAudiosource;

    private bool isInMovement = false;    
    private bool isGrounded;
    private bool wasInTheJump = true;
    private float time;
    private float dir;
    private float currentDistance = 0f;
    private float currentDir = 0f;
    private float rightPosition;
    private float leftPosition;
    private float jSpeed = 0f;
    private readonly float gravity = -9.8f;
    private bool isFallen = true;
    private Vector3 movement;

    public int coinsBalance = 0;
    public float score;
    public bool newRecord;
    public delegate void IsNotAlive();
    public event IsNotAlive PlayerIsNotAlive;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }    

    void Start()
    {
        rightPosition = transform.position.x + distance / 2;
        leftPosition = transform.position.x - distance / 2;
        time = anim.length / 2f;
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        moveAudiosource = GetComponent<AudioSource>();
        cameraAudiosource = mainCamera.GetComponent<AudioSource>();
        StartCoroutine(MoveCoroutine());
        PlayerIsNotAlive += StopAllCoroutines;        
        CollidersIsTrigger();        
        timer.StarTimer();
    }

    void Update()
    {
        isGrounded = cc.isGrounded;
        if (!isGrounded)        
            animator.SetBool("Fall", true);

        if (isGrounded && !isInMovement && wasInTheJump)
        {
            ps[1].Play();
            PlayMoveSound(0);
            wasInTheJump = false;
        }      

        if (transform.position.y < -1f)
        {            
            mainCamera.transform.SetParent(null);

            if (transform.position.y < -10f && isFallen)
            {
                Death();
                PlayMoveSound(3);
                isFallen = false;
            }

        }
    }    

    IEnumerator MoveCoroutine()    
    {         
        while (true)
        {
            yield return null;
            dir = Input.GetAxisRaw("Horizontal");

            if (transform.position.x < leftPosition && dir == -1)
                dir = 0;
            if (transform.position.x > rightPosition && dir == 1)
                dir = 0;            

            if (!isInMovement && dir != 0)
            {
                PlayMoveSound(2);

                isInMovement = true;
                currentDir = dir;
                currentDistance = distance;                

                if (dir > 0 && transform.position.x < rightPosition && isGrounded)
                    animator.SetTrigger("Right");

                if (dir < 0 && transform.position.x > leftPosition && isGrounded)
                    animator.SetTrigger("Left");
            }            

            Move();

            Jump();

            cc.Move(movement);
        }
    }      
    
    private void Move()
    {
        if (isInMovement)
        {            
            PsStop();            
            
            if (currentDistance <= 0)
            {                
                isInMovement = false;
                return;
            }
            float speed = distance / time;
            float tmpDist = Time.deltaTime * speed;

            movement.x = currentDir * tmpDist;
            currentDistance -= tmpDist;
        }
        else movement.x = 0;
    }

    private void Jump ()
    {
        if (isGrounded && !isInMovement)
        {           
            if (Input.GetButtonDown("Jump"))
            {
                PsStop();                
                jSpeed = Mathf.Sqrt(jumpSpeed * -3.0f * gravity);
                animator.SetTrigger("JumpUp");
                PlayMoveSound(1);                
            }
            
            animator.SetBool("Fall", false);
        }
        jSpeed += gravity * Time.deltaTime * 3;
        movement.y = jSpeed * Time.deltaTime;
    }    
    
    private void PsStop()
    {
        ps[1].Stop();
        wasInTheJump = true;
    }

    private void PlayMoveSound(int i)
    {
        moveAudiosource.clip = moveSounds[i];

        if (i == 0) moveAudiosource.loop = true;
        else moveAudiosource.loop = false;

        moveAudiosource.Play();
    }    
    
    private void CollidersIsTrigger ()
    {
        ragDollsSphereColliders.GetComponent<SphereCollider>().isTrigger = true;

        foreach (GameObject ragDollsCollider in ragDollsBoxColliders)
        {
            ragDollsCollider.GetComponent<BoxCollider>().isTrigger = true;
        }

        foreach (GameObject ragDollsCollider in ragDollsCapsuleColliders)
        {
            ragDollsCollider.GetComponent<CapsuleCollider>().isTrigger = true;
        }

        if (animator.enabled == false)
        {
            ragDollsSphereColliders.GetComponent<SphereCollider>().isTrigger = false;

            foreach (GameObject ragDollsCollider in ragDollsBoxColliders)
            {
                ragDollsCollider.GetComponent<BoxCollider>().isTrigger = false;
            }

            foreach (GameObject ragDollsCollider in ragDollsCapsuleColliders)
            {
                ragDollsCollider.GetComponent<CapsuleCollider>().isTrigger = false;
            }
        }
    }

    private void SaveData ()
    {
        timer.StopTimer();
        score = timer.GetTime();

        PlayerPrefsController.instance.coinsBalance += coinsBalance;

        if (score > PlayerPrefsController.instance.recordScore)
        {
            newRecord = true;
            PlayerPrefsController.instance.recordScore = score;
        }

        else newRecord = false;
        PlayerPrefsController.instance.SaveData();        
    }  

    private void Death ()
    {
        ps[0].Play();
        ps[1].Stop();
        animator.enabled = false;
        PlayerIsNotAlive?.Invoke();

        PlayMoveSound(4);

        SaveData();

        CollidersIsTrigger();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Let"))
        {
            Death();   
        }

        if (other.CompareTag("Coin"))
        {
            coinsBalance++;            
            ps[2].Play();
            cameraAudiosource.Play();

        }
    }
    
    private void OnDestroy()
    {
        instance = null;
    }    
}


    
