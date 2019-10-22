using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private bool _isGameOver;
    private AudioSource _audio;

    void Start() {
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
            Debug.LogError("Audio source is null");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
            SceneManager.LoadScene(1);
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void GameOver() {
        _audio.Play();
        _isGameOver = true;
    }
}
