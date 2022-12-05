using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ArrowController : CreatureController
{

    protected override void Init()
    {
        _speed = 10.0f;

        switch (_lastDir)
        {
            case MoveDir.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case MoveDir.Down:
                transform.rotation = Quaternion.Euler(0, 0, -180);
                break;
            case MoveDir.Left:
                transform.rotation = Quaternion.Euler(0, 0, -270);
                break;
            case MoveDir.Right:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }

        base.Init();
    }

    protected override void UpdateAnimation()
    {

    }

    protected override void UpdateIdle()
    {
        if (_dir != MoveDir.None)
        {
            Vector3Int destPos = Cellpos;

            switch (_dir)
            {
                case MoveDir.Up:
                    destPos += Vector3Int.up;
                    break;
                case MoveDir.Down:
                    destPos += Vector3Int.down;
                    break;
                case MoveDir.Left:
                    destPos += Vector3Int.left;
                    break;
                case MoveDir.Right:
                    destPos += Vector3Int.right;
                    break;
            }
            State = CreatureState.Moving;

            if (Managers.Map.CanGO(destPos))
            {
                GameObject go = Managers.Object.Find(destPos);
                if (go != null)
                {
                    Debug.Log(go.name);
                    Managers.Resource.Destroy(gameObject);
                }
                Cellpos = destPos;
            }
            else
            {
                Managers.Resource.Destroy(gameObject);
            }
        }
    }
}
