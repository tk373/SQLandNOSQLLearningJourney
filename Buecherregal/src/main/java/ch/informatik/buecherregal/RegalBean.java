package ch.informatik.buecherregal;


import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;

@ManagedBean
@SessionScoped
public class RegalBean {
    private Regal regal;

    public RegalBean() {

    }

    public String addBuch(){

        return addBuch();
    }

    public Regal getRegal(){
        return this.regal;
    }

    public void setRegal(Regal regal) {
        this.regal = regal;
    }
}
