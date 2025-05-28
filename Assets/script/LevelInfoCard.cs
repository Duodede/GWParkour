using Newtonsoft.Json.Bson;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoCard : MonoBehaviour
{
    public RawImage image;
    public Text levelNameText;
    public Text Id;
    public Transform content;
    public string levelName;
    public SaveManager saveManager;
    public GameManager gameManager;
    private void Start()
    {
        saveManager = Manager.saveManager;
        gameManager = Manager.manager;
    }
    public void EnterThisLevel()
    {
        saveManager.fileName = levelName;
        saveManager.nameInput.text = levelName;
        gameManager.SetMenuActive(false);
        saveManager.Load();
    }
    public void DeleteThisLevel()
    {
        gameManager.deleteConfirmNotice.SetActive(true);
        saveManager.fileToDelete = levelName;
    }
}
