namespace Bank;

public class Konto
{
    private string klient;
    protected decimal bilans;
    private bool zablokowane = false;

    private Konto() { }

    public Konto(string klient, decimal bilansNaStart = 0)
    {
        if (string.IsNullOrWhiteSpace(klient))
            throw new ArgumentException("Nazwa klienta nie może być pusta.");
        if (bilansNaStart < 0)
            throw new ArgumentException("Bilans początkowy nie może być ujemny.");

        this.klient = klient;
        this.bilans = bilansNaStart;
    }

    internal void DodajDoKonta(decimal kwota) => bilans += kwota;

    public string Nazwa => klient;
    public virtual decimal Bilans => bilans;
    public bool Zablokowane => zablokowane;

    public virtual void Wplata(decimal kwota)
    {
        if (zablokowane)
            throw new InvalidOperationException("Konto jest zablokowane.");
        if (kwota <= 0)
            throw new ArgumentException("Kwota wpłaty musi być dodatnia.");

        bilans += kwota;
    }

    public virtual void Wyplata(decimal kwota)
    {
        if (zablokowane)
            throw new InvalidOperationException("Konto jest zablokowane.");
        if (kwota <= 0)
            throw new ArgumentException("Kwota wypłaty musi być dodatnia.");
        if (kwota > bilans)
            throw new InvalidOperationException("Brak wystarczających środków.");

        bilans -= kwota;
    }

    public void BlokujKonto() => zablokowane = true;
    public void OdblokujKonto() => zablokowane = false;
}
