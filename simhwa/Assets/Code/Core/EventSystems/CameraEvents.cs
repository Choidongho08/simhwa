using Unity.Cinemachine;
using UnityEngine;

namespace Code.Players.States
{
    public enum PanDirection
    {
        Up, Down, Left, Right
    }
    
    public class CameraEvents
    {
        public static PanEvent PanEvent = new PanEvent();
        public static SwapCameraEvent SwapCameraEvent = new SwapCameraEvent();
    }

    public class PanEvent : GameEvent
    {
        public float panDistance;
        public float panTime;
        public PanDirection panDirection;
        public bool isRewindToStart;

        public void SetData(float time, float distance, PanDirection direction, bool isRewind)
        {
            panDistance = distance;
            panTime = time;
            panDirection = direction;
            isRewindToStart = isRewind;
        }
    }

    public class SwapCameraEvent : GameEvent
    {
        public CinemachineCamera leftCamera;
        public CinemachineCamera rightCamera;
        public Vector2 moveDirection;
    }
}