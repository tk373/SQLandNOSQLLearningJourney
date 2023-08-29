
/**
 * Bildungszentrum Zürichsee BZZ
 * Fachgruppe IT
 * 
 * M133: Webapplikationen realisieren
 * Buchregal Version 2
 */
package com.example.buchregalmitcontroller.model;

import javax.validation.constraints.DecimalMax;
import javax.validation.constraints.DecimalMin;
import javax.validation.constraints.Pattern;
import javax.validation.constraints.Size;

/**
 * @author Markus Meier, Marcel Suter
 * @date 2016-03-26
 * @version 1.1
 */
public class Buch {

	@Size(min=2, max=30, message="Titel muss 2-30 Zeichen lang sein")
	private String titel;
	@Size(min=2, max=30, message="Autor muss 2-30 Zeichen lang sein")
	private String autor;
	@DecimalMax(value="199.95", message="Preis muss zwischen 0.05 und 199.95 liegen")
	@DecimalMin(value="0.05", message="Preis muss zwischen 0.05 und 199.95 liegen")
	private Float preis;
	@Pattern(regexp="97(8|9)-(([0-9]{1,5})-){3}[0-9]", message="Keine gültige ISBN13-Nummer")
	private String isbn;

	/**
	 * Konstruktor
	 */
	public Buch() {
		setTitel("Titel unbekannt");
	    setAutor("Autor unbekannt");
	    setPreis(0.0f);
	    setIsbn("ISBN unbekannt");
	}

	/**
	 * @return the titel
	 */
	public String getTitel() {
		return titel;
	}

	/**
	 * @param titel the titel to set
	 */
	public void setTitel(String titel) {
		this.titel = titel;
	}

	/**
	 * @return the autor
	 */
	public String getAutor() {
		return autor;
	}

	/**
	 * @param autor the autor to set
	 */
	public void setAutor(String autor) {
		this.autor = autor;
	}

	/**
	 * @return the preis
	 */
	public Float getPreis() {
		return preis;
	}

	/**
	 * @param preis the preis to set
	 */
	public void setPreis(Float preis) {
		this.preis = preis;
	}

	/**
	 * @return the isbn
	 */
	public String getIsbn() {
		return isbn;
	}

	/**
	 * @param isbn the isbn to set
	 */
	public void setIsbn(String isbn) {
		this.isbn = isbn;
	}

	

}
