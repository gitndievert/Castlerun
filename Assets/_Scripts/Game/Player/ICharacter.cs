public interface ICharacter
{
    void SetHit(int damage);
}


public interface IPlayer : ICharacter
{
    void Init(string name, int playernum);
    void Swing();     
}