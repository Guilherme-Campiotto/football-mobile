using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsShop : MonoBehaviour
{
    public static BallsShop instance;
    public List<Ball> balls = new List<Ball>();
    public List<GameObject> ballSuportList = new List<GameObject>();
    public List<GameObject> buyButtonsList = new List<GameObject>();
    public GameObject baseItem;
    public Transform content;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        FillList();
    }

    void Update()
    {
        
    }

    void FillList()
    {
        foreach(Ball ball in balls)
        {
            GameObject ballItem = Instantiate(baseItem) as GameObject;
            ballItem.transform.SetParent(content, false);
            BallsSuport item = ballItem.GetComponent<BallsSuport>();
            item.ballId = ball.ballId;
            item.ballPrice.text = ball.price.ToString();
            item.buyButton.GetComponent<BuyBall>().ballIdClicked = ball.ballId;

            buyButtonsList.Add(item.buyButton);

            ballSuportList.Add(ballItem);

            if(PlayerPrefs.GetInt("BTN"+ item.ballId) == 1)
            {
                ball.sold = true;
            }

            if (PlayerPrefs.HasKey("BTNS" + item.ballId) && ball.sold)
            {
                item.buyButton.GetComponent<BuyBall>().buttonText.text = PlayerPrefs.GetString("BTNS" + item.ballId);
            }

            if (ball.sold)
            {
                item.ballSprite.sprite = Resources.Load<Sprite>("Sprites/Contries/" + ball.name);
                item.ballPrice.text = "Sold out!";

                if(PlayerPrefs.HasKey("BTNS" + item.ballId) == false)
                {
                    item.buyButton.GetComponent<BuyBall>().buttonText.text = "Using";
                }
            } else
            {
                item.ballSprite.sprite = Resources.Load<Sprite>("Sprites/Contries/" + ball.name + "_cinza");
            }
        }
    }
    public void SpriteUpdate(int ballId)
    {
        foreach(GameObject item in ballSuportList)
        {
            BallsSuport ballSuport = item.GetComponent<BallsSuport>();

            if(ballSuport.ballId == ballId)
            {
                foreach(Ball ball in balls)
                {
                    if(ball.ballId == ballId)
                    {
                        if(ball.sold)
                        {
                            ballSuport.ballSprite.sprite = Resources.Load<Sprite>("Sprites/Contries/" + ball.name);
                            ballSuport.ballPrice.text = "SOLD OUT!";
                            SaveBallPurchased(ballSuport.ballId);
                        } else
                        {
                            ballSuport.ballSprite.sprite = Resources.Load<Sprite>("Sprites/Contries/" + ball.name + "_cinza");
                        }
                    }
                }
            }
        }
    }
    void SaveBallPurchased(int ballId)
    {
        for(int i = 0; i < balls.Count; i++)
        {
            BallsSuport ballSuport = ballSuportList[i].GetComponent<BallsSuport>();
            
            if(ballSuport.ballId == ballId)
            {
                PlayerPrefs.SetInt("BTN" + ballSuport.ballId, ballSuport.buyButton ? 1 : 0);
            }
        }
    }

    public void SaveBallSelected(int ballId, string name)
    {
        for (int i = 0; i < balls.Count; i++)
        {
            BallsSuport ballSuport = ballSuportList[i].GetComponent<BallsSuport>();

            if (ballSuport.ballId == ballId)
            {
                PlayerPrefs.SetString("BTNS" + ballSuport.ballId, name);
            }
        }
    }
}


