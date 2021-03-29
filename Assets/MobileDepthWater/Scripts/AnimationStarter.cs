namespace Assets.MobileOptimizedWater.Scripts
{
    using UnityEngine;

    public class AnimationStarter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Motion animat;

        public void Awake()
        {
            animator.Play(animat.name);
        }
    }
}
