using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class SwordDropBox : MonoBehaviour
{
    [SerializeField] GameObject _collectFx;
    [SerializeField] WeaponData[] _weaponsData;

    bool _opened = false;

    void Start()
    {
        transform.localScale = Vector3.zero;

        Vector3 targetJumpPos = Random.insideUnitCircle.normalized * 3;
        targetJumpPos.z = targetJumpPos.y;
        targetJumpPos += transform.position;
        targetJumpPos.y = 0;

        transform.DOJump(targetJumpPos, 2, 1, 1).SetEase(Ease.Linear).OnComplete(() => transform.DOPunchScale(Vector3.one * 0.25f, 0.25f));
        transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_opened && other.CompareTag("Player"))
        {
            _opened = true;

            Player player = Player.instance;
            WeaponData[] newWeaponsData = _weaponsData.Where((w) => w != player.currentWeaponData).ToArray();

            int index = Random.Range(0, newWeaponsData.Length);
            WeaponData weaponData = newWeaponsData[index];

            player.CollectWeapon(weaponData);

            Destroy(Instantiate(_collectFx, transform.position + Vector3.up, transform.rotation), 1);

            transform.DOKill();
            Destroy(gameObject);
        }
    }
}
