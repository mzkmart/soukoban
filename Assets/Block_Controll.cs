using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Block_Controll : MonoBehaviour
{
    #region
    private int[,] _table = new int[10, 20];
    private int _tableX = 0;
    private int _tableY = 0;
    [SerializeField]
    private GameObject[] _block;
    private bool _ismove = true;
    private int _nextBlock1 = 0;
    private int _nextBlock2 = 0;
    private int _nextBlock3 = 0;
    private GameObject _nextContllorBlock;
    [SerializeField]
    private Sprite[] _spriteBlock;
    [SerializeField]
    private Image _nextContllorBlock2;
    [SerializeField]
    private Image _nextContllorBlock3;
    private float _timeMove = 0;
    private float _playerY = 0;
    private float _playerX = 0;
    private int _tableMaxX = 10;
    private int _tableMaxY = 20;
    private int _oBlock = 0;
    private int _lBlock = 1;
    private int _jBlock = 2;
    private int _sBlock = 3;
    private int _zBlock = 4;
    private int _tBlock = 5;
    private int _iBlock = 6;
    private int _newBlock = 7;
    private bool _isfallBlock = true;
    private int _columnCount = 0;
    private int _score = 0;
    [SerializeField]
    private Text _scoreText;
    #endregion

    private void Awake()
    {
        //配列の初期化
        _tableX = 0;
        _tableY = 0;
        while (_tableY < _tableMaxY)
        {
            while (_tableX < _tableMaxX)
            {
                _table[_tableX, _tableY] = 0;
                _tableX++;
            }
            _tableY++;
            _tableX = 0;
        }
        _tableX = 0;
        _tableY = 0;
    }

    private void Start()
    {
        //最初のブロックの生成
        //その後二つまでのブロックの表示
        _nextBlock1 = Random.Range(0, 6);
        _nextBlock2 = Random.Range(0, 6);
        _nextBlock3 = Random.Range(0, 6);
        _nextContllorBlock = Instantiate(_block[_nextBlock1], new Vector2(4, -1), Quaternion.identity);
        _nextContllorBlock2.sprite = _spriteBlock[_nextBlock2];
        _nextContllorBlock3.sprite = _spriteBlock[_nextBlock3];
    }


    private void FixedUpdate()
    {
        _timeMove += Time.deltaTime;
        if (_timeMove >= 0.1f && _isfallBlock)
        {
            _playerX = _nextContllorBlock.transform.position.x;
            _playerY = _nextContllorBlock.transform.position.y;
            _playerY = _playerY * -1;
            if (_nextBlock1 == _oBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _lBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _jBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _sBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _zBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _tBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _iBlock)
            {
                oBlock();
            }
        }
        else if (_isfallBlock == false)
        {
            ClearBlock();
            NewBlock();
        }



    }

    private void Update()
    {
        //操作されたら
        //Dは右矢印で右に移動
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _isfallBlock)
        {
            if (_nextBlock1 == _oBlock)
            {
                oRBlock();
            }
            else if (_nextBlock1 == _lBlock)
            {
                oRBlock();
            }
            else if (_nextBlock1 == _jBlock)
            {
                oRBlock();
            }
            else if (_nextBlock1 == _sBlock)
            {
                oRBlock();
            }
            else if (_nextBlock1 == _zBlock)
            {
                oRBlock();
            }
            else if (_nextBlock1 == _tBlock)
            {
                oRBlock();
            }
            else if (_nextBlock1 == _iBlock)
            {
                oRBlock();
            }
        }
        //Aか左矢印左に移動
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _isfallBlock)
        {
            if (_nextBlock1 == _oBlock)
            {
                oLBlock();
            }
            else if (_nextBlock1 == _lBlock)
            {
                oLBlock();
            }
            else if (_nextBlock1 == _jBlock)
            {
                oLBlock();
            }
            else if (_nextBlock1 == _sBlock)
            {
                oLBlock();
            }
            else if (_nextBlock1 == _zBlock)
            {
                oLBlock();
            }
            else if (_nextBlock1 == _tBlock)
            {
                oLBlock();
            }
            else if (_nextBlock1 == _iBlock)
            {
                oLBlock();
            }
        }
        //スペースで回転
        if ((Input.GetKeyDown(KeyCode.Space)) && _isfallBlock)
        {
            if (_nextBlock1 == _oBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _lBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _jBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _sBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _zBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _tBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _iBlock)
            {
                oBlock();
            }
        }
    }

    //次のブロックを出す
    private void NewBlock()
    {
        _nextContllorBlock = Instantiate(_block[_nextBlock2], new Vector2(4, -1), Quaternion.identity);
        _nextBlock1 = _nextBlock2;
        _nextBlock2 = _nextBlock3;
        _nextBlock3 = Random.Range(0, 6);
        _nextContllorBlock2.sprite = _spriteBlock[_nextBlock2];
        _nextContllorBlock3.sprite = _spriteBlock[_nextBlock3];
        _isfallBlock = true;
    }


    //O ブロックの処理
    #region
    //□ブロックの時間経過処理
    private void oBlock()
    {
        //時間経過で落ちていく
        if (_playerY <= 18 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0))
        {
            _nextContllorBlock.transform.position = new Vector2(_nextContllorBlock.transform.position.x, _nextContllorBlock.transform.position.y - 1);
        }
        //他のオブジェクトにぶつかったら止まる
        else if (_playerY >= 19 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 1] == 1))
        {
            _table[(int)_playerX, (int)_playerY] = 1;
            _table[(int)_playerX + 1, (int)_playerY] = 1;
            _table[(int)_playerX, (int)_playerY - 1] = 1;
            _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
            _isfallBlock = false;
        }
        _timeMove = 0f;

    }
    private void oRBlock()
    {
        if (_playerX >= 8)
        {
            return;
        }
        else if (_table[(int)_playerX + 2, (int)_playerY] == 0)
        {
            _nextContllorBlock.transform.position = new Vector2(_nextContllorBlock.transform.position.x + 1, _nextContllorBlock.transform.position.y);
        }
    }

    private void oLBlock()
    {
        if (_playerX <= 0)
        {
            return;
        }
        else if (_table[(int)_playerX - 1, (int)_playerY] == 0)
        {
            _nextContllorBlock.transform.position = new Vector2(_nextContllorBlock.transform.position.x - 1, _nextContllorBlock.transform.position.y);
        }
    }
    #endregion

    private void ClearBlock()
    {
        _tableX = 9;
        _tableY = 19;
        while (_tableY >= 0)
        {
            while (_tableX >= 0)
            {
                if (_table[_tableX, _tableY] == 1)
                {
                    _columnCount++;
                    ClearBlock2();

                }
                _tableX--;
            }
            _tableY--;
            _tableX = 9;
            _columnCount = 0;
        }
    }
    private void ClearBlock2()
    {
        if (_columnCount >= 10)
        {
            int changeTableX = 9;
            int changeTableY = _tableY;
            GameObject[] killblock = GameObject.FindGameObjectsWithTag("block");
            int searchBlock = killblock.Length - 1;
            while (changeTableX >= 0)
            {
                while (searchBlock >= 0)
                {
                    if (killblock[searchBlock].transform.position.x == changeTableX && killblock[searchBlock].transform.position.y == _tableY * -1)
                    {
                        Destroy(killblock[searchBlock]);
                    }
                    searchBlock--;
                }
                searchBlock = killblock.Length - 1;
                changeTableX--;
            }
            changeTableX = 9;
            while (changeTableY > 0)
            {
                while (changeTableX >= 0)
                {
                    while (searchBlock >= 0)
                    {
                        if (killblock[searchBlock].transform.position.y > _tableY * -1)
                        {
                            killblock[searchBlock].transform.position = new Vector2(killblock[searchBlock].transform.position.x, killblock[searchBlock].transform.position.y - 1f);
                        }
                        searchBlock--;
                    }
                    _table[changeTableX, changeTableY] = _table[changeTableX, changeTableY - 1];
                    changeTableX--;
                }
                changeTableY--;
                changeTableX = 9;
            }
            _tableY++;
            _score = _score + 100;
            _scoreText.text = _score.ToString();
        }
    }
}