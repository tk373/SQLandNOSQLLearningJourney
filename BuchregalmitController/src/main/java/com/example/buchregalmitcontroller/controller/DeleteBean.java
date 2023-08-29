/**
 * Bildungszentrum Zürichsee BZZ
 * Fachgruppe IT
 * 
 * M133: Webapplikationen realisieren
 * Buchregal Version 3
 */
package com.example.buchregalmitcontroller.controller;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;

import com.example.buchregalmitcontroller.model.Buch;

/**
 * @author Markus Meier, Marcel Suter
 * @date 2016-03-26
 * @version 1.1
 */

@ManagedBean(name = "deleteBean")
@ViewScoped

public class DeleteBean {

	@ManagedProperty("#{regalBean}")
	private RegalBean regalBean;
	private Buch aktuell;

	/**
	 * Konstruktor
	 */
	public DeleteBean() {
		setRegalBean( new RegalBean() );
	}

	/**
	 * Buch-Objekt aus ArrayList laden
	 */
	@PostConstruct
	public void init() {
		setAktuell(getRegalBean().getRegal().getBuchById(getBuchId()));
	}

	/**
	 * Änderungen am Buch speichern
	 * 
	 * @return String
	 */
	public String deleteBuch() {
		regalBean.getRegal().delete(aktuell);
		return "list";
	}

	/**
	 * Parameter buchId aus der URL lesen
	 */
	public int getBuchId() {
		FacesContext facesContext = FacesContext.getCurrentInstance();
		String id = facesContext.getExternalContext().getRequestParameterMap().get("buchId");
		if (id == null) {
			id = "0";
		}
		return Integer.parseInt(id);
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