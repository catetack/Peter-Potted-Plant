using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    InputSystem_Actions inputActions;
    public GameObject menuList;

    [SerializeField] private bool menuKeys = true;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Pause.started += ctx=>Menu();
        inputActions.Player.Restart.started += ctx=>Restart();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }
    private void Update()
    {
        MenuKeyboard();
    }

    private void MenuKeyboard()
    {
        if (menuKeys)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(true);
                menuKeys = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuList.SetActive(false);
            menuKeys = true;
        }
    }

    

    private void Menu()
    {
        if (menuKeys)
        {
            
                menuList.SetActive(true);
                menuKeys = false;
            
        }
        else
        {
            menuList.SetActive(false);
            menuKeys = true;
        }
    }

    private void Restart()
    {
        if(!menuKeys)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}
