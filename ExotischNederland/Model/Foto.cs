namespace ExotischNederland.Model;

internal class Foto
{
    private int _id;
    private byte[] _afbeelding;

    public int Id
    {
        get { return _id; }
        private set 
        {
            if (value.ToString().Length == 7)
            {
                _id = value;
            }
            else
            {
                throw new ArgumentException(
                    $"{this}.Id moet 7 getallen lang zijn, " +
                    $"maar een getal van {value} is gegeven.");
            }
        }
    }

    public byte[] Afbeelding
    {
        get { return _afbeelding; }
        // Setter is aparte methode
    }

    public Foto(int id, string afbeeldingPad)
    {
        Id = id;
        _afbeelding = LaadAfbeeldingVanPad(afbeeldingPad);
    }

    public Foto(int id, byte[] afbeeldingBytes)
    {
        Id = id;
        _afbeelding = afbeeldingBytes;
    }

    private void WijzigAfbeelding(string pad)
    {
        _afbeelding = LaadAfbeeldingVanPad(pad);
    }

    private byte[] LaadAfbeeldingVanPad(string pad)
    {
        if (!File.Exists(pad))
        {
            throw new FileNotFoundException($"Het bestand '{pad}' bestaat niet.");
        }

        if (Path.GetExtension(pad).ToLower() != ".png")
        {
            throw new ArgumentException("Alleen PNG bestanden zijn toegestaan.");
        }

        return File.ReadAllBytes(pad);
    }

    public override string ToString()
    {
        return $"""
                ID: {Id}
                Foto gekoppeld: {(Afbeelding != null ? "Ja" : "Nee")}
                """;
    }
}