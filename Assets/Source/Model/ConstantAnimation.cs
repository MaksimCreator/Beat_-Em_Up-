using UnityEngine;

namespace Model
{
    public static class ConstantAnimation
    {
        public static int Idel => Animator.StringToHash(nameof(Idel));
        public static int Kick => Animator.StringToHash(nameof(Kick));
        public static int Jump => Animator.StringToHash(nameof(Jump));
        public static int Attack => Animator.StringToHash(nameof(Attack));
        public static int Crouth => Animator.StringToHash(nameof(Crouth));
        public static int Death => Animator.StringToHash(nameof(Death));
        public static int MoveBakc => Animator.StringToHash(nameof(MoveBakc));
        public static int MoveForward => Animator.StringToHash(nameof(MoveForward));
        public static int MoveRight => Animator.StringToHash(nameof(MoveRight));
        public static int MoveLeft => Animator.StringToHash(nameof(MoveLeft));
        public static int Run => Animator.StringToHash(nameof(Run));
    }
}