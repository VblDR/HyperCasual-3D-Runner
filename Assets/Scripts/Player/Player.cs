using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [HideInInspector]
    public static Player instance;

    public int health;
    public float speed;

    public GameObject shield;

    public Text healthCount;

    public BoxCollider collider;

    private Animator anim;
    private Coroutine coroutine;
    private Vector3 pos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        instance = this;
        pos = transform.position;
    }

    private void Update()
    {
        if(health <= 0)
        {
            GameController.instance.GameOver();
            StopMove();
        }
    }

    public void Move(Direction dir)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(Moving(dir));
    }

    public void AcceptDamage()
    {
        health -= 1;
        healthCount.text = System.Convert.ToString(health);
    }

    private IEnumerator Moving(Direction dir)
    {

        if (dir.Equals(Direction.Right))
        {
            if (transform.position.x == 1 || pos.x == 1)
                yield break;
            else
                pos.x += 1;  
        }
        else
        {
            if (transform.position.x == -1 || pos.x == -1)
                yield break;
            else
                pos.x -= 1;
        }

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

    public void ActivateShield()
    {
        if(!shield.activeSelf)
            shield.SetActive(true);
    }

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

    public void Jump()
    {
        anim.SetBool("Jump", true);
        collider.center += new Vector3(0, 1, 0);
        StartCoroutine(CoroutineHelper.WaitFor(0.5f, delegate ()
        {
            anim.SetBool("Jump", false);
        }));
        StartCoroutine(CoroutineHelper.WaitFor(1, delegate ()
        {
            collider.center -= new Vector3(0, 1, 0);
        }));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hole"))
        {
            health = 0;
            healthCount.text = System.Convert.ToString(health);
            StartCoroutine(Falling());
        }
    }

    private IEnumerator Falling()
    {
        while(transform.position.y > -3)
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            yield return null;
        }
    }
}


public interface IDamageable
{
    public void AcceptDamage();
}