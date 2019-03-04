public interface IAudio
{
    float VolumeLevel { get; set; }
    void Volume(float percent);
}
