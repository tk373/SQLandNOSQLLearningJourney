/**
 * Bildungszentrum Zürichsee BZZ
 * Fachgruppe IT
 * 
 * M133: Webapplikationen realisieren
 * Buchregal Version 2
 */
package com.example.buchregalmitcontroller.controller;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;

import com.example.buchregalmitcontroller.model.Regal;

/**
 * @author	Markus Meier, Marcel Suter
 * @date	2016-03-26
 * @version	1.1
 */

@SessionScoped
@ManagedBean(name="regalBean")
public class RegalBean {
	private Regal regal;
	
	/**
	 * Konstruktor
	 * Erstellt einige Bücher zum Testen
	 */
	public RegalBean() {
		setRegal(new Regal());
	}
	
	/**
	 * @return the regal
	 */
	public Regal getRegal() {
		return regal;
	}
	/**
	 * @param regal the regal to set
	 */
	public void setRegal(Regal regal) {
		this.regal = regal;
	}

}
