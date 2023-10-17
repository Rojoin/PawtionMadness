using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private string[] texts;
    [SerializeField] private Table.Table[] interactions;
    [SerializeField] private GameObject Player;
    [SerializeField] private Vector2ChannelSO playerMovementChannel;
    [SerializeField] private VoidChannelSO interactChannel;
    [SerializeField] private TMPro.TMP_Text tutorialText;
    private int tutorialScreenCounter = 0;
    private int interactableCounter = 0;
   
    [SerializeField] private float timeBetweenText = 0.2f;

    private void Awake()
    {
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
        tutorialText.text = texts[tutorialScreenCounter];
        if (interactableCounter < interactions.Length)
        {
            interactions[interactableCounter].OnInteract.AddListener(ChangeScene);
        }
        else if (interactableCounter == interactions.Length)
        {
            interactChannel.Subscribe(FinalMessage);
        }
        else
        {
            while (timer < timeBetweenText)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            tutorialText.transform.parent.gameObject.SetActive(false);
        }
    }
//TODO: Change so it desactivates the collider of the object if no longer needed.
    public void ChangeScene()
    {
        interactions[interactableCounter].OnInteract.RemoveListener(ChangeScene);
        interactableCounter++;
        StartCoroutine(ChangeToNextScene());
    }
    private void FinalMessage()
    {
        interactChannel.Unsubscribe(FinalMessage);
        ChangeScene();
    }
    
}