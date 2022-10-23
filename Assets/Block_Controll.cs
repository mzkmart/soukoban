using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Block_Controll : MonoBehaviour
{
    #region
    //ブロックの配列
    private int[,] _table = new int[10, 20];
    //配列のX座標Y座標
    private int _tableX = 0;
    private int _tableY = 0;
    //7種類のブロックを配列に
    [SerializeField]
    private GameObject[] _block;
    //次のブロックが配列の何番目かを何番目かを入れる
    private int _nextBlock1 = 0;
    private int _nextBlock2 = 0;
    private int _nextBlock3 = 0;
    //操作するブロック
    private GameObject _ContllorBlock;
    //次のブロックの形の画像の配列
    [SerializeField]
    private Sprite[] _spriteBlock;
    //次のブロックを表示させるImage
    [SerializeField]
    private Image _nextContllorBlock;
    [SerializeField]
    private Image _nextContllorBlock2;
    //ブロックの動く間隔
    private float _timeMove = 0;
    //操作するブロックのX座標とY座標
    private float _playerY = 0;
    private float _playerX = 0;
    //ブロックの配列の要素数
    private int _tableMaxX = 10;
    private int _tableMaxY = 20;
    //どの形のブロック化を数値で管理
    private int _oBlock = 0;
    private int _lBlock = 1;
    private int _jBlock = 2;
    private int _sBlock = 3;
    private int _zBlock = 4;
    private int _tBlock = 5;
    private int _iBlock = 6;
    //ブロックが自然落下出来る状態か
    private bool _isfallBlock = true;
    //ブロックが１行に何個あるか
    private int _columnCount = 0;
    //スコアを入れる
    private int _score = 0;
    //スコアを入れるテキスト
    [SerializeField]
    private Text _scoreText;
    //ブロックをどの状態に回転させたのかを数値で管理　０は一度も回転してないデフォルト
    private int _rotation = 0;
    //回転時に動かすブロックを入れる配列
    private Transform[] _moveBlock = new Transform[4];
    //回転時に何個動かしたかの確認
    private int _childrencount = 0;
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
        //操作するブロックとその後二つのブロックをRandom関数で決める
        _nextBlock1 = Random.Range(0, 7);
        _nextBlock2 = Random.Range(0, 7);
        _nextBlock3 = Random.Range(0, 7);
        //最初のブロックの生成
        //その後二つまでのブロックの表示
        _ContllorBlock = Instantiate(_block[_nextBlock1], new Vector2(4, -1), Quaternion.identity);
        _nextContllorBlock.sprite = _spriteBlock[_nextBlock2];
        _nextContllorBlock2.sprite = _spriteBlock[_nextBlock3];
    }


    private void FixedUpdate()
    {
        _timeMove += Time.deltaTime;
        //指定の時間がたったらブロックが一つ下がる
        //ぶつかる位置が違うのでブロックの形に合わせてメソッドを呼び出す
        if (_timeMove >= 0.1f && _isfallBlock)
        {
            //プレイヤーの座標を二次配列検索用に変数に格納
            //Ｙ座標はマイナスに反転してるので-1をかける
            _playerX = _ContllorBlock.transform.position.x;
            _playerY = _ContllorBlock.transform.position.y;
            _playerY = _playerY * -1;
            if (_nextBlock1 == _oBlock)
            {
                oBlock();
            }
            else if (_nextBlock1 == _lBlock)
            {
                lBlock();
            }
            else if (_nextBlock1 == _jBlock)
            {
                jBlock();
            }
            else if (_nextBlock1 == _sBlock)
            {
                sBlock();
            }
            else if (_nextBlock1 == _zBlock)
            {
                zBlock();
            }
            else if (_nextBlock1 == _tBlock)
            {
                tBlock();
            }
            else if (_nextBlock1 == _iBlock)
            {
                iBlock();
            }
        }
        //ブロックが底に着くかぶつかった際に処理を行う
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
        //ブロックの形に合わせたメソッドを呼び出す
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _isfallBlock)
        {
            if (_nextBlock1 == _oBlock)
            {
                oRBlock();
            }
            else if (_nextBlock1 == _lBlock)
            {
                lRBlock();
            }
            else if (_nextBlock1 == _jBlock)
            {
                jRBlock();
            }
            else if (_nextBlock1 == _sBlock)
            {
                sRBlock();
            }
            else if (_nextBlock1 == _zBlock)
            {
                zRBlock();
            }
            else if (_nextBlock1 == _tBlock)
            {
                tRBlock();
            }
            else if (_nextBlock1 == _iBlock)
            {
                iRBlock();
            }
        }
        //Aか左矢印左に移動
        //ブロックの形に合わせたメソッドを呼び出す
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _isfallBlock)
        {
            if (_nextBlock1 == _oBlock)
            {
                oLBlock();
            }
            else if (_nextBlock1 == _lBlock)
            {
                lLBlock();
            }
            else if (_nextBlock1 == _jBlock)
            {
                jLBlock();
            }
            else if (_nextBlock1 == _sBlock)
            {
                sLBlock();
            }
            else if (_nextBlock1 == _zBlock)
            {
                zLBlock();
            }
            else if (_nextBlock1 == _tBlock)
            {
                tLBlock();
            }
            else if (_nextBlock1 == _iBlock)
            {
                iLBlock();
            }
        }
        //スペースで回転
        //ブロックの形に合わせたメソッドを呼び出す
        if ((Input.GetKeyDown(KeyCode.Space)) && _isfallBlock)
        {
            _rotation++;
            if (_rotation > 3)
            {
                _rotation = 0;
            }
            for (_childrencount = 0; _childrencount < _ContllorBlock.transform.childCount; _childrencount++)
            {
                _moveBlock[_childrencount] = _ContllorBlock.transform.GetChild(_childrencount);
            }
                
            if (_nextBlock1 == _lBlock)
            {
                lRotation();
            }
            else if (_nextBlock1 == _jBlock)
            {
                jRotation();
            }
            else if (_nextBlock1 == _sBlock)
            {
                sRotation();
            }
            else if (_nextBlock1 == _zBlock)
            {
                zRotation();
            }
            else if (_nextBlock1 == _tBlock)
            {
                tRotation();
            }
            else if (_nextBlock1 == _iBlock)
            {
                iRotation();
            }
        }
    }

    //配列の中身確認用
    public void ListOutPut(int[,] outList)
    {
        string outString = "";
        for (int i = 0; outList.GetLength(0) > i; i++)
        {
            for (int j = 0; outList.GetLength(1) > j; j++)
            {
                outString += outList[i, j];
            }
            print(outString);
            outString = "";
        }
    }

    //次のブロックを出す
    private void NewBlock()
    {
        //回転をデフォルトに戻す
        _rotation = 0;
        //_nextBlock2の番号のブロックを生成に操作するブロックにする
        _ContllorBlock = Instantiate(_block[_nextBlock2], new Vector2(4, -1), Quaternion.identity);
        //次のブロックの数値を新しくする
        _nextBlock1 = _nextBlock2;
        _nextBlock2 = _nextBlock3;
        //新しく表示するブロックをランダムに決める
        _nextBlock3 = Random.Range(0, 7);
        //数値に基づき表示されてる画像を変える
        _nextContllorBlock.sprite = _spriteBlock[_nextBlock2];
        _nextContllorBlock2.sprite = _spriteBlock[_nextBlock3];
        //ブロックが落ち始めるように trueにする
        _isfallBlock = true;
    }


    /*　メソッドの説明
    ～Block()は操作しているブロックの形に合わせて底に着いたかと他のブロックに当たったかを確認する
    底に着いてなく他のブロックもない場合はY座標を一つ下げる

    ～RBlock（）は操作しているブロックが右端に着いたかと他のブロックに当たるかを確認する
    右端でなく他のブロックにぶつからない場合はX座標を右に動かす

    ～LBlock（）は操作しているブロックが左端に着いたかと他のブロックに当たるかを確認する
    左端でなく他のブロックにぶつからない場合はX座標を左に動かす

    ～Rotation()は操作しているブロックを回転できるかを確認し回転させる
    他のブロックにぶつかる場合などは座標をずらして回転できるのかも確認する
    まだ修正箇所あり
    */

    //O ブロックの処理
    #region
    //□ブロックの時間経過処理
    private void oBlock()
    {
        //時間経過で落ちていく
        //ブロックが底についてないかとブロックが無いかを確認する
        if (_playerY <= 18 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0))
        {
            //操作ブロックのY座標を一つ下げる
            _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
        }
        //底についたか、他のオブジェクトにぶつかったら止まる
        else if (_playerY >= 19 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 1] == 1))
        {
            //座標を動かさず今の座標の配列の中身を１にする
            _table[(int)_playerX, (int)_playerY] = 1;
            _table[(int)_playerX + 1, (int)_playerY] = 1;
            _table[(int)_playerX, (int)_playerY - 1] = 1;
            _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
            //新しいブロックを生成するためにfalseにする
            _isfallBlock = false;
        }
        //動いたのでまた動くまでの間隔を測る
        _timeMove = 0f;

    }
    private void oRBlock()
    {
        //一番右に行っていないかの確認
        if (_playerX >= 8)
        {
            return;
        }
        //右が壁でなくブロックが無い場合に右に1つ移動する
        else if (_table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0)
        {
            _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
        }
    }

    private void oLBlock()
    {
        //一番左に行っていないかの確認
        if (_playerX <= 0)
        {
            return;
        }
        //左が壁でなくブロックが無い場合に左に1つ移動する
        else if (_table[(int)_playerX - 1, (int)_playerY] == 0 && _table[(int)_playerX - 1, (int)_playerY + 1] == 0)
        {
            _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
        }
    }
    #endregion

    //Lブロックの処理
    #region
    //Lブロックの時間経過処理
    private void lBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            //時間経過で落ちていく
            if (_playerY <= 18 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 19 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY - 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 1)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX + 1, (int)_playerY + 2] == 1 || _table[(int)_playerX , (int)_playerY] == 1))
            {
                _table[(int)_playerX, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 2)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY + 2] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX, (int)_playerY + 2] == 1 || _table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 3)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX + 1, (int)_playerY + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX + 1, (int)_playerY + 2] == 1 || _table[(int)_playerX + 2, (int)_playerY + 2] == 1))
            {
                _table[(int)_playerX + 1, (int)_playerY -1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 2, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        _timeMove = 0f;

    }
    private void lRBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 3, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if(_rotation == 1)
        {
            if (_playerX >= 8)
            {
                return;
            }
            else if (_table[(int)_playerX + 2, (int)_playerY + 1 ] == 0 && _table[(int)_playerX + 2, (int)_playerY  + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 3] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 2)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX , (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 3)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY ] == 0 && _table[(int)_playerX , (int)_playerY + 1] == 0 && _table[(int)_playerX + 3, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
    }

    private void lLBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 2)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX - 1, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 3)
        {
            if (_playerX <= -1)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY] == 0 && _table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX - 1, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
    }

    private void lRotation()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, 1);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, 1);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, 1);
            }
        }
        else if (_rotation == 1)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(0, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 1);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 1);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
        }
        else if (_rotation == 2)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(0, -1);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(0, -1);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(0, -1);
            }
        }
        else if (_rotation == 3)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
        }
    }
    #endregion

    //Jブロックの処理
    #region
    //Jブロックの時間経過処理
    private void jBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            //時間経過で落ちていく
            if (_playerY <= 18 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 19 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX, (int)_playerY - 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 1)
        {
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY + 2] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX, (int)_playerY + 2] == 1 || _table[(int)_playerX + 1, (int)_playerY + 2] == 1))
            {
                _table[(int)_playerX, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1,  (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 2)
        {
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 2] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 3)
        {
            if (_playerY <= 17 && (_table[(int)_playerX + 1, (int)_playerY + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX + 1, (int)_playerY + 2] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1))
            {
                _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 2, (int)_playerY - 1] = 1;
                _isfallBlock = false;
            }
        }
        _timeMove = 0f;

    }
    private void jRBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if(_rotation == 1)
        {
            if (_playerX >= 8)
            {
                return;
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if(_rotation == 2)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 3, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if(_rotation == 3)
        {
            if (_playerX >= 8)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
    }

    private void jLBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX - 1, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 2] == 0 && _table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 2)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 3)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
    }

    private void jRotation()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY - 1] == 1)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(0, 1);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(0, 1);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(0, 1);
            }
        }
        else if (_rotation == 1)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 1);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 1);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
        }
        else if (_rotation == 2)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
        }
        else if (_rotation == 3)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, 1);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, 1);
            }
        }
    }
    #endregion

    //Sブロックの処理
    #region
    //Sブロックの時間経過処理
    private void sBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            //時間経過で落ちていく
            if (_playerY <= 18 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 19 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 2, (int)_playerY - 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 1)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 2] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 2)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY + 2] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX, (int)_playerY + 2] == 1 || _table[(int)_playerX + 1, (int)_playerY + 2] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1))
            {
                _table[(int)_playerX, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 3)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 2] == 1))
            {
                _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        _timeMove = 0f;

    }
    private void sRBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX >= 8)
            {
                return;
            }
            else if (_table[(int)_playerX + 1, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 2)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 3)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 3, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
    }

    private void sLBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY] == 0 && _table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 2)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 2] == 0 && _table[(int)_playerX, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 3)
        {
            if (_playerX <= -1)
            {
                return;
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
    }

    private void sRotation()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY - 1] == 1)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(2, 1);
                _moveBlock[2].transform.localPosition = new Vector2(0, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, 0);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(2, 1);
                _moveBlock[2].transform.localPosition = new Vector2(0, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, 0);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(2, 1);
                _moveBlock[2].transform.localPosition = new Vector2(0, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, 0);
            }
        }
        else if (_rotation == 1)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(0, 1);
                _moveBlock[1].transform.localPosition = new Vector2(0, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 1);
                _moveBlock[1].transform.localPosition = new Vector2(0, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
        }
        else if (_rotation == 2)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(1, 0);
                _moveBlock[1].transform.localPosition = new Vector2(2, 0);
                _moveBlock[2].transform.localPosition = new Vector2(0, -1);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(1, 0);
                _moveBlock[1].transform.localPosition = new Vector2(2, 0);
                _moveBlock[2].transform.localPosition = new Vector2(0, -1);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(1, 0);
                _moveBlock[1].transform.localPosition = new Vector2(2, 0);
                _moveBlock[2].transform.localPosition = new Vector2(0, -1);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
        }
        else if (_rotation == 3)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
        }
    }
        #endregion

        //Zブロックの処理
        #region
        //Zブロックの時間経過処理
        private void zBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 2] == 1 || _table[(int)_playerX + 2, (int)_playerY + 2] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 2, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 1)
        {
            //時間経過で落ちていく
            if (_playerY <= 16 && (_table[(int)_playerX, (int)_playerY + 3] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 17 || (_table[(int)_playerX, (int)_playerY + 3] == 1 || _table[(int)_playerX + 1, (int)_playerY + 2] == 1))
            {
                _table[(int)_playerX, (int)_playerY + 1] = 1;
                _table[(int)_playerX, (int)_playerY + 2] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            };
        }
        else if (_rotation == 2)
        {
            //時間経過で落ちていく
            if (_playerY <= 16 && (_table[(int)_playerX, (int)_playerY + 2] == 0 && _table[(int)_playerX + 1, (int)_playerY + 3] == 0 && _table[(int)_playerX + 2, (int)_playerY + 3] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 17 || (_table[(int)_playerX, (int)_playerY + 2] == 1 || _table[(int)_playerX + 1, (int)_playerY + 3] == 1 || _table[(int)_playerX + 2, (int)_playerY + 3] == 1))
            {
                _table[(int)_playerX, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 2] = 1;
                _table[(int)_playerX + 2, (int)_playerY + 2] = 1;
                _isfallBlock = false;
            };
        }
        else if (_rotation == 3)
        {
            //時間経過で落ちていく
            if (_playerY <= 16 && (_table[(int)_playerX + 1, (int)_playerY + 3] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 17 || (_table[(int)_playerX + 1, (int)_playerY + 3] == 1 || _table[(int)_playerX + 2, (int)_playerY + 2] == 1))
            {
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 2] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            };
        }
        _timeMove = 0f;

    }
    private void zRBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 2, (int)_playerY + 1] == 0 && _table[(int)_playerX + 3, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX >= 8)
            {
                return;
            }
            else if (_table[(int)_playerX + 2, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0 && _table[(int)_playerX + 1, (int)_playerY + 3] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            };
        }
        else if (_rotation == 2)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 2, (int)_playerY + 2] == 0 && _table[(int)_playerX + 3, (int)_playerY + 3] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            };
        }
        else if (_rotation == 3)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 3, (int)_playerY + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 3] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            };
        }
    }

    private void zLBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX , (int)_playerY + 1] == 0 && _table[(int)_playerX - 1, (int)_playerY + 2] == 0 && _table[(int)_playerX - 1, (int)_playerY + 3] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            };
        }
        else if (_rotation == 2)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 2] == 0 && _table[(int)_playerX, (int)_playerY + 3] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            };
        }
        else if (_rotation == 3)
        {
            if (_playerX <= -1)
            {
                return;
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY + 2] == 0 && _table[(int)_playerX, (int)_playerY + 3] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            };
        }
    }

    private void zRotation()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY - 1] == 1)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
        }
        else if (_rotation == 1)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(0, -2);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(0, -2);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
        }
        else if (_rotation == 2)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, -1);
                _moveBlock[2].transform.localPosition = new Vector2(1, -2);
                _moveBlock[3].transform.localPosition = new Vector2(2, -2);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, -1);
                _moveBlock[2].transform.localPosition = new Vector2(1, -2);
                _moveBlock[3].transform.localPosition = new Vector2(2, -2);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, -1);
                _moveBlock[2].transform.localPosition = new Vector2(1, -2);
                _moveBlock[3].transform.localPosition = new Vector2(2, -2);
            }
        }
        else if (_rotation == 3)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(1, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, -2);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(1, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, -2);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(2, -1);
            }
        }
    }
        #endregion

        //Tブロックの処理
        #region
        //Tブロックの時間経過処理
        private void tBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            //時間経過で落ちていく
            if (_playerY <= 18 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 19 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 1)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 2] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 2)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 2] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 3)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX + 1, (int)_playerY + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX + 1, (int)_playerY + 2] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1))
            {
                _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _isfallBlock = false;
            }
        }
        _timeMove = 0f;

    }
    private void tRBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX >= 8)
            {
                return;
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 2)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 3)
        {
            if (_playerX >= 7)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
    }

    private void tLBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 2)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 3)
        {
            if (_playerX <= -1)
            {
                return;
            }
            else if (_table[(int)_playerX , (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
    }
    private void tRotation()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY - 1] == 1)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, 1);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, 1);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, 1);
            }
        }
        else if (_rotation == 1)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 1);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 1);
                _moveBlock[2].transform.localPosition = new Vector2(1, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
        }
        else if (_rotation == 2)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(1, -1);
            }
        }
        else if (_rotation == 3)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, 0);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, 0);
            }
        }
    }
    #endregion

    //Iブロックの処理
    #region
    //Iブロックの時間経過処理
    private void iBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            //時間経過で落ちていく
            if (_playerY <= 18 && (_table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0 && _table[(int)_playerX + 3, (int)_playerY + 1] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 19 || (_table[(int)_playerX, (int)_playerY + 1] == 1 || _table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1 || _table[(int)_playerX + 3, (int)_playerY + 1] == 1))
            {
                _table[(int)_playerX, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX + 3, (int)_playerY] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 1)
        {
            //時間経過で落ちていく
            if (_playerY <= 16 && (_table[(int)_playerX + 1, (int)_playerY + 3] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 17 || (_table[(int)_playerX + 1, (int)_playerY + 3] == 1))
            {
                _table[(int)_playerX + 1, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 2] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 2)
        {
            //時間経過で落ちていく
            if (_playerY <= 17 && (_table[(int)_playerX, (int)_playerY + 2] == 0 && _table[(int)_playerX + 1, (int)_playerY + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0 && _table[(int)_playerX + 3, (int)_playerY + 2] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 18 || (_table[(int)_playerX, (int)_playerY + 2] == 1 || _table[(int)_playerX + 1, (int)_playerY + 2] == 1 || _table[(int)_playerX + 2, (int)_playerY + 2] == 1 || _table[(int)_playerX + 3, (int)_playerY + 1] == 2))
            {
                _table[(int)_playerX, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 1, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 2, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 3, (int)_playerY + 1] = 1;
                _isfallBlock = false;
            }
        }
        else if (_rotation == 3)
        {
            //時間経過で落ちていく
            if (_playerY <= 16 && (_table[(int)_playerX + 2, (int)_playerY + 3] == 0))
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
            }
            //他のオブジェクトにぶつかったら止まる
            else if (_playerY >= 17 || (_table[(int)_playerX + 2, (int)_playerY + 3] == 1))
            {
                _table[(int)_playerX + 2, (int)_playerY - 1] = 1;
                _table[(int)_playerX + 2, (int)_playerY] = 1;
                _table[(int)_playerX + 2, (int)_playerY + 1] = 1;
                _table[(int)_playerX + 2, (int)_playerY + 2] = 1;
                _isfallBlock = false;
            }
        }
        _timeMove = 0f;

    }
    private void iRBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX >= 6)
            {
                return;
            }
            else if (_table[(int)_playerX + 4, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX >= 8)
            {
                return;
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 2] == 0 && _table[(int)_playerX + 2, (int)_playerY + 3] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 2)
        {
            if (_playerX >= 6)
            {
                return;
            }
            else if (_table[(int)_playerX + 4, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 3)
        {
            if (_playerX >= 8)
            {
                return;
            }
            else if (_table[(int)_playerX + 3, (int)_playerY] == 0 && _table[(int)_playerX + 3, (int)_playerY + 1] == 0 && _table[(int)_playerX + 3, (int)_playerY + 2] == 0 && _table[(int)_playerX + 3, (int)_playerY + 3] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
            }
        }
    }

    private void iLBlock()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 1)
        {
            if (_playerX <= -1)
            {
                return;
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY + 1] == 0 && _table[(int)_playerX, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 2)
        {
            if (_playerX <= 0)
            {
                return;
            }
            else if (_table[(int)_playerX - 1, (int)_playerY + 2] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
        else if (_rotation == 3)
        {
            if (_playerX <= -1)
            {
                return;
            }
            else if (_table[(int)_playerX + 1, (int)_playerY] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
            }
        }
    }
    private void iRotation()
    {
        //回転によってぶつかる場所は違うのでパターン分けをしている
        if (_rotation == 0)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0 && _table[(int)_playerX, (int)_playerY - 1] == 1)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(3, 0);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(3, 0);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, 0);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, 0);
                _moveBlock[3].transform.localPosition = new Vector2(3, 0);
            }
        }
        else if (_rotation == 1)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(1, -2);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(1, 1);
                _moveBlock[1].transform.localPosition = new Vector2(1, 0);
                _moveBlock[2].transform.localPosition = new Vector2(1, -1);
                _moveBlock[3].transform.localPosition = new Vector2(1, -2);
            }
        }
        else if (_rotation == 2)
        {
            if (_table[(int)_playerX, (int)_playerY] == 1 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, -1);
                _moveBlock[2].transform.localPosition = new Vector2(2, -1);
                _moveBlock[3].transform.localPosition = new Vector2(3, -1);
            }
            else if (_table[(int)_playerX + 2, (int)_playerY] == 1 && _table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 1, (int)_playerY + 1] == 0)
            {
                _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, -1);
                _moveBlock[2].transform.localPosition = new Vector2(2, -1);
                _moveBlock[3].transform.localPosition = new Vector2(3, -1);
            }
            else if (_table[(int)_playerX, (int)_playerY] == 0 && _table[(int)_playerX + 2, (int)_playerY] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(0, -1);
                _moveBlock[1].transform.localPosition = new Vector2(1, -1);
                _moveBlock[2].transform.localPosition = new Vector2(2, -1);
                _moveBlock[3].transform.localPosition = new Vector2(3, -1);
            }
        }
        else if (_rotation == 3)
        {
            if (_table[(int)_playerX + 1, (int)_playerY + 1] == 1 || _table[(int)_playerX + 2, (int)_playerY + 1] == 1)
            {
                _ContllorBlock.transform.localPosition = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
                _moveBlock[0].transform.localPosition = new Vector2(2, 1);
                _moveBlock[1].transform.localPosition = new Vector2(2, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, -2);
            }
            else if (_table[(int)_playerX + 1, (int)_playerY + 1] == 0 && _table[(int)_playerX + 2, (int)_playerY + 1] == 0)
            {
                _moveBlock[0].transform.localPosition = new Vector2(2, 1);
                _moveBlock[1].transform.localPosition = new Vector2(2, 0);
                _moveBlock[2].transform.localPosition = new Vector2(2, -1);
                _moveBlock[3].transform.localPosition = new Vector2(2, -2);
            }
        }
    }
    #endregion

    private void ClearBlock()
    {
        //配列の終番をいれる　要素数-1
        _tableX = 9;
        _tableY = 19;
        //配列を一番下の行からブロックが一列揃っているかを確認
        while (_tableY >= 0)
        {
            while (_tableX >= 0)
            {
                if (_table[_tableX, _tableY] == 1)
                {
                    //行にあったブロックの数を数える
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
        //ブロックが要素数分（横一列）あった場合ブロックを消す
        if (_columnCount >= 10)
        {
            //_tableで使っていない変数を使う
            //Xの要素数は変わらないので9
            int changeTableX = 9;
            //一列揃ったY座標を変数に入れる
            int changeTableY = _tableY;
            //表示されてるブロックをすべて配列に入れる
            GameObject[] killblock = GameObject.FindGameObjectsWithTag("block");
            //配列の末番を変数に入れる
            int searchBlock = killblock.Length - 1;
            //X座標をひとつづつ減らしていきブロックを検索する
            while (changeTableX >= 0)
            {
                while (searchBlock >= 0)
                {
                    //一列揃ったY座標にあるブロックを消す
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
            //消したY座標より上にあるブロックを一つづつ下げる
            while (changeTableY > 0)
            {
                while (changeTableX >= 0)
                {
                    while (searchBlock >= 0)
                    {
                        //ブロックを検索しあった場合はブロックを下げる
                        if (killblock[searchBlock].transform.position.y > _tableY * -1)
                        {
                            killblock[searchBlock].transform.position = new Vector2(killblock[searchBlock].transform.position.x, killblock[searchBlock].transform.position.y - 1f);
                        }
                        searchBlock--;
                    }
                    //一つ上の配列の中身を下の行に下げる
                    _table[changeTableX, changeTableY] = _table[changeTableX, changeTableY - 1];
                    changeTableX--;
                }
                changeTableY--;
                changeTableX = 9;
            }
            //同時に二列揃っていた場合に下げた列も検索をするようにもう一度同じ高さを検索するために_tableYに１を足す
            _tableY++;
            //一列揃ったことでスコアを増やしtextに入れる
            _score = _score + 100;
            _scoreText.text = _score.ToString();
        }
    }
}