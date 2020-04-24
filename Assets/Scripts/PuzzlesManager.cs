using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
//этот код - менеджр пазла

    public class PuzzlesManager : MonoBehaviour
    {   
        AudioSource _mMyAudioSource; //sound of button
        
        public int puzzleLarge;//x*x, if 9 than puzzleLrge must be 3
        public GameObject[] puzzle3 = new GameObject[9];
        public GameObject[] puzzle4 = new GameObject[9];
        public GameObject[] puzzle5 = new GameObject[9];
        private int[,] _position9 = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}};  //matrix massive
        private int[,] _position16 = {{0, 0, 0,0},{0, 0, 0,0},{0, 0, 0,0},{0, 0, 0,0}};
        private int[,] _position25 = {{0, 0, 0,0,0}, {0, 0, 0,0,0}, {0, 0, 0,0,0},{0, 0, 0,0,0},{0, 0, 0,0,0}};
        public float animSpeed; //animation AnimSpeed
        public TMP_Text scoreCount;
        private int _pointCounter=0;
        //buff
        private int _thisPuzzleId;   
        private bool _goRightNow;   
        public Transform endPosition;
        public bool autoWin;
        private void Start()
        {
            _mMyAudioSource = GetComponent<AudioSource>();
            GeneratePuzzle();
        }

        #region puzzleFucntions
