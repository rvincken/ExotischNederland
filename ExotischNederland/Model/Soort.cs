namespace ExotischNederland.Model;

internal class Soort
{
    private int _id;
    private string _wetenschappelijkeNaam;
    private string _soortNaam;
    private string _type;
    private string _categorie;
    private char _oorsprong;

    public int Id
    {
        get { return _id; }
        private set { _id = value; }
    }

    public string WetenschappelijkeNaam
    {
        get { return _wetenschappelijkeNaam; }
        private set
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
        private set
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

    public string Type
    {
        get { return _type; }
        private set
        {
            if (value == "plant" || value == "dier")
            {
                _type = value;
            }
            else
            {
                throw new ArgumentException("Type moet plant of dier zijn");
            }
        }
    }

    public string Categorie
    {
        get { return _categorie; }
        private set
        {
            if (value.Length <= 50 && value.Length != 0)
            {
                _categorie = value;
            }
            else
            {
                throw new ArgumentException("Categorie mag niet langer zijn dan 50 karakters");
            }
        }
    }

    public char Oorsprong
    {
        get { return _oorsprong; }
        private set
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

    public Soort(int id, string wetenschappelijkeNaam, string soortNaam, string type, string categorie, char oorsprong)
    {
        Id = id;
        WetenschappelijkeNaam = wetenschappelijkeNaam;
        SoortNaam = soortNaam;
        Type = type;
        Categorie = categorie;
        Oorsprong = oorsprong;
    }

    public override string ToString()
    {
        return $"""
                ID: {Id}
                Wetenschappelijke naam: {WetenschappelijkeNaam}
                Soort naam : {SoortNaam}
                Type: {Type}
                Categorie: {Categorie}
                Oorsprong: {(Oorsprong == 'e' ? "exoot" : "inheems")}
                """;
    }
}
