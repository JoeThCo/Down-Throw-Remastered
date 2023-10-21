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

    public void UserLogin(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnPlayFabError);
    }

    public void UserLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = loginEmail.text,
            Password = loginPassword.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnPlayFabError);
    }

    public void DevLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = DEV_EMAIL,
            Password = DEV_PASSWORD
        };

        if (!Input.GetKey(KeyCode.Q)) return;
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnPlayFabError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in successfully");

        PlayFabPlayerInfo.LoadPlayerInfo();

        PlayFabPlayerInfo.isLoggedIn = true;
    }

    public void NewUser()
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Username = registerUserName.text,
            Email = registerEmail.text,
            Password = registerPassword.text,
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnPlayFabError);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Successfully registered user");

        PlayFabPlayerInfo.NewPlayer();
        UpdateDisplayName(registerUserName.text);

        UserLogin(registerEmail.text, registerPassword.text);
    }

    void UpdateDisplayName(string name)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdated, OnPlayFabError);
    }

    private void OnDisplayNameUpdated(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"Updated display name to: {result.DisplayName}");
        PlayFabPlayerInfo.SetName(result.DisplayName);
    }

    private void OnPlayFabError(PlayFabError error)
    {
        Debug.LogError("Playfab Error: " + error.GenerateErrorReport());
    }
}