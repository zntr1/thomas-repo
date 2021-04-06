using Lean.Touch;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.AR
{
    public class RotateScript : MonoBehaviour
    {

        private static RotateScript instance;

        private GameObject _gameObject;
        // Start is called before the first frame update

        public static RotateScript GetInstance()
        {
            return instance;
        }

        private RotateScript()
        {
        }

        void Awake()
        {
            instance = this;
        }

        public void SetObject(GameObject obj)
        {
            this._gameObject = obj;
        }

        public void SetXAxis()
        {
            var rotate = _gameObject.GetComponent<LeanTwistRotateAxis>();
            rotate.Axis.x = -1;
            rotate.Axis.y = 0;
            rotate.Axis.z = 0;
        }
    }
}
