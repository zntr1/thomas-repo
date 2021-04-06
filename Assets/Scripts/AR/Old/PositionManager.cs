using UnityEngine;

namespace Assets.Scripts.AR
{
    public class PositionManager : MonoBehaviour
    {

        private GameObject doorObject;

        private static PositionManager instance;

        private void Awake()
        {
            instance = this;
        }

        public static PositionManager GetInstance()
        {
            return instance;
        }

        public void SetDoor(GameObject door)
        {
            doorObject = door;
            var x = doorObject.transform.position.x;
            var y = doorObject.transform.position.y;
            var z = doorObject.transform.position.z;
            Debug.Log("Current X: " + x);
            Debug.Log("Current Y: " + y);
            Debug.Log("Current Z: " + z);
        }

        public void MoveRight()
        {
            //Debug.Log("Adding 100 to X!");

            var x = doorObject.transform.position.x;
            var y = doorObject.transform.position.y;
            var z = doorObject.transform.position.z;
            x = x + 0.1f;
            // Debug.Log("Current X: " + x);
            // Debug.Log("Current Y: " + y);
            // Debug.Log("Current Z: " + z);
            doorObject.transform.position = new Vector3(x,y ,z);

        }

        public void MoveLeft()
        {
            //Debug.Log("Adding 100 to X!");

            var x = doorObject.transform.position.x;
            var y = doorObject.transform.position.y;
            var z = doorObject.transform.position.z;
            x = x - 0.1f;
            // Debug.Log("Current X: " + x);
            // Debug.Log("Current Y: " + y);
            // Debug.Log("Current Z: " + z);
            doorObject.transform.position = new Vector3(x, y, z);

        }


        public void Moveup()
        {
            var x = doorObject.transform.position.x;
            var y = doorObject.transform.position.y;
            var z = doorObject.transform.position.z;
            y = y + 0.1f;
            doorObject.transform.position = new Vector3(x, y, z);
        }

        public void MoveDown()
        {
            var x = doorObject.transform.position.x;
            var y = doorObject.transform.position.y;
            var z = doorObject.transform.position.z;
            y = y - 0.1f;
            doorObject.transform.position = new Vector3(x, y, z);
        }



        public void MoveForward()
        {
            var x = doorObject.transform.position.x;
            var y = doorObject.transform.position.y;
            var z = doorObject.transform.position.z;
            z = z + 0.1f;
            doorObject.transform.position = new Vector3(x, y, z);
        }

        public void MoveBackward()
        {
            var x = doorObject.transform.position.x;
            var y = doorObject.transform.position.y;
            var z = doorObject.transform.position.z;
            z = z - 0.1f;
            doorObject.transform.position = new Vector3(x, y, z);
        }


    }
}
