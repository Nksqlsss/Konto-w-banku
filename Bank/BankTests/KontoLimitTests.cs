namespace BankTests;

[TestClass]
public class KontoLimitTests
{
    // --- Konstruktor ---

    [TestMethod]
    public void Konstruktor_PoprawnyKlient_TworzyKontoLimit()
    {
        var konto = new Bank.KontoLimit("Jan", 100, 50);
        Assert.AreEqual("Jan", konto.Nazwa);
        Assert.AreEqual(150, konto.Bilans); // 100 + limit 50
    }

    [TestMethod]
    public void Konstruktor_UjemnyLimit_RzucaWyjatek()
    {
        Assert.ThrowsException<ArgumentException>(() => new Bank.KontoLimit("Jan", 100, -50));
    }

    // --- ZmienLimit ---

    [TestMethod]
    public void ZmienLimit_PoprawnaWartosc_ZmieniaLimit()
    {
        var konto = new Bank.KontoLimit("Jan", 100, 50);
        konto.ZmienLimit(200);
        Assert.AreEqual(300, konto.Bilans); // 100 + limit 200
    }

    [TestMethod]
    public void ZmienLimit_UjemnaWartosc_RzucaWyjatek()
    {
        var konto = new Bank.KontoLimit("Jan", 100, 50);
        Assert.ThrowsException<ArgumentException>(() => konto.ZmienLimit(-10));
    }

    // --- Wyplata z debetom ---

    [TestMethod]
    public void Wyplata_WRamachLimitu_BlokajeKonto()
    {
        var konto = new Bank.KontoLimit("Jan", 100, 50);
        konto.Wyplata(120);
        Assert.IsTrue(konto.Zablokowane);
    }

    [TestMethod]
    public void Wyplata_PowylejLimitu_RzucaWyjatek()
    {
        var konto = new Bank.KontoLimit("Jan", 100, 50);
        Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(200));
    }

    [TestMethod]
    public void Wyplata_NormalnaKwota_NieBlokujeKonta()
    {
        var konto = new Bank.KontoLimit("Jan", 100, 50);
        konto.Wyplata(80);
        Assert.IsFalse(konto.Zablokowane);
    }

    // --- Wplata po debecie ---

    [TestMethod]
    public void Wplata_PoDebecie_OdblokujeKonto()
    {
        var konto = new Bank.KontoLimit("Jan", 100, 50);
        konto.Wyplata(120); // debet, konto zablokowane
        konto.Wplata(50);   // bilans > 0, konto odblokowane
        Assert.IsFalse(konto.Zablokowane);
    }

    [TestMethod]
    public void Wplata_PoDebecieNiewystarczajaca_KontoDalejZablokowane()
    {
        var konto = new Bank.KontoLimit("Jan", 100, 50);
        konto.Wyplata(120); // bilans = -20, konto zablokowane
        konto.Wplata(10);   // bilans = -10, nadal <= 0
        Assert.IsTrue(konto.Zablokowane);
    }
}