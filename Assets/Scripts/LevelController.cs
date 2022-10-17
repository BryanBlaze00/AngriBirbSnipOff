using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private static int _nextLevelIndex = 1;
    [SerializeField] private float delayBeforeLoading = 2f;

    private float timeElapsed;
    private AudioSource audioSource;
    private Monster[] _monsters;

    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (Monster monster in _monsters)
        {
            if (monster.GetComponent<Animator>().enabled == true) return;
        }

        Debug.Log("You Killed ALL Monsters!");

        timeElapsed += Time.deltaTime;
        if (timeElapsed > delayBeforeLoading)
        {
        _nextLevelIndex++;
        string nextLevelName = "Level " + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);            
        }
    }
}
