using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayersView playersView;
    [SerializeField] private MainMenuView mainMenuView;

    private HubConnection _connection;
    private string _playerName;

    private void Start()
    {
        mainMenuView.StartButton.onClick.AddListener(StartGame);
    }

    public async void StartGame()
    {
        _connection = new HubConnectionBuilder().WithUrl(mainMenuView.ServerURL.text).Build();
        _connection.Closed += async (error) =>
        {
            await Task.Delay(1000);
            await _connection.StartAsync();
        };
        await _connection.StartAsync();
        _playerName = await _connection.InvokeAsync<string>("Register");
        var playersNames = await _connection.InvokeAsync<string[]>("ConnectToGame", _playerName);
        if (playersNames != null)
        {
            foreach (var playersName in playersNames)
            {
                NewPlayer(playersName);
            }
        }

        _connection.On<string>(
                "NewPlayer",
                serverPlayerName =>
                    MainThreadHelper.MainQueue.Enqueue((() => NewPlayer(serverPlayerName))));

            mainMenuView.gameObject.SetActive(false);
            playersView.gameObject.SetActive(true);
        }
    

    public void NewPlayer(string newPlayerName)
    {
        playersView.AddPlayer(newPlayerName);
    }
    
}
