using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrbitalComntroller : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public float radius = 5f; // Радиус вращения
    public float rotationSpeed = 50f; // Скорость вращения объекта (градусы в секунду)

    private Vector3 currentOffset; // Смещение объекта от игрока
    private Vector3 lastPlayerPosition; // Позиция игрока в предыдущем кадре

    void Start()
    {
        // Установим начальное смещение объекта относительно игрока
        currentOffset = new Vector3(radius, 1, 0); // Начальная позиция на орбите справа от игрока
        lastPlayerPosition = player.position; // Запоминаем начальную позицию игрока
    }

    void Update()
    {
        if (player == null)
            return;

        // Вычисляем изменение позиции игрока (его движение)
        Vector3 playerDelta = player.position - lastPlayerPosition;

        // Обновляем смещение объекта относительно игрока
        currentOffset = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0) * currentOffset;

        // Устанавливаем новую позицию объекта с учетом смещения и движения игрока
        transform.position = player.position + currentOffset + playerDelta;

        // Сохраняем текущую позицию игрока для следующего кадра
        lastPlayerPosition = player.position;

    }
}
