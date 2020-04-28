using DG.Tweening;
using Prime31;
using TMPro;
using UnityEngine;
public class MenuScript : MonoBehaviour
{
    
    public Camera Camera;
    #region GameObjects
    public GameObject StartMenu;
    public GameObject PlayMenu;
    public GameObject PauseMenu;
    public GameObject SettingMenu;
    public GameObject SettingFone;
    public GameObject StatMenu;
    public GameObject Large3;
    public GameObject Large4;
    public GameObject Large5; 
    
    #endregion
    public TMP_Text LargeText;
    private int _PuzzleLarge = 3;
    private bool _InGame=false;
    private int _EscPressed=0;
    private bool _BlackTheme = false;
    public GameObject NightOn;
    public float AnimDuration;
    private GameObject[] PuzzleBuff;
    private int PuzzleNow = 0;
    #region Sound
    public GameObject SoundOff;
    private bool _SoundCanPlay=true;
    private AudioSource _audioSource;
    #endregion
     
    #region TouchControl

    public void TouchInput(int Direction)
    {
        switch (_PuzzleLarge)
        {
            case 3:
                PuzzleBuff[PuzzleNow].GetComponent<PuzzleManagerLarge3>().Touch(Direction);
                break;
            case 4:
                PuzzleBuff[PuzzleNow].GetComponent<PuzzleManagerLarge4>().Touch(Direction);
                break;
            case 5:
                PuzzleBuff[PuzzleNow].GetComponent<PuzzleManager5>().Touch(Direction);
                break;
            default:
                PuzzleBuff[PuzzleNow].GetComponent<PuzzleManagerLarge3>().Touch(Direction);
                break;
        }

    }

   #endregion
    
    #region startAndUpdate

      private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        private void Update()
        {
            if(Input.GetKeyDown( KeyCode.Escape))
            {
                if (_InGame==true && _EscPressed == 1)
                {
                    PauseMenu.SetActive(true);
                    _EscPressed = 0;
                }
                if (_EscPressed == 0)
                {
                    
                    StatMenu.SetActive(false);
                    SettingFone.SetActive(false);
                    SettingMenu.SetActive(false);
                    StartMenu.SetActive(false);
                    if (_InGame == true)
                    {
                        PlayMenu.SetActive(true);
                        _EscPressed = 1;
                    }
                    else
                    {
                        StartMenu.SetActive(true);   
                        _EscPressed = 0;
                    }
    
                }
            }
        }

    #endregion
  
    #region MenuButtons
   public void onStartButton()
   {
       _audioSource.Play();
       _InGame = true;
       _EscPressed = 1;
      StartMenu.SetActive(false);
      PlayMenu.SetActive(true);
      switch (_PuzzleLarge)
      {
          case 3:
              Instantiate(Large3);

              break;
          case 4:
              Instantiate(Large4);
              break;
          case 5:
              Instantiate(Large5);
              break;
          default:
              Instantiate(Large3);
              break;
      }
      PuzzleBuff= GameObject.FindGameObjectsWithTag("Puzzle");
    }

    public void onSettingButton()
    {
        _audioSource.Play();
        SettingFone.SetActive(true);
        SettingMenu.SetActive(true);
    }

    public void onStatisticButton()
    {
      
        _audioSource.Play(); 
        SettingFone.SetActive(true);
        StatMenu.SetActive(true);
    }

    public void onLargeButton()
    {
        _audioSource.Play();
        switch (_PuzzleLarge)
        {
            case 3:
                _PuzzleLarge++;
                break;
            case 4:
                _PuzzleLarge++;
                break;
            case 5:
                _PuzzleLarge--; _PuzzleLarge--;
                break;
            default:
                _PuzzleLarge = 3;
                break;
        }
        LargeText.text = _PuzzleLarge + "x"+_PuzzleLarge;
    }


  
    #endregion

    #region GameButtons

    
    public void onRestartButton()
    {
        Destroy(PuzzleBuff[PuzzleNow]);
        PuzzleNow++;
        _audioSource.Play();
        switch (_PuzzleLarge)
        {
            case 3:
                Instantiate(Large3);
               break;
            case 4:
                Instantiate(Large4);
                break;
            case 5:
                Instantiate(Large5);
                break;
            default:
                Instantiate(Large3);
                break;
        }
        PauseMenu.SetActive(false);
        PuzzleBuff= GameObject.FindGameObjectsWithTag("Puzzle");
    }

    public void onHomeButton()
    {      
        _audioSource.Play();
        foreach (var o in GameObject.FindGameObjectsWithTag("Puzzle")) Destroy(o);
        SettingFone.SetActive(false);
        StatMenu.SetActive(false);
        SettingMenu.SetActive(false);
        StartMenu.SetActive(true);
        PauseMenu.SetActive(false);
        PlayMenu.SetActive(false);
        StartMenu.SetActive(true);   
        _EscPressed = 0;
        _InGame = false;
    }

    public void onAdsButton()
    {
        _audioSource.Play();
    }
    #endregion
  
    #region Settings

    public void onSoundButton()
    {
        _audioSource.Play();
        if (_SoundCanPlay)
        {
            AudioListener.volume = 0;
            SoundOff.SetActive(true);
            _SoundCanPlay = false;
        }
       else
        {
            _SoundCanPlay = true;
            SoundOff.SetActive(false);
             AudioListener.volume = 1;
        }

    }
    public void onGalleryButton()
    {
        _audioSource.Play();
    }
    public void onDarkThemeButton()
    {
        
        _audioSource.Play();
        if (_BlackTheme)
        {
            Camera.DOColor(Color.white, AnimDuration);
            _BlackTheme = false;
            NightOn.SetActive(false);
        }
        else
        {
            NightOn.SetActive(true);
            _BlackTheme = true;
            Camera.DOColor(Color.black, AnimDuration);
        }

    }

    public void onStarButton()
    {
        
    }
    
    #endregion
  
}
