package ch.informatik.roommanager;

import java.util.ArrayList;

public class TerminListe {
    private ArrayList<Termin> termine;

    public TerminListe() {
        this.termine = new ArrayList<Termin>();
    }

    public void add(Termin aktuell) {
        termine.add(aktuell);

    }

    public ArrayList<Termin> getBuecher() {
        return termine;
    }

    public void setTermine(ArrayList<Termin> termine) {
        this.termine = termine;
    }

}


