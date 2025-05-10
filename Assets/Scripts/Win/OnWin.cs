using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnWin : MonoBehaviour
{
    Animator winAnimator;
    public PlayerMovement playerMovement;
    public ReloadLevel reload;
    private void Awake()
    {
        winAnimator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        WinCheck.OnPlayerWin += PlayWinAnimation;
    }
    private void OnDisable()
    {
        WinCheck.OnPlayerWin -= PlayWinAnimation;
    }
    private void PlayWinAnimation()
    {
        winAnimator.SetTrigger("PlayerWon");
        playerMovement.enabled = false;
        reload.ReloadScene();
    }
}
