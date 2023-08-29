package bwzSH;

import java.time.LocalDate;

public class Kunde {
   String Vorname;
   String Nachname;
   String StrasseUndNummer;
   String Plz;
   String Ort;
   String Telefon;
   String Email;
   LocalDate Geburtstag;

   public String getVorname() {
      return Vorname;
   }

   public void setVorname(String vorname) {
      Vorname = vorname;
   }

   public String getNachname() {
      return Nachname;
   }

   public void setNachname(String nachname) {
      Nachname = nachname;
   }

   public String getStrasseUndNummer() {
      return StrasseUndNummer;
   }

   public void setStrasseUndNummer(String strasseUndNummer) {
      StrasseUndNummer = strasseUndNummer;
   }

   public String getPlz() {
      return Plz;
   }

   public void setPlz(String plz) {
      Plz = plz;
   }

   public String getOrt() {
      return Ort;
   }

   public void setOrt(String ort) {
      Ort = ort;
   }

   public String getTelefon() {
      return Telefon;
   }

   public void setTelefon(String telefon) {
      Telefon = telefon;
   }

   public String getEmail() {
      return Email;
   }

   public void setEmail(String email) {
      Email = email;
   }

   public LocalDate getGeburtstag() {
      return Geburtstag;
   }

   public void setGeburtstag(LocalDate geburtstag) {
      Geburtstag = geburtstag;
   }
}

