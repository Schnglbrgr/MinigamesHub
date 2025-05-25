using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{

    [SerializeField] private GameObject[] teleports;
    [SerializeField] private GameObject teleportText;

    private CollectWeapon collectWeapon;
    private GameObject currentPlayer;
    private GameObject newPlayer;

    public float standingTime;
    private bool isStanding;
    private float timer;
    private float coolDown = 2f;

    private void Awake()
    {
        currentPlayer = gameObject;

        collectWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectWeapon>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void TeleportPlayer()
    {
        standingTime = 0f;

        timer = coolDown;

        int selectTeleport = Random.Range(0, teleports.Length);

        GetComponent<MovementSystem>().enabled = false;

        newPlayer = Instantiate(currentPlayer, teleports[selectTeleport].transform.position, Quaternion.identity);

        collectWeapon.currentWeapon.transform.SetParent(newPlayer.transform);

        Destroy(currentPlayer);

        StartCoroutine(TeleportWait());

        StopCoroutine(CountDownTeleport());

        teleportText.SetActive(false);

    }

    IEnumerator TeleportWait()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<MovementSystem>().enabled = true;
        currentPlayer = newPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport")
        {
            isStanding = true;

            teleportText.SetActive(true);

            StartCoroutine(StandingTime());

            StartCoroutine(CountDownTeleport());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport" && timer <= 0)
        {
            isStanding = false;

            standingTime = 0f;

            StopCoroutine(CountDownTeleport());

            teleportText.SetActive(false);

            StopCoroutine(StandingTime());

        }
    }

    IEnumerator CountDownTeleport()
    {
        teleportText.GetComponentInChildren<TMP_Text>().text = "Teleporting: 1";

        yield return new WaitForSeconds(1);
        teleportText.GetComponentInChildren<TMP_Text>().text = "Teleporting: 2";

        yield return new WaitForSeconds(1);
        teleportText.GetComponentInChildren<TMP_Text>().text = "Teleporting: 3";

    }

    IEnumerator StandingTime()
    {
        while (isStanding)
        {
            standingTime += Time.deltaTime;
            yield return null;
        }
    }
}
