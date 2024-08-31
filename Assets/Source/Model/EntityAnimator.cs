using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Model
{
    public class EntityAnimator
    {
        private readonly Animator _entityAnimator;
        private readonly AnimationClip[] _clips;

        public EntityAnimator(Animator entityAnimator)
        {
            _entityAnimator = entityAnimator;
            _clips = _entityAnimator.runtimeAnimatorController.animationClips;
        }

        public void EnterBackMove()
        => TryEnterAnimation(() => _entityAnimator.CrossFade(ConstantAnimation.MoveBakc, 0.1f, Constant.AnimationLayerMovemeg));

        public void EnterRightMove()
        => TryEnterAnimation(() => _entityAnimator.CrossFade(ConstantAnimation.MoveRight, 0.1f, Constant.AnimationLayerMovemeg));

        public void EnterForwardMove()
        => TryEnterAnimation(() => _entityAnimator.CrossFade(ConstantAnimation.MoveForward, 0.1f, Constant.AnimationLayerMovemeg));

        public void EnterRun()
        => TryEnterAnimation(() => _entityAnimator.CrossFade(ConstantAnimation.Run, 0.1f, Constant.AnimationLayerMovemeg));

        public void EnterMoveLeft()
        => TryEnterAnimation(() => _entityAnimator.CrossFade(ConstantAnimation.MoveLeft, 0.1f, Constant.AnimationLayerMovemeg));

        public void EnterDeath()
        => TryEnterAnimation(() => _entityAnimator.CrossFade(ConstantAnimation.Death, 0.1f, Constant.AnimationLayerMovemeg));

        public void EnterIdelMovemeng()
        => TryEnterAnimation(() => _entityAnimator.CrossFade(ConstantAnimation.Idel, 0.1f, Constant.AnimationLayerMovemeg));

        public void EnterIdelAttack()
        => TryEnterAnimation(() => _entityAnimator.CrossFade(ConstantAnimation.Idel, 0.1f, Constant.AnimationLayerAttack));

        public void EnterKick()
        => TryEnterAnimation(() => _entityAnimator.CrossFade(ConstantAnimation.Kick, 0.1f, Constant.AnimationLayerMovemeg));

        public float GetLengchClip(int animationHash)
        {
            AnimatorController animatorController = _entityAnimator.runtimeAnimatorController as AnimatorController;

            if (animatorController != null)
            {
                foreach (var layer in animatorController.layers)
                {
                    foreach (var state in layer.stateMachine.states)
                    {
                        if (state.state.nameHash == animationHash)
                        {
                            AnimationClip clip = state.state.motion as AnimationClip;
                            return clip.length + 0.1f;
                        }
                    }
                }
            }

            throw new InvalidOperationException();
        }

        private void TryEnterAnimation(Action enterAnimation)
        {
            if (_entityAnimator != null)
                enterAnimation();
        }
    }
}