using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//<summary> This script is used for the Game Over Trigger! - Zander :3 </summary>
public class GameOverTrigger : MonoBehaviour
{
    private SpriteRenderer sr;

    private bool isTriggerStay;
    public int currentCountDown = 0;
    public int maxCountDown = 5;

    public GameObject floor;
    public GameObject frontFloorSprite;
    public Image gameOverScreen;
    public GameObject scoreSign;
    public GameObject gameOverUI;
    public GameObject pauseButton;
    public float fadeOutTime;

    private ServiceHub serviceHub;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        isTriggerStay = false;
        serviceHub = ServiceHub.Instance;
        pauseButton.SetActive(true);

        StartCoroutine(CheckTrigger());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isTriggerStay = true;
        sr.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerStay = false;
        sr.enabled = false;

        StartCoroutine(QuickCheckTrigger());
    }

    IEnumerator CheckTrigger()
    {
        while (true)
        {
            if (!isTriggerStay)
            {
                currentCountDown = 0;
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(1f);

            if (isTriggerStay) currentCountDown++;

            if (currentCountDown >= maxCountDown)
            {
                serviceHub.AudioManager.musicAudioSource.Stop();
                pauseButton.SetActive(false);
                StartCoroutine(FadeImage());
                floor.SetActive(false);
                Camera.main.GetComponent<Animator>().enabled = true;
                serviceHub.Spawner.GameOver();
                serviceHub.UIManager.isGameOver = true;
                yield break;
            }
        }
    }

    IEnumerator QuickCheckTrigger() //Used to reset counter when collider leaves
    {
        yield return new WaitForSeconds(.1f);

        if(!isTriggerStay) currentCountDown = 0;
    }

    IEnumerator FadeImage()
    {
        float alpha = 1f;

        while(alpha > 0f)
        {
            yield return new WaitForSeconds(fadeOutTime);

            alpha -= .1f;

            frontFloorSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        }
        frontFloorSprite.SetActive(false);
        scoreSign.SetActive(false);

        alpha = 0f;

        while(alpha < 1f)
        {
            yield return new WaitForSeconds(fadeOutTime);

            alpha += .1f;

            gameOverScreen.color = new Color(1, 1, 1, alpha);
        }
        gameOverUI.SetActive(true);
        serviceHub.AudioManager.PlayGameOver();
    }
}