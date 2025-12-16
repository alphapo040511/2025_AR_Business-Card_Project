using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ImageTrackingSpawner : MonoBehaviour
{
    public Text logText;
    [SerializeField] ARTrackedImageManager trackedImageManager;
    [SerializeField] GameObject cardPrefab;
    private GameObject target;

    // 이미지 ID → 생성된 오브젝트 매핑
    Dictionary<string, GameObject> spawnedObjects = new();

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        logText.text = "이벤트 연결됨";
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        logText.text = "인식됨";

        // 1️ 새로 인식된 이미지
        foreach (var trackedImage in args.added)
        {
            string imageId = trackedImage.referenceImage.name;

            if (spawnedObjects.ContainsKey(imageId))
                continue;

            GameObject obj = Instantiate(cardPrefab, trackedImage.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one * 0.02f;

            spawnedObjects.Add(imageId, obj);
        }

        // 2️ 추적 중인 이미지 업데이트
        foreach (var trackedImage in args.updated)
        {
            string imageId = trackedImage.referenceImage.name;

            if (!spawnedObjects.TryGetValue(imageId, out var obj))
                continue;

            // 인식 상태에 따라 표시 제어
            obj.SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }

        // 3️ 이미지가 사라졌을 때
        foreach (var trackedImage in args.removed)
        {
            logText.text = "제거됨";

            string imageId = trackedImage.referenceImage.name;

            if (spawnedObjects.TryGetValue(imageId, out var obj))
            {
                Destroy(obj);
                spawnedObjects.Remove(imageId);
            }
        }
    }
}