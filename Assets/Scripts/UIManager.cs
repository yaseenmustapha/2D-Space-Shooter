﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start() {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (_gameManager == null)
            Debug.LogError("Game Manager is null");
    }

    public void UpdateScore(int score) {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives) {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0) {
            _gameManager.GameOver();
            _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver() {
        while (true) {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
