using UnityEngine;
using UnityEngine.UI; // Required for accessing the Button component
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // private int clicks = 0;
    public void OnClick()
    {
        // Debug.Log("click" + (++clicks));
        // Add your button functionality here
        SceneManager.LoadScene("Mountain");
    }
}
