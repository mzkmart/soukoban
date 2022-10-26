using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityRandom = UnityEngine.Random;

public class BlockControll : MonoBehaviour
{
    #region
    //ブロックの配列
    private int[,] _field = new int[24, 18];
    //配列のX座標Y座標
    private int _fieldX = 0;
    private int _fieldY = 0;
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
    private int _fieldMaxX = 10;
    private int _fieldMaxY = 20;
    //どの形のブロック化を数値で管理
    private const int _oBlock = 0;
    private const int _lBlock = 1;
    private const int _jBlock = 2;
    private const int _sBlock = 3;
    private const int _zBlock = 4;
    private const int _tBlock = 5;
    private const int _iBlock = 6;
    //ブロックが自然落下出来る状態か
    private bool _isfallBlock = true;
    //ブロックが１行に何個あるか
    private int _rawCount = 0;
    //スコアを入れる
    private int _score = 0;
    //スコアを入れるテキスト
    [SerializeField]
    private Text _scoreText;
    //ブロックをどの状態に回転させたのかを数値で管理　０は一度も回転してないデフォルト
    private int _playerRotate = 0;
    //回転時に動かすブロックを入れる配列
    private Transform[] _moveBlock = new Transform[4];
    //回転時に何個動かしたかの確認
    private int _childrenCount = 0;
    //ブロックの生成が初めてかどうか
    private bool _iscreationFirst = true;
    private float _timeMoveHorizontal = 0;
    //落ちてくるブロックの生成場所
    private const float _respronPlayerX = 8;
    private const float _respronPlayerY = 0;

    //どのブロックの当たり判定を取りたいのか
    private float[,,] _oDecision = new float[1, 4, 4]
    {  {
       {1, 1, 0, 0},
       {1, 1, 0, 0},
       {0, 0, 0, 0},
       {0, 0, 0, 0}
       }
    };
    private float[,,] _lDecision = new float[4, 3, 3]
    { 
        {
            {0, 0, 1},
            {1, 1, 1},
            {0, 0, 0}
        },
        {
            {1, 1, 0},
            {0, 1, 0},
            {0, 1, 0}
        },
        {
            {0, 0, 0},
            {1, 1, 1},
            {1, 0, 0}
        },
        {
            {0, 1, 0},
            {0, 1, 0},
            {0, 1, 1}
        } 
    };
    private float[,,] _jDecision = new float[4, 3, 3]
    {
        {
            {1, 0, 0},
            {1, 1, 1},
            {0, 0, 0}
        },
        {
            {0, 1, 0},
            {0, 1, 0},
            {1, 1, 0}
        },
        {
            {0, 0, 0},
            {1, 1, 1},
            {0, 0, 1}
        },
        {
            {0, 1, 1},
            {0, 1, 0},
            {0, 1, 0}
        }
    };
    private float[,,] _sDecision = new float[4, 3, 3]
    {
        {
            {0, 1, 1},
            {1, 1, 0},
            {0, 0, 0}
        },
        {
            {1, 0, 0},
            {1, 1, 0},
            {0, 1, 0}
        },
        {
            {0, 0, 0},
            {0, 1, 1},
            {1, 1, 0}
        },
        {
            {0, 1, 0},
            {0, 1, 1},
            {0, 0, 1}
        }
    };
    private float[,,] _zDecision = new float[4, 3, 3]
    {
        {
            {1, 1, 0},
            {0, 1, 1},
            {0, 0, 0}
        },
        {
            {0, 1, 0},
            {1, 1, 0},
            {1, 0, 0}
        },
        {
            {0, 0, 0},
            {1, 1, 0},
            {0, 1, 1}
        },
        {
            {0, 0, 1},
            {0, 1, 1},
            {0, 1, 0}
        }
    };
    private float[,,] _tDecision = new float[4, 3, 3]
    {
        {
            {0, 1, 0},
            {1, 1, 1},
            {0, 0, 0}
        },
        {
            {0, 1, 0},
            {1, 1, 0},
            {0, 1, 0}
        },
        {
            {0, 0, 0},
            {1, 1, 1},
            {0, 1, 0}
        },
        {
            {0, 1, 0},
            {0, 1, 1},
            {0, 1, 0}
        }
    };
    private float[,,] _iDecision = new float[4, 4, 4]
    {
        {
            {0, 0, 0, 0},
            {1, 1, 1, 1},
            {0, 0, 0, 0},
            {0, 0, 0, 0}
        },
        {
            {0, 1, 0, 0},
            {0, 1, 0, 0},
            {0, 1, 0, 0},
            {0, 1, 0, 0}
        },
        {
            {0, 0, 0, 0},
            {0, 0, 0, 0},
            {1, 1, 1, 1},
            {0, 0, 0, 0}
        },
        {
            {0, 0, 1, 0},
            {0, 0, 1, 0},
            {0, 0, 1, 0},
            {0, 0, 1, 0}
        }
    };

