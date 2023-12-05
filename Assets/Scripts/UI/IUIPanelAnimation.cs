using DG.Tweening;


public interface IUIPanelAnimation
{
    public float Duration { get; set; }
    public Ease ShowEase { get; set; }
    public Ease HideEase { get; set; }

    void DoShowAnimation();
    void DoHideAnimation();
}