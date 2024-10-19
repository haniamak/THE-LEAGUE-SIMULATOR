namespace Projekt;

public interface IDane {

  public IEnumerable<Statystyka> CzytajStatystyki(string sciezka, char separator);
  public IEnumerable<Druzyna> CzytajDruzyny(string sciezka, char separator);

}
