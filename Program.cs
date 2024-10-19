namespace Projekt;

public class Program {

  public static void Main() {
    
    IDane dane = new Dane();
    Liga liga = new Liga("Liga hiszpanska", dane);
    liga.Pokaz();
    liga.Rozegraj();
    
  }

}
