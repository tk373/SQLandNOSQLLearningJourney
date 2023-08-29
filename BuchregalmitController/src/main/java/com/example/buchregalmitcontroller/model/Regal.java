
/**
 * Bildungszentrum Zürichsee BZZ
 * Fachgruppe IT
 * 
 * M133: Webapplikationen realisieren
 * Buchregal Version 2
 */
package com.example.buchregalmitcontroller.model;

import java.util.ArrayList;

/**
 * @author Markus Meier, Marcel Suter
 * @date 2016-03-26
 * @version 1.1
 */
public class Regal {

	private ArrayList<Buch> buecher;
	

	/**
	 * Konstruktor
	 */
	public Regal() {
		setBuecher(new ArrayList<Buch>());
		
		Buch temp = new Buch();
	    
	    temp.setTitel("Momo");
	    temp.setAutor("Michael Ende");
	    temp.setPreis(19.50f);
	    temp.setIsbn("978-3-522-17783-2");
	    this.add(temp);
	    
	    temp = new Buch();
	    temp.setTitel("Doors of Stone");
	    temp.setAutor("Patrick Rothfuss");
	    temp.setPreis(35.60f);
	    temp.setIsbn("123-4-567-89902-1");
	    this.add(temp);
	}

	/**
	 * Fügt ein Buch ins Regal ein
	 * 
	 * @param buch
	 */
	public void add(Buch aktuell) {
		buecher.add(aktuell);   
	}

	/**
	 * Buch aus der ArrayList löschen
	 */
	public void delete(Buch aktuell) {
	  buecher.remove(aktuell);
	}
	/**
	 * Liest ein Buch aus der ArrayList aufgrund der Id
	 * @return Buch
	 */
	public Buch getBuchById (int id) {
	  Buch gesucht;
	  gesucht = buecher.get(id);
	  return gesucht;
	}
	
	/**
	 * @return the buecher
	 */
	public ArrayList<Buch> getBuecher() {
		return buecher;
	}

	/**
	 * @param buecher
	 *            the buecher to set
	 */
	public void setBuecher(ArrayList<Buch> buecher) {
		this.buecher = buecher;
	}

}
