using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrbitalComntroller : MonoBehaviour
{
    public Transform player; // ������ �� ������ ������
    public float radius = 5f; // ������ ��������
    public float rotationSpeed = 50f; // �������� �������� ������� (������� � �������)

    private Vector3 currentOffset; // �������� ������� �� ������
    private Vector3 lastPlayerPosition; // ������� ������ � ���������� �����

    void Start()
    {
        // ��������� ��������� �������� ������� ������������ ������
        currentOffset = new Vector3(radius, 1, 0); // ��������� ������� �� ������ ������ �� ������
        lastPlayerPosition = player.position; // ���������� ��������� ������� ������
    }

    void Update()
    {
        if (player == null)
            return;

        // ��������� ��������� ������� ������ (��� ��������)
        Vector3 playerDelta = player.position - lastPlayerPosition;

        // ��������� �������� ������� ������������ ������
        currentOffset = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0) * currentOffset;

        // ������������� ����� ������� ������� � ������ �������� � �������� ������
        transform.position = player.position + currentOffset + playerDelta;

        // ��������� ������� ������� ������ ��� ���������� �����
        lastPlayerPosition = player.position;

    }
}
