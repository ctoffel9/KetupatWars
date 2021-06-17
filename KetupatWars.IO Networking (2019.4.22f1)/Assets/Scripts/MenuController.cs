using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersName = "0,1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject RoomPanel;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject ConnectPanel;
    [SerializeField] private GameObject ExitPanel;
    [SerializeField] private GameObject CreditPanel;
    [SerializeField] private GameObject TutorPanel;

    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject MenuButtons;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersName);
    }

    private void Start()
    {
        UsernameMenu.SetActive(true);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");

    }

    public void ChangeUserNameInput()
    {
        if(UsernameInput.text.Length >= 3)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUserName()
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.playerName = UsernameInput.text;
    }
    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { maxPlayers = 20 }, null);
    }
    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }
    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }

   
    public void CreateJoinSelection()
    {
        RoomPanel.SetActive(true);
    }
    public void CreateJoinClose()
    {
        RoomPanel.SetActive(false);
    }
    public void SettingMenuOpen()
    {
        SettingPanel.SetActive(true);
    }
    public void SettingMenuClose()
    {
        SettingPanel.SetActive(false);
    }
    public void MainMenuOpen()
    {
        MenuButtons.SetActive(true);
    }
    public void ExitMenuOpen()
    {
        ExitPanel.SetActive(true);
    }   
    public void ExitMenuClose()
    {
        ExitPanel.SetActive(false);
    }
    public void OpenCredit()
    {
        CreditPanel.SetActive(true);
    }

    public void CloseCredit()
    {
        CreditPanel.SetActive(false);
    }

    public void TutorialOpen()
    {
        TutorPanel.SetActive(true);
    }

    public void TutorialClose()
    {
        TutorPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
