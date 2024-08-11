using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Transform dropBoxesHolder {  get; private set; }

    [SerializeField] Transform _floor;
    [SerializeField] Transform _dropBoxesHolder;

    Player _player;

    void Awake()
    {
        dropBoxesHolder = _dropBoxesHolder;
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
        foreach (Transform child in _dropBoxesHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
