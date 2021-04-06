using Lean.Touch;
using UnityEngine;

namespace Assets.Scripts.AR
{
    public class RotateManager : MonoBehaviour
    {

        private static RotateManager instance;
        private GameObject doorObject;

        public static RotateManager GetInstance()
        {
            return instance;
        }


        private void Awake()
        {
            instance = this;
        }


        public void SetDoor(GameObject door)
        {
            doorObject = door;
        }

        public void SetXAxis()
        {
            var rotate = doorObject.GetComponent<LeanTwistRotateAxis>();
            rotate.Axis.x = -1;
            rotate.Axis.y = 0;
            rotate.Axis.z = 0;
        }

        public void SetYAxis()
        {
            var rotate = doorObject.GetComponent<LeanTwistRotateAxis>();
            rotate.Axis.x = 0;
            rotate.Axis.y = -1;
            rotate.Axis.z = 0;

        }

        public void SetZAxis()
        {
            var rotate = doorObject.GetComponent<LeanTwistRotateAxis>();
            rotate.Axis.x = 0;
            rotate.Axis.y = 0;
            rotate.Axis.z = -1;

        }
    }
}