    //当たり判定の配列をリストに入れる
    private List<float[,,]> _Decision = new List<float[,,]>();
    //当たり判定の座標指定用変数
    private float _decidionX = 0;
    private float _decidionY = 0;
    
    [SerializeField]
    private Text[] _texts; 
    #endregion

    private void Awake()
    {
        kindSet();
        //配列の初期化
        _fieldX = 0;
        _fieldY = 0;
        while (_fieldY < _fieldMaxY)
        {
            while (_fieldX < _fieldMaxX)
            {
                _field[_fieldY, _fieldX] = 0;
                _fieldX++;
            }
            _fieldY++;
            _fieldX = 0;
        }
        _fieldX = 3;
        _fieldY = 23;
        while (_fieldY >= 0)
        {
            while (_fieldX >= 0)
            {
                _field[_fieldY, _fieldX] = 1;
                _fieldX--;
            }
            _fieldY--;
            _fieldX = 3;
        }
        _fieldX = 17;
        _fieldY = 23;
        while (_fieldY >= 0)
        {
            while (_fieldX >= 14)
            {
                _field[_fieldY, _fieldX] = 1;
                _fieldX--;
            }
            _fieldY--;
            _fieldX = 17;
        }
        _fieldX = 17;
        _fieldY = 23;
        while (_fieldY >= 20)
        {
            while (_fieldX >= 0)
            {
                _field[_fieldY, _fieldX] = 1;
                _fieldX--;
            }
            _fieldY--;
            _fieldX = 17;
        }
        ListOutPut2(_field);
        NewBlock();
    }

    private void FixedUpdate()
    {
        _timeMove += Time.deltaTime;
        _timeMoveHorizontal += Time.deltaTime;
        //指定の時間がたったらブロックが一つ下がる
        //ぶつかる位置が違うのでブロックの形に合わせてメソッドを呼び出す
        if (_timeMove >= 0.1f && _isfallBlock)
        {
            //プレイヤーの座標を二次配列検索用に変数に格納
            //Ｙ座標はマイナスに反転してるので-1をかける
            _playerX = _ContllorBlock.transform.position.x;
            _playerY = _ContllorBlock.transform.position.y;
            _playerY = _playerY * -1;
            fallBlock();
        }
        //ブロックが底に着くかぶつかった際に処理を行う
        else if (_isfallBlock == false)
        {
            ClearBlock();
            //スポーンの場所にブロックがあったらゲームオーバー
            if(_field[(int)_respronPlayerY, (int)_respronPlayerX] == 1 || _field[(int)_respronPlayerY, (int)_respronPlayerX + 1] == 1)
            {
                Debug.Log("ゲームオーバー");
                Time.timeScale = 0;
            }
            ListOutPut2(_field);
            NewBlock();
        }
    }

