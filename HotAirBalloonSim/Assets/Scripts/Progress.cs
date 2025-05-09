using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Progress : MonoBehaviour
{
    GameObject[] buildings;
    int destroyedBuildings;
    public TMP_Text text;
    
    void Start()
    {
        buildings = GameObject.FindGameObjectsWithTag("Building");
        
    }

    // Update is called once per frame
    void Update()
    {
        int buildingCheck = 0;
        foreach (GameObject obj in buildings) {
            if (obj.GetComponent<Building>() != null) {
                if (obj.GetComponent<Building>().destroyed == true) {
                    buildingCheck++;
                }
            }
        }
        if (destroyedBuildings != buildingCheck) {
            destroyedBuildings = buildingCheck;
        }

        text.text = (int)(( destroyedBuildings * 100f ) / ( buildings.Length * 100f ) * 100f ) + "% Destroyed";
           // "77% Destroyed";
        if (destroyedBuildings == buildings.Length && buildings.Length > 0) {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
