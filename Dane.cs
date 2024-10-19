namespace Projekt;

public class Dane : IDane {

  public IEnumerable<Statystyka> CzytajStatystyki(string sciezka, char separator) {
    List<Statystyka> statystyki = new List<Statystyka>();
    try {
      using (var odczytTekstu = new StreamReader(sciezka)) {
        string? linia;
        while ((linia = odczytTekstu.ReadLine()) != null) {
          string[] elementy = linia.Split(separator);
          int idDruzyny = int.Parse(elementy[0]);
          double[] bramkiSrednie = new double[]{ double.Parse(elementy[1]), double.Parse(elementy[4]), double.Parse(elementy[7]), double.Parse(elementy[10]) };
          double[] bramkiMin = new double[]{ double.Parse(elementy[2]), double.Parse(elementy[5]), double.Parse(elementy[8]), double.Parse(elementy[11]) };
          double[] bramkiMax = new double[]{ double.Parse(elementy[3]), double.Parse(elementy[6]), double.Parse(elementy[9]), double.Parse(elementy[12]) };
          Statystyka statystyka = new Statystyka(idDruzyny: idDruzyny, bramkiSrednie: bramkiSrednie, bramkiMin: bramkiMin, bramkiMax: bramkiMax);
          statystyki.Add(statystyka);
        }
      }
    } catch (Exception e) {
      Console.WriteLine("Blad odczytu pliku");
      Console.WriteLine(e.Message);
    }
    return statystyki.AsEnumerable();
  }

  public IEnumerable<Druzyna> CzytajDruzyny(string sciezka, char separator) {
    List<Druzyna> druzyny = new List<Druzyna>();
    try {
      using (var odczytTekstu = new StreamReader(sciezka)) {
        string? linia;
        while ((linia = odczytTekstu.ReadLine()) != null) {
          string[] elementy = linia.Split(separator);
          Druzyna druzyna = new Druzyna(id: int.Parse(elementy[1]), nazwa: elementy[0], grupaSily: (GrupaSily)int.Parse(elementy[2]));
          druzyny.Add(druzyna);
        }
      }
    } catch (IOException e) {
      Console.WriteLine("Blad odczytu pliku");
      Console.WriteLine(e.Message);
    }
    return druzyny.AsEnumerable();
  }

}