    private void Update()
    {
        //操作されたら
        //Dは右矢印で右に移動
        //ブロックの形に合わせたメソッドを呼び出す
        if (Input.GetAxis("Horizontal") > 0 && _isfallBlock && _timeMoveHorizontal >= 0.3f)
        {
            RightBlock();
            _timeMoveHorizontal = 0;
        }
        //Aか左矢印左に移動
        //ブロックの形に合わせたメソッドを呼び出す
        if (Input.GetAxis("Horizontal") < 0 && _isfallBlock && _timeMoveHorizontal >= 0.3f)
        {
            LeftBlock();
            _timeMoveHorizontal = 0;
        }
        //スペースで回転
        //ブロックの形に合わせたメソッドを呼び出す
        if (Input.GetButtonDown("Jump") && _isfallBlock)
        {
            RotateBlock();
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
    public void ListOutPut2(int[,] outList)
    {
        int i;
        string outString = "";
        for (i = 0; outList.GetLength(0) > i; i++)
        {
            for (int j = 0; outList.GetLength(1) > j; j++)
            {
                outString += outList[i, j];
            }
            _texts[i].text = outString;
            outString = "";
        }
    }

    //リストに配列を順番に入れる
    public void kindSet()
    {
        _Decision.Add(_oDecision);
        _Decision.Add(_lDecision);
        _Decision.Add(_jDecision);
        _Decision.Add(_sDecision);
        _Decision.Add(_zDecision);
        _Decision.Add(_tDecision);
        _Decision.Add(_iDecision);
    }
    //次のブロックを出す
    private void NewBlock()
    {
        if (_iscreationFirst)
        {
            //操作するブロックとその後二つのブロックをRandom関数で決める
            _nextBlock1 = UnityRandom.Range(0, 7);
            _nextBlock2 = UnityRandom.Range(0, 7);
            _nextBlock3 = UnityRandom.Range(0, 7);
            _iscreationFirst = false;
        }
        else
        {
            //回転をデフォルトに戻す
            _playerRotate = 0;
            //次のブロックの数値を新しくする
            _nextBlock1 = _nextBlock2;
            _nextBlock2 = _nextBlock3;
            //新しく表示するブロックをランダムに決める
            _nextBlock3 = UnityRandom.Range(0, 7);
        }
        //その後二つまでのブロックの表示
        _ContllorBlock = Instantiate(_block[_nextBlock1], new Vector2(_respronPlayerX, _respronPlayerY), Quaternion.identity);
        _nextContllorBlock.sprite = _spriteBlock[_nextBlock2];
        _nextContllorBlock2.sprite = _spriteBlock[_nextBlock3];
        //ブロックが落ち始めるように trueにする
        _isfallBlock = true;
    }

    //fallBlockとfallBlock2で自然落下させる
    //ぶつかるまで下に下げる
    private void fallBlock()
    {
        _decidionY = 0;
        _decidionX = 0;
        while (_decidionY < _Decision[_nextBlock1].GetLength(1))
        {
            while (_decidionX < _Decision[_nextBlock1].GetLength(2))
            {
                //プレイヤーの一個下にブロックがあるかを確認する
                if ((_field[(int)_playerY + (int)_decidionY + 1, (int)_playerX + (int)_decidionX] == _Decision[_nextBlock1][_playerRotate, (int)_decidionY, (int)_decidionX])
                     && _field[(int)_playerY + (int)_decidionY + 1, (int)_playerX + (int)_decidionX] == 1)

                {
                    fallBlock2();
                    return;
                }
                _decidionX++;
            }
            _decidionY++;
            _decidionX = 0;
        }
        //ぶつからない場合は１マス下げる
        _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x, _ContllorBlock.transform.position.y - 1);
        _timeMove = 0f;

    }
    private void fallBlock2()
    {
        //ぶつかった際はその座標に配列の中身を転写する
        _isfallBlock = false;
        _decidionY = 0;
        _decidionX = 0;
        while (_decidionY < _Decision[_nextBlock1].GetLength(1))
        {
            while (_decidionX < _Decision[_nextBlock1].GetLength(2))
            {
                if (_Decision[_nextBlock1][_playerRotate, (int)_decidionY, (int)_decidionX] == 1)
                {
                    _field[(int)_playerY + (int)_decidionY, (int)_playerX + (int)_decidionX] = 1;
                }
                _decidionX++;
            }
            _decidionY++;
            _decidionX = 0;
        }
        _timeMove = 0f;
    }

    //ブロックを右に移動させる
    private void RightBlock()
    {
        _decidionY = 0;
        _decidionX = 0;
        while (_decidionY < _Decision[_nextBlock1].GetLength(1))
        {
            while (_decidionX < _Decision[_nextBlock1].GetLength(2))
            {
                //Playerより1マス右に配列を照らし合わせぶつかるかを確認する
                if ((_field[(int)_playerY + (int)_decidionY, (int)_playerX + (int)_decidionX + 1] == _Decision[_nextBlock1][_playerRotate,(int)_decidionY, (int)_decidionX])
                     && _field[(int)_playerY + (int)_decidionY, (int)_playerX + (int)_decidionX + 1] == 1)

                {
                    _timeMove = 0f;
                    return;
                }
                _decidionX++;
            }
            _decidionY++;
            _decidionX = 0;
        }
        _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x + 1, _ContllorBlock.transform.position.y);
        _timeMove = 0f;
    }

