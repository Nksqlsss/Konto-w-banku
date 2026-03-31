namespace BankTests;

[TestClass]
public class KontoTests
{
    // --- Konstruktor ---

    [TestMethod]
    public void Konstruktor_PoprawnyKlient_TworzyKonto()
    {
        var konto = new Bank.Konto("Jan Kowalski", 100);
        Assert.AreEqual("Jan Kowalski", konto.Nazwa);
        Assert.AreEqual(100, konto.Bilans);
    }

    [TestMethod]
    public void Konstruktor_BezBilansu_BilansZero()
    {
        var konto = new Bank.Konto("Jan Kowalski");
        Assert.AreEqual(0, konto.Bilans);
    }

    [TestMethod]
    public void Konstruktor_PustyKlient_RzucaWyjatek()
    {
        Assert.ThrowsException<ArgumentException>(() => new Bank.Konto(""));
    }

    [TestMethod]
    public void Konstruktor_UjemnyBilans_RzucaWyjatek()
    {
        Assert.ThrowsException<ArgumentException>(() => new Bank.Konto("Jan", -100));
    }

    // --- Wplata ---

    [TestMethod]
    public void Wplata_PoprawnaKwota_ZwiekszyBilans()
    {
        var konto = new Bank.Konto("Jan", 100);
        konto.Wplata(50);
        Assert.AreEqual(150, konto.Bilans);
    }

    [TestMethod]
    public void Wplata_UjemnaKwota_RzucaWyjatek()
    {
        var konto = new Bank.Konto("Jan", 100);
        Assert.ThrowsException<ArgumentException>(() => konto.Wplata(-50));
    }

    [TestMethod]
    public void Wplata_KontoZablokowane_RzucaWyjatek()
    {
        var konto = new Bank.Konto("Jan", 100);
        konto.BlokujKonto();
        Assert.ThrowsException<InvalidOperationException>(() => konto.Wplata(50));
    }

    // --- Wyplata ---

    [TestMethod]
    public void Wyplata_PoprawnaKwota_ZmniejszyBilans()
    {
        var konto = new Bank.Konto("Jan", 100);
        konto.Wyplata(40);
        Assert.AreEqual(60, konto.Bilans);
    }

    [TestMethod]
    public void Wyplata_ZaduzoSrodkow_RzucaWyjatek()
    {
        var konto = new Bank.Konto("Jan", 100);
        Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(200));
    }

    [TestMethod]
    public void Wyplata_UjemnaKwota_RzucaWyjatek()
    {
        var konto = new Bank.Konto("Jan", 100);
        Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(-10));
    }

    [TestMethod]
    public void Wyplata_KontoZablokowane_RzucaWyjatek()
    {
        var konto = new Bank.Konto("Jan", 100);
        konto.BlokujKonto();
        Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(10));
    }

    // --- Blokada ---

    [TestMethod]
    public void BlokujKonto_KontoZostajZablokowane()
    {
        var konto = new Bank.Konto("Jan", 100);
        konto.BlokujKonto();
        Assert.IsTrue(konto.Zablokowane);
    }

    [TestMethod]
    public void OdblokujKonto_KontoZostajOdblokowane()
    {
        var konto = new Bank.Konto("Jan", 100);
        konto.BlokujKonto();
        konto.OdblokujKonto();
        Assert.IsFalse(konto.Zablokowane);
    }
}