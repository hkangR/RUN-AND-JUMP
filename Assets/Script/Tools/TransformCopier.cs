using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCopier : MonoBehaviour
{
    public Transform sourceTransform;
    public Transform targetTransform;

    public void CopyTransform() {
        if (sourceTransform == null || targetTransform == null) {
            Debug.LogWarning("Source or target transform is not set.");
            return;
        }

        CopyTransformRecursive(sourceTransform, targetTransform);
    }

    private void CopyTransformRecursive(Transform source, Transform target) {
        target.localPosition = source.localPosition;
        target.localRotation = source.localRotation;
        target.localScale = source.localScale;

        for (int i = 0; i < source.childCount; i++) {
            Transform sourceChild = source.GetChild(i);
            Transform targetChild = target.childCount > i ? target.GetChild(i) : null;

            if (targetChild != null && sourceChild.name == targetChild.name) {
                CopyTransformRecursive(sourceChild, targetChild);
            }
            else {
                Debug.LogWarning($"Target does not have matching child for: {sourceChild.name}");
            }
        }
    }
}
