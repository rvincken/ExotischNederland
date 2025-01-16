namespace ExotischNederland.Model;

// implementeer:
// id, locatienaam, provincie, breedtegraad, lengtegraad

internal class Locatie
{
    private int _id;
    private string _locatienaam;
    private string _provincie;
    private double _breedtegraad;
    private double _lengtegraad;

    public int Id
    {
        get { return _id; }
    }

    public string Locatienaam
    {
        get { return _locatienaam; }
    }

    public string Provincie
    {
        get { return _provincie; }
    }

    public double Breedtegraad
    {
        get { return _breedtegraad; }
    }

    public double Lengtegraad
    {
        get { return _lengtegraad; }
    }

    public Locatie(int id, string locatienaam, string provincie, double breedtegraad, double lengtegraad)
    {
        _id = id;
        _locatienaam = locatienaam;
        _provincie = provincie;
        _breedtegraad = breedtegraad;
        _lengtegraad = lengtegraad;
    }
}