// перемещает пазл
        private void FixedUpdate()
        {
            if (_goRightNow)
            {
                switch (puzzleLarge)
                {
                    case 5:
                        puzzle5[_thisPuzzleId].transform.position = Vector2.Lerp(puzzle5[_thisPuzzleId].transform.position, endPosition.position, animSpeed);
                        break;
                    case 4:
                        puzzle4[_thisPuzzleId].transform.position = Vector2.Lerp(puzzle4[_thisPuzzleId].transform.position, endPosition.position, animSpeed);
                        break;
                    default:
                        puzzle3[_thisPuzzleId].transform.position = Vector2.Lerp(puzzle3[_thisPuzzleId].transform.position, endPosition.position, animSpeed);
                        break;
                    
                }
            }
        }

        public void OnPuzzleClick(int id)
        {
   
            _mMyAudioSource.Play();
            int x = 0, y = 0;
            //Найдем позицию по ID
            for (int i = 0; i < puzzleLarge*puzzleLarge; i++)
            {
                //перебираем координаты для матрциы
                if (y == puzzleLarge)
                {
                    x++;
                    y = 0;
                }
                //found the right puzzle
                if (positionGet(x, y) == id)
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
                if (positionGet(x - 1, y) == 0)
                {
                    positionSet(x, y, 0);
                    TransromPuzzle(x-1,y,id);
                }
            }

            if (x +1<= 2)
            {
                if (positionGet(x + 1, y) == 0)
                {
                    positionSet(x, y, 0);
                    TransromPuzzle(x+1,y,id);
                }
            }
            if (y-1 >= 0)
            {
                if (positionGet(x , y-1) == 0)
                {
                    positionSet(x, y, 0);
                    TransromPuzzle(x,y-1,id);
                }
            }
            if (y+1<=2)
            {
                if (positionGet(x, y+1) == 0)
                {
                
                    positionSet(x, y, 0);
                    TransromPuzzle(x,y+1,id);
                
                }
            }
        }
        //перемещает пазл в другую клетку матрицы
        private void TransromPuzzle(int x, int y,int id)
        {
            //fust transform, if player pressed on another puzzle
            if (_goRightNow = true)
            {
                switch (puzzleLarge)
                {
                    case 5:
                        puzzle5[_thisPuzzleId].transform.position = new Vector2(endPosition.position.x,endPosition.position.y);
                        break;
                    case 4:
                        puzzle4[_thisPuzzleId].transform.position = new Vector2(endPosition.position.x,endPosition.position.y);
                        break;
                    default:
                        puzzle3[_thisPuzzleId].transform.position = new Vector2(endPosition.position.x,endPosition.position.y);
                        break;
                    
                }
                _goRightNow = false;

            }
            _pointCounter++;
            scoreCount.text = _pointCounter.ToString();
            //передаем id  в update
            _thisPuzzleId = id;
            endPosition.position = new Vector2(RealPosition(x), RealPosition(y));
            //ставим пазл на пустую клетку
            positionSet(x, y, id);
            //задаем конец перемещения
            //разрешаем сейчас использовать
            _goRightNow = true;
            //проверяем, решили ли мы правильно 
            CheckPuzzle();
        }
        //проверяет ВЕСЬ ПАЗЛ на правильную комбинацию
        private void CheckPuzzle()
        {
            int buff = 0, x = 0, y = 0;
            //правильная комбинация для выйгрыша
            // незнаю почему такая)))
            int[] adress = {6,3,0,7,4,1,8,5,2};
            for (int i = 0; i < puzzleLarge*puzzleLarge; i++)
            {
                //перебираем координаты для матрциы
                if (y ==puzzleLarge)
                {
                    x++;
                    y = 0;
                }
              
                if (positionGet(x, y) == adress[i])
                {
                    buff++;
                }
                y++;
            }
            if (buff == puzzleLarge*puzzleLarge){
                Invoke("Win",5);
            }
        }
        private void GeneratePuzzle()
        {
            int[] adress = {2, 5, 8,1, 4, 7,0, 3, 6};
            if (!autoWin)
            {
                Shuffle(adress);
            }
            for (int i = 0; i <puzzleLarge*puzzleLarge; i++)
            {
                switch (puzzleLarge)
                {
                    case 5:
                        PasteOnPosition(puzzle5[i], adress[i], i);
                        break;
                    case 4:
                        PasteOnPosition(puzzle4[i], adress[i], i);
                        break;
                    default:
                       // Debug.Log($"pasteOnposition number {i},adress {adress[i]}");
                        PasteOnPosition(puzzle3[i], adress[i], i);
                        break;
                    
                }
              
            }
        }
        private void PasteOnPosition(GameObject puzzle, int adress, int id)
        {
            int x = 0, y = 0;
            for (int i = 0; i <=puzzleLarge*puzzleLarge; i++)
            {
                if (y == puzzleLarge)
                {
                    x++; 
                    y = 0;
                }
                //находи место для puzzle
                if (adress == i)
                {
                    puzzle.transform.position=new Vector2(RealPosition(x),RealPosition(y));
                    positionSet(x,y,id);
                }
                y++;
            }
        
        }
        private float RealPosition(int x)
        {
            switch (puzzleLarge)
            {
                case 5:
                    switch (x)
                    {
                        case 0:
                            return 0;
                        case 1:
                            return  0;
                        case 2:
                            return 0;
                        case 3:
                            return 0;
                        case 4:
                            return 0;
                 
                    }
                    break;
                case 4:
                    switch (x)
                    {
                        case 0:
                            return -1.5f;
                        case 1:
                            return  -0.5f;
                        case 2:
                            return 0.5f;
                        case 3:
                            return 1.5f;
            
                    }
                    break;
                default:
                    switch (x)
                    {
                        case 0:
                            return -1;
                        case 1:
                            return  0;
                        case 2:
                            return 1;
                    }
                    break;
            }

            return 0;
        }
        #endregion
        private void Win()
        {
            
        }

        int positionGet(int x,int y)
        {
            switch (puzzleLarge)
            {
                case 5:
                    return _position25[x, y];
                case 4:
                    return _position16[x, y];
                default:
                    return _position9[x, y];
            }
        }
        void positionSet(int x,int y,int buff)
        {
            switch (puzzleLarge)
            {
                case 5:
                     _position25[x, y]=buff;
                     break;
                case 4:
                   _position16[x, y]=buff;
                    break;
                default:
                    _position9[x, y]=buff;
                    break;
            }
        }
        //http://www.dotnetperls.com/fisher-yates-shuffle
        // generate Random Massive
        private void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + Random.Range(0,n - i);
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }
    }

