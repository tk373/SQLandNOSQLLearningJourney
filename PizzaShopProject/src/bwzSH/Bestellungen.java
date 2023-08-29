package bwzSH;

import java.time.LocalDate;

public class Bestellungen {
    Integer Bestellnummer;
    LocalDate Bestelldatum;
    String Kunde;
    Double Total;

    public Integer getBestellnummer() {
        return Bestellnummer;
    }

    public void setBestellnummer(Integer bestellnummer) {
        Bestellnummer = bestellnummer;
    }

    public LocalDate getBestelldatum() {
        return Bestelldatum;
    }

    public void setBestelldatum(LocalDate bestelldatum) {
        Bestelldatum = bestelldatum;
    }

    public String getKunde() {
        return Kunde;
    }

    public void setKunde(String kunde) {
        Kunde = kunde;
    }

    public Double getTotal() {
        return Total;
    }

    public void setTotal(Double total) {
        Total = total;
    }
}
