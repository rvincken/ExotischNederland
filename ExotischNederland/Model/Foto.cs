using System.Security.Cryptography;

namespace ExotischNederland.Model;

internal class Foto
{
    private int _id;
    private byte[] _afbeelding;

    public int Id
    {
        get { return _id; }
    }

    public byte[] Afbeelding
    {
        get { return _afbeelding; }
    }

    public Foto(int id, string afbeeldingPad)
    {
        _id = id;
        _afbeelding = LaadAfbeeldingVanPad(afbeeldingPad);
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
}