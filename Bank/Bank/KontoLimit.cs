namespace Bank;

public class KontoLimit
{
    private Konto konto;
    private decimal limit;
    private bool debet = false;

    public KontoLimit(string klient, decimal bilansNaStart = 0, decimal limit = 0)
    {
        if (limit < 0)
            throw new ArgumentException("Limit nie może być ujemny.");
        konto = new Konto(klient, bilansNaStart);
        this.limit = limit;
    }

    public string Nazwa => konto.Nazwa;
    public decimal Bilans => konto.Bilans + (debet ? 0 : limit);
    public bool Zablokowane => konto.Zablokowane;
    public decimal Limit => limit;

    public void ZmienLimit(decimal nowyLimit)
    {
        if (nowyLimit < 0)
            throw new ArgumentException("Limit nie może być ujemny.");
        limit = nowyLimit;
    }

    public void Wplata(decimal kwota)
    {
        if (kwota <= 0)
            throw new ArgumentException("Kwota wpłaty musi być dodatnia.");

        konto.OdblokujKonto(); // tymczasowo odblokuj żeby wpłacić
        konto.Wplata(kwota);
        
        if (debet && konto.Bilans > 0)
        {
            debet = false;
        }
        else if (debet)
        {
            konto.BlokujKonto(); // nadal w debecie, zablokuj z powrotem
        }
    }

    public void Wyplata(decimal kwota)
{
    if (konto.Zablokowane)
        throw new InvalidOperationException("Konto jest zablokowane.");
    if (kwota <= 0)
        throw new ArgumentException("Kwota wypłaty musi być dodatnia.");

    if (kwota > konto.Bilans && kwota <= konto.Bilans + limit && !debet)
    {
        konto.DodajDoKonta(-kwota);
        debet = true;
        konto.BlokujKonto();
    }
    else if (kwota <= konto.Bilans)
    {
        konto.Wyplata(kwota);
    }
    else
    {
        throw new InvalidOperationException("Brak wystarczających środków.");
    }
}

    public void BlokujKonto() => konto.BlokujKonto();
    public void OdblokujKonto() => konto.OdblokujKonto();
}