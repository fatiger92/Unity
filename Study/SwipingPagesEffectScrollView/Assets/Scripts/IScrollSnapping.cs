
public interface IScrollSnapping
{
    void ChangePage(int page);
    void SetLerp(bool value);
    int CurrentPage();
    void StartScreenChange();
}
