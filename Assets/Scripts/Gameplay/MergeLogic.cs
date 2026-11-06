using System.Collections;
using UnityEngine;

//<summary> This script is used for the merging of Pubbies! - Zander :3 </summary>
public class MergeLogic : MonoBehaviour
{
    //Big to Small Pubby (0 = Biggest, 9 = Smallest)
    private string[] pubbyTags = { "Wolf", "Samo", "Rott", "Husky", "Golden", "Beagle", "Shiba", "Dasch", "Pom", "Malt" };
    public float[] pubbyRadius = { 2.5f, 2.2f, 1.85f, 1.55f, 1.25f, 1.1f, .82f, .65f, .48f, .32f };
    private int[] pubbyScore = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
    public Sprite[] pubbySprites;
    public Sprite[] EvilPubbySprites;
    private int currentPubbyIndex;

    private ServiceHub serviceHub;

    public float expandSpeed;
    public bool isEvilPubby = false;

    private ParticleSystem greenAura;

    private void Start()
    {
        serviceHub = ServiceHub.Instance;

        greenAura = gameObject.GetComponentInChildren<ParticleSystem>();
        greenAura.Stop();

        if (serviceHub.Spawner.isEvilPubby)
        {
            isEvilPubby = true;
            greenAura.Play();
        }

        currentPubbyIndex = serviceHub.Spawner.currentPubby;
        UpdatePubbyInformation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Floor")) //Makes sure the Pubby doesn't just roll all the time
        {
            float direction = Random.Range(-1f, 1f);
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * 0.5f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) //For Collision between Pubbies
    {
        if (collision.gameObject.CompareTag(gameObject.tag)) //Makes sure tags match
        {
            if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID()) //This makes sure the method is only called once for each collision
            {
                Destroy(collision.gameObject); //Destroys the other Pubby
                serviceHub.GameManager.AddScore(pubbyScore[currentPubbyIndex]);
                UpgradePubby();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava")) Destroy(gameObject);
    }

    private void UpgradePubby()
    {
        isEvilPubby = false;
        greenAura.Stop();
        currentPubbyIndex--;
        if(currentPubbyIndex < 0)
        {
            serviceHub.AudioManager.PlaySuccess();
            Destroy(gameObject);
        }
        else UpdatePubbyInformation();
    }

    private void UpdatePubbyInformation()
    {
        tag = pubbyTags[currentPubbyIndex];
        gameObject.GetComponent<CircleCollider2D>().radius = pubbyRadius[currentPubbyIndex];
        if (isEvilPubby)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = EvilPubbySprites[currentPubbyIndex];
            StartCoroutine(ExpandRadius());
        }
        else gameObject.GetComponent<SpriteRenderer>().sprite = pubbySprites[currentPubbyIndex];
        
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = currentPubbyIndex;
    }

    IEnumerator ExpandRadius()
    {
        ParticleSystem.ShapeModule shape = greenAura.shape;
        while (isEvilPubby)
        {
            yield return new WaitForSeconds(expandSpeed);
            gameObject.GetComponent<CircleCollider2D>().radius += .01f;
            shape.radius += .01f;
        }
    }
}