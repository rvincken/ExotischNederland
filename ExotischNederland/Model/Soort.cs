namespace ExotischNederland.Model;
//hallo
// implementeer:
// wetenschappelijke naam, soort naam, hoe vaak de soort voorkomt

internal class Soort
{
    private string _wetenschappelijkeNaam;
    private string _soortNaam;
    private int _hoeVaakDeSoortVoorkomt;
    private char _oorsprong;

    public string WetenschappelijkeNaam
    {
        get { return _wetenschappelijkeNaam; }
        set
        {
            if (value.Length <=50)
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
            if (value <= 50)
            {
                _hoeVaakDeSoortVoorkomt = value;
            }
            else
            {
                throw new ArgumentException("de soort komt niet vaak voor");
            }
        }
    }

    public char Oorsprong
    {
        get { return _oorsprong; }
        set
        {
            if (value <= 50)
            {
                _oorsprong = value;
            }
            else
            {
                throw new ArgumentException("oorsprong is niet bekend");
            }


        }
    }
    public Soort(string wetenschappelijkeNaam, string soortNaam, int hoeVaakDeSoortVoorkomt, char oorsprong )
    {
        WetenschappelijkeNaam = wetenschappelijkeNaam;
        SoortNaam = soortNaam;
        HoeVaakDeSoortVoorkomt = hoeVaakDeSoortVoorkomt ;
        Oorsprong = oorsprong;
        
    }
}



