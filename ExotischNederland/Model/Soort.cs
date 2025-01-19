namespace ExotischNederland.Model;

internal class Soort
{
    private int _id;
    private string _wetenschappelijkeNaam;
    private string _soortNaam;
    private int _hoeVaakDeSoortVoorkomt;
    private char _oorsprong;

    public int Id
    {
        get { return _id; }
        set
        {
            if (value.ToString().Length == 7)
            {
                _id = value;
            }
            else
            {
                throw new ArgumentException("soort id moet 7 getallen lang zijn");
            }
        }
    }

    public string WetenschappelijkeNaam
    {
        get { return _wetenschappelijkeNaam; }
        set
        {
            if (value.Length <= 50)
            {
                _wetenschappelijkeNaam = value;
             
            }
            else
            {
                throw new ArgumentException("wetenschappelijke naam is niet bekend");
            }
        }
    }

    public string SoortNaam
    {
        get { return _soortNaam; }
        set
        {
            if (value.Length <= 50)
            {
                _soortNaam = value;
            }
            else
            {
                throw new ArgumentException("soort naam is niet bekend");
            }
        }
    }

    public int HoeVaakDeSoortVoorkomt
    {
        get { return _hoeVaakDeSoortVoorkomt; }
        set
        {
            if (value < 0 || value > 1000000000)
            {
                _hoeVaakDeSoortVoorkomt = value;
            }
            else
            {
                throw new ArgumentException("hoe vaak de soort voorkomt is ongeldig");
            }
        }
    }

    public char Oorsprong
    {
        get { return _oorsprong; }
        set
        {
            value = char.ToLower(value);
            if (value == 'e' || value == 'i')
            {
                _oorsprong = value;
            }
            else
            {
                throw new ArgumentException("oorsprong moet exoot (e) of inheems (i) zijn");
            }


        }
    }

    public Soort(int id, string wetenschappelijkeNaam, string soortNaam, int hoeVaakDeSoortVoorkomt, char oorsprong)
    {
        Id = id;
        WetenschappelijkeNaam = wetenschappelijkeNaam;
        SoortNaam = soortNaam;
        HoeVaakDeSoortVoorkomt = hoeVaakDeSoortVoorkomt ;
        Oorsprong = oorsprong;
    }
}



