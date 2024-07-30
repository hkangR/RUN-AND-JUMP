using UnityEngine;
using System.Collections.Generic;

public class InputCheckArea : MonoBehaviour {
    [Tooltip("在Inspector中设置的预定义数字组合")]
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
            playerInputSequence.RemoveAt(0); // 保持序列长度不超过预定义组合长度
        }

        if (IsSequenceMatch()) {
            Debug.Log("输入正确！");
            Destroy(gameObject);
            // 在这里添加玩家输入正确时的逻辑
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
