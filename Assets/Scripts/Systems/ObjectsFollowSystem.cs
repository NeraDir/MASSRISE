using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectsFollowSystem : MonoBehaviour
{
    public static UnityEvent onUpdateFollows = new UnityEvent();

    private GameSettings _gameSettings;

    public void Init(GameSettings settings)
    {
        _gameSettings = settings;
        StartCoroutine(UpdateFollows());
    }

    private IEnumerator UpdateFollows()
    {
        while (true)
        {
            onUpdateFollows?.Invoke();
            yield return new WaitForSeconds(_gameSettings.gameSetting.followUpdateRate);
        }
    }
}
