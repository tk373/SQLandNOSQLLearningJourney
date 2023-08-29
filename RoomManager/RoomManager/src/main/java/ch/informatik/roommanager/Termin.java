package ch.informatik.roommanager;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.Date;

import org.bson.types.ObjectId;

public class Termin {

    private ObjectId _id;
    private Date datum;
    private String von;
    private String bis;
    private int zimmer;
    private String bemerkung;
    private String teilnehmer;
    private String privatekey;
    private String Publickey;

    public String getPrivatekey() {
        return privatekey;
    }

    public void setPrivatekey(String privatekey) {
        this.privatekey = privatekey;
    }

    public String getPublickey() {
        return Publickey;
    }

    public void setPublickey(String publickey) {
        Publickey = publickey;
    }

    public Termin() {

    }

    public ObjectId get_id() {
        return _id;
    }

    public void set_id(ObjectId _id) {
        this._id = _id;
    }

    public Date getDatum(){
        return datum;
    }

    public void setDatum(Date datum) {
        this.datum = datum;
    }

    public String getVon() {
        return von;
    }

    public void setVon(String von) {
        this.von = von;
    }

    public String getBis() {
        return bis;
    }

    public void setBis(String bis) {
        this.bis = bis;
    }

    public int getZimmer() {
        return zimmer;
    }

    public void setZimmer(int zimmer) {
        this.zimmer = zimmer;
    }

    public String getBemerkung() {
        return bemerkung;
    }

    public void setBemerkung(String bemerkung) {
        this.bemerkung = bemerkung;
    }

    public String getTeilnehmer() {
        return teilnehmer;
    }

    public void setTeilnehmer(String teilnehmer) {
        this.teilnehmer = teilnehmer;
    }
}

