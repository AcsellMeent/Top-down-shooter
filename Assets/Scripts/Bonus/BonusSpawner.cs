using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BonusSpawner : MonoBehaviour, IPauseHandler
{
    [SerializeField]
    private List<WeaponBouns> _weaponBouns;

    [SerializeField]
    private float _weaponBounsInterval;

    private PlayerWeaponHandler _weaponHandler;

    [SerializeField]
    private Bonus[] _gainBonus;

    [SerializeField]
    private float _gainBounsInterval;

    private Camera _mainCamera;

    private void Awake()
    {
        if (_weaponBouns.Count == 0) return;

        _mainCamera = Camera.main;
        StartCoroutine(SpawnBonusWithInterval(_weaponBounsInterval, SpawnWeaponBonus));
        StartCoroutine(SpawnBonusWithInterval(_gainBounsInterval, SpawnGainBonus));
    }

    [Inject]
    public void Construct(PauseManager pauseManager, PlayerWeaponHandler weaponHandler)
    {
        pauseManager.Register(this);
        _weaponHandler = weaponHandler;
    }

    private IEnumerator SpawnBonusWithInterval(float interval, Action spawn)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            spawn.Invoke();
        }
    }

    private void SpawnWeaponBonus()
    {
        List<WeaponBouns> availableWeapons = _weaponBouns.FindAll(bonus => bonus.Weapon != _weaponHandler.WeaponPrefab);
        if (availableWeapons.Count == 0) throw new Exception("availableWeapons count is 0");

        WeaponBouns weaponBouns = availableWeapons[Random.Range(0, availableWeapons.Count)];

        GameObject gameObject = Instantiate(weaponBouns, GetRandomCameraViewPoint(), Quaternion.identity).gameObject;

        StartCoroutine(DestroyAfterDuration(10, gameObject));
    }

    private void SpawnGainBonus()
    {
        Bonus gainBonus = _gainBonus[Random.Range(0, _gainBonus.Length)];

        GameObject gameObject = Instantiate(gainBonus, GetRandomCameraViewPoint(), Quaternion.identity).gameObject;

        StartCoroutine(DestroyAfterDuration(10, gameObject));
    }

    private Vector2 GetRandomCameraViewPoint()
    {
        float viewportXDelta = _mainCamera.transform.position.x - _mainCamera.ViewportToWorldPoint(Vector3.zero).x - 2;
        float viewportYDelta = _mainCamera.transform.position.y - _mainCamera.ViewportToWorldPoint(Vector3.zero).y - 2;

        float randomX = Random.Range(_mainCamera.transform.position.x - viewportXDelta, _mainCamera.transform.position.x + viewportXDelta);
        float randomy = Random.Range(_mainCamera.transform.position.y - viewportYDelta, _mainCamera.transform.position.y + viewportYDelta);
        return new Vector2(randomX, randomy);
    }

    private IEnumerator DestroyAfterDuration(float duration, GameObject gameObject)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    public void SetPaused(bool isPaused)
    {
        StopAllCoroutines();
    }
}
