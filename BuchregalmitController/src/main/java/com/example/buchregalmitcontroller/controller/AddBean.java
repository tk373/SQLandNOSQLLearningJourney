/**
 * Bildungszentrum Zürichsee BZZ
 * Fachgruppe IT
 * 
 * M133: Webapplikationen realisieren
 * Buchregal Version 3
 */
package com.example.buchregalmitcontroller.controller;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;
import javax.faces.bean.ViewScoped;

import com.example.buchregalmitcontroller.model.Buch;

/**
 * @author Markus Meier, Marcel Suter
 * @date 2016-03-26
 * @version 1.1
 */

@ViewScoped
@ManagedBean(name = "addBean")
public class AddBean {
	@ManagedProperty("#{regalBean}")
	private RegalBean regalBean;
	private Buch aktuell;

	/**
	 * Konstruktor
	 */
	public AddBean() {
		setRegalBean( new RegalBean() );
		setAktuell(new Buch());
	}

	/**
	   * Speichert das aktuelle Buch im Regal
	   */
	  public String addBuch() {
	    regalBean.getRegal().add(aktuell);
	    return "list";
	  }

	/**
	 * @return the regalBean
	 */
	public RegalBean getRegalBean() {
		return regalBean;
	}

	/**
	 * @param regalBean
	 *            the regalBean to set
	 */
	public void setRegalBean(RegalBean regalBean) {
		this.regalBean = regalBean;
	}

	/**
	 * @return the aktuell
	 */
	public Buch getAktuell() {
		return aktuell;
	}

	/**
	 * @param aktuell
	 *            the aktuell to set
	 */
	public void setAktuell(Buch aktuell) {
		this.aktuell = aktuell;
	}

}
