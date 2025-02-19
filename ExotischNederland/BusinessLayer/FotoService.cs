﻿namespace ExotischNederland.BusinessLayer;

internal class FotoService
{
    private readonly DataLayer.FotoRepository _repository;

    public FotoService()
    {
        _repository = new DataLayer.FotoRepository();
    }

    public void RegistreerFoto(string afbeeldingPad)
    {
        var foto = new Model.Foto(1, afbeeldingPad);

        _repository.VoegFotoToe(foto);
    }

    public void RegistreerFoto(byte[] afbeelding)
    {
        var foto = new Model.Foto(1, afbeelding);

        _repository.VoegFotoToe(foto);
    }

    public List<Model.Foto> KrijgAlleFotos()
    {
        return _repository.HaalAlleFotosOp();
    }

    public Model.Foto KrijgFotoVanId(int id)
    {
        return _repository.HaalFotoVanIdOp(id);
    }

    public Model.Foto KrijgFotoVanByteArray(byte[] afbeelding)
    {
        return _repository.HaalFotoVanByteArrayOp(afbeelding);
    }
}
