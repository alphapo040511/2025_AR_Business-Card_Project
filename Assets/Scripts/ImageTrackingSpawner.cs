using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ImageTrackingSpawner : MonoBehaviour
{
    public Text logText;
    [SerializeField] ARTrackedImageManager trackedImageManager;
    public NameCardVIew cardPrefab;
    public List<UserDataSO> userList = new List<UserDataSO>();
    private GameObject target;

    // 이미지 ID → 생성된 오브젝트 매핑
    Dictionary<string, UserDataSO> userDatas = new();
    Dictionary<string, NameCardVIew> spawnedObjects = new();

    private void Start()
    {
        foreach(var user in userList)
        {
            userDatas.Add(user.imageId, user);
        }
    }

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
        logText.text = "명함 인식됨";

        // 1️ 새로 인식된 이미지
        foreach (var trackedImage in args.added)
        {
            string imageId = trackedImage.referenceImage.name;

            if (spawnedObjects.ContainsKey(imageId))
                continue;

            if(!userDatas.ContainsKey(imageId)) continue;

            NameCardVIew obj = Instantiate(cardPrefab, trackedImage.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one * 0.022f;

            obj.SetUserData(userDatas[imageId]);

            spawnedObjects.Add(imageId, obj);

            logText.text = $"새로운 명함 생성 {imageId}";
        }

        // 2️ 추적 중인 이미지 업데이트
        foreach (var trackedImage in args.updated)
        {
            string imageId = trackedImage.referenceImage.name;

            if (!spawnedObjects.TryGetValue(imageId, out var obj))
                continue;

            // 인식 상태에 따라 표시 제어
            obj.gameObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }

        // 3️ 이미지가 사라졌을 때
        foreach (var trackedImage in args.removed)
        {
            logText.text = "제거됨";

            string imageId = trackedImage.referenceImage.name;

            if (spawnedObjects.TryGetValue(imageId, out var obj))
            {
                Destroy(obj.gameObject);
                spawnedObjects.Remove(imageId);
            }
        }
    }
}