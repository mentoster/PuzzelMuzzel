﻿using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

//этот код - менеджр пазла

public class PuzzleManagerLarge3 : MonoBehaviour
{
    AudioSource _mMyAudioSource;
    public GameObject[] puzzle = new GameObject[9];

    private int[,] position = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}};

    //конец движения
    public Transform endPosition;

    public float speed;

    //передача ID в fixedUpdate
    private int _thisPuzzleId;
    private bool _goRightNow;
    public const float positionDifference = 1;
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
        GeneratePuzzle();
        _score = 0;
        _mMyAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_goRightNow)
            puzzle[_thisPuzzleId].transform.position =
                Vector2.Lerp(puzzle[_thisPuzzleId].transform.position, endPosition.position, speed);
        if (puzzle[_thisPuzzleId].transform.position == endPosition.position)
            _goRightNow = false;

    }

    public void Touch(int direction)
    {
        switch (direction)
        {
            //left
            case 0:
                if (_zeroPosX + 1 <= 2)
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
                if (_zeroPosY + 1 <= 2)
                {
                    OnPuzzleClick(position[_zeroPosX, _zeroPosY + 1]);
                }

                break;
        }
    }

    #region puzzleFucntions

    private void GeneratePuzzle()
    {
        //правильная комбинация для выйграша
        int[] adress = {2, 5, 8, 1, 4, 7, 0, 3, 6};

        if (!autoWin)
        {
            //перемешка случайным образом массив adress
            Shuffle(adress);
        }

        for (int i = 0; i < 9; i++)
        {
            PasteOnPosition(puzzle[i], adress[i], i);
        }
    }

    public void OnPuzzleClick(int id)
    {
        _mMyAudioSource.Play();
        int x = 0, y = 0;
        //Найдем позицию по ID
        for (int i = 0; i < 9; i++)
        {
            //перебираем координаты для матрциы
            if (y == 3)
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

        if (x + 1 <= 2)
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

        if (y + 1 <= 2)
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
        //задаем конец перемещения
        //разрешаем сейчас использовать
        //puzzle[_thisPuzzleId].transform.DOMove(endPosition.position, speed);
        _goRightNow = true;
        //проверяем, решили ли мы правильно 
        CheckPuzzle();
    }

    //проверяет ВЕСЬ ПАЗЛ на правильную комбинацию
    private void CheckPuzzle()
    {
        int buff = 0, x = 0, y = 0;
        //правильная комбинация для выйгрыша
        //незнаю почему такая)))
        int[] adress = {6, 3, 0, 7, 4, 1, 8, 5, 2};
        for (int i = 0; i < 9; i++)
        {
            //перебираем координаты для матрциы
            if (y == 3)
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

        if (buff == 9)
        {
            Invoke("Win", 0.4f);
        }
    }


    //генерирует случайный пазл

    //вставляет на место по адресу 
    private void PasteOnPosition(GameObject ThisPuzzle, int adress, int id)
    {
        int x = 0, y = 0;
        for (int i = 0; i <= 9; i++)
        {
            if (y == 3)
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
                return -positionDifference;
            case 1:
                return 0;
            case 2:
                return positionDifference;
            default:
                return 0;
        }
    }

    #endregion

    //функция при победе
    private void Win()
    {
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