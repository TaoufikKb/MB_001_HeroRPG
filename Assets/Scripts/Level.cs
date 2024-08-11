using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Transform dropsHolder {  get; private set; }

    [SerializeField] Transform _floor;
    [SerializeField] Transform _dropsHolder;

    Player _player;

    void Awake()
    {
        dropsHolder = _dropsHolder;
    }
    void Start()
    {
        _player=Player.instance;
    }


    void Update()
    {
        Vector3 targetFloorPos = _player.transform.position;

        targetFloorPos = new Vector3((int)(targetFloorPos.x / 10), 0, (int)(targetFloorPos.z / 10)) * 10;

        _floor.position = targetFloorPos;
    }

    public void ClearLevel()
    {
        foreach (Transform child in _dropsHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
