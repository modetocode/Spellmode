using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectComponent : MonoBehaviour {

    [SerializeField]
    private Text goldAmountText;

    private PlayerModel PlayerModel { get { return PlayerModel.Instance; } }

    public void Start() {
        this.PlayerModel.Initialize();
        this.goldAmountText.text = PlayerModel.Instance.PlayerGameData.GoldAmount.ToString();
    }

    public void OpenLevel() {
        SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
    }
}
