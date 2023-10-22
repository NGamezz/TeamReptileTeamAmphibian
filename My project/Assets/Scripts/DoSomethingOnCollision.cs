using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
    public class DoSomethingOnCollision : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCollision;
        [SerializeField] private int layerMask;
        [SerializeField] private bool collideWithEverything;

        protected void OnTriggerEnter(Collider other)
        {
            if (!collideWithEverything && other.gameObject.layer == layerMask)
            {
                onCollision.Invoke();
            }
            else if (collideWithEverything)
            {
                onCollision.Invoke();
            }
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (!collideWithEverything && collision.transform.gameObject.layer == layerMask)
            {
                onCollision.Invoke();
            }
            else if (collideWithEverything)
            {
                onCollision.Invoke();
            }
        }
    }
}