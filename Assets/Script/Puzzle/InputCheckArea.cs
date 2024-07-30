using UnityEngine;
using System.Collections.Generic;

public class InputCheckArea : MonoBehaviour {
    [Tooltip("��Inspector�����õ�Ԥ�����������")]
    public List<int> predefinedSequence;

    private List<int> playerInputSequence = new List<int>();
    private bool playerInArea = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerInArea = false;
        }
    }

    void Update() {
        if (playerInArea) {
            if (Input.GetKeyDown(KeyCode.Alpha0)) {
                AddInput(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                AddInput(1);
            }
        }
    }

    void AddInput(int input) {
        playerInputSequence.Add(input);

        if (playerInputSequence.Count > predefinedSequence.Count) {
            playerInputSequence.RemoveAt(0); // �������г��Ȳ�����Ԥ������ϳ���
        }

        if (IsSequenceMatch()) {
            Debug.Log("������ȷ��");
            Destroy(gameObject);
            // ������������������ȷʱ���߼�
        }
    }

    bool IsSequenceMatch() {
        if (playerInputSequence.Count != predefinedSequence.Count) {
            return false;
        }

        for (int i = 0; i < predefinedSequence.Count; i++) {
            if (playerInputSequence[i] != predefinedSequence[i]) {
                return false;
            }
        }
        return true;
    }
}
