using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Controll : MonoBehaviour
{
    #region
    private int[,] _table = new int[10, 22];
    private int _tableX = 0;
    private int _tableY = 0;
    [SerializeField]
    private GameObject[] _block;
    private bool _ismove = true;
    private int _nextBlock1 = 0;
    private int _nextBlock2 = 0;
    private int _nextBlock3 = 0;
    private GameObject _nextContllorBlock1;
    private GameObject _nextContllorBlock2;
    private GameObject _nextContiiolBlock3;
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
        _nextContllorBlock1 = Instantiate(_block[_nextBlock1], new Vector2(4, 0), Quaternion.identity);
        _nextContllorBlock2 = Instantiate(_block[_nextBlock2], new Vector2(11, -1), Quaternion.identity);
        _nextContiiolBlock3 = Instantiate(_block[_nextBlock3], new Vector2(11, -4), Quaternion.identity);
    }


    private void FixedUpdate()
    {
        _timeMove += Time.deltaTime;
        if (_timeMove >= 1f && _isfallBlock)
        {
            _playerX = _nextContllorBlock1.transform.position.x;
            _playerY = _nextContllorBlock1.transform.position.y;
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
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_nextBlock1 == _oBlock)
            {
                Debug.Log("右に移動");
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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("左に移動");
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
        if (Input.GetKeyDown(KeyCode.Space))
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
        _nextContllorBlock2.transform.position = new Vector2(4, 0);
        _nextContllorBlock1 = _nextContllorBlock2;
        _nextContiiolBlock3.transform.position = new Vector2(11, -1);
        _nextContllorBlock2 = _nextContiiolBlock3;
        _nextBlock1 = _nextBlock2;
        _nextBlock2 = _nextBlock3;
        _nextBlock3 = Random.Range(0, 6);
        _nextContiiolBlock3 = Instantiate(_block[_nextBlock3], new Vector2(11, -4), Quaternion.identity);
        _isfallBlock = true;
    }


    //O ブロックの処理
    #region
    //□ブロックの時間経過処理
    private void oBlock()
    {
        //時間経過で落ちていく
        if (_table[(int)_playerX, (int)_playerY + 2] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0)
        {
            _nextContllorBlock1.transform.position = new Vector2(_nextContllorBlock1.transform.position.x, _nextContllorBlock1.transform.position.y - 1);
            //底に着いたら
            if (_nextContllorBlock1.transform.position.y <= -18)
            {
                _table[(int)_playerX, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX, (int)_playerY + 2] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 2] = 1;
                _isfallBlock = false;
            }
        }
        //他のオブジェクトにぶつかったら止まる
        else if (_table[(int)_playerX, (int)_playerY + 2] == 1  || _table[(int)_playerX + 1, (int)_playerY + 2] == 1)
        {
            _table[(int)_playerX, (int)_playerY] = 1;
            _table[(int)_playerX + 1, (int)_playerY] = 1;
            _table[(int)_playerX, (int)_playerY + 1] = 1;
            _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
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
            _nextContllorBlock1.transform.position = new Vector2(_nextContllorBlock1.transform.position.x + 1, _nextContllorBlock1.transform.position.y);
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
            _nextContllorBlock1.transform.position = new Vector2(_nextContllorBlock1.transform.position.x - 1, _nextContllorBlock1.transform.position.y);
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
                if(_table[_tableX, _tableY] == 1)
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
        if(_columnCount >= 10)
        {
            int changeTableX = 9;
            int changeTableY = _tableY;
            while (changeTableY > 0)
            {
                while (changeTableX >= 0)
                {
                    _table[changeTableX , changeTableY] = _table[changeTableX, changeTableY - 1];
                    changeTableX--;
                }
                changeTableY--;
                changeTableX = 9;
            }
        }
    }
}