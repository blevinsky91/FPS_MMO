using UnityEngine;
using UnityEngine.UI;

public class CreateScene_Manager : MonoBehaviour
{
    public Canvas selectCharacterCanvas;
    public Canvas nameCharacterCanvas;
    public Canvas characterCustomizationCanvas;
    public GameObject characterUIs;
    public Button createCharacterButton;
    public Button characterPanel_1;
    public Button characterPanel_2;
    public Button characterPanel_3;
    public Button characterPanel_4;
    public Image highlighter_1;
    public Image highlighter_2;
    public Image highlighter_3;
    public Image highlighter_4;
    private Image chosenHighlighter;
    public CreateCharacter createCharacter; //referencing CreateCharacter class for SaveCharacterDetials() in ConfirmNameButton()
    public SavedCharacters savedCharacters; //referencing SavedCharacter class for RetrieveSavedCharacter() in Start()

    private void Start()
    {
        SetUI();
        StartCoroutine(savedCharacters.RetrieveSavedCharacters());
    }

    private void SetUI()
    {
        nameCharacterCanvas.enabled = false;
        characterCustomizationCanvas.enabled = false;
        createCharacterButton.interactable = false;
        highlighter_1.enabled = false;
        highlighter_2.enabled = false;
        highlighter_3.enabled = false;
        highlighter_4.enabled = false;
    }

    public void CreateCharacterButton()
    {
        selectCharacterCanvas.enabled = false;
        nameCharacterCanvas.enabled = true;
        characterCustomizationCanvas.enabled = false;
        characterUIs.gameObject.SetActive(false);
    }

    public void AssignHighligther(Image highlighter)
    {
        chosenHighlighter = highlighter;
    }

    internal int characterSlot; //1-4 which panel the character is being assigned to when created/loaded

    public void AssignCharacterSlot(int slot)
    {
        characterSlot = slot;
        Debug.Log("Chracter Slot assigned: " + slot);

        if (createCharacterButton.interactable == false)
        {
            createCharacterButton.interactable = true;
            chosenHighlighter.enabled = true;
        }
        else
        {
            createCharacterButton.interactable = false;
            chosenHighlighter.enabled = false;
        }
    }

    public void ConfirmNameButton()
    {
        nameCharacterCanvas.enabled = false;
        characterCustomizationCanvas.enabled = true;
        characterUIs.SetActive(true);
        StartCoroutine(createCharacter.SaveCharacterDetails());
    }
}
