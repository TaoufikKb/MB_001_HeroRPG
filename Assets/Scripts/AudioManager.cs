using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _swordMetalWhooshClips;
    [SerializeField] AudioClip[] _swordWoodWhooshClips;
    [SerializeField] AudioClip[] _hitClips;
    [SerializeField] AudioClip[] _footstepsClips;

    [SerializeField, Range(0f, 1f)] float _randomPitchPercent;

    void Awake()
    {
        instance = this;
    }

    public void PlaySwordWhoosh(WeaponMaterialType weaponMaterialType)
    {
        _audioSource.pitch *= 1 + Random.Range(-_randomPitchPercent, _randomPitchPercent);

        switch (weaponMaterialType)
        {
            case WeaponMaterialType.Metal:

                _audioSource.PlayOneShot(_swordMetalWhooshClips[Random.Range(0, _swordMetalWhooshClips.Length)]);

                break;

            case WeaponMaterialType.Wood:

                _audioSource.PlayOneShot(_swordWoodWhooshClips[Random.Range(0, _swordWoodWhooshClips.Length)]);

                break;
            default:
                break;
        }
        
    }
    public void PlayHit()
    {
        _audioSource.pitch *= 1 + Random.Range(-_randomPitchPercent, _randomPitchPercent);
        _audioSource.PlayOneShot(_hitClips[Random.Range(0, _hitClips.Length)]);
    }
    public void PlayFootsteps()
    {
        _audioSource.pitch *= 1 + Random.Range(-_randomPitchPercent, _randomPitchPercent);
        _audioSource.PlayOneShot(_footstepsClips[Random.Range(0, _footstepsClips.Length)]);
    }
}
