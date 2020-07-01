using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Advertisements;
public class MenuScript : MonoBehaviour {

	public Camera Camera;
	public int CameraSize = 5;

	#region GameObjects
	[SerializeField] private GameObject _Gradient = null;
	[SerializeField] private GameObject _NotGradient = null;
	[SerializeField] private GameObject _Photo = null;
	[SerializeField] private GameObject _settingFoneTutorial = null;
	[SerializeField] private GameObject _settingGameTutorial = null;
	[SerializeField] private GameObject _tutorial = null;
	[SerializeField] private bool _DevBuildAd = false;
	[SerializeField] private Texture2D _defaultImagine = null;
	[SerializeField] private AudioClip _winSound = null;
	[SerializeField] private AudioClip _BallonSound = null;
	private bool _WinAlready = false;
	private GameObject PuzzleBuff;
	public GameObject DeleteImageTrash;
	public GameObject NightOn;
	public GameObject PauseMenu;
	public GameObject PlayMenu;
	public GameObject SettingFone;
	public GameObject SettingMenu;
	public GameObject StartMenu;
	public GameObject StatMenu;
	public GameObject YesDelete;
	public GameObject soundOff;
	public GameObject winMenu;
	public GameObject[] FonePuzzle;
	public GameObject[] PuzzlePrefabs;
	public LoadManager SaveSystem;
	public TMP_Text StatNumbers;
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
	private void Awake () => Application.targetFrameRate = 60;

	#endregion

	#region startAndUpdate
	private void Start () {
		statics.source = _defaultImagine;
		if (!SaveSystem.Sound) {
			soundOff.SetActive (true);
			AudioListener.volume = 1;
		} else {
			soundOff.SetActive (false);
			AudioListener.volume = 0;
		}

		if (!SaveSystem.DarkTheme) {
			NightOn.SetActive (true);
			Camera.backgroundColor = Color.black;
		}

		if (SaveSystem.frstStart) {
			//   popUps[_popUpIndex]
		}
		if (Advertisement.isSupported)
			Advertisement.Initialize ("3678875", _DevBuildAd);
		_audioSource = GetComponent<AudioSource> ();
	}

