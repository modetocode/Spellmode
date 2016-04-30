using UnityEngine;
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

    private GameData gameData;

    public void Awake() {
        if (this.usernameInputField == null) {
            throw new System.NullReferenceException("usernameInputField is null");
        }

        if (this.confirmButton == null) {
            throw new System.NullReferenceException("confirmButton is null");
        }

        this.confirmButton.onClick.AddListener(Login);
    }

    public void Start() {
        this.gameData = GameDataManager.LoadGameData();
        this.usernameInputField.text = gameData.LastLogonUsername;
    }

    public void Destroy() {
        this.confirmButton.onClick.RemoveListener(Login);
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

}