    //ブロックを左に移動させる
    private void LeftBlock()
    {
        _decidionY = 0;
        _decidionX = 0;
        while (_decidionY < _Decision[_nextBlock1].GetLength(1))
        {
            while (_decidionX < _Decision[_nextBlock1].GetLength(2))
            {
                //Playerより1マス左に配列を照らし合わせぶつかるかを確認する
                if ((_field[(int)_playerY + (int)_decidionY, (int)_playerX + (int)_decidionX - 1] == _Decision[_nextBlock1][_playerRotate,(int)_decidionY, (int)_decidionX])
                     && _field[(int)_playerY + (int)_decidionY, (int)_playerX + (int)_decidionX - 1] == 1)

                {
                    _timeMove = 0f;
                    return;
                }
                _decidionX++;
            }
            _decidionY++;
            _decidionX = 0;
        }
        _ContllorBlock.transform.position = new Vector2(_ContllorBlock.transform.position.x - 1, _ContllorBlock.transform.position.y);
        _timeMove = 0f;
    }
    
    //ブロックを回転させる
    private void RotateBlock()
    {
        _playerRotate++;
        //回転は４種類しかないので４になったら０に戻す
        //またOブロックは回転がないので０固定にする
        if (_playerRotate > 3 || _nextBlock1 == 0)
        {
            _playerRotate = 0;
        }
        //プレイヤーの子オブジェクトを検索し取得する
        //回転時に子オブジェクトを動かすために配列に入れておく
        for (_childrenCount = 0; _childrenCount < _ContllorBlock.transform.childCount; _childrenCount++)
        {
            _moveBlock[_childrenCount] = _ContllorBlock.transform.GetChild(_childrenCount);
        }

        _decidionY = 0;
        _decidionX = 0;
        int moveChildren = 0;
        //ブロックごとに作った配列に照らし合わせて1の部分にブロックを配置する
        while (_decidionY < _Decision[_nextBlock1].GetLength(1))
        {
            while (_decidionX < _Decision[_nextBlock1].GetLength(2))
            {
                if (_Decision[_nextBlock1][_playerRotate, (int)_decidionY, (int)_decidionX] == 1)
                {                    
                    _moveBlock[moveChildren].transform.localPosition = new Vector2(_decidionX, _decidionY * -1);
                    moveChildren++;
                }
                _decidionX++;
            }
            _decidionY++;
            _decidionX = 0;
        }
    }

    private void ClearBlock()
    {
        //配列の終番をいれる　要素数-1
        _fieldX = 13;
        _fieldY = 19;
        //配列を一番下の行からブロックが一列揃っているかを確認
        while (_fieldY >= 0)
        {
            while (_fieldX >= 4)
            {
                if (_field[_fieldY, _fieldX] == 1)
                {
                    //行にあったブロックの数を数える
                    _rawCount++;
                    ClearBlock2();

                }
                _fieldX--;
            }
            _fieldY--;
            _fieldX = 13;
            _rawCount = 0;
        }
    }
    private void ClearBlock2()
    {
        //ブロックが要素数分（横一列）あった場合ブロックを消す
        if (_rawCount >= 10)
        {
            //_tableで使っていない変数を使う
            //Xの要素数は変わらないので9
            int changeTableX = 13;
            //一列揃ったY座標を変数に入れる
            int changeTableY = _fieldY;
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
                    if (killblock[searchBlock].transform.position.x == changeTableX && killblock[searchBlock].transform.position.y == _fieldY * -1)
                    {
                        Destroy(killblock[searchBlock]);
                    }
                    searchBlock--;
                }
                searchBlock = killblock.Length - 1;
                changeTableX--;
            }
            changeTableX = 13;
            //消したY座標より上にあるブロックを一つづつ下げる
            while (changeTableY > 0)
            {
                while (changeTableX >= 4)
                {
                    while (searchBlock >= 0)
                    {
                        //ブロックを検索しあった場合はブロックを下げる
                        if (killblock[searchBlock].transform.position.y > _fieldY * -1)
                        {
                            killblock[searchBlock].transform.position = new Vector2(killblock[searchBlock].transform.position.x, killblock[searchBlock].transform.position.y - 1f);
                        }
                        searchBlock--;
                    }
                    //一つ上の配列の中身を下の行に下げる
                    _field[changeTableY, changeTableX] = _field[changeTableY - 1, changeTableX];
                    changeTableX--;
                }
                changeTableY--;
                changeTableX = 13;
            }
            //同時に二列揃っていた場合に下げた列も検索をするようにもう一度同じ高さを検索するために_tableYに１を足す
            _fieldY++;
            //一列揃ったことでスコアを増やしtextに入れる
            _score = _score + 100;
            _scoreText.text = _score.ToString();
        }
    }
}