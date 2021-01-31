using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBall : MonoBehaviour
{
    public int ballIdClicked;
    public Text buttonText;
    public Animator noMoneyAnimator;

    public void BuyBallButton()
    {
        foreach(Ball ball in BallsShop.instance.balls)
        {
            if(ball.ballId == ballIdClicked && !ball.sold && PlayerPrefs.GetInt("coinsSaved") >= ball.price )
            {
                ball.sold = true;
                UpdateBuyOption();
                ScoreManager.instance.LoseCoin(ball.price);
                GameObject.Find("TextCoins").GetComponent<Text>().text = PlayerPrefs.GetInt("coinsSaved").ToString();
            } else if(ball.ballId == ballIdClicked && !ball.sold && PlayerPrefs.GetInt("coinsSaved") < ball.price)
            {
                noMoneyAnimator = GameObject.FindGameObjectWithTag("NoMoney").GetComponent<Animator>();
                noMoneyAnimator.Play("NoMoney");
                GameObject.Find("ButtonCloseMessage").GetComponent<Button>().onClick.AddListener(CloseNoMoneyMessage);

            }
            else if(ball.ballId == ballIdClicked && ball.sold)
            {
                UpdateBuyOption();
            }
        }

        BallsShop.instance.SpriteUpdate(ballIdClicked);
    }

    public void CloseNoMoneyMessage()
    {
        noMoneyAnimator = GameObject.FindGameObjectWithTag("NoMoney").GetComponent<Animator>();
        noMoneyAnimator.Play("NoMoneyReverse");
    }

    private void UpdateBuyOption()
    {
        buttonText.text = "Using";

        foreach (GameObject button in BallsShop.instance.buyButtonsList)
        {
            BuyBall buyBall = button.GetComponent<BuyBall>();

            foreach(Ball ball in BallsShop.instance.balls)
            {
                if(ball.ballId == buyBall.ballIdClicked)
                {
                    BallsShop.instance.SaveBallSelected(buyBall.ballIdClicked, "Using");

                    if(ball.ballId == buyBall.ballIdClicked && ball.sold && ball.ballId == ballIdClicked)
                    {
                        CurrentPosition.instance.ballSelected = buyBall.ballIdClicked;
                        PlayerPrefs.SetInt("BallSelected", buyBall.ballIdClicked);
                    }
                }

                if(ball.ballId == buyBall.ballIdClicked && ball.sold && ball.ballId != ballIdClicked)
                {
                    buyBall.buttonText.text = "Use";
                    BallsShop.instance.SaveBallSelected(buyBall.ballIdClicked, "Use");
                }
            }
        }
    }

}
