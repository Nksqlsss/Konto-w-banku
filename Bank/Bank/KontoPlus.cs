namespace Bank;

public class KontoPlus : Konto
{
    private decimal limit;
    private bool debet = false;

    public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limit = 0) 
        : base(klient, bilansNaStart)
    {
        if (limit < 0)
            throw new ArgumentException("Limit nie może być ujemny.");
        this.limit = limit;
    }

    public decimal Limit => limit;

    public void ZmienLimit(decimal nowyLimit)
    {
        if (nowyLimit < 0)
            throw new ArgumentException("Limit nie może być ujemny.");
        limit = nowyLimit;
    }

    public override decimal Bilans => base.Bilans + (debet ? 0 : limit);

    public override void Wplata(decimal kwota)
{
    if (kwota <= 0)
        throw new ArgumentException("Kwota wpłaty musi być dodatnia.");
    
    bilans += kwota;
    
    if (debet && bilans > 0)
    {
        debet = false;
        OdblokujKonto();
    }
}

    public override void Wyplata(decimal kwota)
    {
        if (Zablokowane)
            throw new InvalidOperationException("Konto jest zablokowane.");
        if (kwota <= 0)
            throw new ArgumentException("Kwota wypłaty musi być dodatnia.");

        if (kwota > bilans && kwota <= bilans + limit && !debet)
        {
            bilans -= kwota;
            debet = true;
            BlokujKonto();
        }
        else if (kwota <= bilans)
        {
            bilans -= kwota;
        }
        else
        {
            throw new InvalidOperationException("Brak wystarczających środków.");
        }
    }
}