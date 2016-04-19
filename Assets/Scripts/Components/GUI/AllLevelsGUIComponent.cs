using UnityEngine;
using UnityEngine.SceneManagement;

public class AllLevelsGUIComponent : MonoBehaviour {

    public void OpenLevel() {
        SceneManager.LoadScene(Constants.Scenes.LevelRunSceneName);
    }
}
