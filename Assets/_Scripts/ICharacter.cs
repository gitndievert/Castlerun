public interface IPlayer
{
    void Init();
    void Swing();
    void SwingStop();
    void SetHit(int damage);    
}

public interface ICharacter : IPlayer
{
    
}
