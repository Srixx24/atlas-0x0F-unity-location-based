using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraViewSwitch : MonoBehaviour
{
    public Button cameraSwitchButton;
    private int currentSceneIndex = 0;

    void Start()
    {
        cameraSwitchButton.onClick.AddListener(SwitchViews);
    }

    public void SwitchViews()
    {
        // Increment the scene index
        currentSceneIndex++;

        // Allow for scene looping
        if (currentSceneIndex > 2)
            currentSceneIndex = 0;

        switch (currentSceneIndex)
        {
            case 0:
                SceneManager.LoadScene("Follow View");
                break;
            case 1:
                SceneManager.LoadScene("Map View");
                break;
            case 2:
                SceneManager.LoadScene("AR View");
                break;
        }
    }
}