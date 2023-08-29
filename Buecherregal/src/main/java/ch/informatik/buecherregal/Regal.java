package ch.informatik.buecherregal;

import java.util.ArrayList;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;

@ManagedBean
@SessionScoped
public class Regal {
    private ArrayList<Buch> buecher;
    private Buch aktuell = new Buch();

    public Regal(){

    }

    public void add(){

    }

    public ArrayList<Buch> getBuecher() {

        return buecher;
    }

    public void setBuecher(ArrayList<Buch> buecher) {

        this.buecher = buecher;
    }

    public Buch getAktuell() {
        return aktuell;
    }

    /*
    public void setAktuell(Buch aktuell) {
        this.aktuell = aktuell;
    }
     */

}
