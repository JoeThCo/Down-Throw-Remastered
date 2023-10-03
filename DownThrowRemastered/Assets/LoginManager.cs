using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Header("Register")]
    [SerializeField] TMP_InputField registerEmail;
    [SerializeField] TMP_InputField registerUserName;
    [SerializeField] TMP_InputField registerPassword;
    [Header("Login")]
    [SerializeField] TMP_InputField loginEmail;
    [SerializeField] TMP_InputField loginPassword;

    private const string DEV_EMAIL = "yenow22371@klanze.com";
    private const string DEV_PASSWORD = "mypass123";

    public void UserLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = loginEmail.text,
            Password = loginPassword.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void DevLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = DEV_EMAIL,
            Password = DEV_PASSWORD
        };

        if (!Input.GetKey(KeyCode.Q)) return;
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in successfully");
        PlayFabInfo.LoadPlayerInfo();
        MenuManager.Instance.LoadAScene("MainMenu");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Error logging in user with PlayFab: " + error.GenerateErrorReport());
    }

    public void NewUser()
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Username = registerUserName.text,
            Email = registerEmail.text,
            Password = registerPassword.text,
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Successfully registered user");
        MenuManager.Instance.DisplayMenus("Login");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError("Error registering user: " + error.GenerateErrorReport());
    }
}