using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopComponent : MonoBehaviour {

    public void GoToLevelSelect() {
        SceneManager.LoadScene(Constants.Scenes.LevelSelectSceneName);
    }
}
