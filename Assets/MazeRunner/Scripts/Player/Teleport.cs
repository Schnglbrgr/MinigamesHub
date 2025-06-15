using System.Collections;
using TMPro;
using UnityEngine;

public class Teleport : MonoBehaviour, IInteractive
{
    [SerializeField] private GameObject teleportText;

    public float standingTime;
    private bool canTeleport;
    private bool isStanding;

    private GameManagerMazeRunner gameManagerMazeRunner;
    private GameObject player;

    private void Awake()
    {
        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>();

        player = GameObject.FindGameObjectWithTag("Player");

        canTeleport = true;
    }

    private void Update()
    {

        if (standingTime > 3)
        {
            TeleportPlayer();
        }
    }

    public void TeleportPlayer()
    {
        if (canTeleport)
        {
            standingTime = 0f;

            int selectTeleport = Random.Range(0, gameManagerMazeRunner.teleports.Length);

            player.transform.position = gameManagerMazeRunner.teleports[selectTeleport].transform.position;

            StopCoroutine(CountDownTeleport());

            teleportText.SetActive(false);

            canTeleport = false;

            StartCoroutine(CanTeleport());
        }
    }

    public void StartInteraction()
    {
        if (canTeleport)
        {
            isStanding = true;

            teleportText.SetActive(true);

            StartCoroutine(StandingTime());

            StartCoroutine(CountDownTeleport());
        }
    }

    public void ExitInteraction()
    {
        isStanding = false;

        standingTime = 0f;

        StopCoroutine(CountDownTeleport());

        teleportText.SetActive(false);

        StopCoroutine(StandingTime());

    }

    IEnumerator CountDownTeleport()
    {
        teleportText.GetComponentInChildren<TMP_Text>().text = "Teleporting: 1";

        yield return new WaitForSeconds(1);
        teleportText.GetComponentInChildren<TMP_Text>().text = "Teleporting: 2";

        yield return new WaitForSeconds(1);
        teleportText.GetComponentInChildren<TMP_Text>().text = "Teleporting: 3";
    }

    IEnumerator CanTeleport()
    {
        yield return new WaitForSeconds(5f);
        canTeleport = true;
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
