using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

//этот код - менеджр пазла

public class PuzzleManagerLarge4 : MonoBehaviour
{
    public const int PuzzleLarge = 4;
    public const int fullPuzzleLarge = PuzzleLarge * PuzzleLarge;
    AudioSource _mMyAudioSource;
    public GameObject[] puzzle = new GameObject[9];

    private int[,] position = {{0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}};

    private LoadManager _loadManager;

    //конец движения
    public Transform endPosition;

    public float speed;

    //передача ID в fixedUpdate
    private int _thisPuzzleId;
    private bool _goRightNow;
    private TMP_Text ScoreText;
    private int _score = 0;
    private int _zeroPosX;
    private int _zeroPosY;
    public bool autoWin;
    private MenuScript MenuScript;

    private void Start()
    {
        MenuScript = GameObject.Find("UI  + (GameManager)").GetComponent<MenuScript>();
        ScoreText = GameObject.Find("PointCounterText").GetComponent<TMP_Text>();
        _loadManager = GameObject.Find("SaveLoadSystem").GetComponent<LoadManager>();
        _score = 0;
        _mMyAudioSource = GetComponent<AudioSource>();
        GeneratePuzzle();
    }

    #region puzzleFucntions

    private void Update()
    {
        if (_goRightNow)
            puzzle[_thisPuzzleId].transform.position =
                Vector2.Lerp(puzzle[_thisPuzzleId].transform.position, endPosition.position, speed);
        if (puzzle[_thisPuzzleId].transform.position == endPosition.position)
            _goRightNow = false;
    }

// перемещает пазл
    public void Touch(int direction)
    {
        switch (direction)
        {
            //left
            case 0:
                if (_zeroPosX + 1 < PuzzleLarge)
                {
                    OnPuzzleClick(position[_zeroPosX + 1, _zeroPosY]);
                }

                break;
            //right
            case 1:
                if (_zeroPosX - 1 >= 0)
                {
                    OnPuzzleClick(position[_zeroPosX - 1, _zeroPosY]);
                }

                break;
            //up
            case 2:
                if (_zeroPosY - 1 >= 0)
                {
                    OnPuzzleClick(position[_zeroPosX, _zeroPosY - 1]);
                }

                break;
            //down
            case 3:
                if (_zeroPosY + 1 < PuzzleLarge)
                {
                    OnPuzzleClick(position[_zeroPosX, _zeroPosY + 1]);
                }

                break;
        }
    }
 public void onAdPuzlleClick(GameObject itPuzzle, int id)
    {
        if (statics.pressAD)
        {
            var colorPuzzle = itPuzzle.GetComponent<SpriteRenderer>().color;
            itPuzzle.GetComponent<SpriteRenderer>().color =
                new Color(0.5f, 0.5f, 0.5f, 0.5f);
            statics.PuzzlesNow++;
            // if we select 2 puzzles, we change they positions
            if (statics.PuzzlesNow == 2)
            {
                statics.SelectPuzzles.GetComponent<SpriteRenderer>().color =
                    new Color(colorPuzzle.r, colorPuzzle.g, colorPuzzle.b, 1);
                itPuzzle.GetComponent<SpriteRenderer>().color =
                    new Color(colorPuzzle.r, colorPuzzle.g, colorPuzzle.b, 1);

                statics.pressAD = false;
                statics.PuzzlesNow = 0;
                int x = 0, y = 0;
                for (int i = 0; i < PuzzleLarge; i++)
                for (int t = 0; t < PuzzleLarge; t++)
                    if (position[i, t] == id)
                    {
                        x = i;
                        y = t;
                    }
                bool stop =true;
                for (int i = 0; i < PuzzleLarge; i++)
                for (int t = 0; t < PuzzleLarge; t++)
                    if (position[i, t] == statics.SelectPuzzlesID&&stop)
                    {
                        position[i, t] = id;
                        position[x, y] = statics.SelectPuzzlesID;
                        puzzle[id].transform
                            .DOMove(new Vector2(RealPosition(i), RealPosition(t)), speed);
                        puzzle[statics.SelectPuzzlesID].transform
                            .DOMove(new Vector2(RealPosition(x), RealPosition(y)), speed);
                        Debug.Log($"{i},{t},,,{x},{i}");
                        stop = false;
                        break;
                    }

                statics.pressAD = false;
                statics.PuzzlesNow = 0;
            }
            else
            {
                statics.SelectPuzzles = itPuzzle;
                statics.SelectPuzzlesID = id;
            }
        }
        else OnPuzzleClick(id);
    }

