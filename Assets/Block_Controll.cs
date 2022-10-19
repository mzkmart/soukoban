using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Controll : MonoBehaviour
{
    private int[,] _table = new int[10, 20];
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
        _playerX = _nextContllorBlock1.transform.position.x;
        _playerY = _nextContllorBlock1.transform.position.y;
        _playerY = _playerY * -1;
        if (_nextBlock1 == _oBlock)
        {
            oBlock();
        }
        else if (_nextBlock1 == _oBlock)
        {
            oBlock();
        }
        else if (_nextBlock1 == _oBlock)
        {
            oBlock();
        }
        else if (_nextBlock1 == _oBlock)
        {
            oBlock();
        }
        else if (_nextBlock1 == _oBlock)
        {
            oBlock();
        }
        else if (_nextBlock1 == _oBlock)
        {
            oBlock();
        }
        else if (_nextBlock1 == _oBlock)
        {
            oBlock();
        }
        else if (_nextBlock1 == _oBlock)
        {
            NewBlock();
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
    }

    //□ブロックの時間経過処理
    private void oBlock()
    {
        //一番底に着いたら
        if (_playerY >= 18)
        {
            _table[(int)_playerX, (int)_playerY] = 1;
            _table[(int)_playerX + 1, (int)_playerY] = 1;
            _table[(int)_playerX, (int)_playerY + 1] = 1;
            _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
            _nextBlock1 = 7;
            return;
        }
        //時間経過で落ちていく
        else if (_timeMove >= 1f)
        {
            if (_playerY < 18 && _table[(int)_playerX, (int)_playerY + 2] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0)
            {
                _nextContllorBlock1.transform.position = new Vector2(_nextContllorBlock1.transform.position.x, _nextContllorBlock1.transform.position.y - 1);
                _timeMove = 0f;
                Debug.Log(_nextContllorBlock1.transform.position.y);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_table[(int)_playerX, (int)_playerY + 2] == 1 && _table[(int)_playerX + 1, (int)_playerY + 2] == 1)
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _timeMove = 0f;
                _nextBlock1 = 7;
                return;
            }

        }
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
}