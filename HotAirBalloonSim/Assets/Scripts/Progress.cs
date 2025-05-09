using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Progress : MonoBehaviour
{
    GameObject[] buildings;
    float destroyedBuildings;
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
                if (obj.GetComponent<Building>().destroyed == false) {
                    buildingCheck++;
                }
            }
        }
        if (destroyedBuildings != buildingCheck) {
            destroyedBuildings = buildingCheck;
        }
        
        text.text = (int)( 100 - destroyedBuildings/buildings.Length * 100) + "% Destroyed";
        if (destroyedBuildings == buildings.Length) {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
