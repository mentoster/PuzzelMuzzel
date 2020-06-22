using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class MenuScript : MonoBehaviour
{
    public Camera Camera;
    public int CameraSize = 5;

    #region GameObjects

    public LoadManager SaveSystem;
    public GameObject StartMenu;
    public GameObject PlayMenu;
    public GameObject PauseMenu;
    public GameObject SettingMenu;
    public GameObject SettingFone;
    public GameObject StatMenu;
    public GameObject winMenu;
    public GameObject NightOn;
    public GameObject DeleteImageTrash;
    public GameObject YesDelete;
    public GameObject soundOff;
    private GameObject PuzzleBuff;
    public GameObject[] PuzzlePrefabs;
    public GameObject[] FonePuzzle;
    public LoadManager LoadMan;
    public TMP_Text StatNumbers;
    public GameObject Gradient;

    #endregion

    public TMP_Text score;
    public TMP_Text LargeText;
    private int _PuzzleLarge = 3;
    private bool _InGame = false;
    private int _EscPressed = 0;
    public float animDuration;


    #region Sound

    private AudioSource _audioSource;

    #endregion


    #region TouchControl

    public void TouchInput(int Direction)
    {
        if (_InGame)
        {
            switch (_PuzzleLarge)
            {
                case 3:
                    PuzzleBuff.GetComponent<PuzzleManagerLarge3>().Touch(Direction);
                    break;
                case 4:
                    PuzzleBuff.GetComponent<PuzzleManagerLarge4>().Touch(Direction);
                    break;
                case 5:
                    PuzzleBuff.GetComponent<PuzzleManager5>().Touch(Direction);
                    break;
                default:
                    PuzzleBuff.GetComponent<PuzzleManagerLarge3>().Touch(Direction);
                    break;
            }
        }
    }

    #endregion

    #region startAndUpdate

    private void Start()
    {
        if (!SaveSystem.Sound)
        {
            soundOff.SetActive(true);
            AudioListener.volume = 1;
        }
        else
        {
            soundOff.SetActive(false);
            AudioListener.volume = 0;
        }

        if (!SaveSystem.DarkTheme)
        {
            NightOn.SetActive(true);
            Camera.backgroundColor = Color.black;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    // press escape to pause
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_EscPressed == 0)
            {
                StatMenu.SetActive(false);
                SettingFone.SetActive(false);
                SettingMenu.SetActive(false);
                if (_InGame == true)
                {
                    winMenu.SetActive(false);
                    PauseMenu.SetActive(false);
                    _EscPressed = 1;
                    Camera.DOOrthoSize(CameraSize, 0.4f);
                }
                else
                {
                    _EscPressed = 0;
                }
            }
            else
            {
                Camera.DOOrthoSize(CameraSize + 2, 0.4f);
                PauseMenu.SetActive(true);
                _EscPressed = 0;
            }
        }
    }

    #endregion

    #region MenuButtons

    public void onStartButton()
    {
        score.text = "0";
        Camera.DOOrthoSize(CameraSize, 0.4f);
        _audioSource.Play();
        _InGame = true;
        _EscPressed = 1;
        SettingFone.SetActive(false);
        StatMenu.SetActive(false);
        SettingMenu.SetActive(false);
        StartMenu.SetActive(false);
        winMenu.SetActive(false);
        PlayMenu.SetActive(true);
        PuzzleBuff = Instantiate(PuzzlePrefabs[_PuzzleLarge - 3]);
        staticsColor.RndPalleteColor =(byte) Random.Range(0, 3);
    }

    public void onSettingButton()
    {
        StatMenu.SetActive(false);
        _audioSource.Play();
        SettingFone.SetActive(true);
        SettingMenu.SetActive(true);
        winMenu.SetActive(false);
    }

    public void onStatisticButton()
    {
        SettingMenu.SetActive(false);
        _audioSource.Play();
        SettingFone.SetActive(true);
        StatMenu.SetActive(true);
        winMenu.SetActive(false);
        //print data info
        //transform seconds to date format
        var ts = TimeSpan.FromSeconds(LoadMan.timeInGame + (int) Time.time);
        //print info text
        StatNumbers.text =
            $"{ts.Hours}:{ts.Minutes}:{ts.Seconds}\n\n{LoadMan.totalMoves}\n\n{LoadMan.numberOfWin}\n\n\n";
    }

    public void onLargeButton()
    {
        _audioSource.Play();
        FonePuzzle[_PuzzleLarge - 3].SetActive(false);
        switch (_PuzzleLarge)
        {
            case 3:
                _PuzzleLarge++;
                CameraSize++;
                break;
            case 4:
                _PuzzleLarge++;
                CameraSize++;
                break;
            case 5:
                _PuzzleLarge--;
                _PuzzleLarge--;
                CameraSize--;
                CameraSize--;
                break;
            default:
                _PuzzleLarge = 3;
                CameraSize = 5;
                break;
        }

        Camera.DOOrthoSize(CameraSize, 0.4f);
        FonePuzzle[_PuzzleLarge - 3].SetActive(true);
        LargeText.text = _PuzzleLarge + "x" + _PuzzleLarge;
    }

    #endregion

    #region GameButtons

    public void onRestartButton()
    {
        _audioSource.Play();
        SettingFone.SetActive(false);
        SettingMenu.SetActive(false);
        StatMenu.SetActive(false);
        Camera.DOOrthoSize(CameraSize, 0.4f);
        Camera.DOOrthoSize(5, 0.4f);
        Destroy(PuzzleBuff);
        score.text = "0";
        PuzzleBuff = Instantiate(PuzzlePrefabs[_PuzzleLarge - 3]);
        Camera.DOOrthoSize(CameraSize, 0.4f);
        PauseMenu.SetActive(false);
        staticsColor.RndPalleteColor =(byte) Random.Range(0, 3);
    }

    public void onHomeButton()
    {
        Camera.DOOrthoSize(CameraSize, 0.4f);
        _audioSource.Play();
        foreach (var o in GameObject.FindGameObjectsWithTag("Puzzle")) Destroy(o);
        SettingFone.SetActive(false);
        StatMenu.SetActive(false);
        SettingMenu.SetActive(false);
        StartMenu.SetActive(true);
        PauseMenu.SetActive(false);
        PlayMenu.SetActive(false);
        winMenu.SetActive(false);
        StartMenu.SetActive(true);
        _EscPressed = 0;
        _InGame = false;
    }


    public void onAdsButton()
    {
        _audioSource.Play();
        //after ad we can replace 2 puzzles
        statics.pressAD = true;
    }


    public void Win()
    {
        SettingFone.SetActive(true);
        winMenu.SetActive(true);
    }

    #endregion

    #region Settings

    public void onSoundButton()
    {
        _audioSource.Play();
        if (!SaveSystem.Sound)
        {
            soundOff.SetActive(false);
            AudioListener.volume = 0;
        }
        else
        {
            soundOff.SetActive(true);
            AudioListener.volume = 1;
        }

        SaveSystem.Sound = !SaveSystem.Sound;
    }

    public void onGalleryButton()
    {
        _audioSource.Play();
        Gradient.SetActive(staticsColor.UseMaterial);
        staticsColor.UseMaterial = !staticsColor.UseMaterial;
    }

    public void onDarkThemeButton()
    {
        _audioSource.Play();
        if (SaveSystem.DarkTheme == false)
        {
            Camera.DOColor(Color.white, animDuration);
        }
        else
        {
            Camera.DOColor(Color.black, animDuration);
        }

        NightOn.SetActive(SaveSystem.DarkTheme);
        SaveSystem.DarkTheme = !SaveSystem.DarkTheme;
    }

    public void onTrashButton()
    {
        _audioSource.Play();
        DeleteImageTrash.SetActive(false);
        YesDelete.SetActive(true);
    }

    public void NoDeleteButton()
    {
        _audioSource.Play();
        YesDelete.SetActive(false);
        DeleteImageTrash.SetActive(true);
    }

    public void YesDeleteButton()
    {
        _audioSource.Play();
        YesDelete.SetActive(false);
        DeleteImageTrash.SetActive(true);
        LoadMan.totalMoves = 0;
        LoadMan.numberOfWin = 0;
        LoadMan.timeInGame = 0;
    }

    public void onStarButton()
    {
    }

    #endregion
}