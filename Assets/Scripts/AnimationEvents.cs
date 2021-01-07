using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public static bool isPlaying = false;
    public UnityEvent onAnimalStop;
    public UnityEvent onAnimalHalf;

    #region --变量定义
    private Animator animator;
    private AnimationClip[] clips;
    #endregion

    #region --系统函数
    private void Start()
    {
        animator = this.GetComponent<Animator>();
        clips = animator.runtimeAnimatorController.animationClips;

        AnimationClip _clip = clips[0];
        AddAnimationEvent(animator, _clip.name, "StartEvent", 0);
        AddAnimationEvent(animator, _clip.name, "HalfEvent", _clip.length * 0.5f);
        AddAnimationEvent(animator, _clip.name, "EndEvent", _clip.length);
    }
    private void OnDestroy()
    {
        CleanAllEvent();
    }
    #endregion

    #region --自定义函数
    private void StartEvent()
    {
        Debug.Log("开始播放动画");
        isPlaying = true;
    }
    private void HalfEvent()
    {
        Debug.Log("动画播放了一半");
        onAnimalHalf.Invoke();
    }
    private void EndEvent()
    {
        Debug.Log("播放动画完毕");
        isPlaying = false;
        onAnimalStop.Invoke();
    }

    /// <summary>
    /// 添加动画事件
    /// </summary>
    /// <param name="_animator"></param>
    /// <param name="_clipName">动画名称</param>
    /// <param name="_eventFunctionName">事件方法名称</param>
    /// <param name="_time">添加事件时间。单位：秒</param>
    private void AddAnimationEvent(Animator _animator, string _clipName, string _eventFunctionName, float _time)
    {
        AnimationClip[] _clips = _animator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < _clips.Length; i++)
        {
            if (_clips[i].name == _clipName)
            {
                AnimationEvent _event = new AnimationEvent();
                _event.functionName = _eventFunctionName;
                _event.time = _time;
                _clips[i].AddEvent(_event);
                break;
            }
        }
        _animator.Rebind();
    }
    /// <summary>
    /// 清除所有事件
    /// </summary>
    private void CleanAllEvent()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i].events = default(AnimationEvent[]);
        }
        Debug.Log("清除所有事件");
    }
    #endregion
}