using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    // Load the game scene
    public void LoadLevel(string name) {
        Debug.Log("Level load requested for: " + name);
        ResetSceneVariables();
        SceneManager.LoadScene(name);
    }

    public void QuitRequest() {
        Debug.Log("Quit requested.");
        Application.Quit();
    }
    
    public void BrickDestroyed() {
        LoadNextLevel();
    }

    public void LoadNextLevel() {
        ResetSceneVariables();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ResetSceneVariables() {
        
    }
}
