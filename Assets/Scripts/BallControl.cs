using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    private Transform wallLeft;
    private Transform wallRight;
    public float zPosition;
    public bool canRotate = false;
    public bool canShoot = false;
    public GameObject arrowObject;

    private Rigidbody2D ballBody;
    public float force = 0f;
    public GameObject arrowGreenObject;
    public GameObject balldeathEffect;

    private void Awake()
    {
        arrowObject = GameObject.Find("Arrow");
        arrowGreenObject = arrowObject.transform.GetChild(0).gameObject;
        arrowObject.GetComponent<Image>().enabled = false;
        arrowGreenObject.GetComponent<Image>().enabled = false;
        wallLeft = GameObject.Find("WallLeft").GetComponent<Transform>();
        wallRight = GameObject.Find("WallRight").GetComponent<Transform>();
    }
    void Start()
    {
        ballBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ArrowRotation();
        InputRotation();
        LimitRotation();
        PosicionateArrow();

        ApplyForce();
        ControlForce();
        WallCheck();
    }

    void PosicionateArrow()
    {
        arrowObject.GetComponent<Image>().rectTransform.position = transform.position;
    }

    void ArrowRotation()
    {
        arrowObject.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zPosition);
    }

    void InputRotation()
    {

        if (canRotate)
        {
            float moveX = Input.GetAxis("Mouse X");
            float moveY = Input.GetAxis("Mouse Y");

            if (zPosition < 90)
            {
                if (moveY > 0)
                {
                    zPosition += 2.5f;
                }
            }

            if (zPosition > 0)
            {
                if (moveY < 0)
                {
                    zPosition -= 2.5f;
                }
            }

        }

    }

    void LimitRotation()
    {
        if (zPosition >= 90)
        {
            zPosition = 90;
        }

        if (zPosition <= 0)
        {
            zPosition = 0;
        }
    }

    private void OnMouseDown()
    {
        if(GameManager.instance.shoot == 0)
        {
            canRotate = true;
            arrowObject.GetComponent<Image>().enabled = true;
            arrowGreenObject.GetComponent<Image>().enabled = true;
        }
    }

    private void OnMouseUp()
    {
        canRotate = false;
        arrowObject.GetComponent<Image>().enabled = false;
        arrowGreenObject.GetComponent<Image>().enabled = false;

        if(GameManager.instance.shoot == 0 && force > 0)
        {
            AudioManager.instance.PlaySoundEffect(1);
            canShoot = true;
            GameManager.instance.shoot = 1;
            arrowGreenObject.GetComponent<Image>().fillAmount = 0;
        }

    }

    void ApplyForce()
    {

        if (canShoot)
        {
            float forceX = force * Mathf.Cos(zPosition * Mathf.Deg2Rad);
            float forceY = force * Mathf.Sin(zPosition * Mathf.Deg2Rad);
            canShoot = false;
            ballBody.AddForce(new Vector2(forceX, forceY));
        }
    }

    void makeBallDynamic()
    {
        ballBody.isKinematic = false;
    }

    void ControlForce()
    {
        if (canRotate)
        {
            float moveX = Input.GetAxis("Mouse X");

            if (moveX < 0)
            {
                arrowGreenObject.GetComponent<Image>().fillAmount += 0.8f * Time.deltaTime;
                force = arrowGreenObject.GetComponent<Image>().fillAmount * 700;
            }

            if (moveX > 0)
            {
                arrowGreenObject.GetComponent<Image>().fillAmount -= 0.8f * Time.deltaTime;
                force = arrowGreenObject.GetComponent<Image>().fillAmount * 700;
            }
        }
    }

    void WallCheck()
    {
        if(gameObject.transform.position.x > wallRight.position.x)
        {
            Destroy(gameObject);
            GameManager.instance.ballInsideScene -= 1;
            GameManager.instance.ballsNumber -= 1;
        }

        if (gameObject.transform.position.x < wallLeft.position.x)
        {
            Destroy(gameObject);
            GameManager.instance.ballInsideScene -= 1;
            GameManager.instance.ballsNumber -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && !GameManager.instance.win)
        {
            Instantiate(balldeathEffect, transform.position, Quaternion.identity);
            GameManager.instance.ballsNumber -= 1;
            Destroy(gameObject);
            GameManager.instance.ballInsideScene -= 1;
        } else if(collision.gameObject.CompareTag("Goal"))
        {
            GameManager.instance.win = true;
            int level = CurrentPosition.instance.level + 1;
            level++;
            PlayerPrefs.SetInt("Level"+ level, 1);
        }
    }
}
