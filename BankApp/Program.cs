using Bank;

Console.WriteLine("=== Konto ===");
var konto = new Konto("Jan Kowalski", 500);
Console.WriteLine($"Klient: {konto.Nazwa}, Bilans: {konto.Bilans}");

konto.Wplata(200);
Console.WriteLine($"Po wpłacie 200: {konto.Bilans}");

konto.Wyplata(100);
Console.WriteLine($"Po wypłacie 100: {konto.Bilans}");

konto.BlokujKonto();
Console.WriteLine($"Konto zablokowane: {konto.Zablokowane}");

try { konto.Wplata(100); }
catch (InvalidOperationException e) { Console.WriteLine($"Błąd: {e.Message}"); }

konto.OdblokujKonto();
Console.WriteLine($"Konto odblokowane: {konto.Zablokowane}");

// --- KontoPlus ---
Console.WriteLine("\n=== KontoPlus ===");
var kontoPlus = new KontoPlus("Anna Nowak", 100, 50);
Console.WriteLine($"Klient: {kontoPlus.Nazwa}, Bilans: {kontoPlus.Bilans}");

kontoPlus.Wyplata(120); // debet
Console.WriteLine($"Po wypłacie 120 (debet): Bilans={kontoPlus.Bilans}, Zablokowane={kontoPlus.Zablokowane}");

kontoPlus.Wplata(50);
Console.WriteLine($"Po wpłacie 50: Bilans={kontoPlus.Bilans}, Zablokowane={kontoPlus.Zablokowane}");

// --- KontoLimit ---
Console.WriteLine("\n=== KontoLimit ===");
var kontoLimit = new KontoLimit("Piotr Wiśniewski", 100, 50);
Console.WriteLine($"Klient: {kontoLimit.Nazwa}, Bilans: {kontoLimit.Bilans}");

kontoLimit.Wyplata(120); // debet
Console.WriteLine($"Po wypłacie 120 (debet): Bilans={kontoLimit.Bilans}, Zablokowane={kontoLimit.Zablokowane}");

kontoLimit.Wplata(50);
Console.WriteLine($"Po wpłacie 50: Bilans={kontoLimit.Bilans}, Zablokowane={kontoLimit.Zablokowane}");