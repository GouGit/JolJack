﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void LoadScene(string name, SceneOption option)
    {
        Time.timeScale = 0;
        SceneOptionTransporter transporter = Instantiate(Resources.Load("SceneOptionTransporter") as GameObject).GetComponent<SceneOptionTransporter>();
        transporter.sceneOption = option;
        SceneManager.LoadScene(name);
    }

    public static string GetNowSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static int GetNowSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public static void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadSceneWithFade(string name)
    {
        FadeUI[] fadePanels = FindObjectsOfType<FadeUI>();
        foreach (var fadePanel in fadePanels)
        {
            fadePanel.m_OnFadeInEnd += (obj) =>
          {
              LoadScene(name);
          };
            fadePanel.FadeIn();
        }
    }

    public void LoadSceneWithFade(int index)
    {
        FadeUI[] fadePanels = FindObjectsOfType<FadeUI>();
        foreach (var fadePanel in fadePanels)
        {
            fadePanel.m_OnFadeInEnd += (obj) =>
            {
                LoadScene(index);
            };
            fadePanel.FadeIn();
        }
    }

    public void ChangeSceneToMenu()
    {
        LoadScene("Menu");
    }

    public void ChangeSceneToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ChangeSceneToWaveGame()
    {
        SceneManager.LoadScene("Game_Wave");
    }
}
