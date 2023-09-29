namespace DnDProbabilityCalculator;

public class Character
{
    public int Dexterity { get; set; }
    public int Strength { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }

    public IEnumerable<Character> FromFile(string filePath)
    {
        throw new NotImplementedException();
    }
}