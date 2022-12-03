using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    public Grid _grid;
    public float _speed = 5.0f;
    Vector3Int _cellPos = Vector3Int.zero;

    MoveDir _dir = MoveDir.None;
    bool _isMoving = false;


    void Start()
    {
        Vector3 pos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f);
        transform.position = pos;
    }

    void Update()
    {
        GetDirInput();
        UpdatePosition();
        UpdateIsMoving();
        
    }
    // 키보드 입력을 받아 방향을 정하는 함수
    void UpdatePosition()
    {
        if (_isMoving == false)
            return;

        Vector3 destPos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f);
        Vector3 moveDir = destPos - transform.position;

        // 도착 여부 체크
        // if조건문 : 이동해야하는 거리(목적지까지의 거리 dist)가 한 프레임에 움직일 수 있는 거리보다 작다면 도착했다고 간주
        float dist = moveDir.magnitude;
        if (dist< _speed * Time.deltaTime)
        {
            transform.position = destPos;
            _isMoving = false;
        }
        else 
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
        }
    }

    // 시각적인 이동을 구현하는 함수 (실제 이동x)
    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _dir = MoveDir.Right;
        }
        else
        {
            _dir = MoveDir.None;
        }
    }

    // 이동가능한 상태일 때 실질적인 위치인 _cellPos를 움직이는 함수
    void UpdateIsMoving()
    {
        if (_isMoving == false)
        {
            switch (_dir)
            {
                case MoveDir.Up:
                    _cellPos += Vector3Int.up;
                    _isMoving = true;
                    break;
                case MoveDir.Down:
                    _cellPos += Vector3Int.down;
                    _isMoving = true;
                    break;
                case MoveDir.Left:
                    _cellPos += Vector3Int.left;
                    _isMoving = true;
                    break;
                case MoveDir.Right:
                    _cellPos += Vector3Int.right;
                    _isMoving = true;
                    break;

            }
        }
    }
}
