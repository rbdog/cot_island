using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PianoKeyObserver
{
    void OnStrikeKey(Piano.Key key);
}

public class PianoKey : MonoBehaviour
{
    // 押されたことを知らせる相手
    public PianoKeyObserver observer;
    // どのキーを担当しているか
    public Piano.Key key;
    // これ以上 上がらない位置 Y
    float maxY;
    // これ以上 下がらない位置 Y
    float minY;
    // 音を出す助走として必要な最低高さ 位置 Y
    float chargeY;
    // 音が出て 再び助走が必要になる 位置 Y
    float strikeY;
    // 下がるスピード
    readonly float downSpeed = 1.0f;

    // 判定カプセルの半径
    float radius = 0.4f;
    // 判定カプセルを飛ばす方向
    Vector3 dir = Vector3.up;
    // 判定カプセルを飛ばす距離
    float distance = 0.8f;

    // 音を出す準備ができているか
    bool charged = true;

    // 鍵盤が押されているかどうか
    bool isPushed()
    {
        // 判定カプセルの前端
        var start = transform.position - Vector3.forward * 0.2f;
        // 判定カプセルの後端
        var end = transform.position + Vector3.forward * 0.2f;
        // 判定カプセルが何かにぶつかったかどうか
        var hasHit = Physics.CapsuleCast(start, end, radius, dir, distance);
        return hasHit;
    }

    void Start()
    {
        this.maxY = transform.position.y;
        this.minY = transform.position.y - 0.8f;
        this.chargeY = transform.position.y - 0.3f;
        this.strikeY = transform.position.y - 0.7f;
    }

    void Update()
    {
        var push = isPushed();
        Vector3 pos = transform.position;
        if (push)
        {
            // 押されている時

            if (pos.y > minY)
            {
                // まだ沈む余裕がある
                
                pos.y -= downSpeed * Time.deltaTime;
                transform.position = pos; // 少し下げる

                if (pos.y < strikeY)
                {
                    // 音を出す高さまで来た

                    if (charged)
                    {
                        // 助走がついていた

                        charged = false; // 助走を解除する
                        observer!.OnStrikeKey(key!); // 音を鳴らす
                    }
                    
                }
            }
        }
        else
        {
            // 押されていない

            if (pos.y < maxY)
            {
                // 最高地点よりも沈んでいた
                
                pos.y += downSpeed * Time.deltaTime;
                transform.position = pos; // 少し上げて元に戻す

                if (pos.y > chargeY)
                {
                    // 音を鳴らす助走が十分な高さまで来た

                    if (!charged)
                    {
                        // 助走完了していなかった
                        charged = true; // 助走完了にする
                    }
                }
            }
        }
    }
}