	// press escape to pause
	private void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (_EscPressed == 0) {
				StatMenu.SetActive (false);
				SettingFone.SetActive (false);
				SettingMenu.SetActive (false);
				if (_InGame == true) {
					winMenu.SetActive (false);
					PauseMenu.SetActive (false);
					_EscPressed = 1;
					Camera.DOOrthoSize (CameraSize, 0.4f);
				} else {
					_EscPressed = 0;
				}
			} else {
				Camera.DOOrthoSize (CameraSize + 2, 0.4f);
				PauseMenu.SetActive (true);
				_EscPressed = 0;
			}
			TutorialOff ();
		}
	}

	#endregion

	#region MenuButtons
	public void TutorialOff () {
		_tutorial.SetActive (false);
		_settingFoneTutorial.SetActive (false);
		_settingGameTutorial.SetActive (false);
	}
	public void onStartButton () {
		score.text = "0";
		Camera.DOOrthoSize (CameraSize, 0.4f);
		_audioSource.Play ();
		_InGame = true;
		_EscPressed = 1;
		PlayMenu.SetActive (true);
		SettingFone.SetActive (false);
		SettingMenu.SetActive (false);
		StartMenu.SetActive (false);
		StatMenu.SetActive (false);
		winMenu.SetActive (false);

		PuzzleBuff = Instantiate (PuzzlePrefabs[_PuzzleLarge - 3]);
		staticsColor.RndPalleteColor = (byte) Random.Range (0, 3);
		//you can swap 2 of the puzzle, if you watch the advertising
		if (SaveSystem.frstStart) {
			_tutorial.SetActive (true);
			_settingGameTutorial.SetActive (true);
			SaveSystem.frstStart = false;
		}
	}

	public void onSettingButton () {
		SettingFone.SetActive (true);
		SettingMenu.SetActive (true);
		StatMenu.SetActive (false);
		_audioSource.Play ();
		winMenu.SetActive (false);
		if (SaveSystem.settingFrstStart) {
			SaveSystem.settingFrstStart = false;
			_settingFoneTutorial.SetActive (true);
			_tutorial.SetActive (true);
		}
	}

	public void onStatisticButton () {
		SettingFone.SetActive (true);
		SettingMenu.SetActive (false);
		StatMenu.SetActive (true);
		_audioSource.Play ();
		winMenu.SetActive (false);
		//print data info
		//transform seconds to date format
		var ts = TimeSpan.FromSeconds (SaveSystem.timeInGame + (int) Time.time);
		//print info text
		StatNumbers.text =
			$"{ts.Hours}:{ts.Minutes}:{ts.Seconds}\r\n\r\n{SaveSystem.totalMoves}\r\n\r\n{SaveSystem.numberOfWin}\r\n\r\n\r\n";
	}

	public void onLargeButton () {
		_audioSource.Play ();
		FonePuzzle[_PuzzleLarge - 3].SetActive (false);
		switch (_PuzzleLarge) {
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

		Camera.DOOrthoSize (CameraSize, 0.4f);
		FonePuzzle[_PuzzleLarge - 3].SetActive (true);
		LargeText.text = _PuzzleLarge + "x" + _PuzzleLarge;
	}

	#endregion

	#region GameButtons

	public void onRestartButton () {
		_audioSource.Play ();
		SettingFone.SetActive (false);
		SettingMenu.SetActive (false);
		StatMenu.SetActive (false);
		Camera.DOOrthoSize (CameraSize, 0.4f);
		Camera.DOOrthoSize (5, 0.4f);
		Destroy (PuzzleBuff);
		score.text = "0";
		PuzzleBuff = Instantiate (PuzzlePrefabs[_PuzzleLarge - 3]);
		Camera.DOOrthoSize (CameraSize, 0.4f);
		PauseMenu.SetActive (false);
		staticsColor.RndPalleteColor = (byte) Random.Range (0, 3);
	}

	public void onHomeButton () {
		_WinAlready = false;
		Camera.DOOrthoSize (CameraSize, 0.4f);
		_audioSource.Play ();
		foreach (var o in GameObject.FindGameObjectsWithTag ("Puzzle")) Destroy (o);
		SettingFone.SetActive (false);
		StatMenu.SetActive (false);
		SettingMenu.SetActive (false);
		StartMenu.SetActive (true);
		PauseMenu.SetActive (false);
		PlayMenu.SetActive (false);
		winMenu.SetActive (false);
		StartMenu.SetActive (true);
		_EscPressed = 0;
		_InGame = false;
	}

	public void onAdsButton () {
		_audioSource.Play ();
		//after ad we can replace 2 puzzles
		const string RewardedZoneId = "rewardedVideo";
		if (Advertisement.IsReady (RewardedZoneId)) {
			Advertisement.Show (RewardedZoneId);
			statics.pressAD = true;
		}

	}
	public void onTutorialButton () {
		_tutorial.SetActive (true);
		_settingGameTutorial.SetActive (true);
	}
	public void Win () {
		if (!_WinAlready) {
			_WinAlready = true;
			_audioSource.clip = _winSound;
			_audioSource.Play ();
			Invoke ("winMenuTrue", 3.5f);
		}
	}
	private void winMenuTrue () {
		_WinAlready = true;
		winMenu.SetActive (true);
		_audioSource.clip = _BallonSound;
	}

	#endregion

	#region Settings

	public void onSoundButton () {
		_audioSource.Play ();
		if (!SaveSystem.Sound) {
			soundOff.SetActive (false);
			AudioListener.volume = 0;
		} else {
			soundOff.SetActive (true);
			AudioListener.volume = 1;
		}

		SaveSystem.Sound = !SaveSystem.Sound;
	}

	public void onGradientButton () {
		_audioSource.Play ();
		if (staticsColor.UseMaterial == 0) {
			_NotGradient.SetActive (true);
			staticsColor.UseMaterial++;
		} else if (staticsColor.UseMaterial == 1) {
			_Gradient.SetActive (false);
			_NotGradient.SetActive (false);
			_Photo.SetActive (true);
			staticsColor.UseMaterial++;
			onGalleryButton ();
		} else {
			_NotGradient.SetActive (false);
			_Photo.SetActive (false);
			_Gradient.SetActive (true);
			staticsColor.UseMaterial = 0;
		}
	}

	public void onDarkThemeButton () {
		_audioSource.Play ();
		if (SaveSystem.DarkTheme == false) {
			Camera.DOColor (Color.white, animDuration);
		} else {
			Camera.DOColor (Color.black, animDuration);
		}

		NightOn.SetActive (SaveSystem.DarkTheme);
		SaveSystem.DarkTheme = !SaveSystem.DarkTheme;
	}

	public void onTrashButton () {
		_audioSource.Play ();
		DeleteImageTrash.SetActive (false);
		YesDelete.SetActive (true);
	}

	public void NoDeleteButton () {
		_audioSource.Play ();
		YesDelete.SetActive (false);
		DeleteImageTrash.SetActive (true);
	}

	public void YesDeleteButton () {
		_audioSource.Play ();
		YesDelete.SetActive (false);
		DeleteImageTrash.SetActive (true);
		SaveSystem.totalMoves = 0;
		SaveSystem.numberOfWin = 0;
		SaveSystem.timeInGame = 0;
	}
	public void onGalleryButton () {
		//https://github.com/yasirkula/UnityNativeGallery
		var maxSize = 4064;
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery ((path) => {
			Debug.Log ("Image path: " + path);
			if (path != null) {
				// Create Texture from selected image
				statics.source = NativeGallery.LoadImageAtPath (path, maxSize);
				statics.source = duplicateTexture (statics.source);
				if (statics.source == null) {
					Debug.Log ("Couldn't load texture from " + path);
					return;
				}

				//Destroy (statics.source, 5f);
			}
		}, title: "Select the  image", mime: "image/*");
	}
	//https://stackoverflow.com/questions/44733841/how-to-make-texture2d-readable-via-script
	Texture2D duplicateTexture (Texture2D source) {
		RenderTexture renderTex = RenderTexture.GetTemporary (
			source.width,
			source.height,
			0,
			RenderTextureFormat.Default,
			RenderTextureReadWrite.Linear);

		Graphics.Blit (source, renderTex);
		RenderTexture previous = RenderTexture.active;
		RenderTexture.active = renderTex;
		Texture2D readableText = new Texture2D (source.width, source.height);
		readableText.ReadPixels (new Rect (0, 0, renderTex.width, renderTex.height), 0, 0);
		readableText.Apply ();
		RenderTexture.active = previous;
		RenderTexture.ReleaseTemporary (renderTex);
		return readableText;
	}
	#endregion
}