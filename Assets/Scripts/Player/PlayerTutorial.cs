using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTutorial : MonoBehaviour, IDamageable
{
    public static PlayerTutorial instance;

    public GameObject cameraInScene;

    public int health;
    public float speed;

    public GameObject shield;
    public bool activeShield { get; private set; }

    public Image[] healthCount;
    public Sprite healthOn, healthOff;

    public BoxCollider _collider;

    public GameObject leftPose, rightPose, upPose, downPose;

    private Animator anim;
    private Coroutine coroutine;
    private Vector3 pos;

    [HideInInspector]
    public int money = 0;

    public float immortalTime;
    private bool immortality = false;

    private Coroutine falling;

    private void Awake()
    {
        CheckPlayerPrefs();
        anim = GetComponent<Animator>();
        instance = this;
        pos = transform.position;
        _collider.size = new Vector3(0.31f, 1f, 0.25f);
    }

    private void Start()
    {
        cameraInScene.GetComponent<Camera>().SetStart();
    }

    public void Move(Direction dir)
    {
        if (dir.Equals(Direction.Right))
        {
            if (transform.position.x == 1 || pos.x == 1)
                return;
            else
                pos.x += 1;
        }
        else
        {
            if (transform.position.x == -1 || pos.x == -1)
                return;
            else
                pos.x -= 1;
        }

        if (coroutine != null)
        {

            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveRight", false);
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(Moving(dir));
    }

    public void AcceptDamage()
    {
        /*
        if (!immortality)
        {
            if (vibrating) Handheld.Vibrate();
            SetImmortality();
            health -= 1;
            if (health <= 0)
            {
                health = 0;
                Death();
            }
            healthCount[health].sprite = healthOff;
            PlayerPrefs.SetInt("Hearts", health);
        }
        */
    }

    public void Slide()
    {
        anim.SetBool("Slide", true);
        _collider.center -= new Vector3(0, 0.5f, 0);
        _collider.size = new Vector3(0.31f, 1f, 0.25f);

        StartCoroutine(CoroutineHelper.WaitFor(0.5f, delegate ()
        {
            anim.SetBool("Slide", false);
        }));
        StartCoroutine(CoroutineHelper.WaitFor(1, delegate ()
        {
            _collider.center += new Vector3(0, 0.5f, 0);
            _collider.size = new Vector3(0.31f, 1f, 0.25f);
        }));
    }


    private IEnumerator Moving(Direction dir)
    {
        if (dir.Equals(Direction.Right))
            anim.SetBool("MoveRight", true);
        else
            anim.SetBool("MoveLeft", true);

        while (Vector3.Distance(transform.position, pos) > 0.04f)
        {
            Vector3 movement = dir.Equals(Direction.Right) ? new Vector3(speed, 0, 0) : new Vector3(-speed, 0, 0);
            transform.position += movement * Time.deltaTime;

            if (transform.position.x > 1 || transform.position.x < -1)
                break;

            yield return null;
        }
        transform.position = pos;

        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);
    }


    //pickups
    public void ActivateShield()
    {
        if (!shield.activeSelf)
            shield.SetActive(true);

    }

    public void IncreaseHP()
    {
        if (health < 3)
        {
            healthCount[health].sprite = healthOn;
            health += 1;
            PlayerPrefs.SetInt("Hearts", health);
        }
    }

    public void IncreaseMoney(int money)
    {
        this.money += money;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1);
    }


    //animations setting
    public void StartMoving()
    {
        anim.SetBool("StopMove", false);
        anim.SetBool("StartMove", true);
    }

    public void StopMove()
    {
        anim.SetBool("StartMove", false);
        anim.SetBool("StopMove", true);
    }

    public void SetLeftPose()
    {
        anim.SetBool("SetLeftPose", true);
        leftPose.SetActive(true);
        ColliderOff();
        StartCoroutine(CoroutineHelper.WaitFor(0.2f, delegate ()
        {
            anim.SetBool("SetLeftPose", false);
        }));

        StartCoroutine(CoroutineHelper.WaitFor(0.9f, delegate ()
        {
            SetEndPose();
        }));
    }

    public void SetRightPose()
    {
        anim.SetBool("SetRightPose", true);
        rightPose.SetActive(true);
        ColliderOff();

        StartCoroutine(CoroutineHelper.WaitFor(0.2f, delegate ()
        {
            anim.SetBool("SetRightPose", false);
        }));

        StartCoroutine(CoroutineHelper.WaitFor(0.9f, delegate ()
        {
            SetEndPose();
        }));
    }

    public void SetUpPose()
    {
        anim.SetBool("SetUpPose", true);
        upPose.SetActive(true);
        ColliderOff();

        StartCoroutine(CoroutineHelper.WaitFor(0.2f, delegate ()
        {
            anim.SetBool("SetUpPose", false);
        }));

        StartCoroutine(CoroutineHelper.WaitFor(0.9f, delegate ()
        {
            SetEndPose();
        }));
    }

    public void SetDownPose()
    {
        anim.SetBool("SetDownPose", true);
        downPose.SetActive(true);
        ColliderOff();
        StartCoroutine(CoroutineHelper.WaitFor(0.2f, delegate ()
        {
            anim.SetBool("SetDownPose", false);
        }));

        StartCoroutine(CoroutineHelper.WaitFor(0.7f, delegate ()
        {
            SetEndPose();
        }));
    }

    public void SetEndPose()
    {
        anim.SetBool("SetRightPose", false);
        anim.SetBool("SetLeftPose", false);
        anim.SetBool("SetUpPose", false);
        anim.SetBool("SetDownPose", false);
        anim.SetBool("EndPose", true);

        ColliderOn();

        if (leftPose.activeSelf) leftPose.SetActive(false);
        if (rightPose.activeSelf) rightPose.SetActive(false);
        if (upPose.activeSelf) upPose.SetActive(false);
        if (downPose.activeSelf) downPose.SetActive(false);

        StartCoroutine(CoroutineHelper.WaitFor(0.5f, delegate ()
        {
            anim.SetBool("EndPose", false);
        }));
    }

    public void SetVictory()
    {
        anim.SetBool("Victory", true);
    }


    public void Jump()
    {
        anim.SetBool("Jump", true);
        _collider.center += new Vector3(0, 0.5f, 0);
        _collider.size = new Vector3(0.31f, 1f, 0.25f);
        StartCoroutine(CoroutineHelper.WaitFor(0.5f, delegate ()
        {
            anim.SetBool("Jump", false);
        }));
        StartCoroutine(CoroutineHelper.WaitFor(1, delegate ()
        {
            _collider.center -= new Vector3(0, 0.5f, 0);
            _collider.size = new Vector3(0.31f, 1f, 0.25f);
        }));
    }

    public void Death()
    {
        TutorialController.instance.GameOver();
        StopMove();
    }

    public void Fall()
    {
        if (!immortality)
        {
            SwipeManagerTutorial.instance.enabled = false;

            falling = StartCoroutine(Falling());
            Death();
        }
    }

    public void StopFallingCoroutine()
    {
        if (falling != null)
            StopCoroutine(falling);
    }

    //other setting
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BridgeHole"))
        {
            TutorialController.instance.bridgeHole = true;
            health = 0;
            SwipeManagerTutorial.instance.enabled = false;
            PlayerPrefs.SetInt("Hearts", 0);
            foreach (var c in healthCount)
            {
                c.sprite = healthOff;
            }

            StartCoroutine(Falling());
            Death();
        }
        else if (other.CompareTag("Bridge"))
        {
            SwipeManagerTutorial.instance.SetFinish(true);
            transform.position = Vector3.forward;
            StartCoroutine(CoroutineHelper.WaitFor(1.05f, delegate ()
            {
                cameraInScene.GetComponent<Camera>().SetFinish();
                TutorialController.instance.ActivateCastles();
            }));
        }
        else if (other.CompareTag("Castle"))
        {
            SetEndPose();
        }
    }

    private IEnumerator Falling()
    {
        while (transform.position.y > -2)
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            yield return null;
        }
    }

    public void SetImmortality()
    {
        immortality = true;
        StartCoroutine(CoroutineHelper.WaitFor(0.8f, delegate ()
        {
            immortality = false;
        }));
    }

    private void CheckPlayerPrefs()
    {

        health = PlayerPrefs.GetInt("Hearts");
        for (int i = 0; i < health; i++)
        {
            healthCount[i].sprite = healthOn;
        }
        if (health == 0)
        {
            health = 1;
            healthCount[0].sprite = healthOn;
        }
    }

    private void ColliderOff()
    {
        _collider.enabled = false;
    }

    private void ColliderOn()
    {
        _collider.enabled = true;
    }
}
