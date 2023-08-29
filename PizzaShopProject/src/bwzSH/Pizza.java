package bwzSH;

import java.lang.reflect.Array;

public class Pizza {
    String name;
    Array Zutaten[];
    Double Einzelpreis;
    Double DurchmesserinCM;
    String Groesse;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Array[] getZutaten() {
        return Zutaten;
    }

    public void setZutaten(Array[] zutaten) {
        Zutaten = zutaten;
    }

    public double getEinzelpreis() {
        return Einzelpreis;
    }

    public void setEinzelpreis(double einzelpreis) {
        Einzelpreis = einzelpreis;
    }

    public double getDurchmesserinCM() {
        return DurchmesserinCM;
    }

    public void setDurchmesserinCM(double durchmesserinCM) {
        DurchmesserinCM = durchmesserinCM;
    }

    public String getGroesse() {
        return Groesse;
    }

    public void setGroesse(String groesse) {
        Groesse = groesse;
    }
}
