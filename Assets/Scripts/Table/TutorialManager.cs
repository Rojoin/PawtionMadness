using System;
using System.Collections;
using Table;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private string[] texts;
    [SerializeField] private Table.Table[] interactions;
    [SerializeField] private Table.Table[] interactableTable;
    [SerializeField] private Vector2ChannelSO playerMovementChannel;
    [SerializeField] private VoidChannelSO onTurretPlaced;
    [SerializeField] private TMP_Text kitchenTutorialText;
    [SerializeField] private int tutorialScreenCounter = 0;
    [SerializeField] private int interactableCounter = 0;
    [SerializeField] private ControlPointer controlPointerPlayerMovement;

    public UnityEvent OnTutorialEnd;
    [SerializeField] private float timeBetweenText = 0.2f;
    [SerializeField] private ArrowPointer arrowPointer;
    private Coroutine changingTutorialInteractions;
    [SerializeField] private float secondsUntilMovementTutorialFades = 5.0f;

    private Table.Table currentInteractable;
    private Table.Table previousInteractable;
    [SerializeField] private BaseTurretSO turretForTutorial;
    [SerializeField] private Grid.GridController grid;
    [Header("Text Settings")]
    [SerializeField] private RectTransform textPositionInGrid;
    [SerializeField] private float timeBetweenChars = 20;
    private RectTransform parentTextRectTransform;
    private Vector2 defaultPosition;
    private int _currentVisibleCharactersIndex;
    private Coroutine textBeingWritten;
    private WaitForSeconds _textLetterDelay;

    private void Awake()
    {
        if (turretForTutorial)
        {
            grid.SpawnDefaultTurret(turretForTutorial);
        }

        parentTextRectTransform = kitchenTutorialText.transform.parent.GetComponent<RectTransform>();
        parentTextRectTransform.gameObject.SetActive(true);
        defaultPosition = parentTextRectTransform.anchoredPosition;
        _textLetterDelay = new WaitForSeconds(timeBetweenChars);

        foreach (Table.Table table in interactableTable)
        {
            table.InteractionState(false);
        }

        SetText(texts[tutorialScreenCounter], kitchenTutorialText);
        playerMovementChannel.Subscribe(ChangeToNextScene);
        controlPointerPlayerMovement.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        OnTutorialEnd.RemoveAllListeners();
        playerMovementChannel.Unsubscribe(ChangeToNextScene);
        StopAllCoroutines();
        previousInteractable = null;
        foreach (Table.Table interaction in interactions)
        {
            RemoveEventInteraction(interaction);

            previousInteractable = interaction;
        }

        onTurretPlaced.Unsubscribe(FinalMessage);
    }

    private void RemoveEventInteraction(Table.Table interaction)
    {
        interaction.OnInteract.RemoveAllListeners();
        if (interaction is CutTable && interaction == previousInteractable)
        {
            interaction.OnItemPickUp.RemoveAllListeners();
        }
        else if (interaction is GridTableController controller && controller == previousInteractable)
        {
            controller.OnBackInput.Unsubscribe(ChangeScene);
            if (parentTextRectTransform)
            {
                parentTextRectTransform.anchoredPosition = defaultPosition;
            }

            interaction._controlPointer.UseAlternativeImages = false;
        }
        else if (interaction is Cauldron cauldron && cauldron == previousInteractable)
        {
            cauldron.OnItemPickUp.RemoveAllListeners();
        }
        else if (interaction is BookTable book && book == previousInteractable)
        {
            book.OnItemDrop.RemoveAllListeners();
        }
    }

    private void SetText(string text, TMP_Text textBox)
    {
        textBox.text = text;
        textBox.maxVisibleCharacters = 0;
        _currentVisibleCharactersIndex = 0;
        if (textBeingWritten != null)
        {
            StopCoroutine(textBeingWritten);
        }

        textBeingWritten = StartCoroutine(WrittingText(textBox));
    }

    private IEnumerator WrittingText(TMP_Text textBox)
    {
        yield return null;
        TMP_TextInfo textInfo = textBox.textInfo;
        while (_currentVisibleCharactersIndex < textInfo.characterCount)
        {
            char character = textInfo.characterInfo[_currentVisibleCharactersIndex].character;
            textBox.maxVisibleCharacters++;
            yield return _textLetterDelay;
            _currentVisibleCharactersIndex++;
        }
    }

    private void ChangeToNextScene(Vector2 dir)
    {
        playerMovementChannel.Unsubscribe(ChangeToNextScene);
        changingTutorialInteractions = StartCoroutine(ChangeToNextScene());
        StartCoroutine(DeactivateMovementPointer());
    }

    private IEnumerator DeactivateMovementPointer()
    {
        yield return new WaitForSeconds(secondsUntilMovementTutorialFades);
        controlPointerPlayerMovement.gameObject.SetActive(false);
    }

    private IEnumerator ChangeToNextScene()
    {
        float timer = 0.0f;
        tutorialScreenCounter++;
        string textToBeDisplayed = tutorialScreenCounter < texts.Length ? texts[tutorialScreenCounter] : "";
        SetText(textToBeDisplayed, kitchenTutorialText);

        if (interactableCounter < interactions.Length - 1)
        {
            currentInteractable = interactions[interactableCounter];
            previousInteractable = interactableCounter - 1 < 0 ? null : interactions[interactableCounter - 1];

            AddEventInteraction();

            if (currentInteractable._controlPointer)
            {
                currentInteractable._controlPointer.gameObject.SetActive(true);
            }

            currentInteractable.InteractionState(true);
            arrowPointer.LookPosition = currentInteractable.transform.position;
            if (!arrowPointer.gameObject.activeSelf)
            {
                arrowPointer.gameObject.SetActive(true);
            }
        }
        else if (interactableCounter == interactions.Length - 1)
        {
            if (currentInteractable is GridTableController)
            {
                arrowPointer.gameObject.SetActive(false);
                parentTextRectTransform.anchoredPosition = textPositionInGrid.anchoredPosition;
                onTurretPlaced.Subscribe(FinalMessage);
            }
            else
            {
                currentInteractable = interactions[interactableCounter];
                Debug.Log(currentInteractable.name);
                currentInteractable.OnInteract.AddListener(FinalMessage);

                currentInteractable.InteractionState(true);
                arrowPointer.LookPosition = currentInteractable.transform.position;
                if (currentInteractable._controlPointer)
                {
                    currentInteractable._controlPointer.gameObject.SetActive(true);
                }

                if (!arrowPointer.gameObject.activeSelf)
                {
                    arrowPointer.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            while (timer < timeBetweenText * 4)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            arrowPointer.gameObject.SetActive(false);
            OnTutorialEnd.Invoke();
            parentTextRectTransform.gameObject.SetActive(false);
        }
    }

    private void AddEventInteraction()
    {
        if (currentInteractable is CutTable && currentInteractable == previousInteractable)
        {
            currentInteractable.OnItemPickUp.AddListener(ChangeScene);
        }
        else if (currentInteractable is GridTableController controller && controller == previousInteractable)
        {
            controller.OnBackInput.Subscribe(ChangeScene);
            parentTextRectTransform.anchoredPosition = textPositionInGrid.anchoredPosition;
            currentInteractable._controlPointer.UseAlternativeImages = true;
        }
        else if (currentInteractable is Cauldron cauldron && cauldron == previousInteractable)
        {
            cauldron.OnItemPickUp.AddListener(ChangeScene);
        }
        else if (currentInteractable is BookTable book && book == previousInteractable)
        {
            book.OnItemDrop.AddListener(ChangeScene);
        }
        else
        {
            currentInteractable.OnInteract.AddListener(ChangeScene);
        }
    }


    private void ChangeScene()
    {
        RemoveEventInteraction(currentInteractable);

        if (currentInteractable._controlPointer)
        {
            currentInteractable._controlPointer.gameObject.SetActive(false);
        }

        if (currentInteractable is GridTableController)
        {
            currentInteractable.InteractionState(interactableCounter == interactions.Length - 2 ? true : false);
        }
        else
        {
            currentInteractable.InteractionState(interactableCounter == interactions.Length ? true : false);
        }

        interactableCounter++;

        if (changingTutorialInteractions != null)
        {
            StopCoroutine(changingTutorialInteractions);
        }

        changingTutorialInteractions = StartCoroutine(ChangeToNextScene());
    }

    private void FinalMessage()
    {
        if (currentInteractable is GridTableController)
        {
            onTurretPlaced.Unsubscribe(FinalMessage);
        }
        else
        {
            currentInteractable.OnInteract.RemoveListener(FinalMessage);
            arrowPointer.gameObject.SetActive(false);
        }

        ChangeScene();
        foreach (Table.Table table in interactableTable)
        {
            table.InteractionState(true);
        }
    }
}