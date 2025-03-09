using System;
using System.Collections.Generic;
using System.Linq;
using Code.Players.States;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace Code.Core.Managers
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineCamera currentCamera;
        [SerializeField] private int activeCameraPriority = 15;
        [SerializeField] private int disableCameraPriority = 10;
        [SerializeField] private GameEventChannelSO cameraChannel;
        
        private Vector2 _originalTrackPosition;
        private CinemachinePositionComposer _positionComposer;

        private Dictionary<PanDirection, Vector2> _panDirections;
        private Tween _panningTween;

        private void Awake()
        {
            _panDirections = new Dictionary<PanDirection, Vector2>
            {
                { PanDirection.Up, Vector2.up },
                { PanDirection.Down, Vector2.down },
                { PanDirection.Left, Vector2.left },
                { PanDirection.Right, Vector2.right }    
            };

            cameraChannel.AddListener<PanEvent>(HandleCameraPanning);
            cameraChannel.AddListener<SwapCameraEvent>(HandleSwapCamera);
            currentCamera = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None)
                .FirstOrDefault(cam => cam.Priority == activeCameraPriority);
            
            Debug.Assert(currentCamera != null, $"Check camera priority, there is no active camera");
            ChangeCamera(currentCamera);
        }

        private void OnDestroy()
        {
            cameraChannel.RemoveListener<PanEvent>(HandleCameraPanning);
            cameraChannel.RemoveListener<SwapCameraEvent>(HandleSwapCamera);
            KillTweenIfActive();
        }

        public void ChangeCamera(CinemachineCamera newCamera)
        {
            currentCamera.Priority = disableCameraPriority; // 현재 카메라 꺼주고
            Transform followTarget = currentCamera.Follow;
            currentCamera = newCamera;
            currentCamera.Priority = activeCameraPriority;
            currentCamera.Follow = followTarget;

            _positionComposer = currentCamera.GetComponent<CinemachinePositionComposer>();
            _originalTrackPosition = _positionComposer.TargetOffset;
        }

        private void HandleSwapCamera(SwapCameraEvent evt)
        {
            if(currentCamera == evt.leftCamera && evt.moveDirection.x > 0.001)
                ChangeCamera(evt.rightCamera);
            if(currentCamera == evt.rightCamera && evt.moveDirection.x < 0.001)
                ChangeCamera(evt.leftCamera);
        }

        private void HandleCameraPanning(PanEvent evt)
        {
            Vector3 endPostion = evt.isRewindToStart ? _originalTrackPosition :
                _panDirections[evt.panDirection] * evt.panDistance + _originalTrackPosition ;
            //  원위치로 리와인드 시켜주는 이벤트면 원위치로 돌리고, 아니면 방향대로 움직여줌

            KillTweenIfActive();

            _panningTween = DOTween.To(
                () => _positionComposer.TargetOffset,
                value => _positionComposer.TargetOffset = value,
                endPostion, evt.panTime);
        }

        private void KillTweenIfActive()
        {
            if (_panningTween != null && _panningTween.IsActive())
                _panningTween.Kill();
        }
    }
}