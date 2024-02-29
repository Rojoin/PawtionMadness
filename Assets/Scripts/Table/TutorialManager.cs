using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private string[] texts;
    [SerializeField] private Table.Table[] interactions;
    [SerializeField] private Table.Table[] interactableTable;
    [SerializeField] private GameObject Player;
    [SerializeField] private Vector2ChannelSO playerMovementChannel;
    [SerializeField] private VoidChannelSO interactChannel;
    [SerializeField] private TMPro.TMP_Text tutorialText;
    [SerializeField] private int tutorialScreenCounter = 0;
    [SerializeField] private int interactableCounter = 0;

    [SerializeField] private float timeBetweenText = 0.2f;
    [SerializeField] private ArrowPointer arrowPointer;
    private void Awake()
    {
        _enemySpawner.enabled = false;
        foreach (Table.Table table in interactableTable)
        {
            table.InteractionState(false);
        }

        Player.SetActive(true);
        tutorialText.text = texts[tutorialScreenCounter];
        playerMovementChannel.Subscribe(ChangeToNextScene);
    }


    private void ChangeToNextScene(Vector2 dir)
    {
        playerMovementChannel.Unsubscribe(ChangeToNextScene);
        StartCoroutine(ChangeToNextScene());
    }

    private IEnumerator ChangeToNextScene()
    {
        float timer = 0.0f;
        while (timer < timeBetweenText)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0.0f;
        tutorialScreenCounter++;
        tutorialText.text = tutorialScreenCounter < texts.Length ? texts[tutorialScreenCounter] : "";
        if (interactableCounter < interactions.Length - 1)
        {
            interactions[interactableCounter].OnInteract.AddListener(ChangeScene);
            interactions[interactableCounter].InteractionState(true);
            arrowPointer.LookPosition = interactions[interactableCounter].transform.position;
            if (!arrowPointer.gameObject.activeSelf)
            {
                arrowPointer.gameObject.SetActive(true);
            }
        }
        else if (interactableCounter == interactions.Length - 1)
        {
            arrowPointer.gameObject.SetActive(false);
            interactChannel.Subscribe(FinalMessage);
        }
        else
        {
            while (timer < timeBetweenText *4)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            tutorialText.transform.parent.gameObject.SetActive(false);
        }
    }

//TODO: Change so it desactivates the collider of the object if no longer needed.
    private void ChangeScene()
    {
        interactions[interactableCounter].OnInteract.RemoveListener(ChangeScene);
        interactions[interactableCounter].InteractionState(false);
        interactableCounter++;
        StartCoroutine(ChangeToNextScene());
    }

    private void FinalMessage()
    {
        interactChannel.Unsubscribe(FinalMessage);
        ChangeScene();
        foreach (Table.Table table in interactableTable)
        {
            table.InteractionState(true);
        }

        _enemySpawner.enabled = true;
        _enemySpawner.gameObject.SetActive(true);
    }
}