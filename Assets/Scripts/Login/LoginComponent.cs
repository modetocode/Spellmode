using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controller for the login scene 
/// </summary>
public class LoginComponent : MonoBehaviour {
    [SerializeField]
    private InputField usernameInputField;
    [SerializeField]
    private Button confirmButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private YesNoPopupComponent yesNoPopupComponent;

    private GameData gameData;

    public void Awake() {
        if (this.usernameInputField == null) {
            throw new System.NullReferenceException("usernameInputField is null");
        }

        if (this.confirmButton == null) {
            throw new System.NullReferenceException("confirmButton is null");
        }

        if (this.exitButton == null) {
            throw new System.NullReferenceException("exitButton is null");
        }

        if (this.yesNoPopupComponent == null) {
            throw new System.NullReferenceException("yesNoPopupComponent is null");
        }

        this.confirmButton.onClick.AddListener(Login);
        this.exitButton.onClick.AddListener(ShowYesNoMessageForExitApplication);
    }

    public void Start() {
        this.gameData = GameDataManager.LoadGameData();
        this.usernameInputField.text = gameData.LastLogonUsername;
    }

    public void Destroy() {
        this.confirmButton.onClick.RemoveListener(Login);
        this.exitButton.onClick.RemoveListener(ShowYesNoMessageForExitApplication);
    }

    private void Login() {
        string username = this.usernameInputField.text.ToLower();

        if (username.Equals(string.Empty)) {
            return;
        }

        this.gameData.LastLogonUsername = username;
        GameDataManager.SaveGameData(this.gameData);
        PlayerModel.Instance.Initialize(username);
        SceneManager.LoadScene(Constants.Scenes.LevelSelectSceneName);
    }

    private void ShowYesNoMessageForExitApplication() {
        UnityAction onYesClickedAction = this.ExitApplication;
        this.yesNoPopupComponent.Show(Constants.Strings.ExitApplicationMessage, onYesClickedAction);
    }

    private void ExitApplication() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
