using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For scene transitioning

public class InputFieldValidator : MonoBehaviour
{
    [Header("UI Components")]
    public InputField inputField1;
    public InputField inputField2;
    public Button submitButton;
    public GameObject incorrectText;
    public Button signUpButton;

    [Header("Validation Parameters")]
    public string validInput1 = "CorrectValue1";
    public string validInput2 = "CorrectValue2";

    [Header("Next Scene Settings")]
    public string nextSceneName; // Name of the scene to transition to

    private void Start()
    {
        // Ensure the button is interactable only when fields have text
        submitButton.interactable = false;

        // Listen for input field changes
        inputField1.onValueChanged.AddListener(delegate { CheckFields(); });
        inputField2.onValueChanged.AddListener(delegate { CheckFields(); });

        // Add button click event listener
        submitButton.onClick.AddListener(ValidateAndTransition);
        signUpButton.onClick.AddListener(ChangeValidation);
    }

    // Enable button only if both input fields are not empty
    private void CheckFields()
    {
        submitButton.interactable = !string.IsNullOrEmpty(inputField1.text) && !string.IsNullOrEmpty(inputField2.text);
    }

    private void ChangeValidation()
    {
        validInput1 = inputField1.text;
        validInput2 = inputField2.text;
    }

    // Validate inputs and transition to the next scene
    private void ValidateAndTransition()
    {
        if (inputField1.text == validInput1 && inputField2.text == validInput2)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            incorrectText.SetActive(true);
            Debug.Log("Invalid inputs");
        }
    }
}