    public void OnPuzzleClick(int id)
    {
        _mMyAudioSource.Play();
        int x = 0, y = 0;
        //Найдем позицию по ID
        for (int i = 0; i < fullPuzzleLarge; i++)
        {
            //перебираем координаты для матрциы
            if (y == PuzzleLarge)
            {
                x++;
                y = 0;
            }

            if (position[x, y] == id)
            {
                break;
            }

            y++;
        }

        // запоммнает позицию
        // Визуализация проверки нажатого пазла
        //          |check|
        // |check| |Click|  |check|
        //         |check|
        //Если пустая клетка нашлась среди check, то переместить туда клетку
        //
        //если не выходит за рамки
        if (x - 1 >= 0)
        {
            //и если клетка пуста
            if (position[x - 1, y] == 0)
            {
                position[x, y] = 0;
                _zeroPosX = x;
                _zeroPosY = y;
                TransromPuzzle(x - 1, y, id);
            }
        }

        if (x + 1 <= 3)
        {
            if (position[x + 1, y] == 0)
            {
                //ставит пустую клетку на место пазла
                position[x, y] = 0;
                _zeroPosX = x;
                _zeroPosY = y;
                TransromPuzzle(x + 1, y, id);
            }
        }

        if (y - 1 >= 0)
        {
            if (position[x, y - 1] == 0)
            {
                position[x, y] = 0;
                _zeroPosX = x;
                _zeroPosY = y;
                TransromPuzzle(x, y - 1, id);
            }
        }

        if (y + 1 <= 3)
        {
            if (position[x, y + 1] == 0)
            {
                position[x, y] = 0;
                _zeroPosX = x;
                _zeroPosY = y;
                TransromPuzzle(x, y + 1, id);
            }
        }
    }

    //перемещает пазл в другую клетку матрицы
    private void TransromPuzzle(int x, int y, int ID)
    {
        if (_goRightNow)
            puzzle[_thisPuzzleId].transform.position = endPosition.position;
        _score++;
        ScoreText.text = _score.ToString();
        //передаем id  в update
        _thisPuzzleId = ID;
        endPosition.position = new Vector2(RealPosition(x), RealPosition(y));
        //ставим пазл на пустую клетку
        position[x, y] = ID;

        _goRightNow = true;
        //   puzzle[_thisPuzzleId].transform.DOMove(endPosition.position, speed);
        //проверяем, решили ли мы правильно 
        CheckPuzzle();
        _loadManager.totalMoves++;
    }

    //проверяет ВЕСЬ ПАЗЛ на правильную комбинацию
    private void CheckPuzzle()
    {
        int buff = 0, x = 0, y = 0;
        //правильная комбинация для выйгрыша
        // незнаю почему такая)))
        //int[] adress = {3,7,11,15,2,6,10,14,1,5,9,13,0,4,8,12};
        int[] adress = {12, 8, 4, 0, 13, 9, 5, 1, 14, 10, 6, 2, 15, 11, 7, 3};
        for (int i = 0; i < fullPuzzleLarge; i++)
        {
            //перебираем координаты для матрциы
            if (y == PuzzleLarge)
            {
                x++;
                y = 0;
            }

            if (position[x, y] == adress[i])
            {
                buff++;
            }

            y++;
        }

        if (buff == fullPuzzleLarge)
        {
            Invoke("Win", 0.4f);
        }
    }


    //генерирует случайный пазл
    private void GeneratePuzzle()
    {
        //правильная комбинация для выйграша

        int[] adress = {3, 7, 11, 15, 2, 6, 10, 14, 1, 5, 9, 13, 0, 4, 8, 12};
        if (!autoWin)
        {
            //перемешка случайным образом массив adress
            Shuffle(adress);
        }

        for (int i = 0; i < fullPuzzleLarge; i++)
        {
            PasteOnPosition(puzzle[i], adress[i], i);
        }
    }

    //вставляет на место по адресу 
    private void PasteOnPosition(GameObject ThisPuzzle, int adress, int id)
    {
        int x = 0, y = 0;
        for (int i = 0; i <= fullPuzzleLarge; i++)
        {
            if (y == PuzzleLarge)
            {
                x++;
                y = 0;
            }

            //находи место для puzzle
            if (adress == i)
            {
                ThisPuzzle.transform.position = new Vector2(RealPosition(x), RealPosition(y));
                position[x, y] = id;
                if (ThisPuzzle == puzzle[0])
                {
                    _zeroPosX = x;
                    _zeroPosY = y;
                }
            }

            y++;
        }
    }

    //переводит из id местоположение в игровое 
    private float RealPosition(int x)
    {
        switch (x)
        {
            case 0:
                return -1.5f;
            case 1:
                return -0.5f;
            case 2:
                return 0.5f;
            case 3:
                return 1.5f;
            default:
                return 0;
        }
    }

    #endregion

    //функция при победе
    private void Win()
    {
        _loadManager.numberOfWin++;
        MenuScript.Win();
    }

    //http://www.dotnetperls.com/fisher-yates-shuffle
    // перемешка  массива
    private void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < (n - 1); i++)
        {
            int r = i + Random.Range(0, n - i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
}